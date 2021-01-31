using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keg
	:
	MonoBehaviour
{
	public GameObject drinkFilled;

	void Start()
	{
		mugPrefab = drinkFilled;
	}

	public GameObject SpawnMug( Transform spawnSpot )
	{
		var curMug = Instantiate( mugPrefab,spawnSpot );
		curMug.GetComponent<MugData>().DrinkType = drinkType;
		return( curMug );
	}

	[SerializeField] public string drinkType = "water";

	GameObject mugPrefab;
}
