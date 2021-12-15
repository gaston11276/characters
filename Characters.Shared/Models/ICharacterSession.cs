using System;

namespace Gaston11276.Characters.Shared.Models
{
	public interface ICharacterSession
	{
		Guid Id { get; set; }
		DateTime Created { get; set; }
		DateTime? Connected { get; set; }
		DateTime? Disconnected { get; set; }
		Guid CharacterId { get; set; }
	}
}
