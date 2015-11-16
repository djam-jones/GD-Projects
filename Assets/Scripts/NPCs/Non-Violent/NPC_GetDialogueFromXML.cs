using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class NPC_GetDialogueFromXML : MonoBehaviour {

	public Text fileDataTextbox;
	
	private string _fileName;

	private XmlDocument _xmlDoc;
	public TextAsset xmlText;

	private List<Dictionary<string, string>> _dialogues = new List<Dictionary<string, string>>();
	private Dictionary<string, string> _obj;

	void Awake()
	{
		_fileName = "npc_dialogue";
		fileDataTextbox.text = "";
		GetDialogue();
	}

	void Update()
	{
		ReadDialogue();
	}

	public void GetDialogue()
	{
		LoadXMLFromAsset(); //Loads the XML File
		XmlNodeList dialoguesList = _xmlDoc.GetElementsByTagName("dialogue"); //Array of Dialogue Nodes

		foreach(XmlNode dialogueInfo in dialoguesList)
		{
			XmlNodeList dialogueContent = dialogueInfo.ChildNodes;
			_obj = new Dictionary<string, string>(); //Create an object(Dictionary) to collect both nodes and put them in the _dialogues[] List.

			foreach(XmlNode dialogueItems in dialogueContent) //Dialogue Item nodes.
			{
				if(dialogueItems.Name == "text")
				{
					//_obj.Add("npc_text", dialogueItems.InnerText); //Put this in the dictionary
				}

				if(dialogueItems.Name == "option")
				{
					switch(dialogueItems.Attributes["name"].Value)
					{
					case "optionOne": _obj.Add("optionOne", dialogueItems.InnerText); //Put this in the dictionary
						break;
					case "optionTwo": _obj.Add("optionTwo", dialogueItems.InnerText); //Put this in the dictionary
						break;
					case "optionThree": _obj.Add("optionThree", dialogueItems.InnerText); //Put this in the dictionary
						break;
					case "optionClose": _obj.Add("optionClose", dialogueItems.InnerText); //Put this in the dictionary
						break;
					}
				}
			}
			_dialogues.Add(_obj); //Add the _obj List in the _dialogues[] List
		}
	}

	public void ReadDialogue()
	{

	}

	/// <summary>
	/// This Method will Load an XML file from the XML Files folder under Assets.
	/// </summary>
	private void LoadXMLFromAsset()
	{
		_xmlDoc = new XmlDocument();
		_xmlDoc.LoadXml(xmlText.text);
	}

	/// <summary>
	/// This Method will retrieve the relative path as device platform.
	/// </summary>
	private string GetPath ()
	{
		#if UNITY_EDITOR
		return Application.dataPath + "/XML Files/" + _fileName;
		#else
		return Application.dataPath + "/" + _fileName;
		#endif
	}
}