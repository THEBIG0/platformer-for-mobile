using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	//Author: Owen.Gunter
	//Purpose: To give functionality to the player see Update()

	//References and Notes:
	// https://docs.unity3d.com/ScriptReference/Rigidbody2D-velocity.html
	//make sure to include using UnityStandardAssets.CrossPlatformInput
	//using CrossPlatformInputManager to make it work for all devices including MOBILE
	
	//movement speed of our character
	[SerializeField] private float speed = 5f;
	//the higher the jump speed the more you jump
	[SerializeField] private float jumpSpeed = 5f;

	//using rigidbody instead of transform as we will be using physics
	Rigidbody2D playerbody;
	//reference to Animator
	Animator anim;
	//my body collider
	CapsuleCollider2D collider2d;
	//feet collider box
	BoxCollider2D feetcollider;
	//see ClimbLadder for usage
	float gravityScaleAtStart;
	// if player is alive functions may be called
	bool isAlive = true;

	void Start () {
		//get component Rigidbody2d on player prefab
		playerbody = GetComponent<Rigidbody2D> ();
		//get component Animator on player prefab
		anim = GetComponent<Animator> ();
		//get component CapsuleCollider2D on player prefab
		collider2d = GetComponent<CapsuleCollider2D> ();
		//get component BoxCollider2D on player prefab
		feetcollider = GetComponent<BoxCollider2D> ();
		//reference to rigidbody gravity in editor value is 1
		gravityScaleAtStart = playerbody.gravityScale;
		anim.SetBool ("isAlive", true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// if player is alive functions may be called
		if (isAlive == true) 
		{
			Run ();
			Jump ();
			BetterJump ();
			ClimbLadder ();
			FlipSprite ();
			Die ();
		} 

	}

	private void Run()
	{
		
		// if Input on the horizontal axis which is 'd' or 'a' on keyboard 
		// move in the x direction based on the movementspeed
		float input = CrossPlatformInputManager.GetAxis("Horizontal");

		//velocity is a vector with components in x and y directions
		playerbody.velocity = new Vector2 (input * speed, playerbody.velocity.y);

		//if player is moving in any x direction...
		bool isMoving = Mathf.Abs (playerbody.velocity.x) > Mathf.Epsilon;
		//set animation to running
		anim.SetBool ("Running", isMoving);
	}

	private void Jump()
	{
		//if the players feet is touching the ground...
		bool OnGround = feetcollider.IsTouchingLayers(LayerMask.GetMask ("Ground"));
		//and you press jump button button you may jump	
		if (CrossPlatformInputManager.GetButtonDown ("Jump") && OnGround) 
		{
			//Vector2 jumpVelocity = new Vector2 (0f, jumpSpeed);
			//playerbody.velocity += jumpVelocity;
			playerbody.velocity += new Vector2(0f, jumpSpeed);
		}

			

	}

	private void BetterJump()
	{
		 // how fast you fall
		 float fallMultiplier = 1.5f;
		 // how low you jump based on how much you press spacebar
		 float lowJumpMultiplier = 2f;

		//faster falling regardless of jump button pressed
		if (playerbody.velocity.y < 0) 
		{
			//Maths: current velocity + (0,1) * -20 * (variable - 1) * Time.deltaTime = -10 fall speed
			playerbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}

		//control jump height by length of time jump button held
		//same as above but condition is different
		if (playerbody.velocity.y > 0 && !Input.GetButton ("Jump")) 
		{
			//Maths: current velocity + (0,1) * -20 * (variable - 1) * Time.deltaTime = -20 fall speed
			playerbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}


	}

	private void ClimbLadder()
	{
		//if BoxCollider2D is touching the ladder layer...
		bool TouchingLadder = feetcollider.IsTouchingLayers(LayerMask.GetMask ("Ladder"));

		if (TouchingLadder == true) 
		{
			playerbody.gravityScale = 0f;
		
			// if Input on the horizontal axis which is 'w' or 's' on keyboard 
			// move in the y direction based on the movementspeed
			float input = CrossPlatformInputManager.GetAxis("Vertical");
			// same as Run() but in opposite direction
			playerbody.velocity = new Vector2 (playerbody.velocity.x, input * speed);

			//if 'w' or 's' is pressed play animation
			if (CrossPlatformInputManager.GetButton ("Vertical")) 
			{
				anim.SetBool ("Climbing", true);
			} 
			else 
			{
				anim.SetBool ("Climbing", false);
			}
		}

		// else gravity is 1f and climbing animation set to false
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
			//x changes based on player velocity in x direction
			transform.localScale = new Vector2 (Mathf.Sign (playerbody.velocity.x), 1f);
		}

	}

	private void Die()
	{
		
		//if CapsuleCollider2D is touching enemy or hazard layers...
		//if (collider2d.IsTouchingLayers (LayerMask.GetMask ("Enemy", "Hazards"))) 
		if (collider2d.IsTouchingLayers (LayerMask.GetMask ("Hazards"))) 
		{
			
			//player dies and can't move
			isAlive = false;
			anim.SetBool ("isAlive", false);
			speed = 0f;
			//See GameSession.cs to see PlayerDeath function
			FindObjectOfType<GameSession> ().PlayerDeath (true);
		}
			
	}

	int counter = 0;
	void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.tag == "Slime" && counter == 0)
		{
		    FindObjectOfType<GameSession> ().PlayerDeath (false);
			counter++;
			print (counter);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Slime" && counter == 1) 
		{
			counter = 0;
			print ("counter is" + counter);
		}
	}
}
