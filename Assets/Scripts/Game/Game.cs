using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game {

	public static Game current;
	public Character character;

	public Game()
	{
		character = new Character();
	}

}
