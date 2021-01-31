using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner
	:
	MonoBehaviour
{
	void Start()
	{
		wepPrefab = Resources.Load<GameObject>( "Prefabs/Weapon" );

		var spotHolder = transform.Find( "WepSpots" );
		for( int i = 0; i < spotHolder.childCount; ++i )
		{
			wepSpots.Add( spotHolder.GetChild( i ) );
			weps.Add( null );
		}

		RefillWeps();
	}

	public void RefillWeps()
	{
		// foreach( var spot in wepSpots )
		for( int i = 0; i < wepSpots.Count; ++i )
		{
			// if( spot.childCount < 1 )
			if( weps[i] == null )
			{
				CreateWeapon( wepSpots[i],i );
			}
		}
	}

	void CreateWeapon( Transform spot,int i )
	{
		var wep = Instantiate( wepPrefab );
		wep.transform.position = spot.position;

		weps[i] = wep;
	}

	List<Transform> wepSpots = new List<Transform>();
	GameObject wepPrefab;
	List<GameObject> weps = new List<GameObject>();
}
