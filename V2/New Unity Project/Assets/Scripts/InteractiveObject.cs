using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractiveObject : MonoBehaviour { 

	public bool showHint = true;
	public bool currentlyEnabled = true;


	public GameObject hintText;

	public void Awake() {
		hintText.SetActive(false);
	}

	public virtual void Interact(){}

}