using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer
	:
	MonoBehaviour
{
	void Start()
	{
		audSrc = gameObject.AddComponent<AudioSource>();
	}

	public void PlaySFX( string path )
	{
		if( audSrc == null ) audSrc = gameObject.AddComponent<AudioSource>();
		// if( path.Length > 0 )
		{
			if( !audCodex.ContainsKey( path ) )
			{
				audCodex.Add( path,Resources.Load<AudioClip>( "Audio/" + path ) );
			}

			audSrc.PlayOneShot( audCodex[path] );
		}
	}

	AudioSource audSrc;
	static Dictionary<string,AudioClip> audCodex = new Dictionary<string,AudioClip>();
}
