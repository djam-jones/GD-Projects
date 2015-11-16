using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemDatabase : MonoBehaviour {
	
	public static List<Item> itemList = new List<Item>();

	void Awake()
	{
		//LoadAllPrefabsContaining<Weapon>("Items/Weapons");

		itemList.AddRange( LoadAllPrefabsContaining<Item>("Items/Weapons") );
		itemList.AddRange( LoadAllPrefabsContaining<Item>("Items/Armor") );
		itemList.AddRange( LoadAllPrefabsContaining<Item>("Items/Potions") );

		for(int i = 0; i < itemList.Count; i++)
		{
			itemList[i].itemID = i;
		}
	}

	public static List<Item> GetItemsById(List<int> itemIDs)
	{
		List<Item> newItemList = new List<Item>();
		for(int i = 0; i < itemIDs.Count; i++)
		{
			newItemList.Add(itemList[itemIDs[i]]);
		}
		return newItemList;
	}
	
	/// <summary>
	/// Loads all prefabs containing a Specific Component.
	/// </summary>
	/// <returns>All prefabs containing this component.</returns>
	/// <param name="path">Path.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static List<T> LoadAllPrefabsContaining<T>(string path) where T:Component
	{
		List<T> result = new List<T>();

		var allFiles = Resources.LoadAll<Object>(path);

		foreach(var file in allFiles)
		{
			if(file is GameObject)
			{
				GameObject go = file as GameObject;
				if(go.GetComponent<T>() != null)
				{
					result.Add(go.GetComponent<T>());
				}
			}
		}
		return result;
	}
}