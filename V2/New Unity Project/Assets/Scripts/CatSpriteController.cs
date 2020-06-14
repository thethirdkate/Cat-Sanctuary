using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpriteController : MonoBehaviour {

	bool isMoving = false;
	public Transform currentTarget;

	// Use this for initialization
	void Start () {
		//isMoving = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (isMoving) { 

			float step =  60 * Time.deltaTime; // calculate distance to move
	        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, step);


			//TO DO: add animation control

			//TO DO: add so the cat finishes in right position on object - even jumps onto an object?

			if (Vector3.Distance(transform.position, currentTarget.position) < 0.001f)
	        {
	            isMoving = false;
	        }
		}
	}

	public void GoToObject(Transform targetObject) {
		//eventually we should add some basic pathfinding in here

		//by default, the cat will just move to the position of the object

		//but for most objects there'll be a specific position next to/on top of it where the cat should go
		//in future we can use this (or an array of positions) to control the sequence of a cat getting
		//to an object - e.g. move to position 1, play jump animation, move to position 2, etc

		Transform targetItem = targetObject.GetChild(0);

		currentTarget = targetItem;

		if (targetItem.gameObject.GetComponent<ItemCatPositioner>() == null) {
			Debug.Log("ERROR: no cat positioner script on " + targetItem);
		}

		if (targetItem.gameObject.GetComponent<ItemCatPositioner>().catPosition != null) {
			currentTarget = targetItem.gameObject.GetComponent<ItemCatPositioner>().catPosition;
		}

		isMoving = true;

	}
}
