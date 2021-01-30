using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePassage : MonoBehaviour
{
    //The sun 
    [SerializeField]
    GameObject theSun;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        theSun.transform.Rotate(Vector3.right * Time.deltaTime * 30);
    }
}
