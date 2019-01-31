using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MUST INCLUDE FOR IT TO WORK
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour {

	public void StartFirstLevel()
	{
		//go to file > build settings and you will scenes mapped to numbers
		// 1 is level1
		SceneManager.LoadScene (1);
	}

	public void MainMenu()
	{
		// 0 is main menu
		SceneManager.LoadScene (0);
	}
}
