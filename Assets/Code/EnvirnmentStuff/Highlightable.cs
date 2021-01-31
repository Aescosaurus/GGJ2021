using System.Collections;
using UnityEngine;

public class Highlightable : MonoBehaviour
{
	Timer timerHighlight = new Timer(0.05f);
    Timer timerFlash = new Timer(2f);
    //for flashing
    MeshRenderer renderer;
    public Color wrongColor;
    Color normalColor;
    private void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
        normalColor = renderer.material.color;
        transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(1).gameObject.SetActive(false);
	}
    private void Update()
    {
        if(timerHighlight.Update(Time.deltaTime))
        {
			Unhighlight();
        }
    }
    public void Highlight()
	{
		timerHighlight.Reset();
		transform.GetChild(0).gameObject.SetActive(false);
		transform.GetChild(1).gameObject.SetActive(true);
	}
	public void Unhighlight()
	{
		transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(1).gameObject.SetActive(false);
	}
    public void StartFlash()
    {
        StartCoroutine(Flasher());
    }
	private IEnumerator Flasher()
    {
        for(int i = 0; i < 5; i++)
        {
            renderer.material.color = wrongColor;
            yield return new WaitForSeconds(.1f);
            renderer.material.color = normalColor;
            yield return new WaitForSeconds(.1f);
        }
    }
}
