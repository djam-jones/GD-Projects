using UnityEngine;
using System.Collections;

public class Armor : Item {

	public enum ArmorBodyType
	{
		Head, 
		Body, 
		Shoulders, 
		Arms, 
		Hands, 
		Legs, 
		Feet, 

		Offhand, 
		Neck, 
		Finger
	}

	public enum ArmorType
	{
		Light, 
		Medium, 
		Heavy, 
		Shield
	}

	//The Amount of Defense
	protected int 	_minimumPhysicalDefense;

	//Enums for The Quality of the Armor and the Armor Type
	public ItemQuality 		itemQualityArmor;
	public ArmorType 		armorType;
	public ArmorBodyType	armorBodyType;

	void Awake()
	{
		SetArmorTypeStats();
		SetArmorQualityStats();

		_minimumPhysicalDefense = (_minimumPhysicalDefense * itemLvl);
	}

	/// <summary>
	/// Sets Minimum Physical Defense for the respective Armor Type
	/// </summary>
	void SetArmorTypeStats()
	{
		switch(armorType)
		{
		case ArmorType.Light:
			_minimumPhysicalDefense = 28;
			break;
			
		case ArmorType.Medium:
			_minimumPhysicalDefense = 52;
			break;
			
		case ArmorType.Heavy:
			_minimumPhysicalDefense = 84;
			break;
			
		case ArmorType.Shield:
			_minimumPhysicalDefense = 5;
			break;
		}
	}

	/// <summary>
	/// Sets Extra Physical Defense by Armor Quality
	/// </summary>
	void SetArmorQualityStats()
	{
		switch(itemQualityArmor)
		{
			case ItemQuality.Common:
				_minimumPhysicalDefense += 0;
				break;
				
			case ItemQuality.Unique:
				_minimumPhysicalDefense += 15;
				break;
				
			case ItemQuality.Rare:
				_minimumPhysicalDefense += 25;
				break;
				
			case ItemQuality.Mythical:
				_minimumPhysicalDefense += 40;
				break;
		}
	}

}
