using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // The UI day progress bar
    [SerializeField]
    DayBar dayBar;
    [SerializeField]
    Image endOfDayUI;
    Text[] endOfDayText;

    [SerializeField] Timer dayTimer = new Timer( 120.0f );
    [SerializeField] Timer dayEndTimer = new Timer( 3.0f );

    private bool secondHalfOfDay;

    private const int RANGE_OF_DAY_CYCLE = 179;

    WeaponSpawner wepSpawner;

    // Start is called before the first frame update
    void Start()
    {
        endOfDayUI.enabled = false;
        secondHalfOfDay = false;
        endOfDayText = endOfDayUI.GetComponentsInChildren<Text>();
        endOfDayText[0].enabled = false;
        endOfDayText[1].enabled = false;

        dayEndTimer.Update( dayEndTimer.GetDuration() );

        wepSpawner = FindObjectOfType<WeaponSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if( dayEndTimer.Update( Time.deltaTime ) )
        {
            if( endOfDayText[0].enabled )
			{
                EndOfDay( true );
                wepSpawner.RefillWeps();
			}

            dayTimer.Update( Time.deltaTime );

            //This roates the light source to make it look like a day night cycle
            // theSun.transform.Rotate(Vector3.right * Time.deltaTime * speedOfSun);
            var rot = theSun.transform.eulerAngles;
            rot = Vector3.zero;
            // rot.x = dayTimer.GetPercent() * 360.0f;
            theSun.transform.eulerAngles = rot;
            theSun.transform.Rotate( Vector3.right,dayTimer.GetPercent() * 360.0f );

            // Update the UI bar with the rotation of the sun
            SetUIBar();

            //Check if the sun is at a specific angle or passed it. If so set end of day to true and reset the suns position
            // if (theSun.transform.eulerAngles.x > 195)
            if( dayTimer.IsDone() )
            {
                endOfDay = true;
                DAY++;
                // theSun.transform.Rotate(Vector3.right * Time.deltaTime * speedOfSun * 5);
                dayTimer.Reset();
                EndOfDay();
                dayEndTimer.Reset();
            }
            // else if (endOfDay && theSun.transform.eulerAngles.x < 16)
            // {
            //     endOfDay = false;
            //     EndOfDay(true);
            // }
        }
    }

    //This sets the UI bar in reference to the rotation of the sun
    private void SetUIBar()
    {
        float currentRotation = theSun.transform.eulerAngles.x;

        if (currentRotation > 89)
        {
            secondHalfOfDay = true;
        }

        if (secondHalfOfDay)
        {
            float offsetAdjustment = 90 - currentRotation;
            float totalOffset = offsetAdjustment + 90;

            // dayBar.setSize(1 - ((totalOffset - 16) / RANGE_OF_DAY_CYCLE));
        } else
        {
            // dayBar.setSize(1 - ((theSun.transform.eulerAngles.x - 16) / RANGE_OF_DAY_CYCLE));
        }
        dayBar.setSize( 1.0f - dayTimer.GetPercent() );

        if (endOfDay) secondHalfOfDay = false; 
    }

    private void EndOfDay(bool reset = false)
    {
        if (!reset)
        {
            endOfDayUI.enabled = true;
            endOfDayText[0].enabled = true;
            endOfDayText[1].enabled = true;
            endOfDayText[0].text = "DAY " + DAY;
        } else
        {
            endOfDayText[0].enabled = false;
            endOfDayText[1].enabled = false;
            endOfDayUI.enabled = false;
        }
    }
}
