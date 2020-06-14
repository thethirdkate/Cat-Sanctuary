using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCreator : MonoBehaviour {

	public GameObject catUIPrefab; 
	public GameObject catInstancePrefab;
	public Cat[] catScriptables;

	public Transform catUIHolder;
	public Transform catSpriteHolder;
	public GameObject catSpritePrefab;

	public GameObject catModelPrefab;

	public int catCost = 150;

	private int catCount = 0; //we'll just use this to calculate the position of the cat UI

	public int startingCats = 3;

	bool freeCats = true;

	// Use this for initialization
	void Start () {
		for(int i=0; i<startingCats; i++) {
			CreateCat();
		}
		freeCats = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void CreateCat() {

		//ULTIMATELY THE PARTS HERE ABOUT GENERATING UI AND
		//PHYSICAL CAT REPRESENTATIONS IN THE SCENE
		//WOULD BE SEPARATED AND CALLED DEPENDING ON
		//THE VIEW WE NEED TO GENERATE

		//temporary design:
		//getting a new cat costs money

		if (GetComponent<GameManager>().CanAfford(catCost) | freeCats) {

			if (!freeCats) { GetComponent<GameManager>().SpendMoney(catCost); }

			//create the empty which represents all the cat data 
			GameObject newCatInstance = Instantiate(catInstancePrefab, transform);

			//create the UI prefab
			GameObject newCatUIInstance = Instantiate(catUIPrefab, catUIHolder);
			//old 2d stuff
			GameObject newCatSprite = Instantiate(catModelPrefab, catSpriteHolder);
			//newCatSprite.transform.position = catSpriteHolder.position;
			//GameObject newCatSprite = Instantiate(catSpritePrefab, catSpriteHolder);


			//link them all up
			newCatInstance.GetComponent<CatInstance>().catUIInstance = newCatUIInstance.GetComponent<CatUIManager>();
			newCatUIInstance.GetComponent<CatUIManager>().catInstance = newCatInstance.GetComponent<CatInstance>();
			newCatSprite.GetComponent<CatInteractions>().catInstance = newCatInstance.GetComponent<CatInstance>();

			newCatInstance.GetComponent<CatInstance>().catSprite = newCatSprite.GetComponent<CatSpriteController>();

			//now create the cat from the scriptable and attach it to that
			newCatInstance.GetComponent<CatInstance>().catScriptable = catScriptables[catCount];

			catCount++;

			//put it in position
			newCatUIInstance.transform.position += Vector3.right * 350f * catCount;

			//starting things here rather than in Start() functions so we can control the order they happen
			//now start the cat
			newCatInstance.GetComponent<CatInstance>().StartCat();

			GetComponent<HUDManager>().UpdateHUD();

		}

	}

}
