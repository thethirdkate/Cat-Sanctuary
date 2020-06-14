using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInstance : MonoBehaviour {

	//REFERENCES TO OTHER SCRIPTS AND OBJECTS
	public Cat catScriptable; //refers to the scriptable with character info about the cat
	public ItemInventory itemInventory;
	public RoomItemsManager roomItemsManager;

	//GENERAL CAT AI DATA
	public float hungerRate = 0.3f; //how many hunger points are removed every time unit
	public float maxHunger = 10f; //max hungry a cat can be
	public float maxTreats = 3f; //how many treats a cat can be fed at once
	public float treatRate = 0.2f; //how quickly the cat gets hungry for treats again
	public float currentTreats = 0;
	public float maxPets = 3f; //how many times you can pet a cat
	public float petRate = 0.2f; //how quickly the cat gets hungry for pets again
	public float maxTrust = 10f;
	public float tiredRate = 0.3f;
	public float maxTired = 10f;

	public int currentHappiness = 50; //how happy the cat currently is - starts at 50% each day

	//CAT TRAITS
	public List<CatTrait> catTraits;

	//STATUS INFO ABOUT THIS CAT
	public float currentTrust;
	public float currentHunger;
	public float currentTiredness = 5;

	//OTHER OBJECT RELATIONS
	//these are created alongside the cat when it is created
	public CatUIManager catUIInstance;
	public CatSpriteController catSprite; 


	//SPRITES FOR ACTIONS
	//in future these would be generated depending on the cat + action
	//but that's not for now!
	public Sprite sittingSprite;
	public Sprite playingSprite;
	public Sprite eatingSprite;
	public Sprite sleepingSprite;

	//ACTIONS A CAT MAY DECIDE TO DO
	public class Action
   {
       public string actionName;
       public float weight;
       public Sprite actionSprite;
       //stuff can be added here like references to possible/required/current item or item type

       public bool itemRequired;
       public string itemType;
   }

	//these are used for weighting the decision possibilities in the cat AI
	public List<Action> actionList;
	float _totalActionWeight;

	public Action sittingAction;
	public Action sleepingAction;
	public Action eatingAction;
	public Action playingAction;

	public Action currentAction;

	public ItemSpace currentSpace;


	// Use this for initialization
	void Start () {
	}

	public void StartCat() {

		actionList = new List<Action>();
		catTraits = new List<CatTrait>();

		itemInventory = GameObject.FindObjectOfType<ItemInventory>();
		roomItemsManager = GameObject.FindObjectOfType<RoomItemsManager>();


		//I'm sure there's a better way of doing this, but for now
		//we are cloning the info from the cat scriptable into the cat instance
		//so that the data on whether or not a trait has been discovered
		//is stored on the instance and doesn't permanently change
		//the data on the scriptable

		for (int i = 0; i < catScriptable.catTraits.Count; i++)
		 {
		 	CatTrait newTrait = new CatTrait();
		 	newTrait.traitName = catScriptable.catTraits[i].traitName;
		 	newTrait.discovered = catScriptable.catTraits[i].discovered;
		     catTraits.Add(newTrait);
		 }

		currentTrust = catScriptable.startingTrust;
		currentHunger = catScriptable.startingHunger;

		//create the list of actions
		//should probably just be in the scriptable?! not sure
		sittingAction = new Action();
		sittingAction.actionName = "sitting";
		sittingAction.weight = 4;
		sittingAction.actionSprite = sittingSprite;
		sittingAction.itemRequired = true;
		sittingAction.itemType = "furniture";

		sleepingAction = new Action();
		sleepingAction.actionName = "sleeping";
		sleepingAction.weight = 4;
		sleepingAction.actionSprite = sleepingSprite;
		sleepingAction.itemRequired = true;
		sleepingAction.itemType = "furniture";

		eatingAction = new Action();
		eatingAction.actionName = "eating";
		eatingAction.weight = 4;
		eatingAction.actionSprite = eatingSprite;
		eatingAction.itemRequired = true;
		eatingAction.itemType = "foodBowl";

		playingAction = new Action();
		playingAction.actionName = "playing";
		playingAction.weight = 4;
		playingAction.actionSprite = playingSprite;
		playingAction.itemRequired = true;
		playingAction.itemType = "toy";

		actionList.Add(sittingAction);
		actionList.Add(sleepingAction);
		actionList.Add(eatingAction);
		actionList.Add(playingAction);

		currentAction = sittingAction;

	//	GetComponent<CatUIManager>().UpdateCatUI(); 
		UpdateCat();
		
	}
	 
	// Update is called once per frame
	void Update () {
		
	}

	public void WakeUp() {
		//stuff that happens to the cat first thing in the morning
		currentHappiness=50;
		currentTiredness=10;
	}

	//START CAT AI

	public void UpdateCat() { 
	//we're keeping this out of the Update() function for now
	//because we don't know exactly how we want time to work - will it progress in realtime?
	//or will it only happen after a player does an action, more like an energy system? etc

		Debug.Log("Updating " + catScriptable.displayName);

		//decide what to do next - note, this probably shouldn't happen every time!
		DecideNextAction();

		//GENERAL CAT UPDATES
		currentTreats -= treatRate;
		if (currentTreats<0) { currentTreats = 0; }


		//we're going to need a new bunch of logic to handle
		//the cat going to the food bowl and then eating
/*
		//Any happiness boosts/reductions
		if (currentHunger<=3 && currentHappiness>30) {
			currentHappiness -= 10;
		}

		//UPDATE STATS OF CAT DEPENDING ON WHAT IT'S UP TO

		if (currentHunger>0 && currentAction != eatingAction) {
			currentHunger-=hungerRate; //in future this will probably be more of a drop-off curve
		}

		if (currentAction == eatingAction) { //if the cat is eating, it's getting less hungry
			currentHunger=catScriptable.maxHunger; //let's assume eating is instant
			//we're also going to therefore take osome food out of the food bowl
			//if we later change eating to not be instant, we might want to call this
			//at the decision moment rather than in the update
			currentHappiness+=5;
			itemInventory.foodBowl.ReduceFood(1);
		}
*/

		if (currentTiredness>0 && currentAction != sleepingAction) {
			currentTiredness-=tiredRate; //in future this will probably be more of a drop-off curve
		}

		if (currentAction == sleepingAction) { //if the cat is eating, it's getting less hungry
			currentTiredness++;
			if (currentTiredness > catScriptable.maxTired) { currentTiredness = catScriptable.maxTired; }
		}

		if (currentAction == playingAction && currentTiredness>1) { //cat gets more tired if it's playing
			currentTiredness--; //the rate of tiredness could depend on the item too, later
		}


		//do any updates according to whatever the cat is doing now
		//Debug.Log(this.GetComponent<CatUIManager>());
		catUIInstance.UpdateCatUI(); //I don't know how we'll handle it later when this might be a different script depending on the scene?


	}

	void DecideNextAction() {

		//SET THE WEIGHTS ACCORDING TO THE CAT'S CURRENT STATS
		//later: could also be influenced by personality, e.g. how lazy cat is
		//as well as maybe availability of favourite toy/food/etc ?

		//sitting is the default activity so we'll leave that as it is
		sittingAction.weight = 4;

		//sleeping has a weight according to how tired the cat is
		sleepingAction.weight = 10-currentTiredness;

		//but also cats like to sleep a lot
		sleepingAction.weight += 2;

		//eating has a weight depending on how hungry the cat is
		eatingAction.weight = 10-currentHunger;

		//cat is more likely to play if it's not tired
		playingAction.weight = currentTiredness;

		//whatever the cat is currently doing, we'll add some extra weight to that
		currentAction.weight = currentAction.weight*3;

		//OVERRIDES

		/*
		//if there is no food, the weight is 0
		//UPDATE: changed this - a hungry cat will still go to the bowl, just not eat
		if (itemInventory.foodBowl.currentFood==0) {
			eatingAction.weight = 0;
			//TO DO: 
			//reduce happiness ?
		}
		*/

		//end of overrides


		//get the total weightings

		_totalActionWeight = 0f;
        foreach(Action action in actionList) {
        //	Debug.Log(action.actionName + " : " + action.weight);
	          _totalActionWeight += action.weight;
        }

        //TO DO: 
        //figure out here: if some actions _require_ an object to be available
        //and there isn't one available, then this needs to be checked
     	//before the decision is finalised


	      // Generate a random position in the list.
	      float pick = Random.value * _totalActionWeight;
	     // int chosenIndex = 0;
	      float cumulativeWeight = 0;
	      Action chosenAction = actionList[0];

	    //  Debug.Log(pick);

	      //for each item in the list
	        foreach(Action action in actionList) {
		        cumulativeWeight += action.weight; 
		        chosenAction = action;
	        	if (pick <= cumulativeWeight) { break; }
	        }

	    // Debug.Log(chosenAction.actionName);
	     currentAction = chosenAction;

	     Debug.Log(chosenAction.itemRequired);

	     //do we need an item for this action?
	     if (chosenAction.itemRequired) {
	     	//Debug.Log("need an item");

		     //now choose an item to go and do it with
		     ItemSpace newSpace = ChooseObject(chosenAction.itemType);

		     if (newSpace==null) { Debug.Log("no item found of type " + chosenAction.itemType); }
		     else {

		     	//set the cat to go to the new item
		     	//Debug.Log("going to " + newSpace.currentItem.displayName);
		     	catSprite.GoToObject(newSpace.transform);
		     	newSpace.isInUse = true;
		     	if (currentSpace != null) { currentSpace.isInUse = false; }
		     	currentSpace = newSpace;
		     } 
		 }

	}

	ItemSpace ChooseObject(string itemType) {
		//this function will choose an object in the scene for the cat to use
		//it will choose an object of a certain type

		//first of all, let's call a function in the room items manager to get a list of relevant items
		List<ItemSpace> itemList = new List<ItemSpace>();
		itemList = roomItemsManager.FindRoomItemsOfType(itemType, true);

		//if none are avialable, return null
		if (itemList == null) { return null; }


		//we'll put in some extra checks for special cases - e.g. food bowls, try to pick one that isn't empty
		if (itemType=="foodBowl") {
			List<ItemSpace> fullBowlList = new List<ItemSpace>();
			foreach (ItemSpace someItemSpace in itemList) {
				if (someItemSpace.transform.GetChild(0).GetComponent<FoodBowl>().currentFood>0) {
					Debug.Log("this food bowl has food");
					fullBowlList.Add(someItemSpace); 
				}
			}
			//basically, if there are non-empty bowls, we'll return a list of those
			//if they are all empty, the cat will still go to one of the empty bowls
			//and just look sad about it
			if (fullBowlList.Count==0) { 
				return itemList[Random.Range(0,itemList.Count)];
				Debug.Log("there were no food in bowls");
			}		
			else {
				//otherwise, pick one at random
				return fullBowlList[Random.Range(0,fullBowlList.Count)]; 
			}
		}

		else {
			//otherwise, pick one at random
			return itemList[Random.Range(0,itemList.Count)];
		}


	}

	//END CAT AI

	//START PLAYER INTERACTIONS
	public bool FeedCatTreat() { //can later add more info about types of treat etc
		//we're assuming that the check of whether the player has a treat has been done elsewhere
		if (Mathf.Ceil(currentTreats)<maxTreats) {
			currentTreats++;
			IncreaseTrust();
			currentHappiness+=10;
			return true;
		}
		else { return false; }
	}

	//END PLAYER INTERACTIONS

	public void IncreaseTrust() {

		if ((currentTrust+1)<catScriptable.maxTrust) {
			currentTrust++;
			DiscoverTrait();
		}

		if (currentTrust>catScriptable.maxTrust) { currentTrust = catScriptable.maxTrust; }

	}

	void DiscoverTrait() {

		List<CatTrait> unknownTraits = new List<CatTrait>();

		//loop through the list of traits and create an array of unknown traits
		foreach (CatTrait trait in catTraits) {
			if (!trait.discovered) { unknownTraits.Add(trait); }
		}

		//if there's something in the array, let's pick one at random
		if (unknownTraits.Count>0) {
			var index = Random.Range(0,unknownTraits.Count-1);

			//switch it to discovered
			unknownTraits[index].discovered = true;

		}
	}

/*
	public void IncreaseTrust() { //this can be called to increase a cat's trust level 
		//increase trust, if it's not at max (should this check be here?)
		if ((currentTrust+1)<catScriptable.maxTrust) {
			currentTrust++;
		}
		if (currentTrust<catScriptable.maxTrust) { currentTrust = catScriptable.maxTrust; }

		//is it time to discover a new trait?
		//LATER: these traits should be somehow distributed so that you
		//only learn a trait every few trust points
		//for now, we'll just check if you already know all the traits
		//if not, we'll learn one
		//this might mean that you never learn all the traits, if a cat
		//has more traits than trust points you can gain

		if (unknownTraits.Length>0) {

			//if so, pick one to discover
			int i = Random.Range(0,unknownTraits.Length-1);

			//and discover it
			Debug.Log("Discovered trait: " + unknownTraits[i]);

			knownTraits.
		}

	}
*/
}

