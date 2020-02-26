using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBowl : MonoBehaviour {

	public Text bowlText;
	public int maxFood = 5;
	public int currentFood = 3;

	// Use this for initialization
	void Start () {
		UpdateBowlUI();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateBowlUI() {
		bowlText.text = currentFood + "/" + maxFood;
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
}
