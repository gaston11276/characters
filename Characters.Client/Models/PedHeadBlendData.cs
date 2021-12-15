using System;
using NFive.SDK.Core.Models;
using Gaston11276.Characters.Shared.Models;

namespace Gaston11276.Characters.Client.Models
{
	public class PedHeadBlendData : IdentityModel, IPedHeadBlendData
	{
		public int Parent1 { get; set; }
		public int Parent2 { get; set; }
		public float ShapeMix { get; set; }
		public float SkinMix { get; set; }
	}
}
