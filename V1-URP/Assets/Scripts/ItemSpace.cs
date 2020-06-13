using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpace : MonoBehaviour {

	public Item currentItem; 

	//right now, we're referencing the scriptable directly
	//but later this would probably need to point to an
	//item in the inventory, which would then reference the scriptable

	// Use this for initialization
	void Start () {

		if (currentItem != null) {
			//load the sprite 
			this.GetComponent<SpriteRenderer>().sprite = currentItem.itemSprite;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
