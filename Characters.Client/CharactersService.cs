using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NFive.SDK.Core.Models.Player;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Core.Input;
using NFive.SDK.Client.Commands;
using NFive.SDK.Client.Communications;
using NFive.SDK.Client.Events;
using NFive.SDK.Client.Interface;
using NFive.SDK.Client.Services;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Shared;
using Gaston11276.Characters.Client.Models;

using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using NFive.SDK.Core.Extensions;
using NFive.SDK.Client.Extensions;


namespace Gaston11276.Characters.Client
{
	[PublicAPI]
	public class CharactersService : Service
	{
		private Configuration config;

		WindowCharacters windowCharacters = new WindowCharacters();
		WindowAppearance windowAppearance = new WindowAppearance();

		List<Character> characters = new List<Character>();
		private Character activeCharacter = null;

		public CharactersService(ILogger logger, ITickManager ticks, ICommunicationManager comms, ICommandManager commands, IOverlayManager overlay, User user) : base(logger, ticks, comms, commands, overlay, user) { }

		public override async Task Started()
		{
			this.config = await this.Comms.Event(CharactersEvents.Configuration).ToServer().Request<Configuration>();
			this.Comms.Event(CharactersEvents.Configuration).FromServer().On<Configuration>((e, c) => this.config = c);

			WindowManager.AddWindow(windowCharacters);
			windowCharacters.CreateUi();
			windowCharacters.SetUi();
			windowCharacters.RegisterOnOpenCallback(OnCharactersOpen);
			windowCharacters.RegisterOnCloseCallback(OnCharactersClose);
			windowCharacters.RegisterOnPlayCallback(OnPlay);
			windowCharacters.RegisterOnCreateCallback(OnCreate);
			windowCharacters.RegisterOnDeleteCallback(OnDelete);
			windowCharacters.SetHotkey(InputControl.ReplayStartStopRecording); // F1
			

			WindowManager.AddWindow(windowAppearance);
			windowAppearance.CreateUi();
			windowAppearance.RegisterOnOpenCallback(OnAppearanceOpen);
			windowAppearance.RegisterOnCloseCallback(OnAppearanceClose);
			windowAppearance.RegisterOnSaveCallback(OnSave);
			windowAppearance.RegisterOnRevertCallback(OnRevert);
			windowAppearance.SetHotkey(InputControl.ReplayStartStopRecordingSecondary); // F2

			windowCharacters.Open();
			await Delay(10);
		}

		void OnCharactersOpen()
		{
			this.Ticks.Off(OnAutoSaveCharacter);
			this.Ticks.Off(OnAutoSavePosition);
			LoadCharacters();
		}

		void OnCharactersClose(Guid characterId)
		{
			if (activeCharacter != null)
			{
				this.Ticks.On(OnAutoSaveCharacter);
				this.Ticks.On(OnAutoSavePosition);
			}
		}

		async void OnPlay(Guid characterId)
		{
			if (activeCharacter != null)
			{
				if (activeCharacter.Id == characterId)
				{
					return;
				}
			}

			await SetNewCharacter(characterId);

			this.Ticks.On(OnAutoSaveCharacter);
			this.Ticks.On(OnAutoSavePosition);
		}

		void OnAppearanceOpen()
		{
			this.Ticks.Off(OnAutoSaveCharacter);
			this.Ticks.Off(OnAutoSavePosition);
		}

		void OnAppearanceClose()
		{
			if (activeCharacter != null)
			{
				this.Ticks.On(OnAutoSaveCharacter);
				this.Ticks.On(OnAutoSavePosition);
			}
		}

		private async void LoadCharacters()
		{
			await RequestCharacters();
			windowCharacters.SetCharacters(characters);
			windowCharacters.UpdateCharacterList();
		}

		private async Task RequestCharacters()
		{
			characters = await this.Comms.Event(CharactersEvents.GetCharactersForUser).ToServer().Request<List<Character>>();
		}

		private async Task SetNewCharacter(Guid selectedCharacterId)
		{
			API.DoScreenFadeOut(500);

			Character selectedCharacter = characters.First(c => c.Id == selectedCharacterId);
			await this.Comms.Event(CharactersEvents.Select).ToServer().Request<CharacterSession>(selectedCharacter.Id);

			await windowAppearance.SetCharacter(selectedCharacter);
			await windowAppearance.SetUi();
			await windowAppearance.ApplyToPed();

			API.SwitchInPlayer(API.PlayerPedId());

			while (API.IsScreenFadingOut()) await WindowManager.Delay(10);
			API.ShutdownLoadingScreenNui();

			Game.Player.Character.Position = selectedCharacter.Position.ToVector3().ToCitVector3();
			Game.Player.Character.Armor = selectedCharacter.Armor;
			Game.Player.Character.IsPositionFrozen = false;

			API.DoScreenFadeIn(500);
			while (API.IsScreenFadingIn()) await WindowManager.Delay(10);
			
			activeCharacter = selectedCharacter;
		}

		private async void OnCreate()
		{
			Character character = new Character();
			windowCharacters.SetCharacterInfo(character);

			character = await this.Comms.Event(CharactersEvents.CreateCharacter).ToServer().Request<Character>(character);

			await windowAppearance.SetCharacter(character);
			await windowAppearance.SetDefaults();

			await this.Comms.Event(CharactersEvents.SaveCharacter).ToServer().Request<Character>(character);

			await RequestCharacters();
			windowCharacters.SetCharacters(characters);
			windowCharacters.UpdateCharacterList();
			windowCharacters.ClearCharacterListEdit();
		}

		private async void OnDelete(Guid selectedCharacterId)
		{
			characters = await this.Comms.Event(CharactersEvents.DeleteCharacter).ToServer().Request<List<Character>>(selectedCharacterId);
			windowCharacters.SetCharacters(characters);
			windowCharacters.UpdateCharacterList();
		}

		private void OnSave()
		{
			SaveCharacter();
		}

		private async void OnRevert()
		{
			await RequestCharacter();
			await windowAppearance.SetCharacter(activeCharacter);
			await windowAppearance.ApplyToPed();
		}

		public async Task OnAutoSaveCharacter()
		{
			SaveCharacter();
			await Delay(this.config.Autosave.CharacterInterval);
		}

		public async Task OnAutoSavePosition()
		{
			SavePosition();
			await Delay(this.config.Autosave.PositionInterval);
		}

		private void SaveCharacter()
		{
			this.activeCharacter.Position = Game.Player.Character.Position.ToVector3().ToPosition();
			this.Comms.Event(CharactersEvents.SaveCharacter).ToServer().Emit(this.activeCharacter);
		}

		private async Task RequestCharacter()
		{
			activeCharacter = await this.Comms.Event(CharactersEvents.GetCharacterForUser).ToServer().Request<Character>(activeCharacter);
		}

		private void SavePosition()
		{
			this.Comms.Event(CharactersEvents.SavePosition).ToServer().Emit(this.activeCharacter.Id, Game.Player.Character.Position.ToVector3().ToPosition());
		}
	}
}
