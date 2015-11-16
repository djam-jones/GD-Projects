using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour {

	private NPCReadText _readDialogue;

	public bool canBeDamaged;
	public bool canDoDamage;
	public bool canTalkTo;

	[HideInInspector]
	public bool isTalking;

	[HideInInspector]
	public bool talkable;

	[SerializeField]
	private bool _isMoving;

	[SerializeField]
	private bool _isQuestgiver;

	public GameObject interactionCanvas;
	public GameObject eButton;

	public Text npcDialogue;
	public Image npcImage;

	public Sprite npcPicture;

	private GameObject _player;
	private float _talkingDist = 4f;

	void Awake()
	{
		_readDialogue = GetComponent<NPCReadText>();
		_player = GameObject.FindGameObjectWithTag(Tags.PlayerTag);
	}

	void Start()
	{
		CheckForQuestGiver();
	}

	void Update()
	{
		CheckTalkingDistance();

		if(talkable)
		{
			if(Input.GetKeyDown(KeyCode.E) && interactionCanvas.active == false)
			{
				//Set Interaction Canvas active
				interactionCanvas.SetActive(true);
				//Hide Interact System
				eButton.SetActive(false);

				isTalking = true;
			}
			_readDialogue.StartConvo();
		}
		else if(!talkable)
		{
			isTalking = false;
		}
	}

	void CheckTalkingDistance()
	{
		if(Vector3.Distance(this.transform.position, _player.transform.position) < _talkingDist)
			TalkToMe();
	}

	void CheckForQuestGiver()
	{
		if(_isQuestgiver)
		{
			SpriteRenderer _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
			_spriteRenderer.sprite = (Sprite)Resources.Load("Sprites/NPC Sprites/quest_circle.png");
			
//			Sprite questSprite = gameObject.AddComponent(Sprite) as Sprite;
//			questSprite.texture = Resources.Load("Sprites/NPC Sprites/Quest_Mark");
		}
	}

	public void TalkToMe()
	{
		if(canTalkTo)
		{
			talkable = true;
			
			//Show the Interact Button
			if(eButton.active == false)
			{
				eButton.SetActive(true);
			}
			
			//Set The Image to a picture of the NPC
			npcImage.overrideSprite = npcPicture;
		}
	}

	public void DontTalkToMe()
	{
		if(canTalkTo)
		{
			talkable = false;
			
			//Set Interaction Canvas inactive
			if(interactionCanvas.active == true)
			{
				interactionCanvas.SetActive(false);
			}
			
			//Hide Interact Button
			if(eButton.active == true)
			{
				eButton.SetActive(false);
			}
			
			//Remove The Image to a picture of the NPC
			npcImage.overrideSprite = null;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.transform.tag == Tags.PlayerTag)
		{
			DontTalkToMe();
		}
	}
}