using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefBook
	:
	MonoBehaviour
{
	class InfoPage
	{
		public string title;
		public string desc;
	}


	void Start()
	{
		refInfoPanel = GameObject.Find( "RefBookPanel" );
		refInfoTitle = refInfoPanel.transform.Find( "Title" ).GetComponent<Text>();
		refInfoDesc = refInfoPanel.transform.Find( "Desc" ).GetComponent<Text>();
		refInfoPanel.transform.Find( "Prev" ).GetComponent<Button>().onClick.AddListener( delegate { Prev(); } );
		refInfoPanel.transform.Find( "Next" ).GetComponent<Button>().onClick.AddListener( delegate { Next(); } );
		refInfoPanel.SetActive( false );

		GenPage( "test","asdasd" );
		GenPage( "test2","aaaaaaaaaaaaaaaaaaa" );

		ReloadPage();
	}

	void Update()
	{
		if( Input.GetAxis( "Exit" ) > 0.0f )
		{
			Close();
		}
	}

	void Next()
	{
		if( ++curPage > pages.Count - 1 ) curPage = pages.Count - 1;
		else ReloadPage();
	}

	void Prev()
	{
		if( --curPage < 0 ) curPage = 0;
		else ReloadPage();
	}

	void ReloadPage()
	{
		refInfoTitle.text = pages[curPage].title;
		refInfoDesc.text = pages[curPage].desc;
	}

	public void Preview()
	{
		refInfoPanel.SetActive( true );
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	void Close()
	{
		refInfoPanel.SetActive( false );
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void GenPage( string title,string info )
	{
		var page = new InfoPage();
		page.title = title;
		page.desc = info;
		pages.Add( page );
	}

	GameObject refInfoPanel;
	Text refInfoTitle;
	Text refInfoDesc;

	List<InfoPage> pages = new List<InfoPage>();
	int curPage = 0;
}
