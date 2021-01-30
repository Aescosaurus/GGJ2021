using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCharStats
	:
	CharStats
{
	protected override Dictionary<string,Rate> GeneratePrefInfo()
	{
		var stats = base.GeneratePrefInfo();

		stats["sword"] = Rate.Rarely;
		stats["staff"] = Rate.Sometimes;
		stats["wand"] = Rate.Sometimes;
		stats["fire_magic"] = Rate.Sometimes;
		stats["ice_magic"] = Rate.Sometimes;
		stats["water_magic"] = Rate.Sometimes;
		stats["wine_drink"] = Rate.Sometimes;
		stats["hat"] = Rate.Always;

		return( stats );
	}
}
