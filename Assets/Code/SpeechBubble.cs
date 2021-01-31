using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble
	:
	MonoBehaviour
	{

	string message;
	string status;
	TextMesh printer;
	void Start()
	{
		bubblePrefab = Resources.Load<GameObject>( "Prefabs/SpeechBubble" );

		curBubble = Instantiate( bubblePrefab,transform );
		printer = curBubble.GetComponent<TextMesh>();
		curBubble.transform.position += Vector3.up * heightOffset;
		SpawnText( "" );
		if( startingText.Length > 0 )
		{
			SpawnText( startingText );
		}
	}

	public void SpawnText( string info )
	{
		// DestroyText();
		// curBubble = Instantiate( bubblePrefab,transform );
		//curBubble.GetComponent<TextMesh>().text = info;
		message = info;
	}

	public void DestroyText()
	{
		// Destroy( curBubble );
		curBubble.GetComponent<TextMesh>().text = "";
	}

	public void AddStatus(string status)
    {
		this.status = status;
    }
    public void Update()
    {
		printer.text = message + "\n" + status;
	}

	[SerializeField] string startingText = "";

	GameObject bubblePrefab;
	GameObject curBubble;

	[SerializeField] float heightOffset = 0.0f;
}
