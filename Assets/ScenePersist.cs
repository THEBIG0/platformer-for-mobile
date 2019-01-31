using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {

	int startingSceneIndex;

	//a singleton
	private void Awake(){
		//gets number of objects with GameSession script in a scene
		int ScenePersistSessions = FindObjectsOfType<ScenePersist> ().Length;
		// we only want 1 GameSession object within a scene
		if (ScenePersistSessions > 1) 
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
		startingSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		//Debug.Log (startingSceneIndex);
	}
	
	// Update is called once per frame
	void Update () {
		int currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		if (currentSceneIndex != startingSceneIndex) 
		{
			//Debug.Log (currentSceneIndex);
			Destroy (gameObject);
		}
	}
}
