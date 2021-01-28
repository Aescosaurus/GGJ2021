using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCharStats
	:
	CharStats
{
	protected override Dictionary<string,Rate> GeneratePrefInfo()
	{
		var stats = base.GeneratePrefInfo();

		stats["sword"] = Rate.Sometimes;
		stats["axe"] = Rate.Sometimes;
		stats["bow"] = Rate.Sometimes;
		stats["ale_drink"] = Rate.Sometimes;
		stats["wine_drink"] = Rate.Sometimes;
		stats["water_drink"] = Rate.Sometimes;
		stats["hat"] = Rate.Sometimes;

		return( stats );
	}
}
