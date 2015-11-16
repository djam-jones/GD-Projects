using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Menus 
{
	MainMenu, 
	Continue, 
	NewGame, 
	Options, 
	Credits
}

public class MainMenu : MonoBehaviour {

	public Button continueBTN;
	public Menus currentMenu;

	public GameObject mainMenu;
	public GameObject continueMenu;
	public GameObject newGameMenu;
	public GameObject optionsMenu;
	public GameObject creditsMenu;

	void Start()
	{
		mainMenu.SetActive(true);

		continueMenu.SetActive(false);
		newGameMenu.SetActive(false);
		optionsMenu.SetActive(false);
		creditsMenu.SetActive(false);
	}

	void Update()
	{
		switch(currentMenu)
		{
		case Menus.Continue:
			//Load Saved Game File
			break;

		case Menus.Credits:
			//Show Credits Panel
			creditsMenu.SetActive(true);

			//Hide others
			HideMenu(mainMenu);
			HideMenu(continueMenu);
			HideMenu(newGameMenu);
			HideMenu(optionsMenu);
			break;

		case Menus.MainMenu:
			//Show Main Menu
			mainMenu.SetActive(true);

			//Hide others
			HideMenu(continueMenu);
			HideMenu(newGameMenu);
			HideMenu(optionsMenu);
			HideMenu(creditsMenu);
			break;

		case Menus.NewGame:
			//Show New Game Menu
			newGameMenu.SetActive(true);

			//Hide others
			HideMenu(mainMenu);
			HideMenu(continueMenu);
			HideMenu(creditsMenu);
			HideMenu(optionsMenu);
			break;

		case Menus.Options:
			//Show Options
			optionsMenu.SetActive(true);

			//Hide others
			HideMenu(mainMenu);
			HideMenu(continueMenu);
			HideMenu(newGameMenu);
			HideMenu(creditsMenu);
			break;
		}
	}



	public void ContinueGame()
	{
		//Continue Current Game File
		currentMenu = Menus.Continue;
	}

	public void NewGame()
	{
		//Go to Character Creation Screen
		currentMenu = Menus.NewGame;
	}

	public void Options()
	{
		//Go to Options Menu
		currentMenu = Menus.Options;
	}

	public void Credits()
	{
		//Go to Credits Screen
		currentMenu = Menus.Credits;
	}

	public void BackToMenu()
	{
		//Back to Main Menu screen.
		currentMenu = Menus.MainMenu;
	}

	//Quit Game
	public void Quit()
	{
		Application.Quit();
	}


	private void HideMenu(GameObject menu)
	{
		if(menu.activeInHierarchy)
			menu.SetActive(false);
	}

}
