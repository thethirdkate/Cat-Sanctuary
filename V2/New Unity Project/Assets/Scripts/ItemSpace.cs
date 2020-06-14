using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpace : MonoBehaviour {

	public Item currentItem; 

	public bool isInUse = false; //we might want to later track which cat is using it, but for now keep it simple

	//right now, we're referencing the scriptable directly
	//but later this would probably need to point to an
	//item in the inventory, which would then reference the scriptable

	// Use this for initialization
	void Start () {

		if (currentItem != null) {
			//load the sprite 
		//OLD 2D VERSION
		//	this.GetComponent<SpriteRenderer>().sprite = currentItem.itemSprite;
			GameObject newItem = Instantiate(currentItem.itemPrefab, transform);
			newItem.transform.position = transform.position;

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
