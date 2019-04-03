using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float lifeTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
		Destroy (gameObject, lifeTime);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		Destroy (gameObject);
		//print ("i have collied with object and destroyed myself" + other.tag);
	}
}
