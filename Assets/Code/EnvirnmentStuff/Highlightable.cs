using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour
{
	Timer timer = new Timer(0.05f);
    private void Start()
    {
		transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(1).gameObject.SetActive(false);
	}
    private void Update()
    {
        if(timer.Update(Time.deltaTime))
        {
			Unhighlight();
        }
    }
    public void Highlight()
	{
		timer.Reset();
		transform.GetChild(0).gameObject.SetActive(false);
		transform.GetChild(1).gameObject.SetActive(true);
	}
	public void Unhighlight()
	{
		transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(1).gameObject.SetActive(false);
	}
}
