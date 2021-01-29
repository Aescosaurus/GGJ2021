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

	public void SpawnText( string info )
	{
		Destroy( curBubble );
		curBubble = Instantiate( bubblePrefab,transform );
		curBubble.GetComponent<TextMesh>().text = info;
	}
	
	[SerializeField] string startingText = "";

	GameObject bubblePrefab;
	GameObject curBubble;
}
