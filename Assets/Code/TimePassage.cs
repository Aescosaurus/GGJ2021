using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePassage : MonoBehaviour
{
    //A value that tracks the day progress in the game.
    static public int DAY = 1;

    public bool endOfDay = false;
    //The sun 
    [SerializeField]
    GameObject theSun;
    //The speed of the sun. This is how many degrees of rotation per second
    [SerializeField]
    int speedOfSun = 30;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //This roates the light source to make it look like a day night cycle
        theSun.transform.Rotate(Vector3.right * Time.deltaTime * speedOfSun);

        //Check if the sun is at a specific angle or passed it. If so set end of day to true and reset the suns position
        if (theSun.transform.eulerAngles.x > 195)
        {
            endOfDay = true;
            theSun.transform.Rotate(Vector3.right * Time.deltaTime * speedOfSun * 5);
        }

    }
}
