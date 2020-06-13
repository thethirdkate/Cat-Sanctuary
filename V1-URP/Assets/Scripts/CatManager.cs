using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void WakeUpCats() { //reset the cats for the next day
		foreach (CatInstance someCat in GetCatList()) {
			someCat.WakeUp();
		}
	}

	public int GetHappiness() {
		int totalHappiness = 0;

		if (CatCount() == 0) { return 0; }

		foreach (CatInstance someCat in GetCatList()) {
			totalHappiness += someCat.currentHappiness;
		}
		return totalHappiness / CatCount();
	}

	public List<CatInstance> GetCatList() {
		List<CatInstance> catList = new List<CatInstance>();
		foreach (CatInstance someCat in GetComponentsInChildren<CatInstance>()) {
			catList.Add(someCat);
		}

		return catList;
	}

	public int CatCount () {
		int catCount=0;
		foreach (CatInstance someCat in GetCatList()) {
			catCount++;
		}
		return catCount;
	}

}
