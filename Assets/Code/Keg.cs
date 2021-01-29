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

	public GameObject GetMugPrefab()
	{
		//Based on what keg it is, have the mug be that type of drink
		mugPrefab.GetComponent<MugData>().DrinkType = drinkType;
		return( mugPrefab );
	}

	[SerializeField] string drinkType = "water";

	GameObject mugPrefab;
}
