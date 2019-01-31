using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
	

	[SerializeField] private float speed = 5f;
	[SerializeField] private float jumpSpeed = 5f;
	//using rigidbody instead of transform as we will be using physics
	Rigidbody2D playerbody;
	//reference to Animator
	Animator anim;
	//my body collider
	CapsuleCollider2D collider2d;
	//feet collider box
	BoxCollider2D feetcollider;
	float gravityScaleAtStart;

	bool isAlive = true;

	void Start () {
		//get component rigidbody2d on player prefab
		playerbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		collider2d = GetComponent<CapsuleCollider2D> ();
		feetcollider = GetComponent<BoxCollider2D> ();
		gravityScaleAtStart = playerbody.gravityScale;
		anim.SetBool ("isAlive", true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// if Input on the horizontal axis which is 'd' or 'a' on keyboard 
		// move in the x direction based on the movementspeed
		//if (Input.GetAxis ("Horizontal")) 
		//{
			//translate moves the transform in the direction and distance of translation
		if (isAlive == true) 
		{
			Run ();
			FlipSprite ();
			Jump ();
			BetterJump ();
			ClimbLadder ();
			Die ();
		} 
		//}

	}

	private void Run()
	{
		
		//transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime,0f,0f));
		//make sure to include using UnityStandardAssets.CrossPlatformInput
		//using CrossPlatformInputManager to make it work for all devices including MOBILE
		float input = CrossPlatformInputManager.GetAxis("Horizontal");
		//
	    //Vector2 playerVelocity = new Vector2(input * speed, playerbody.velocity.y);
		//playerbody.velocity = playerVelocity;

		//velocity is a vector with components in x and y directions
		playerbody.velocity = new Vector2 (input * speed, playerbody.velocity.y);
		// https://docs.unity3d.com/ScriptReference/Rigidbody2D-velocity.html
		bool isMoving = Mathf.Abs (playerbody.velocity.x) > Mathf.Epsilon;
		anim.SetBool ("Running", isMoving);
		/*if (input != 0) {
			anim.SetBool ("Running", true);
		} else {
			anim.SetBool ("Running", false);
		}*/
	}

	private void Jump()
	{
		bool OnGround = feetcollider.IsTouchingLayers(LayerMask.GetMask ("Ground"));
			
		if (CrossPlatformInputManager.GetButtonDown ("Jump") && OnGround) 
		{
			Vector2 jumpVelocity = new Vector2 (0f, jumpSpeed);
			playerbody.velocity += jumpVelocity;
		}

			

	}

	private void BetterJump()
	{
		 float fallMultiplier = 1.5f;
		 float lowJumpMultiplier = 2f;

		//faster falling
		if (playerbody.velocity.y < 0) {

			playerbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}

		//control juimp height by length of time jump button held
		if (playerbody.velocity.y > 0 && !Input.GetButton ("Jump")) {
			playerbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}


	}

	private void ClimbLadder()
	{
		bool TouchingLadder = feetcollider.IsTouchingLayers(LayerMask.GetMask ("Ladder"));
		if (TouchingLadder == true) 
		{
			playerbody.gravityScale = 0f;
			//playerbody.gravityScale = 0;
			float input = CrossPlatformInputManager.GetAxis("Vertical");
			//velocity is a vector with components in x and y directions
			playerbody.velocity = new Vector2 (playerbody.velocity.x, input * speed);

			if (CrossPlatformInputManager.GetButton ("Vertical")) 
			{
				anim.SetBool ("Climbing", true);
			} 
			else 
			{
				anim.SetBool ("Climbing", false);
			}
		}

		else 
		{
			playerbody.gravityScale = gravityScaleAtStart;
			anim.SetBool ("Climbing", false);
		}
	
	}

	private void FlipSprite()
	{
		//Mathf.Abs is absolute value(minus means nothing) Mathf.Epsilon tiny float variable but not 0
		// if the player is moving horizontally
		bool isMoving = Mathf.Abs (playerbody.velocity.x) > Mathf.Epsilon;

		if(isMoving)
		{
			//accessing scale in transform of player
			//new vector2 if velocity is postive 'd' sprite scale is 1,1
			//if velocity is negative 'a' sprite scale is -1,1
			transform.localScale = new Vector2 (Mathf.Sign (playerbody.velocity.x), 1f);

			/*
			//my attempt at it works the same
			//reverse the current scaling of x axis
			float aDir = Mathf.Sign(playerbody.velocity.x);
			Debug.Log (aDir);
			if (aDir == -1) 
			{
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			} 
			if(aDir == 1)
			{
				transform.localScale = new Vector3 (1f, 1f, 1f);
			}*/

		}

	}

	/*private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "Enemy" || col.name == "Hazards") 
		{
			playerbody.constraints = RigidbodyConstraints2D.FreezePositionX;
			isAlive = false;
			anim.SetBool ("isAlive", false);
			Debug.Log ("GameObject2 collided with " + col.name);
			speed = 0f;
			FindObjectOfType<GameSession> ().PlayerDeath ();
		}
	}*/

	private void Die()
	{
		if (collider2d.IsTouchingLayers (LayerMask.GetMask ("Enemy", "Hazards"))) 
		{
			isAlive = false;
			anim.SetBool ("isAlive", false);
			//Debug.Log ("GameObject2 collided with " + col.name);
			speed = 0f;
			FindObjectOfType<GameSession> ().PlayerDeath ();
		}


	}
}
