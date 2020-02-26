using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatUIManager : MonoBehaviour {

	//The reason we are putting this into it's own class
	//is that the type of UI that needs managing will ultimately
	//vary between screens, so later there would be multiple
	//different UI management scripts rather than a single
	//CatUIManager
	//This goes on the individual cat

	// Use this for initialization



	public CatInstance catInstance;

	public Text catNameText;
	public Text hungerText;
	public Text tiredText;
	public Text trustText;
	public Text statusText;
	public Text treatText;
	public Text traitsText;
	public Text happinessText;

	public Image catImage;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateCatUI() { //things that need frequent updating


		catNameText.text = catInstance.catScriptable.displayName;

		tiredText.text = Mathf.Ceil(catInstance.currentTiredness) + "/" + catInstance.maxTired;
		hungerText.text = Mathf.Ceil(catInstance.currentHunger) + "/" + catInstance.maxHunger;
		trustText.text = Mathf.Ceil(catInstance.currentTrust) + "/" + catInstance.maxTrust;
		statusText.text = catInstance.currentAction.actionName;
		treatText.text = Mathf.Ceil(catInstance.currentTreats) + "/" + catInstance.maxTreats;
		happinessText.text = catInstance.currentHappiness + "%";

		//for traits, we'll need to loop through which traits we know about and add them to the string
		var traitsString = "";

		foreach (CatTrait trait in catInstance.catTraits) {
			if (trait.discovered) { traitsString += trait.traitName + ", "; }
		}

		traitsText.text = traitsString;

		//Debug.Log(catInstance.currentAction.ToString());
		catImage.sprite = catInstance.currentAction.actionSprite;

	}

	public void FeedCatTreat() {
		//to do: make a function in the item inventory script and call it here to check if the player
		//even has a treat to feed
		catInstance.FeedCatTreat(); 
		//note the above function will return false if the cat is already full of treats
		UpdateCatUI();
	}


}