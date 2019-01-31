using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {
	[SerializeField] int playerlives = 3;
	[SerializeField] int score = 0;

	[SerializeField] Text livesText;

	[SerializeField] Text scoreText;

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
		livesText.text = playerlives.ToString();
		scoreText.text = score.ToString();

	}

	void Update(){
		PlayerWins ();
	}

	public void AddToScore(int pointsToAdd)
	{
		score += pointsToAdd;
		scoreText.text = score.ToString();
	}
	
	public void PlayerDeath()
	{
		if (playerlives > 1) {
			TakeLife ();
		}
		else 
		{
			 ResetGameSession();
		}
	}

	public void PlayerWins()
	{
		//get currentSceneIndex
		var currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		//if index equals success destroy this gameobject to reset score and lives
		if (currentSceneIndex == 3) 
		{
			
			Destroy (gameObject);
		}
	}

	public void ResetGameSession()
	{
		SceneManager.LoadScene (0);
		Destroy (gameObject);
	}

	private void TakeLife()
	{
		playerlives--;
		//Reload the current scene
		var currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentSceneIndex);
		livesText.text = playerlives.ToString();
	}
}
