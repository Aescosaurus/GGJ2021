using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechRotate
	:
	MonoBehaviour
{
	void Start()
	{
		cam = Camera.main;
	}

	void Update()
	{
		var rot = transform.eulerAngles;
		var diff = cam.transform.position - transform.position;
		rot.y = Mathf.Atan2( diff.x,diff.z ) * Mathf.Rad2Deg + 180.0f;
		transform.eulerAngles = rot;
	}

	Camera cam;
}
