using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCharStats
	:
	CharStats
{
	protected override Dictionary<string,Rate> GeneratePrefInfo()
	{
		var stats = base.GeneratePrefInfo();

		stats["sword"] = Rate.Sometimes;
		stats["axe"] = Rate.Sometimes;
		stats["bow"] = Rate.Rarely;
		stats["staff"] = Rate.Sometimes;
		stats["wand"] = Rate.Sometimes;
		stats["fire_magic"] = Rate.Sometimes;
		stats["ale_drink"] = Rate.Sometimes;
		stats["wine_drink"] = Rate.Sometimes;
		stats["tail"] = Rate.Sometimes;
		stats["horns"] = Rate.Sometimes;
		stats["is_red"] = Rate.Sometimes;
		stats["is_green"] = Rate.Sometimes;
		stats["is_blue"] = Rate.Sometimes;

		return( stats );
	}
}
