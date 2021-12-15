using System;
using JetBrains.Annotations;

using Gaston11276.Characters.Server.Models;

namespace Gaston11276.Characters.Server.Events
{
	[PublicAPI]
	public class CharacterSessionEventArgs : EventArgs
	{
		public CharacterSession CharacterSession { get; }

		public CharacterSessionEventArgs(CharacterSession session)
		{
			this.CharacterSession = session;
		}
	}
}
