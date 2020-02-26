using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(menuName = "Items/Cat", fileName = "Cat.asset")]

public class Cat : ScriptableObject {

	//THIS IS JUST A CAT DEFINITION FILE for the scriptable objects

	//these are game constants for all cats
	//they probably shouldn't live here - they don't need to be adjusted per cat type

	//note - hunger actually refers to fedness, I just can't be bothered to use a better word for it (10 hunger = full)
	public float hungerRate = 0.3f; //how many hunger points are removed every time unit
	public float maxHunger = 10f; //max hungry a cat can be
	public float maxTreats = 3f; //how many treats a cat can be fed at once
	public float treatRate = 0.2f; //how quickly the cat gets hungry for treats again
	public float maxPets = 3f; //how many times you can pet a cat
	public float petRate = 0.2f; //how quickly the cat gets hungry for pets again
	public float maxTrust = 10f;
	public float tiredRate = 0.3f;
	public float maxTired = 10f;


	//how will energy work? how does cat sleeping work? tbd
	//	int energyRate = 1;
	//	int maxEnergy = 10;

	//stats for an individual cat
	public string displayName = "";
	public float startingTrust = 5f; //how much the cat trusts the owner when it's first caught
	public float startingHappiness = 5f; //how happy the cat is when you first pick it up
	public float startingHunger = 6f;

	//personality traits
	//these are generic traits - might later be used to determine a cat's AI
	public List<CatTrait> catTraits;

	//other stats for later:
		//favourite stuff
		//place it was found



}
