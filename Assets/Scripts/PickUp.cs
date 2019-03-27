using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
	private Inventory inventory;
	public GameObject itemButton;
	BoxCollider2D box;
    private void Start()
    {
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory> ();
		box = GetComponent<BoxCollider2D> ();
    }
	void Update()
	{
		ItemEvent ();
	}

	private void ItemEvent()
	{
		if(box.IsTouchingLayers(LayerMask.GetMask("Player")))
		{
			for (int i = 0; i < inventory.slots.Length; i++) 
			{
				if (inventory.isFull [i] == false) 
				{
					inventory.isFull [i] = true;
					Instantiate (itemButton, inventory.slots[i].transform, false);
					Destroy (gameObject);
					break;
				}
			}
		}
	}
}
