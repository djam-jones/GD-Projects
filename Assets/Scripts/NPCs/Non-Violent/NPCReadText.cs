using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCReadText : MonoBehaviour {

	private NPC _npcScript;

	public TextAsset npcTextFile;
	private string[] _dialogueLines;
	private string _dialogLine;

	public Text dialogueBox;

	void Awake()
	{
		_npcScript = GetComponent<NPC>();
	}

	void Start()
	{
		//Check if there is a text file to get dialogue from
		if(npcTextFile != null)
		{
			//Add every line of the file to the dialogue
			//By using the next line as the separator
			_dialogueLines = ( npcTextFile.text.Split('\n') );
		}
	}

	public void StartConvo()
	{
		if(_npcScript.canTalkTo)
		{
			int currentLine = 0;

			for(int i = 0; i < _dialogueLines.Length; i++)
			{
				if(_dialogueLines[i] == _dialogLine)
				{
					currentLine = i;
				}
				if(currentLine == (_dialogueLines.Length - 1))
				{
					EndConvo();
					currentLine = 0;
				}
			}
			dialogueBox.text = _dialogueLines[currentLine];

			if(Input.GetKeyDown(KeyCode.T) && _npcScript.isTalking)
			{
				currentLine = (currentLine + 1) % _dialogueLines.Length;
				_dialogLine = _dialogueLines[currentLine];
			}
		}
	}

	public void EndConvo()
	{
		_npcScript.DontTalkToMe();
	}
}
