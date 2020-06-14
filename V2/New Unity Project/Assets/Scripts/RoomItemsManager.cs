using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class RoomItemsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//this fuction returns a list of available items currently in the room
	//of a given type

	public List<ItemSpace> FindRoomItemsOfType(string itemType, bool checkIfAvailable = false) {

		//create a new list
		List<ItemSpace> itemList = new List<ItemSpace>();

		ItemSpace[] itemsInRoom = GetComponentsInChildren<ItemSpace>();

		//for each item in the room
		foreach (ItemSpace someItemSpace in itemsInRoom) {

			//check if it's the type
			if (itemType.ToLower()==someItemSpace.currentItem.itemType.ToString().ToLower()) {

				//if so, add it to the list

				//if we're also doing an availability check, add an extra check
				if (checkIfAvailable) {
					if (!someItemSpace.isInUse) { itemList.Add(someItemSpace); }
				}
				else { itemList.Add(someItemSpace); }

			}
		}

		if (itemList.Count==0) { return null; }
		else { return itemList; }

	}


}
