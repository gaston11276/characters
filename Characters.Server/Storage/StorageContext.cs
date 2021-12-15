using System.Data.Entity;
using NFive.SDK.Core.Models.Player;
using NFive.SDK.Server.Storage;
using Gaston11276.Characters.Server.Models;

namespace Gaston11276.Characters.Server.Storage
{
	public class StorageContext : EFContext<StorageContext>
	{
		public DbSet<Character> Characters { get; set; }
		public DbSet<PedHeadBlendData> PedHeadBlendData { get; set; }
		public DbSet<PedFaceFeatures> PedFaceFeatures { get; set; }
		public DbSet<PedHeadOverlays> PedHeadOverlays { get; set; }
		public DbSet<PedDecorations> PedDecorations { get; set; }
		public DbSet<PedComponents> PedComponents { get; set; }
		public DbSet<PedProps> PedProps { get; set; }

		public DbSet<CharacterSession> CharacterSessions { get; set; }
	}
}
