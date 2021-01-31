using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkLocation : MonoBehaviour
{
    //based on serialized field of my choice, when clicked, player recieves a specific cup type.
    public GameObject CupType;

	public GameObject SpawnCup(Transform spawnSpot)
	{
		var curCup = Instantiate(CupType, spawnSpot);
		return curCup;
	}
}
