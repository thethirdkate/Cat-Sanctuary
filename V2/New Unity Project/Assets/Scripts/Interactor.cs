using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{ 

	private InteractiveObject currentObject;
	private bool objectInRange = false;

	 void OnTriggerEnter(Collider other) 
	 {

	 	Debug.Log("colliding");

	 	if (other.gameObject.GetComponent<InteractiveObject>() != null) {

		 	InteractiveObject interact = other.gameObject.GetComponent<InteractiveObject>();

		 	Debug.Log("let's interact");

	 		//show the hint
	 		if (interact.showHint) {
	 			Debug.Log("show hint");
	 			interact.hintText.SetActive(true);
	 		}
	 		objectInRange = true;

	 		currentObject = interact;
		}

	}

	 void OnTriggerExit(Collider other) 
	 {

	 	Debug.Log("uncolliding");

	 	if (other.gameObject.GetComponent<InteractiveObject>() != null) {

		 	InteractiveObject interact = other.gameObject.GetComponent<InteractiveObject>();

		 	Debug.Log("let's not interact");

	 		//show the hint
	 		if (interact.showHint) {
	 			Debug.Log("show hint");
	 			interact.hintText.SetActive(false);
	 		}
	 		objectInRange = false;
		}

	}

	void Update() {
		if (Input.GetKeyDown("f"))
        {
            print("space key was pressed");
            if (objectInRange) {
            	currentObject.Interact();
            }
        }
	}



}