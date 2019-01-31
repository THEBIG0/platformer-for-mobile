using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

	//Author: Owen.Gunter
	//Purpose: To keep track of the variables and update them accordingly

	[SerializeField] int playerlives = 3;
	[SerializeField] int score = 0;

	[SerializeField] Text livesText;

	[SerializeField] Text scoreText;

	//a singleton
	private void Awake()
	{
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
	}

	// Use this for initialization
	void Start () {
		//Converts variables to string
		livesText.text = playerlives.ToString();
		scoreText.text = score.ToString();

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
	
	public void PlayerDeath()
	{
		//if player has more than 1 life call TakeLife()...
		if (playerlives > 1) {
			TakeLife ();
		}
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

	private void TakeLife()
	{
		//update lives
		playerlives--;
		//Reload the current scene
		var currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentSceneIndex);
		livesText.text = playerlives.ToString();
	}
}
