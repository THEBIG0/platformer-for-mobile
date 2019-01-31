using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MUST INCLUDE FOR IT TO WORK
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour {

	//Author: Owen.Gunter
	//Purpose: To be able to start the game and load main menu via buttons

	//Note: Both of these methods are called by pressing the button in game or editor

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
