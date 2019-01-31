using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	
	//Author: Owen.Gunter
	//Purpose: To give properties to the coin

	//gives abitlity to set up audioclip you want to play in editor
	[SerializeField] AudioClip coinPickUpSFX;
	//amount of points picked up for each coin
	[SerializeField] int pointsForCoinPickup = 100;

	BoxCollider2D box;

	void Start()
	{
		//reference to box collider surrounding the coin
		box = GetComponent<BoxCollider2D> ();
	}

	void Update()
	{
		CoinEvent ();
	}

	private void CoinEvent()
	{
		//if box collider is touching the player...
		if (box.IsTouchingLayers (LayerMask.GetMask ("Player"))) 
		{
			//call AddToScore from GameSession and pass in a variable
			FindObjectOfType<GameSession> ().AddToScore (pointsForCoinPickup);
			//Play the audio clip where the main camera is
			AudioSource.PlayClipAtPoint (coinPickUpSFX, Camera.main.transform.position);
			//then destory the gameobject
			Destroy (gameObject);
		}
	}
}
