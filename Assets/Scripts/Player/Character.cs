using UnityEngine;
using System.Collections;

public enum PlayerClass
{
	NonSelected, 
	Warrior, 
	Mage, 
	Rogue, 
	Monk
}

public enum PlayerRace
{
	NonSelected, 
	Human, 
	Elf, 
	Dwarf, 
	Halfling
}

public enum PlayerGender
{
	Unspecified, 
	Male, 
	Female
}

[System.Serializable]
public class Character : MonoBehaviour {

	[HideInInspector]
	public string characterName;

	[HideInInspector]
	public PlayerClass charClass;

	[HideInInspector]
	public PlayerRace charRace;

	[HideInInspector]
	public PlayerGender charGender;

	public Character()
	{
		this.characterName = "Henry" + " " + "Schaeffer";
	}

	void Awake()
	{
		charGender = PlayerGender.Unspecified;

		charClass = PlayerClass.NonSelected;
		charRace = PlayerRace.NonSelected;
	}

	void Start()
	{
		//
	}

	void Update()
	{
		//
	}

	/// <summary>
	/// Picks the player's gender.
	/// </summary>
	/// <param name="pGender">Player Gender.</param>
	void PickGender(PlayerGender pGender)
	{
		//
	}

	/// <summary>
	/// Picks the player's class.
	/// </summary>
	/// <param name="pClass">Player Class.</param>
	void PickClass(PlayerClass pClass)
	{
		//
	}

	/// <summary>
	/// Picks the player's race.
	/// </summary>
	/// <param name="pRace">Player Race.</param>
	void PickRace(PlayerRace pRace)
	{
		//
	}
}
