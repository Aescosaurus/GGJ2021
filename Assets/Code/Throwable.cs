using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody>();
	}

	public void Throw( Vector3 dir )
	{
		body.AddForce( dir * throwForce,ForceMode.Impulse );
		body.useGravity = true;
	}

	Rigidbody body;
	[SerializeField] float throwForce = 5.0f;
}
