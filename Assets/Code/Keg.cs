using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keg
	:
	MonoBehaviour
{
	void Start()
	{
		mugPrefab = Resources.Load<GameObject>( "Prefabs/Mug" );
	}

	// !! no use this bad !!
	private GameObject GetMugPrefab()
	{
		//Based on what keg it is, have the mug be that type of drink
		mugPrefab.GetComponent<MugData>().DrinkType = drinkType;
		return( mugPrefab );
	}

	public GameObject SpawnMug( Transform spawnSpot )
	{
		var curMug = Instantiate( mugPrefab,spawnSpot );
		curMug.GetComponent<MugData>().DrinkType = drinkType;
		return( curMug );
	}

	[SerializeField] string drinkType = "water";

	GameObject mugPrefab;
}
