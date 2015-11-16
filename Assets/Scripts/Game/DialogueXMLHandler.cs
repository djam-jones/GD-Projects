using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class DialogueXMLHandler : MonoBehaviour {

	[System.Serializable]
	public struct Node
	{
		private string _dialogueText;
		private Node[] responses;
	}

	public Text npcDialogueText;
	private Button[] _playerResponseOptions;

	private string _fileName;

	private XmlDocument _xmlDoc;
	public TextAsset textXml;

	public Node initialNode;

	void Awake()
	{
		_fileName = "npc_dialogue";
		npcDialogueText.text = "";
	}

	public void GetDialogue()
	{
		LoadXMLFile();

		XmlNodeList nodeList = _xmlDoc.SelectNodes("/dialogue");

		foreach(XmlNode node in nodeList)
		{

		}
	}

	/// <summary>
	/// This Method will Load an XML file from the XML Files folder under Assets.
	/// </summary>
	public void LoadXMLFile()
	{
		_xmlDoc = new XmlDocument();
		_xmlDoc.Load(_fileName);
		_xmlDoc.LoadXml(textXml.text);
	}
}