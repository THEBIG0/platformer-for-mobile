using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public Rigidbody2D projectile;
	public Transform Launcher;
	public float projectileSpeed = 50f;
	//TO DO: ADD LIFE TIME TO BULLET AND ADD COLLIDER TO IT

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonDown ("Fire1")) 
		{
			Rigidbody2D projectileInstance;
			if (transform.localScale.x == 1)
			{
				projectileInstance = Instantiate (projectile, Launcher.position, Launcher.rotation) as Rigidbody2D;
				projectileInstance.AddForce (Launcher.right * projectileSpeed);
			} 
			else 
			{
				projectileInstance = Instantiate (projectile, Launcher.position, Launcher.rotation) as Rigidbody2D;
				projectileInstance.AddForce (-Launcher.right * projectileSpeed);
			}
		}

    }
}
