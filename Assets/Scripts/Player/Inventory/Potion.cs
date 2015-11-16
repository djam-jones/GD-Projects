using UnityEngine;
using System.Collections;

public class Potion : Item {

	public enum PotionType
	{
		HealingPotion, 
		ManaPotion, 
		StaminaPotion,
		AllHealPotion
	}

	//Amount of Healing, Mana or Stamina to be gained.
	protected int 	_healingAmount;
	protected int 	_magicAmount;
	protected int 	_staminaAmount;

	//Enums for The Quality of the Potion and the Potion Type
	ItemQuality 	_itemQualityPotion;
	PotionType		_potionType;

}