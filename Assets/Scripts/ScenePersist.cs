using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {

	//Author: Owen.Gunter
	//Purpose: To save information like pickups so that when you die (and still have lives left) and have already picked up the coin the coin will not reappear.

	int startingSceneIndex;

	//a singleton
	private void Awake()
	{
		//gets number of objects with GameSession script in a scene
		int ScenePersistSessions = FindObjectsOfType<ScenePersist> ().Length;
		// we only want 1 ScenePersist object within a scene
		if (ScenePersistSessions > 1) 
		{
			Destroy (gameObject);
		} 
		// if there is 1 ScenePersist object, which their always will be dont destroy it
		else 
		{
			DontDestroyOnLoad (gameObject);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		//gets index of the active Scene, see file > build settings
		int currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		//if current scene equals main menu or success destory ScenePersist to reset pickups
		if (currentSceneIndex == 0 || currentSceneIndex == 3) 
		{
			//Destroy object with this script attached
			Destroy (gameObject);
		}
	}
}
