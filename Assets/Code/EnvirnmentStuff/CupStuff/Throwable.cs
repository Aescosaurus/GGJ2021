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

	// void FixedUpdate()
	// {
	// 	if( follow != null ) transform.position = follow.position;
	// 	print( body.velocity );
	// }

	public GameObject PickUp( Transform holdPoint )
	{
		if( body == null ) body = GetComponent<Rigidbody>();
		transform.SetParent( holdPoint,true );
		body.useGravity = false;
		// body.constraints = RigidbodyConstraints.FreezePosition;
		GetComponentInChildren<Collider>().isTrigger = true;

		// follow = holdPoint;

		return( gameObject );
	}

	public void Throw( Vector3 dir )
	{
		transform.SetParent( null,true );
		// body.velocity = Vector3.zero;
		GetComponentInChildren<Collider>().isTrigger = false;
		
		// body.constraints = RigidbodyConstraints.None;
		body.AddForce( dir * throwForce,ForceMode.Impulse );
		body.useGravity = true;
		// follow = null;
	}

	// Transform follow = null;
	Rigidbody body;
	[SerializeField] float throwForce = 5.0f;
}
