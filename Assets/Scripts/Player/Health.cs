using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum PlayerHealthState
{
	Alive,
	Dead
}


[System.Serializable]
public class Health : MonoBehaviour {

	[HideInInspector]
	public PlayerHealthState playerState;

	[HideInInspector]
	public int maxHP;

	[HideInInspector]
	public int currentHP;

	[HideInInspector]
	public int nextHP;

	public Slider healthSlider;
	public Image damageIMG;
	public Text HealthText;

	private float _damageTime = 5f;
	private Color _damageColor = new Color(1f, 0f, 0f, 0.5f);
	
	Movement playerMovement;
	Animator anim;
	bool damaged;
	bool isDead;

	void Awake()
	{
		maxHP = 121;
		currentHP = maxHP;

		playerState = PlayerHealthState.Alive;

		playerMovement = GetComponent<Movement>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		healthSlider.maxValue = maxHP;
		healthSlider.value = currentHP;
		HealthText.text = currentHP.ToString() + " / " + maxHP.ToString();

		if(damaged)
		{
			damageIMG.color = _damageColor;
		}
		else
		{
			damageIMG.color = Color.Lerp(damageIMG.color, Color.clear, _damageTime * Time.deltaTime);
		}
		damaged = false;

		if(currentHP >= maxHP)
		{
			currentHP = maxHP;
		}
		if(currentHP < 0)
		{
			currentHP = 0;
			Death();
		}
	}

	public void TakeDamage(int dmg)
	{
		damaged = true;

		currentHP -= dmg;
		
		if(currentHP <= 0 && !isDead)
		{
			//Die!
			Death();

			//Lose XP
			ExperienceBar xp = GetComponent<ExperienceBar>();
			xp.AddXP( -(50 * xp.level) );
		}
	}

	void Death()
	{
		playerState = PlayerHealthState.Dead;

		isDead = true;
		SendMessage("ToggleInputCursor", true);
		print("deaded is " + isDead);

		HealthText.text = "YOU ARE DEAD";
		HealthText.color = Color.white;

		//anim.SetTrigger("Die");
		//playerAudio.clip = deathSound;
		//playerAudio.Play();

		//Movement.enabled = false;
	}
}
