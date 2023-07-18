using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneLine;

namespace Cards
{
	[Serializable]
	public struct CardPropertyData
	{
		[SerializeField]
		private int _cost;
		[SerializeField] 
		private Texture _image;
		[SerializeField]
		private string _name;
		[SerializeField]
		private string _description;
		[SerializeField]
		private int _attack;
		[SerializeField]
		private int _health;
		[SerializeField]
		private CardUnitType _type;


	}

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

		public CardParamsData GetParams()
		{
			return new CardParamsData(Cost, Attack, Health);
		}
	}

	public struct CardParamsData
	{
		public int Cost;
		public int Attack;
		public int Health;

		public CardParamsData(int cost, int attack, int health)
		{
			Cost = cost; Attack = attack; Health = health;
		}
	}
}
