using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] float moveSpeed = 1f;
	Rigidbody2D enemyrb;


	// Use this for initialization
	void Start () 
	{
		enemyrb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (IsFacingRight ()) 
		{
			enemyrb.velocity = new Vector2 (moveSpeed, 0f);

		} 
		else 
		{
			enemyrb.velocity = new Vector2 (-moveSpeed, 0f);
		
		}
	}

	bool IsFacingRight ()
	{
		return transform.localScale.x > 0;

	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.name == "ForeGround") {
			transform.localScale = new Vector2 (-(Mathf.Sign (enemyrb.velocity.x)), 1f);
		}
		//Debug.Log ("Enemy has exited collided with " + col.name);
	}


}
