using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble
	:
	MonoBehaviour
{
	void Start()
	{
		bubblePrefab = Resources.Load<GameObject>( "Prefabs/SpeechBubble" );

		curBubble = Instantiate( bubblePrefab,transform );
		curBubble.transform.position += Vector3.up * heightOffset;
		if( startingText.Length > 0 )
		{
			SpawnText( startingText );
		}
	}

	public void SpawnText( string info )
	{
		// DestroyText();
		// curBubble = Instantiate( bubblePrefab,transform );
		curBubble.GetComponent<TextMesh>().text = info;
	}

	public void DestroyText()
	{
		// Destroy( curBubble );
		curBubble.GetComponent<TextMesh>().text = "";
	}
	
	[SerializeField] string startingText = "";

	GameObject bubblePrefab;
	GameObject curBubble;

	[SerializeField] float heightOffset = 0.0f;
}
