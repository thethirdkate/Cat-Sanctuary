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
	//note: there is currently no concept of items being available/in use

	public List<ItemSpace> FindRoomItemsOfType(string itemType) {

		//create a new list
		List<ItemSpace> itemList = new List<ItemSpace>();

		ItemSpace[] itemsInRoom = GetComponentsInChildren<ItemSpace>();

		//for each item in the room
		foreach (ItemSpace someItemSpace in itemsInRoom) {

			//check if it's the type
			if (itemType.ToLower()==someItemSpace.currentItem.itemType.ToString().ToLower()) {

				//if so, add it to the list
			
				itemList.Add(someItemSpace);

			}
		}

		if (itemList.Count==0) { return null; }
		else { return itemList; }

	}


}
