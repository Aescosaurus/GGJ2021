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
		prefInfo = GeneratePrefInfo();
	}

	protected virtual Dictionary<string,Rate> GeneratePrefInfo()
	{
		Dictionary<string,Rate> prefs = new Dictionary<string,Rate>();

		prefs.Add( "sword",Rate.Never );
		prefs.Add( "axe",Rate.Never );
		prefs.Add( "bow",Rate.Never );
		prefs.Add( "staff",Rate.Never );
		prefs.Add( "wand",Rate.Never );
		prefs.Add( "fire_magic",Rate.Never );
		prefs.Add( "ice_magic",Rate.Never );
		prefs.Add( "water_magic",Rate.Never );
		prefs.Add( "ale_drink",Rate.Never );
		prefs.Add( "wine_drink",Rate.Never );
		prefs.Add( "water_drink",Rate.Never );
		prefs.Add( "tail",Rate.Never );
		prefs.Add( "horns",Rate.Never );
		prefs.Add( "hat",Rate.Never );
		prefs.Add( "is_red",Rate.Never );
		prefs.Add( "is_green",Rate.Never );
		prefs.Add( "is_blue",Rate.Never );

		return( prefs );
	}

	Dictionary<string,Rate> prefInfo;
}
