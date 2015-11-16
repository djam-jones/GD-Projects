using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Magic : MonoBehaviour {

	[HideInInspector]
	public float currentMP;

	[HideInInspector]
	public float maxMP = 97;

	private float _nextMP;

	public Slider magicSlider;
	public Text MagicText;

	private Animator _anim;

	void Start()
	{
		currentMP = maxMP;

		_anim = GetComponent<Animator>();
	}

	void Update()
	{
		magicSlider.maxValue = maxMP;
		magicSlider.value = currentMP;
		MagicText.text = currentMP.ToString("f0") + " / " + maxMP.ToString("f0");

		if(currentMP < maxMP)
		{
			currentMP += (Time.deltaTime * 0.3f);
		}

		if(currentMP >= maxMP)
		{
			currentMP = maxMP;
		}
		if(currentMP < 0)
		{
			currentMP = 0;
		}
	}

	public void DecreaseMagic(float amount)
	{
		currentMP -= amount;
	}
}
