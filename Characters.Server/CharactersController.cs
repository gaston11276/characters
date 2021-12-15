using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Core.Helpers;
using NFive.SDK.Core.Models;
using NFive.SDK.Core.Models.Player;
using NFive.SDK.Server.Controllers;
using NFive.SDK.Server.Communications;
using NFive.SDK.Server.Events;
using Gaston11276.Characters.Server.Models;
using Gaston11276.Characters.Server.Storage;
using Gaston11276.Characters.Shared;

namespace Gaston11276.Characters.Server
{
	[PublicAPI]
	public class CharactersController : ConfigurableController<Configuration>
	{
		private readonly List<CharacterSession> characterSessions = new List<CharacterSession>();
		private readonly ICommunicationManager comms;

		public CharactersController(ILogger logger, Configuration configuration, ICommunicationManager comms) : base(logger, configuration)
		{
			this.comms = comms;
			comms.Event(CharactersEvents.Configuration).FromClients().OnRequest(e => e.Reply(this.Configuration));
			comms.Event(CharactersEvents.GetCharactersForUser).FromClients().OnRequest(GetCharactersForUser);
			comms.Event(CharactersEvents.GetCharacterForUser).FromClients().OnRequest<Character>(GetCharacterForUser);
			comms.Event(CharactersEvents.CreateCharacter).FromClients().OnRequest<Character>(CreateCharacter);
			comms.Event(CharactersEvents.DeleteCharacter).FromClients().OnRequest<Guid>(DeleteCharacter);
			comms.Event(CharactersEvents.Select).FromClients().OnRequest<Guid>(Select);
			comms.Event(CharactersEvents.SaveCharacter).FromClients().OnRequest<Character>(SaveCharacter);
			comms.Event(CharactersEvents.SavePosition).FromClients().On<Guid, Position>(SavePosition);
			comms.Event(CharactersEvents.GetActive).FromServer().OnRequest(e => e.Reply(this.characterSessions));
			comms.Event(SessionEvents.ClientDisconnected).FromServer().On<IClient, Session>(OnClientDisconnected);
		}

		public override Task Started()
		{
			Cleanup();

			return base.Started();
		}

		private async void Cleanup()
		{
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				var activeSessions = context.CharacterSessions.Where(s => s.Disconnected == null).ToList();
				var lastServerActiveTime = await this.comms.Event(BootEvents.GetLastActiveTime).ToServer().Request<DateTime?>() ?? DateTime.UtcNow;
				foreach (var characterSession in activeSessions)
				{
					characterSession.Connected = null;
					characterSession.Disconnected = lastServerActiveTime;
					context.CharacterSessions.AddOrUpdate(characterSession);
				}

				context.SaveChanges();
				transaction.Commit();
			}
		}

		private async void OnClientDisconnected(ICommunicationMessage e, IClient client, Session session)
		{
			await DeselectAll(session.UserId);
			await Delay(1);
		}

