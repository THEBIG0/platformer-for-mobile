using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	[SerializeField] AudioClip coinPickUpSFX;
	[SerializeField] int pointsForCoinPickup = 100;

	BoxCollider2D box;

	void Start(){
		box = GetComponent<BoxCollider2D> ();
	}

	void Update(){

		CoinEvent ();
	}

	private void CoinEvent()
	{
		if (box.IsTouchingLayers (LayerMask.GetMask ("Player"))) 
		{
			FindObjectOfType<GameSession> ().AddToScore (pointsForCoinPickup);
			AudioSource.PlayClipAtPoint (coinPickUpSFX, Camera.main.transform.position);
			Destroy (gameObject);
		}
	}
}
