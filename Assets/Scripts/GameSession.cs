using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

	//Author: Owen.Gunter
	//Purpose: To keep track of the variables and update them accordingly

	//[SerializeField] int PlayerHealth = 3;
	[SerializeField] int score = 0;

	[SerializeField] Text healthText;

	[SerializeField] Text scoreText;

	public float PlayerHealth = 100f;

	public float HazardDamage = 10f;
	public float EnemyDamage = 20f;
	Health hs;
	public Slider HealthBarSlider;
	//a singleton
	private void Awake()
	{
		//player.GetComponent<GameObject> ();
		//gets number of objects with GameSession script in a scene
		int numGameSessions = FindObjectsOfType<GameSession> ().Length;
		// we only want 1 GameSession object within a scene
		if (numGameSessions > 1) 
		{
			Destroy (gameObject);
		} 
		// if there is 1 gameSession, which their always will be dont destroy it
		else 
		{
			DontDestroyOnLoad (gameObject);
		}
		hs = GetComponent<Health> ();

	}

	// Use this for initialization
	void Start () {
		//Converts variables to string
		healthText.text = PlayerHealth.ToString();
		scoreText.text = score.ToString();
		//player.GetComponent<GameObject> ();
		hs.currentHealth = PlayerHealth;
	}

	void Update()
	{
		//check to see if player has won
		PlayerWins ();
	}

	//refer to coin.cs 
	public void AddToScore(int pointsToAdd)
	{
		//update score
		score += pointsToAdd;
		scoreText.text = score.ToString();
	}
	
	public void PlayerDeath(bool isHazard)
	{
		//if player has more than 1 life call TakeDamage()...
		if (PlayerHealth > 1 && isHazard) 
		{
			TakeDamage ();
		} 
		else if (PlayerHealth > 1 && isHazard == false)
		{
			TakeEnemyDamage ();
		}
		//TakeDamage ();
		//else reset the game back to the beginning
		else 
		{
			 ResetGameSession();
		}
	}

	public void PlayerWins()
	{
		//get currentSceneIndex
		var currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		//if index equals success which is 3, destroy this gameobject to reset score and lives
		if (currentSceneIndex == 3) 
		{
			//Destroy this GameSession
			Destroy (gameObject);
		}
	}

	public void ResetGameSession()
	{
		//Loads the Main Menu
		SceneManager.LoadScene (0);
		//Destroy this GameSession
		Destroy (gameObject);
	}

	private void TakeDamage()
	{
		//update lives
		PlayerHealth -= HazardDamage;
		HealthBarSlider.value = -PlayerHealth;
		//take hazard damage
		hs.CalculateDamage (HazardDamage);

		if (PlayerHealth >= 1) 
		{
			//Reload the current scene
			var currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
			SceneManager.LoadScene (currentSceneIndex);
			healthText.text = PlayerHealth.ToString ();
			print (PlayerHealth);
		} 

		else 
		{
			ResetGameSession ();
		}
	


	}

	private void TakeEnemyDamage()
	{
		PlayerHealth -= EnemyDamage;
		HealthBarSlider.value = -PlayerHealth;
		healthText.text = PlayerHealth.ToString();
		hs.CalculateDamage (EnemyDamage);
	}
}
