using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodBowl : InteractiveObject {


	public Text bowlText;
	public TextMeshPro bowlWorldText;
	public int maxFood = 5;
	public int currentFood = 3;

	public GameObject foodMesh;

	// Use this for initialization
	void Start () {
		UpdateBowlUI();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateBowlUI() {
		//bowlText.text = currentFood + "/" + maxFood;
		bowlWorldText.text = currentFood + "/" + maxFood;

		//this it to update the 3d visual of the bowl
		foodMesh.SetActive(currentFood == maxFood);

	}

	void FillBowl() {
		currentFood = maxFood;
		UpdateBowlUI();
	}

	public void FillBowlButton() {
		FillBowl();
	}

	public void ReduceFood(int amount) {
		currentFood--;
		UpdateBowlUI();
	}


	public override void Interact() {
		FillBowl();
	}
}
