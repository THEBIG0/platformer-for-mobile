using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
	//Author Owen.Gunter
	//Purpose: To give the enemy the ability to look at the player and shoot at a particular distance


	//rigidbody for enemy bullet
	public Rigidbody2D projectile;

	public Transform Launcher;
	// how fast the projectile goes
	public float projectileSpeed = 20f;
	// How long it takes for the next shot to fire
	public float delay = 1f;
	//this is the maximum distance from the enemy to the player that the enemy will be able to shoot
	public int maxAttackDistance = 50;

	//target is the player
	public Transform target;

	// starts at zero and equals whatever Time.time was before
	private float lastFireTime;

	Animator anim;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		anim = GetComponent<Animator> ();
	}
	void Update()
	{
		//if target is not null which will always be true
		//Look at the target which is the player
		if (target != null) 
		{
			//transform.LookAt (target);
		}
		//this gets the distance from the enemy to the player at every frame
		float distance = Vector3.Distance (target.position, transform.position);
		//print (distance);
		//print (maxAttackDistance);

		//Time.time means the time in seconds since start of the game
		//if the current distance is less than the maxAttackDistance and...
		// if time is greater than the last time it fired plus whatever the delay is
		// set lastFireTime = to the current time
		// and call Fire function
		if ((distance <= maxAttackDistance) && Time.time > (lastFireTime + delay)) 
		{

			//print (Time.time);
			//	print (lastFireTime);
				anim.SetBool ("IsReadyToFire", true);

				lastFireTime = Time.time;
				Fire ();
			

		}

		else 
		{
			anim.SetBool ("IsReadyToFire", false);
		}

	}


	private void Fire()
	{
		Rigidbody2D projectileInstance;
		projectileInstance = Instantiate (projectile, Launcher.position, Launcher.rotation) as Rigidbody2D;

		projectileInstance.AddForce (-Launcher.right * projectileSpeed);

	}

}
