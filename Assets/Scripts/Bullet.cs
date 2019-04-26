using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float lifeTime = 0.5f;
	public float attackDamage = 10f;
	Health hs;
	void Start()
	{
		
	}
    // Update is called once per frame
    void Update()
    {
		Destroy (gameObject, lifeTime);
    }
	int counter = 0;
	void OnTriggerEnter2D(Collider2D other)
	{
		if (counter == 0) {
			print (gameObject.tag + " has collided with " + other.gameObject.tag);
			hs = other.GetComponent<Health> ();

			Destroy (gameObject);
			Damage ();
			counter++;
		}
		//print ("i have collied with object and destroyed myself" + other.tag);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (counter == 1) 
		{
			counter = 0;
		}
	}

	void Damage()
	{
		hs.CalculateDamage (attackDamage);
	}
}
