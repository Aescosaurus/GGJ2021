using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkLocation : MonoBehaviour
{
    //based on serialized field of my choice, when clicked, player recieves a specific cup type.
    public GameObject CupType;
    private void Start()
    {
        // (Instantiate(CupType, new Vector3(0,0,0), Quaternion.identity) as GameObject).transform.parent = this.gameObject.transform;

    }
    public GameObject SpawnCup(Transform spawnSpot)
	{
		var curCup = Instantiate(CupType, spawnSpot);
		return curCup;
	}
}