		public async Task DeselectAll(Guid id)
		{
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				var activeSessions = context.CharacterSessions.Where(s =>
					s.Character.UserId == id
					&& s.Disconnected == null
				).ToList();

				foreach (var characterSession in activeSessions)
				{
					this.comms.Event(CharactersEvents.Deselecting).ToServer().Emit(characterSession);
					characterSession.Connected = null;
					characterSession.Disconnected = DateTime.UtcNow;
					context.CharacterSessions.AddOrUpdate(characterSession);
				}

				await context.SaveChangesAsync();
				transaction.Commit();

				foreach (var characterSession in activeSessions)
				{
					this.characterSessions.RemoveAll(c => c.Id == characterSession.Id);
					this.comms.Event(CharactersEvents.Deselected).ToServer().Emit(characterSession);
				}
			}
		}

		private async void DeleteCharacter(ICommunicationMessage e, Guid id)
		{
			using (var context = new StorageContext())
			{
				var character = context.Characters.First(c => c.Id == id);
				character.Deleted = DateTime.UtcNow;
				await context.SaveChangesAsync();
				GetCharactersForUser(e, e.User.Id);
			}
		}

		private async void Select(ICommunicationMessage e, Guid id)
		{
			await DeselectAll(e.User.Id);

			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				var character = context.Characters.Include(c => c.User).FirstOrDefault(c => c.Id == id);

				if (character == null)
				{
					e.Reply(null);
					throw new Exception($"No character found for Id \"{id}\""); // TODO: CharacterException
				}
				this.comms.Event(CharactersEvents.Selecting).ToServer().Emit(character);

				var newSession = new CharacterSession
				{
					Id = GuidGenerator.GenerateTimeBasedGuid(),
					CharacterId = character.Id,
					Character = character,
					Created = DateTime.UtcNow,
					Connected = DateTime.UtcNow,
					SessionId = e.Session.Id
				};

				context.CharacterSessions.Add(newSession);
				await context.SaveChangesAsync();
				transaction.Commit();

				//this.Logger.Debug("Created character session");
				//this.Logger.Debug($"Session: {new Serializer().Serialize(e.Session)}");

				newSession.Session = e.Session;
				e.Reply(newSession);
				this.characterSessions.Add(newSession);
				this.comms.Event(CharactersEvents.Selected).ToServer().Emit(newSession);
			}
		}

		private void GetCharactersForUser(ICommunicationMessage e)
		{
			//this.Logger.Info($"GetCharactersForUser(ICommunicationMessage e, Guid userId)");
			GetCharactersForUser(e, e.User.Id);
		}

		private void GetCharactersForUser(ICommunicationMessage e, Guid userId)
		{
			//this.Logger.Info($"GetCharactersForUser(ICommunicationMessage e, Guid userId) - userId: {userId}");
			using (var context = new StorageContext())
			{
				var characters = context.Characters.Where(c => c.Deleted == null && c.UserId == userId).ToList();
				e.Reply(characters);
			}
		}

		private void GetCharacterForUser(ICommunicationMessage e, Character character)
		{
			//this.Logger.Info($"GetCharactersForUser(ICommunicationMessage e, Guid userId)");
			GetCharacterForUser(e, e.User.Id, character.Id);
		}

		private void GetCharacterForUser(ICommunicationMessage e, Guid userId, Guid characterId)
		{
			//this.Logger.Info($"GetCharactersForUser(ICommunicationMessage e, Guid userId) - userId: {userId}");
			using (var context = new StorageContext())
			{
				var character = context.Characters.Find(characterId);
				e.Reply(character);
			}
		}

		private async void CreateCharacter(ICommunicationMessage e, Character character)
		{

			this.Logger.Debug($"Server: Creating character {character.FullName}");
			// TODO: Validate client sent values

			// Don't trust important values from clients
			character.Id = GuidGenerator.GenerateTimeBasedGuid();
			character.UserId = e.User.Id;
			character.Alive = true;
			character.Health = 10000;
			character.Armor = 0;
			character.Ssn = Character.GenerateSsn();
			character.LastPlayed = DateTime.UtcNow;
			//character.Position = new Position(0f, 0f, 71f);
			character.PedHeadBlendData = new PedHeadBlendData();
			character.PedFaceFeatures = new PedFaceFeatures();
			character.PedHeadOverlays = new PedHeadOverlays();
			character.PedDecorations = new PedDecorations();
			character.PedComponents = new PedComponents();
			character.PedProps = new PedProps();

			// Save character
			using (var context = new StorageContext())
			{
				using (var transaction = context.Database.BeginTransaction())
				{
					try
					{
						context.Characters.Add(character);
						await context.SaveChangesAsync();
						transaction.Commit();

						this.Logger.Debug($"Server: Character was created: {character.FullName}");
						// Send back updated user
						e.Reply(character);
					}
					catch (Exception ex)
					{
						this.Logger.Error(ex);
						transaction.Rollback();

						// TODO: Reply with an error so client doesn't hang
					}
				}
			}
		}

		public async void SaveCharacter(ICommunicationMessage e, Character character)
		{
			this.Logger.Debug($"Server: Saving character {character.FullName}");
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					context.Characters.AddOrUpdate(character);

					context.PedHeadBlendData.AddOrUpdate(character.PedHeadBlendData);
					context.PedFaceFeatures.AddOrUpdate(character.PedFaceFeatures);
					context.PedHeadOverlays.AddOrUpdate(character.PedHeadOverlays);
					context.PedDecorations.AddOrUpdate(character.PedDecorations);
					context.PedComponents.AddOrUpdate(character.PedComponents);
					context.PedProps.AddOrUpdate(character.PedProps);

					await context.SaveChangesAsync();
					transaction.Commit();

					// Send back updated user
					e.Reply(character);
				}
				catch (Exception ex)
				{
					this.Logger.Error(ex);

					transaction.Rollback();
				}

				var activeSession = this.characterSessions.FirstOrDefault(s => s.Character.Id == character.Id);
				if (activeSession == null) return;
				activeSession.Character = character;
			}
		}

		public async void SavePosition(ICommunicationMessage e, Guid characterGuid, Position position)
		{
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					var saveCharacter = context.Characters.Single(c => c.Id == characterGuid);
					saveCharacter.Position = position;

					await context.SaveChangesAsync();
					transaction.Commit();
				}
				catch (Exception ex)
				{
					this.Logger.Error(ex);

					transaction.Rollback();
				}

				var activeSession = this.characterSessions.FirstOrDefault(s => s.Character.Id == characterGuid);
				if (activeSession == null) return;
				activeSession.Character.Position = position;
			}
		}

		public override void Reload(Configuration configuration)
		{
			// Update local configuration
			base.Reload(configuration);

			// Send out new configuration
			this.comms.Event(CharactersEvents.Configuration).ToClients().Emit(this.Configuration);
		}

	}
}
