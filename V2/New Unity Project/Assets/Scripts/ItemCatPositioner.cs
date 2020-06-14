using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCatPositioner : MonoBehaviour
{


	//for now this script just holds data about where the cat should be positioned relevant to the item
	//later, this could also control a sequence of movements or animations related to how the cat interacts
	//with the item

	//in theory this could have gone in the item definition, BUT I'm keeping the definition for the
	//item as an abstract piece of data in the inventory separate to the script that controls
	//the item's physical presence in the scene 

	public Transform catPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
