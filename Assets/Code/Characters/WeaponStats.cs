using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats
	:
	MonoBehaviour
{
	public enum Rate
	{
		Never,
		Rarely,
		Sometimes,
		Always
	}

	private void Start()
	{
		particlePrefabs.Add( Resources.Load<GameObject>( "Prefabs/Particles/FireParticles" ) );
		particlePrefabs.Add( Resources.Load<GameObject>( "Prefabs/Particles/IceParticles" ) );
		particlePrefabs.Add( Resources.Load<GameObject>( "Prefabs/Particles/WaterParticles" ) );

		weaponModels.Add( Resources.Load<GameObject>( "Prefabs/Models/Weapons/Sword" ) );
		weaponModels.Add( Resources.Load<GameObject>( "Prefabs/Models/Weapons/Axe" ) );
		weaponModels.Add( Resources.Load<GameObject>( "Prefabs/Models/Weapons/Bow" ) );
		weaponModels.Add( Resources.Load<GameObject>( "Prefabs/Models/Weapons/Staff" ) );
		weaponModels.Add( Resources.Load<GameObject>( "Prefabs/Models/Weapons/Wand" ) );

		UpdatePrefs();
	}

	// generates a unique random character
	void UpdatePrefs()
	{
		int wepChoice = Random.Range( 0,5 );
		SetWepType( wepChoice );

		var model = Instantiate( weaponModels[wepChoice],transform );
		model.layer = LayerMask.NameToLayer( "Weapon" );

		var randMagic = ChooseRandom( 0.3f,0.3f,0.3f );
		if( randMagic >= 0 ) SetMagic( randMagic );
		prefs.Add( "fire_magic",randMagic == 0 );
		prefs.Add( "ice_magic",randMagic == 1 );
		prefs.Add( "water_magic",randMagic == 2 );
	}

	int ChooseRandom( float a,float b,float c )
	{
		float chance = Random.Range( 0.0f,1.0f );
		// if( chance < a && a >= b && a >= c ) return( 0 );
		// else if( chance < b && b >= a && b >= c ) return( 1 );
		// else if( chance < c && c >= a && c >= b ) return( 2 );
		if( a == b && b == c ) return ( Random.Range( 0,3 ) );
		else if( a == b ) return ( Random.Range( 0,2 ) );
		else if( b == c ) return ( Random.Range( 1,3 ) );

		if( chance < a ) return ( 0 );
		else if( chance < b ) return ( 1 );
		else if( chance < c ) return ( 2 );

		return ( -1 );
	}

	void SetMagic( int type )
	{
		// foreach( var part in skinParts )
		// {
		// 	part.GetComponentInChildren<MeshRenderer>().material = colorMats[color];
		// }
		Instantiate( particlePrefabs[type],transform );

		if( type == 0 ) magicType = "Fire";
		else if( type == 1 ) magicType = "Ice";
		else magicType = "Water";
	}

	void SetWepType( int type )
	{
		prefs.Add( "sword",type == 0 );
		prefs.Add( "axe",type == 1 );
		prefs.Add( "bow",type == 2 );
		prefs.Add( "staff",type == 3 );
		prefs.Add( "wand",type == 4 );

		if( type == 0 ) wepType = "Sword";
		else if( type == 1 ) wepType = "Axe";
		else if( type == 2 ) wepType = "Bow";
		else if( type == 3 ) wepType = "Staff";
		else if( type == 4 ) wepType = "Wand";
	}

	public string GetTitle()
	{
		return( "todo cool wep name " + wepType );
	}

	public string GetDesc()
	{
		return( "Type: " + wepType + "\nMagic: " + magicType );
	}

	// public void GenerateOrder

	// public abstract string GetRace();

	Dictionary<string,bool> prefs = new Dictionary<string,bool>();

	List<GameObject> particlePrefabs = new List<GameObject>();
	List<GameObject> weaponModels = new List<GameObject>();

	string wepType;
	string magicType;
}
