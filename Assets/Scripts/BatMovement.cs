using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
	//Author: Owen.Gunter
	//Purpose: To give enemy movement

	//movement speed of the enemy
	[SerializeField] float moveSpeed = 1f;
	//enemys rigidbody2d
	Rigidbody2D enemyrb;


	// Use this for initialization
	void Start () 
	{
		//reference to enemys rigidbody2d component
		enemyrb = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () 
	{

		if (transform.localScale.y > 0) 
		{
			enemyrb.velocity = new Vector2 (0f, moveSpeed);
		} 
		else 
		{
			enemyrb.velocity = new Vector2 (0f, -moveSpeed);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		//if boxcollider attached to enemy exits the foreground(which is what the platform is painted on), reverse direction of the enemy sprite
		// we use - to reverse direction 
		if (col.name == "ForeGround") 
		{
			//transform.localScale = new Vector2 (-(Mathf.Sign (enemyrb.velocity.x)), 1f);
			transform.localScale = new Vector2 (1f,-(Mathf.Sign(enemyrb.velocity.y)));
		}
	}
}
