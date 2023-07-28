using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneLine;

namespace Cards
{

	[Serializable]
	public struct CardPropertiesData
	{
		[Width(30)]
		public uint Id;
		public int Cost;
		public string Description;
		public string Name;
		[Width(50)]
		public Texture Texture;
		[Width(40)]
		public int Attack;
		[Width(40)]
		public int Health;
		[Width(65)]
		public CardUnitType Type;

		public bool IsBattlecry, IsTaunt, IsCharge,IsTimeEffect;



		
	}

}
