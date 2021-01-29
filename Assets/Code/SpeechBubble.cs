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

		if( startingText.Length > 0 )
		{
			SpawnText( startingText );
		}
	}

	public void SpawnText( string info,float heightOffset = 0.0f )
	{
		DestroyText();
		curBubble = Instantiate( bubblePrefab,transform );
		curBubble.GetComponent<TextMesh>().text = info;
		curBubble.transform.position += Vector3.up * heightOffset;
	}

	public void DestroyText()
	{
		Destroy( curBubble );
	}
	
	[SerializeField] string startingText = "";

	GameObject bubblePrefab;
	GameObject curBubble;
}
