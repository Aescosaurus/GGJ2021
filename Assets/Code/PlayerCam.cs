using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerCam
	:
	MonoBehaviour
{
	void Start()
	{
		cam = transform.Find( "Main Camera" );

		Cursor.lockState = CursorLockMode.Locked;

		Assert.IsTrue( verticalCutoff > 0.0f );

		rayMask = ~LayerMask.GetMask( "Player" );
		holdSpot = cam.transform.Find( "HoldSpot" );
	}

	void Update()
	{
		if( Input.GetKeyDown( KeyCode.Escape ) )
		{
			Cursor.lockState = CursorLockMode.None;
		}
		if( Input.GetMouseButtonDown( 0 ) )
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		var aim = new Vector2( Input.GetAxis( "Mouse X" ),
			Input.GetAxis( "Mouse Y" ) );

		// cam.transform.eulerAngles = new Vector3(
		// 	cam.eulerAngles.x - aim.y * rotationSpeed * Time.deltaTime,
		// 	cam.eulerAngles.y + aim.x * rotationSpeed * Time.deltaTime,
		// 	cam.eulerAngles.z );

		if( aim.y > maxAimMove ) aim.y = maxAimMove;
		if( aim.y < -maxAimMove ) aim.y = -maxAimMove;

		var tempAng = cam.transform.eulerAngles;
		tempAng.x = tempAng.x - aim.y * rotationSpeed * Time.deltaTime;
		// if( tempAng.x < 0.0f + verticalCutoff ) tempAng.x = 0.0f + verticalCutoff;
		if( tempAng.x > 90.0f - verticalCutoff && tempAng.x < 180.0f ) tempAng.x = 90.0f - verticalCutoff;
		if( tempAng.x < 270.0f + verticalCutoff && tempAng.x > 180.0f ) tempAng.x = 270.0f + verticalCutoff;
		tempAng.y = tempAng.y + aim.x * rotationSpeed * Time.deltaTime;
		tempAng.z = 0.0f;
		cam.transform.eulerAngles = tempAng;

		var ray = new Ray( cam.transform.position,cam.transform.forward );
		RaycastHit hit;
		if( Physics.Raycast( ray,out hit,5.0f,rayMask ) )
		{
			// hit.transform.GetComponentInParent<Throwable>()?.Throw( cam.transform.forward );
			var throwable = hit.transform.GetComponentInParent<Throwable>();
			if( throwable != null && Input.GetAxis( "Interact" ) > 0.0f && heldItem == null )
			{
				heldItem = throwable.gameObject;
				heldItem.transform.SetParent( holdSpot,true );
				var body = heldItem.GetComponent<Rigidbody>();
				body.useGravity = false;
				body.velocity = Vector3.zero;
				heldItem.GetComponentInChildren<Collider>().isTrigger = true;
			}
		}

		if( Input.GetAxis( "Fire1" ) > 0.0f && heldItem != null )
		{
			heldItem.transform.SetParent( null,true );
			heldItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
			heldItem.GetComponentInChildren<Collider>().isTrigger = false;
			heldItem.GetComponent<Throwable>().Throw( cam.transform.forward );
			heldItem = null;
		}
	}

	Transform cam;

	[SerializeField] float rotationSpeed = 5.0f;
	[SerializeField] float verticalCutoff = 10.0f;
	const float maxAimMove = 90.0f - 1.0f;

	LayerMask rayMask;

	Transform holdSpot;
	GameObject heldItem = null;
}
