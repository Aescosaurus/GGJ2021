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
		return( mugPrefab );
	}

	[SerializeField] string drinkType = "water";

	GameObject mugPrefab;
}
