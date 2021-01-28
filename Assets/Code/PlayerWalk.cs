using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk
	:
	MonoBehaviour
{
	void Start()
	{
		charCtrl = GetComponent<CharacterController>();
		cam = Camera.main;
	}

	void Update()
	{
		var forwardVec = cam.transform.forward;
		forwardVec.y = 0.0f;
		var rightVec = cam.transform.right;

		if( charCtrl.isGrounded ) grav = 0.0f;
		else grav += gravAcc * Time.deltaTime;

		charCtrl.Move( ( forwardVec * Input.GetAxis( "Vertical" ) +
			rightVec * Input.GetAxis( "Horizontal" ) +
			Vector3.down * grav ) *
			moveSpeed * Time.deltaTime );
	}

	CharacterController charCtrl;
	Camera cam;

	[SerializeField] float moveSpeed = 5.0f;
	[SerializeField] float gravAcc = 1.0f;
	float grav = 0.0f;
}
