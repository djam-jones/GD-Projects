using UnityEngine;
using System.Collections;

public enum ItemQuality 
{
	Common, 
	Rare, 
	Unique, 
	Mythical
}

public class Item : MonoBehaviour {
	
	//Enumerator for The ItemQuality
	private ItemQuality 	_itemQuality;

	//Item Image
	public Sprite			itemSprite;

	//Item Name
	public string 			itemName;

	//Item Level
	public int				itemLvl = 1;
	
	//Item ID
	[HideInInspector]
	public int 				itemID;

	//Item Value
	public int				itemValue;

	//Item Description
	public string			itemDescription;

	//Level Requirement
	public int 				levelRequired;
	public bool 			levelRequirement;

	//Class Requirement
	public bool 			classRequirement;

/*
	//Item Types
	public Weapon 			weapon;
	public Armor 			armor;
	public Potion			potion;
*/
}