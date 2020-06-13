using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	public Text moneyText;
	public Text timeText;
	public GameObject catButton;

	GameManager gameManager;
	CatCreator catCreator;


	// Use this for initialization
	void Start () {

		gameManager = GetComponent<GameManager>();
		catCreator = GetComponent<CatCreator>();

		UpdateHUD();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateHUD() {
		moneyText.text = gameManager.playerMoney.ToString();
		timeText.text = gameManager.CurrentTime();

		if (gameManager.CanAfford(catCreator.catCost)) {
			catButton.GetComponent<Button>().interactable = true;
		}
		else {
			catButton.GetComponent<Button>().interactable = false;
		}

	}
}
