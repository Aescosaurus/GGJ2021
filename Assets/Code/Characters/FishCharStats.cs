using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCharStats
	:
	CharStats
{
	protected override Dictionary<string,Rate> GeneratePrefInfo()
	{
		var stats = base.GeneratePrefInfo();

		stats["sword"] = Rate.Sometimes;
		stats["staff"] = Rate.Sometimes;
		stats["wand"] = Rate.Sometimes;
		stats["water_magic"] = Rate.Sometimes;
		stats["water_drink"] = Rate.Sometimes;
		stats["tail"] = Rate.Sometimes;
		stats["hat"] = Rate.Sometimes;
		stats["is_red"] = Rate.Sometimes;
		stats["is_green"] = Rate.Sometimes;
		stats["is_blue"] = Rate.Sometimes;

		return( stats );
	}
}
