using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatienceTimer : MonoBehaviour
{
    Timer timer;
    public float initPatiance = 20.0f;
    float patienceBoost = 55.0f;
    // float time = 10f;
    CharacterAI characterState;
    SpeechBubble text;
    DrunkAI DrunkState;
    int losingAmount;

    bool addedTime;

    // Start is called before the first frame update
    void Start()
    {
        DrunkState = GetComponent<DrunkAI>();
        addedTime = false;
        
        //start timer
        timer = new Timer(initPatiance);
        characterState = this.gameObject.GetComponent<CharacterAI>();
        text = this.gameObject.GetComponent<SpeechBubble>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DrunkState != null)
        {
            if( DrunkState.drinkGiven )
            {
                DrunkState.drinkGiven = false;
                timer.Reset();
            }
        }
        //if drink recieved
        else if (characterState.drinkGiven == true)
        {
            //check if lost and found item is a question, if so, add 10 seconds to timer.
            if (characterState.continueTimer == true)
            {
                if (!addedTime)
                {
                    timer.Add(patienceBoost);
                    addedTime = true;
                }
            }
            
            else
            {
                //if not, delete this script
                Destroy(this);
            }
        }

        timer.Update(Time.deltaTime);
        float time = 1 - timer.GetPercent();
        if (time >= .6)
        {
            text.AddStatus(":^)");
        }
        else if (time >= .3)
        {
            text.AddStatus(":^/");
        }
        else
        {
            text.AddStatus(":^(");
        }

        if(timer.IsDone())
        {
            text.AddStatus(">:^(");
            losingAmount = Random.Range(7, 13);
            //if customer, leave
            if ( !this.gameObject.name.Contains( "Drunkard" ) )
            {
                GameObject trigger = GameObject.Find("ExitTrigger");
                characterState.exit = true;
                // DrunkState.exit = true;
                // Destroy( gameObject );
            }
            else if (this.gameObject.name.Contains( "Drunkard" ) )
            {
                //if drunkard, lose money.
                MoneyManager.changeMoneyAmount(-losingAmount);
                text.AddStatus(">:^(");
                Destroy(gameObject);
            }
        }
    }

    public void ResetPatience()
	{
        timer.Reset();
	}
}
