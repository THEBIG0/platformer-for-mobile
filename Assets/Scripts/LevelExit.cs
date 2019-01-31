using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

	// delays for 2 seconds until it loads the next level
	// Note: in editor value is 0.4 as its a fifth(1/5) of 2 so it scales
	// with out timescale variable
	[SerializeField] float LevelLoadDelay = 2f;
	[SerializeField] float LevelExitSlowMotion = 0.2f;

	void OnTriggerEnter2D(Collider2D other)
	{
		//On collision with LevelExit object load next level with 2 second delay
		StartCoroutine(LoadNextLevel());
	}

	IEnumerator LoadNextLevel()
	{
		//time is reduced by 1/5 of its normal time to create slow-mo effect
		Time.timeScale = LevelExitSlowMotion;
		//wait 2 seconds to execute below code
		yield return new WaitForSeconds (LevelLoadDelay);
		Time.timeScale = 1f;
		//Load the next Scene
		var currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentSceneIndex + 1);
	}
}
