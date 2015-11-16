using UnityEngine;
using System.Collections;

public enum EnemyState 
{
	Idle, 
	Patrol, 
	Attacking, 
	Defending, 
	Dead
}

public class Enemy : MonoBehaviour {

	
	[HideInInspector]
	public EnemyState enemyState;
	
	public int enemyLvl;

	[HideInInspector]
	public int health;
	private int _maxHealth;

	[HideInInspector]
	public bool isDead;

	private float _totalDamage;
	protected float _attackStat;
	protected float _damage;

	protected float _defenseStat;
	

	[HideInInspector]
	public float experiencePoints;
	private float _minXP = 14f;
	private float _maxXP = 36f;

	void Start()
	{
		_minXP = _minXP * enemyLvl;
		_maxXP = _maxXP * enemyLvl;

		experiencePoints = Random.Range(_minXP, _maxXP);

		_maxHealth = 75;
		health = _maxHealth;
	}

	void Update()
	{
		CheckHealth();
	}

	/// <summary>
	/// Checks the health.
	/// </summary>
	void CheckHealth()
	{
		//If health goes above maximum health return to maximum health.
		if(health >= _maxHealth)
		{
			health = _maxHealth;
		}
		//If health goes below zero return to zero.
		if(health < 0)
		{
			health = 0;
		}
	}

	public void TakeDamage(int dmg)
	{
		health -= dmg;

		//If health drops to zero trigger Death Function.
		if(health <= 0 && !isDead)
		{
			OnDeath();
		}
	}

	/// <summary>
	/// Raises the death event.
	/// </summary>
	void OnDeath()
	{
		enemyState = EnemyState.Dead;

		//Set Death Bool to True.
		isDead = true;

		//Trigger Death Animation.

		//Send Message to give out Experience Points.
		ExperienceBar.instance.SendMessage("AddXP", experiencePoints);
	}
}
