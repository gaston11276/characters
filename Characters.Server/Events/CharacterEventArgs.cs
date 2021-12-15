using System;
using Gaston11276.Characters.Server.Models;
using JetBrains.Annotations;

namespace Gaston11276.Characters.Server.Events
{
	[PublicAPI]
	public class CharacterEventArgs : EventArgs
	{
		public Character Character { get; }

		public CharacterEventArgs(Character character)
		{
			this.Character = character;
		}
	}
}
