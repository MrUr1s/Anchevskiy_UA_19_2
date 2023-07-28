using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public enum CardUnitType : byte
	{
		None = 0,
		Murloc = 1,
		Beast = 2,
		Elemental = 3,
		Mech = 4,
		Hero = 254
	}

	public enum SideType : byte
	{
		Common = 0,
		Mage = 1,
		Warrior = 2
	}
	public enum TypePlayer:byte
    {
		Player1, Player2
	}
	public enum TypeAbilityIsTarget:byte
	{
	 None, AbilityOnEnemy, AbilityOnSelf
}


