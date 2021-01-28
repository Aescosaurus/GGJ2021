using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfCharStats
	:
	CharStats
{
	protected override Dictionary<string,Rate> GeneratePrefInfo()
	{
		var stats = base.GeneratePrefInfo();

		stats["bow"] = Rate.Sometimes;
		stats["staff"] = Rate.Sometimes;
		stats["wand"] = Rate.Sometimes;
		stats["ice_magic"] = Rate.Sometimes;
		stats["water_magic"] = Rate.Sometimes;
		stats["wine_drink"] = Rate.Sometimes;
		stats["hat"] = Rate.Sometimes;
		stats["blue"] = Rate.Sometimes;

		return( stats );
	}
}
