using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

	public static List<Game> savedGames = new List<Game>();

	public static void SaveGame()
	{
		SaveLoad.savedGames.Add(Game.current);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/" + Game.current.character.characterName + ".psg");
		bf.Serialize(file, SaveLoad.savedGames);
		file.Close();
	}

	public static void LoadGame()
	{
		if(File.Exists(Application.persistentDataPath + "/" + Game.current.character.characterName + ".psg"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + Game.current.character.characterName + ".psg", FileMode.Open);
			SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
			file.Close();
		}
	}


}
