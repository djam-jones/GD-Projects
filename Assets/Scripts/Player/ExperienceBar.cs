using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class ExperienceBar : MonoBehaviour {

	[HideInInspector]
	public static ExperienceBar instance;

	public GameObject levelUpEffect;

	[HideInInspector]
	public int level = 1;

	private int _maxLevel = 60;

	private float _currentExperience;
	private float _maxExperience;
	private float _nextExperience;

	public Slider expSlider;
	public Text expText;
	public Text levelText;

	private bool _xpAddable;

	private Health _playerHP;
	private Magic _playerMP;

	void Awake()
	{
		instance = this;

		_xpAddable = true;

		_maxExperience = 400;
		_currentExperience = 0;
		_nextExperience = Random.Range(490f, 510f);

		_playerHP = GetComponent<Health>();
		_playerMP = GetComponent<Magic>();
	}

	void Update()
	{
		levelText.text = level.ToString();
		expText.text = _currentExperience.ToString("f0") + " / " + _maxExperience.ToString("f0");
		expSlider.value = _currentExperience;
		expSlider.maxValue = _maxExperience;

		if(Input.GetKeyDown(KeyCode.T))
		{
			AddXP(Random.Range(1000, 1200));
		}

		if(level >= _maxLevel)
		{
			_xpAddable = false;
		}

		if(_currentExperience <= 0)
		{
			_currentExperience = 0;
		}
	}

	public void AddXP(float experience)
	{
		if(_xpAddable)
		{
			_currentExperience += experience;
			UpdateExperienceBar(_currentExperience);
		}
		else if(!_xpAddable)
		{
			_currentExperience = 0;
		}
	}

	IEnumerator DoLevelUpEffect(float waitTime)
	{
		if(!levelUpEffect.activeInHierarchy)
		{
			levelUpEffect.SetActive(true);
			yield return new WaitForSeconds(waitTime);
		}
		levelUpEffect.SetActive(false);
	}

	void UpdateExperienceBar(float experience)
	{
		if(_currentExperience >= _maxExperience && _xpAddable)
		{
			UpdateMaxExperience();
			expSlider.value = _currentExperience;

			level++;
			StartCoroutine(DoLevelUpEffect(4));
			UpgradesPerLevel();
		}
	}

	void UpdateMaxExperience()
	{
		_currentExperience -= _maxExperience;
		_maxExperience += _nextExperience;
		expSlider.value = _currentExperience;
	}

	void UpgradesPerLevel()
	{
		//Add Skill Point
		
		//Add Attribute Points:
		_playerHP.maxHP += Random.Range(10, 20) * (level);
		_playerHP.currentHP = GetComponent<Health>().maxHP;

		_playerMP.maxMP += Random.Range(5, 15) * (level);
		_playerMP.currentMP = GetComponent<Magic>().maxMP;
		//Upgrade Stamina Points
		
		//Upgrade Attack Stat
		//Upgrade Special Attack Stat
		//Upgrade Defense Stat
		//Upgrade Special Defense Stat
	}

}
