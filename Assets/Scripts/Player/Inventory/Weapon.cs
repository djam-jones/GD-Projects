using UnityEngine;
using System.Collections;

public class Weapon : Item {

	public enum WeaponHandType
	{
		OneHanded, 
		TwoHanded
	}

	public enum WeaponType
	{
		Sword, 
		Greatsword, 
		Dagger, 
		Staff
	}

	//Amount of Damage and DamagePerSecond
	protected int 			_damage;
	protected float 		_attackSpeed;
	
	//Enums for The Quality of the Weapon and the Weapon Type
	public ItemQuality 		_itemQualityWeapon;
	public WeaponType 		weaponType;
	public WeaponHandType	weaponHandType;

	void Awake()
	{
		SetWeaponTypeStats();
		SetWeaponQualityStats();

		_damage = (_damage * itemLvl);
		_attackSpeed = (_attackSpeed * itemLvl);
	}

	/// <summary>
	/// Sets the Minimum Damage done by the respective Weapon Type.
	/// </summary>
	void SetWeaponTypeStats ()
	{
		switch(weaponType)
		{
			case WeaponType.Sword: 
				_damage = 37;
				_attackSpeed = 55;
			break;

			case WeaponType.Greatsword: 
				_damage = 49;
				_attackSpeed = 22;
			break;

			case WeaponType.Dagger: 
				_damage = 32;
				_attackSpeed = 61;
			break;

			case WeaponType.Staff: 
				_damage = 24;
				_attackSpeed = 36;
			break;
		}
	}

	/// <summary>
	/// Sets Extra Damage done by Weapon Quality.
	/// </summary>
	void SetWeaponQualityStats ()
	{
		switch(_itemQualityWeapon)
		{
			case ItemQuality.Common:
				_damage += 0;
				_attackSpeed += 0;
				break;

			case ItemQuality.Rare:
				_damage += 20;
				_attackSpeed += 20;
				break;

			case ItemQuality.Unique:
				_damage += 30;
				_attackSpeed += 27;
				break;

			case ItemQuality.Mythical:
				_damage += 50;
				_attackSpeed += 43;
				break;
		}
	}

}