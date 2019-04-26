using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public float currentHealth;
	public float maxHealth;


    void Start()
    {
		
		currentHealth = maxHealth;
    }
		
	public void CalculateDamage(float damage)
	{
		//print (damage);

		float taken = damage;
		//print(taken);
		currentHealth -= taken;

		print (gameObject.tag + " has taken " + taken + " damage! It now has " + currentHealth + "HP");

		if (currentHealth <= 0) {
			Death ();
		}
	}

	void Death(){
		print (gameObject.tag + " has died.");
		if (gameObject.tag == "GameManager") 
		{
			FindObjectOfType<GameSession> ().ResetGameSession ();
		}
		Destroy (gameObject);
	}
}
