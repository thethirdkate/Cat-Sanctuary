using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	//TIME MANAGEMENT VARIABLES
	public string[] timeChunks;
	public int currentTimeChunk = 0;
	public Text finalHappinessText;
	float lastCall = 0f;

	public float minutesPerChunk = 0.5f;
	public GameObject endDayUI;
	public bool gameTimerActive = true;
	float gameCounter=0;

	//PLAYER VARIABLES
	//Might want to split this out if it gets more complex later
	public int playerMoney = 200;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//we can later add some code here to make this pausable
		//eg if the player is looking inside UIs (if we want!)
        gameCounter += Time.deltaTime;

        if (gameCounter>=(minutesPerChunk*60f)) {
        	gameCounter=0;
        	ProgressTime();
        }

        //let's make some assumptions here:
        //to begin with, money will be going up every few seconds
        //later it'll be racking up quickly
        //let's update them every second

        //random balance values:
        //let's assume that every minute you earn 1 coin per 1 happiness of a cat

        lastCall += Time.deltaTime;

        if (lastCall>=1f) {
        	if (GetComponent<CatManager>().GetHappiness() > 0) {
	        	playerMoney += Mathf.RoundToInt(GetComponent<CatManager>().GetHappiness() / 60f);
				GetComponent<HUDManager>().UpdateHUD();
	        }

        	lastCall=0;

        } 


	}


	public void ProgressTime() {

		currentTimeChunk++;

		//update the game time
		if (currentTimeChunk>=timeChunks.Length) {
			FinishDay();
			//it's the end of the day!
		}
		else {

			GetComponent<HUDManager>().UpdateHUD();

			//get all the cats to decide what to do next
			//note we probably don't actually want all the cats to
			//do this at the same time!
			foreach (CatInstance someCat in GetComponentsInChildren<CatInstance>()) {
				someCat.UpdateCat();
			}
		}

	}

	public void FinishDay() {
		gameTimerActive = false;
		currentTimeChunk=0;

		//get the end of day happiness for each cat
		int finalHappiness = GetComponent<CatManager>().GetHappiness();

		finalHappinessText.text = finalHappiness.ToString();

		playerMoney += finalHappiness;

		GetComponent<HUDManager>().UpdateHUD();

		endDayUI.SetActive(true);
	}

	public void StartNewDay() {
		gameTimerActive = true;
		GetComponent<CatManager>().WakeUpCats();
		endDayUI.SetActive(false);
		ProgressTime();
	}

	public string CurrentTime() {
		return timeChunks[currentTimeChunk];
	}

	public bool CanAfford(int cost) {
		if (playerMoney>=cost) { return true; }
		else { return false; }
	}

	public bool SpendMoney(int cost) {
		if (CanAfford(cost)) { playerMoney-=cost; return true; }
		else { return false; }
	}

}
