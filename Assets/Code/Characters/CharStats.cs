using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharStats
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
		skinParts.Add( transform.Find( "Head" ).gameObject );
		skinParts.Add( transform.Find( "Arm1" ).gameObject );
		skinParts.Add( transform.Find( "Arm2" ).gameObject );

		prefInfo = GeneratePrefInfo();

		colorMats.Add( Resources.Load<Material>( "Materials/Red" ) );
		colorMats.Add( Resources.Load<Material>( "Materials/Green" ) );
		colorMats.Add( Resources.Load<Material>( "Materials/Blue" ) );

		accessories.Add( Resources.Load<GameObject>( "Prefabs/Models/Accessories/Tail" ) );
		accessories.Add( Resources.Load<GameObject>( "Prefabs/Models/Accessories/Horns" ) );
		accessories.Add( Resources.Load<GameObject>( "Prefabs/Models/Accessories/Hat" ) );

		UpdatePrefs();
	}

	protected virtual Dictionary<string,Rate> GeneratePrefInfo()
	{
		Dictionary<string,Rate> stats = new Dictionary<string,Rate>();

		stats.Add( "sword",Rate.Never );
		stats.Add( "axe",Rate.Never );
		stats.Add( "bow",Rate.Never );
		stats.Add( "staff",Rate.Never );
		stats.Add( "wand",Rate.Never );

		stats.Add( "fire_magic",Rate.Never );
		stats.Add( "ice_magic",Rate.Never );
		stats.Add( "water_magic",Rate.Never );

		stats.Add( "ale_drink",Rate.Never );
		stats.Add( "wine_drink",Rate.Never );
		stats.Add( "water_drink",Rate.Never );

		stats.Add( "tail",Rate.Never );
		stats.Add( "horns",Rate.Never );
		stats.Add( "hat",Rate.Never );

		stats.Add( "is_red",Rate.Never );
		stats.Add( "is_green",Rate.Never );
		stats.Add( "is_blue",Rate.Never );

		return( stats );
	}

	// generates a unique random character
	void UpdatePrefs()
	{
		prefs.Add( "sword",prefInfo["sword"] > Rate.Never );
		prefs.Add( "axe",prefInfo["axe"] > Rate.Never );
		prefs.Add( "bow",prefInfo["bow"] > Rate.Never );
		prefs.Add( "staff",prefInfo["staff"] > Rate.Never );
		prefs.Add( "wand",prefInfo["wand"] > Rate.Never );

		prefs.Add( "fire_magic",prefInfo["fire_magic"] > Rate.Never );
		prefs.Add( "ice_magic",prefInfo["ice_magic"] > Rate.Never );
		prefs.Add( "water_magic",prefInfo["water_magic"] > Rate.Never );

		prefs.Add( "ale_drink",prefInfo["ale_drink"] > Rate.Never );
		prefs.Add( "wine_drink",prefInfo["wine_drink"] > Rate.Never );
		prefs.Add( "water_drink",prefInfo["water_drink"] > Rate.Never );

		prefs.Add( "tail",Random.Range( 0.0f,1.0f ) < CalcChance( prefInfo["tail"] ) );
		prefs.Add( "horns",Random.Range( 0.0f,1.0f ) < CalcChance( prefInfo["horns"] ) );
		prefs.Add( "hat",Random.Range( 0.0f,1.0f ) < CalcChance( prefInfo["hat"] ) );

		if( prefs["tail"] ) ApplyAccessory( 0 );
		if( prefs["horns"] ) ApplyAccessory( 1 );
		if( prefs["hat"] ) ApplyAccessory( 2 );

		var red = CalcChance( prefInfo["is_red"] );
		var green = CalcChance( prefInfo["is_green"] );
		var blue = CalcChance( prefInfo["is_blue"] );
		var randColor = ChooseRandom( red,green,blue );
		if( randColor >= 0 ) SetColor( randColor );
		prefs.Add( "is_red",randColor == 0 );
		prefs.Add( "is_green",randColor == 1 );
		prefs.Add( "is_blue",randColor == 2 );
	}

	float CalcChance( Rate chance )
	{
		return( ( ( float )( ( int )chance ) ) / 3.0f );
	}

	int ChooseRandom( float a,float b,float c )
	{
		float chance = Random.Range( 0.0f,1.0f );
		// if( chance < a && a >= b && a >= c ) return( 0 );
		// else if( chance < b && b >= a && b >= c ) return( 1 );
		// else if( chance < c && c >= a && c >= b ) return( 2 );
		if( a == b && b == c ) return( Random.Range( 0,3 ) );
		else if( a == b ) return( Random.Range( 0,2 ) );
		else if( b == c ) return( Random.Range( 1,3 ) );

		if( chance < a ) return( 0 );
		else if( chance < b ) return( 1 );
		else if( chance < c ) return( 2 );

		return( -1 );
	}

	void SetColor( int color )
	{
		foreach( var part in skinParts )
		{
			part.GetComponentInChildren<MeshRenderer>().material = colorMats[color];
		}
	}

	public bool LiarChance()
    {
		int chance = Random.Range(0, 2);
		if (chance == 1)
		{
			liar = true;
		}
		else
		{
			liar = false;
		}
		return true;
	}

	public bool AskAboutLostAndFound()
	{
		int chance = Random.Range(0, 2);
		if (liar == true)
		{
			if (chance == 1)
				return true;
			else
				return false;
		}
		else
			return true;
	}

	void ApplyAccessory( int type )
	{
		Transform parentSpot;
		if( type == 0 ) parentSpot = transform.Find( "TailSpot" );
		else parentSpot = transform.Find( "HatSpot" );

		Instantiate( accessories[type],parentSpot );
	}

	public string GenerateOrder()
	{
		var choice = ChooseRandom( CalcChance( prefInfo["ale_drink"] ),
			CalcChance( prefInfo["wine_drink"] ),
			CalcChance( prefInfo["water_drink"] ) );

		if( choice == 0 ) return( "Ale" );
		else if( choice == 1 ) return( "Wine" );
		else return( "Water" );
	}

	public bool CheckPref( string pref )
	{
		return( prefs[pref] );
	}

	public bool IsLiar()
	{
		return( liar );
	}

	// public abstract string GetRace();

	// chance
	Dictionary<string,Rate> prefInfo;
	// ability
	Dictionary<string,bool> prefs = new Dictionary<string,bool>();

	List<GameObject> skinParts = new List<GameObject>();

	List<Material> colorMats = new List<Material>();

	List<GameObject> accessories = new List<GameObject>();

	[SerializeField] bool liar = false;
}
