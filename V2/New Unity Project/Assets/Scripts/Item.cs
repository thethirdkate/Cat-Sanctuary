using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(menuName = "Items/Item", fileName = "Item.asset")]

public class Item : ScriptableObject {

	//this is info about the items that can be in the game

	public string displayName;
	public bool canBePlaced;
	public ItemType itemType;

	public Sprite itemSprite;

	public GameObject itemPrefab;

	
	public enum ItemType {
		Toy, Furniture, Decor
	}


	//other info that can go here later:
	//cost
	//positioning info
	//stacking info
	//etc

}