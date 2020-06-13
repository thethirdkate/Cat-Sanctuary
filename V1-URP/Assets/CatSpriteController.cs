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
	        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, step);


			//TO DO: add animation control

			//TO DO: add so the cat finishes in right position on object - even jumps onto an object?

			if (Vector2.Distance(transform.position, currentTarget.position) < 0.001f)
	        {
	            isMoving = false;
	        }
		}
	}

	public void GoToObject(Transform targetObject) {
		//eventually we should add some basic pathfinding in here
		isMoving = true;
		currentTarget = targetObject;


	}
}
