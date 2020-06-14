using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInteractions : InteractiveObject { 

	public CatInstance catInstance;

	public override void Interact() {
		catInstance.FeedCatTreat();
		Debug.Log("given treat!");
	}

}