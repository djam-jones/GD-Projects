using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Inventory : MonoBehaviour {

	[HideInInspector] //Integer for the amount of total gold.
	public int 				totalGold;

	[HideInInspector] //Integer for the current capacity of the Inventory.
	public int 				currentInventoryCapacity;
	[HideInInspector] //Integer for the maximum capacity of the Inventory.
	public int 				maximumInventoryCapacity;

	[SerializeField] //Displays the amount of the Player has.
	private Text 			_goldInfo;

	[SerializeField] //Displays the current and maximum capacity of the inventory.
	public Text 			inventoryCapacity;

	[SerializeField] //Displays the Inventory User Interface.
	public GameObject 		inventoryUI;

	[SerializeField] //Displays the info and stats of a certain item.
	public GameObject 		inventoryItemStats;

	[SerializeField] //Displays the Equip/Use & Remove panel.
	public GameObject		inventoryEquipPanel;

	[SerializeField] //Holds all the Items Buttons.
	public GameObject		inventory;

	[SerializeField] //Button for Inventory Item.
	public Button 			inventoryItemButton;

	[HideInInspector] //Boolean for if the button is selected.
	public bool				aButtonIsSelected = false;

	[SerializeField]
	private List<Item> 		_inventoryList = new List<Item>();

	[SerializeField]
	private List<Item>		_selectedItem = new List<Item>();

	private Button			_selectedButton;

	private GameObject		_itemBodyPlacement;

	private GameObject		_mainHand;
	private GameObject		_offHand;
	private GameObject		_body;


	private GameObject		_fpc;

	void Awake()
	{
		_fpc = this.gameObject;

		_mainHand = GameObject.Find("MainHand");

		inventoryItemStats.SetActive(false);
		inventoryEquipPanel.SetActive(false);
	}

	void Start()
	{
		_selectedItem.Capacity = 1;

		//Set Maximum Inventory Capacity.
		maximumInventoryCapacity = 20;

		AddItem(ItemDatabase.itemList[1]);
		AddItem(ItemDatabase.itemList[3]);

		UpdateItemButtons();
		ToggleInventoryScreen();
	}

	void Update()
	{
		//Methods
		if(Input.GetKeyDown(KeyCode.I))
		{
			ToggleInventoryScreen();
			CameraRotation.instance.SendMessage("ToggleRotation");
		}

		//Others
		currentInventoryCapacity = _inventoryList.Count;
		_inventoryList.Capacity = maximumInventoryCapacity;

		_goldInfo.text = totalGold.ToString();
		inventoryCapacity.text = currentInventoryCapacity.ToString() + "/" + maximumInventoryCapacity.ToString();


		if( currentInventoryCapacity >= maximumInventoryCapacity )
		{
			currentInventoryCapacity = maximumInventoryCapacity;
			inventoryCapacity.color = new Color(255, 0, 0);
		}

		if( totalGold <= 0 )
		{
			totalGold = 0;
			_goldInfo.color = new Color(255, 0, 0);
		}


		if(_selectedItem != null && aButtonIsSelected && Input.GetKeyDown(KeyCode.E))
		{
			UseItem(_selectedItem[0]);
		}

		if(_selectedItem != null && aButtonIsSelected && Input.GetKeyDown(KeyCode.R))
		{
			RemoveItem(_selectedItem[0]);
		}

		if(Input.GetKeyDown(KeyCode.Z))
		{
			Deselect();
		}
	}

	public void AddItem(Item item)
	{
		//Add item to list.
		_inventoryList.Add(item);
	}

	public void RemoveItem(Item item)
	{
		//Remove item from list.
		_inventoryList.Remove(item);

		//Destroy Button in Inventory List.
		UpdateItemButtons();

		//Hide Equip/Use Panel
		if(inventoryEquipPanel.activeInHierarchy)
		{
			inventoryEquipPanel.SetActive(false);
		}
	}

	//Use the selected item.
	public void UseItem(Item item)
	{
		Weapon weaponIsItem = null;
		Armor armorIsItem = null;
		Potion potionIsItem = null;

		GameObject itemObject;

		//Instantiate Item
		if(item is Weapon)
		{
			weaponIsItem = item as Weapon;
			switch(weaponIsItem.weaponHandType)
			{
				case Weapon.WeaponHandType.OneHanded:
				_itemBodyPlacement = _mainHand;
				break;

				case Weapon.WeaponHandType.TwoHanded:
				_itemBodyPlacement = _mainHand;
				//Occupy Offhand.
				break;
			}
		}
		itemObject = Instantiate(item.gameObject, _itemBodyPlacement.transform.position, _itemBodyPlacement.transform.rotation) as GameObject;
		itemObject.transform.SetParent(_itemBodyPlacement.transform);
		itemObject.transform.localScale = new Vector3(1, 1, 1);

		//Hide Equip/Use Panel
		if(inventoryEquipPanel.activeInHierarchy)
		{
			inventoryEquipPanel.SetActive(false);
		}
	}

	public void UpdateItemButtons()
	{
		int inventoryIndex = 0;

		foreach( Item item in _inventoryList )
		{
			//Create a button for every item in the inventory.
			GameObject button;
			button = Instantiate(inventoryItemButton.gameObject, inventory.transform.position, Quaternion.identity) as GameObject;

			//Set Button Parent.
			button.transform.SetParent(inventory.transform);

			//Set Button Position.
			RectTransform buttonPosition = button.GetComponent<RectTransform>();
			button.transform.position = new Vector3(inventory.transform.position.x, (inventory.transform.position.y + 450) + (buttonPosition.sizeDelta.y * -inventoryIndex), 0);

			//Set Button Scale.
			button.transform.localScale = new Vector3(1, 1, 1);

			//Set Button Name.
			button.name = inventoryIndex + "_" + item.itemName + "_Button";

			//Set Button Texts to Name and Value.
			Text[] itemTexts = button.GetComponentsInChildren<Text>();
			itemTexts[0].text = item.itemName;
			itemTexts[1].text = item.itemValue.ToString() + " Gold";

			//Set Button Image to Item Sprite.
			Image[] itemImg = button.GetComponentsInChildren<Image>();
			itemImg[1].sprite = item.itemSprite;

			//Add Button Events
			AddButtonEvents( button.GetComponent<Button>(), item.itemName, item );

			inventoryIndex++;
		}
	}

	public void AddButtonEvents(Button thisButton, string name, Item item)
	{
		//Add Click Event to Button.
		thisButton.onClick.AddListener(() => Selected(thisButton, name, item));



		//Add OnHover Event to Button.
		EventTrigger.TriggerEvent triggerOn = new EventTrigger.TriggerEvent();
		EventTrigger.Entry entryOn = new EventTrigger.Entry();

		triggerOn.AddListener((eventData) => ShowInfoOnHover(name, item));
		entryOn.eventID = EventTriggerType.PointerEnter;
		entryOn.callback = triggerOn;


		//Add OffHover Event to Button.
		EventTrigger.TriggerEvent triggerOff = new EventTrigger.TriggerEvent();
		EventTrigger.Entry entryOff = new EventTrigger.Entry();

		triggerOff.AddListener((eventData) => HideInfoOffHover(name, item));
		entryOff.eventID = EventTriggerType.PointerExit;
		entryOff.callback = triggerOff;

		//Add Both Events to EventTrigger Component.
		thisButton.GetComponent<EventTrigger>().enabled = true;
		thisButton.GetComponent<EventTrigger>().triggers.Add(entryOn);
		thisButton.GetComponent<EventTrigger>().triggers.Add(entryOff);
	}

	/// <summary>
	/// Shows the information regarding the Item of the Button being hovered on.
	/// </summary>
	/// <param name="itemName">Item name.</param>
	/// <param name="item">Item.</param>
	public void ShowInfoOnHover(string itemName, Item item)
	{
		//Show Item Information Panel
		if(!inventoryItemStats.activeInHierarchy)
		{
			inventoryItemStats.SetActive(true);
			
			Text[] itemInfoTexts = inventoryItemStats.GetComponentsInChildren<Text>();
			itemInfoTexts[1].text = itemName;
			itemInfoTexts[2].text = "Level: " + item.itemLvl.ToString();
			itemInfoTexts[3].text = "Value: " + item.itemValue.ToString();
			itemInfoTexts[4].text = "Description:" + "\n" + item.itemDescription;
		}
	}

	/// <summary>
	/// Hides the information regarding the Item of the Button it was hovered on.
	/// </summary>
	/// <param name="itemName">Item name.</param>
	/// <param name="item">Item.</param>
	public void HideInfoOffHover(string itemName, Item item)
	{
		//Hide Item Information Panel
		if(inventoryItemStats.activeInHierarchy)
		{
			Text[] itemInfoTexts = inventoryItemStats.GetComponentsInChildren<Text>();
			itemInfoTexts[1].text = "";
			itemInfoTexts[2].text = "";
			itemInfoTexts[3].text = "";
			itemInfoTexts[4].text = "";
			
			inventoryItemStats.SetActive(false);
		}
	}

	public void Selected(Button thisButton, string itemName, Item item)
	{
		print("Selected: " + itemName);

		if(_selectedButton != null)
		{
			_selectedButton.interactable = true;
			_selectedButton = null;
		}
		_selectedButton = thisButton;

		//Show Equip/Use Panel
		if(!inventoryEquipPanel.activeInHierarchy)
		{
			inventoryEquipPanel.SetActive(true);
		}

		//Add Selected Item to List.
		if(_selectedItem.Count == 0)
		{
			_selectedItem.Add(item);
		}
		else
		{
			_selectedItem.Clear();
			_selectedItem.Add(item);
		}

		thisButton.interactable = false;
		aButtonIsSelected = true;
	}

	public void Deselect()
	{
		if(_selectedButton != null)
		{
			_selectedButton.interactable = true;
			aButtonIsSelected = false;
		}

		//Hide Equip/Use Panel
		if(inventoryEquipPanel.activeInHierarchy)
		{
			inventoryEquipPanel.SetActive(false);
		}
	}

	public void ToggleInventoryScreen()
	{
		if(inventoryUI.activeInHierarchy == true)
		{
			inventoryUI.SetActive(false);
		}
		else if(inventoryUI.activeInHierarchy == false)
		{
			inventoryUI.SetActive(true);
		}
	}

}
