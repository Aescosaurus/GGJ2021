using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    private GameObject centerTrigger;
    private GameObject exitTrigger;
    private GameObject itemLookingFor;

    public float rate;
    CharStats type;

    //so that the customer knows when to leave
    bool drinkAsked;
    public bool drinkGiven;
    // public string nameOfDrink = "";
    string preferredDrink = "none";
    bool lostAndFoundAnswer;

    public bool exit;
    bool mid;

    [SerializeField] float triggerHitDist = 0.8f;

    SpeechBubble speech;

    // Update is called once per frame
    void Start()
    {
        type = this.gameObject.GetComponent<CharStats>();
        if(type.LiarChance() == false)
        {
            itemLookingFor = GameObject.FindGameObjectWithTag("Weapons");
            //create a type of person based on the weapon.)
        }
        drinkGiven = false;

        centerTrigger = GameObject.Find("CenterTrigger").gameObject;
        exitTrigger = GameObject.Find("ExitTrigger").gameObject;
        
        lostAndFoundAnswer = false;
        /*
        If player wants lost and found item behind counter, show text bubble asking for it.
        Wait for answer
        */

        speech = GetComponent<SpeechBubble>();
    }
    private void Update()
    {
        //hit center?
        if(Move(centerTrigger) == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, centerTrigger.transform.position, rate);
        }
        //stop
        else if(Move(centerTrigger) == true && drinkAsked == false)
        {
            AskForDrink();
            //check if want item
            drinkAsked = true;
        }

        if( drinkGiven )
        {
            //after everything, move again
            if( Move( exitTrigger ) == false && mid == true )
            {
                transform.position = Vector3.MoveTowards( transform.position,exitTrigger.transform.position,rate );
            }
            //give ok to die
            else if( Move( exitTrigger ) == true )
            {
                exit = true;
            }
            /*If player is hit with bottle AND liar = true, answered = true, they move left
            If player is handed an item AND liar = false, answered = true, they move left.
            Else
            Game Over prints on screen.*/
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        //until drink given
        //If the object that hit the customer is not a mug
        var mugData = collision.gameObject.GetComponent<MugData>();
        if (mugData != null)
        {
            //Check based off of what drink type the mug is, ask question, then attach the mug to the customer. 
            //BUG: DRINKTYPE IS NULL FOR SOME REASON WHEN IT GETS HERE
            if( mugData.DrinkType == preferredDrink )
			{
                drinkGiven = true;
                LostAndFoundQuestion();
                mid = true;
                mugData.transform.SetParent( transform,true );
                Destroy( mugData.GetComponent<Rigidbody>() );
                speech.DestroyText();
            }
            // Debug.Log("HIT");
            // if ( mugData.DrinkType == "Ale")
            // {
            //     // Debug.Log("ALE");
            //     LostAndFoundQuestion();
            //     //permission to move from mid
            //     mid = true;
            //     // collision.gameObject.transform.parent = this.gameObject.transform;
            // }
            // if ( mugData.DrinkType == "Wine")
            // {
            //     // Debug.Log("Wine");
            //     LostAndFoundQuestion();
            //     //permission to move from mid
            //     mid = true;
            // }
            // if ( mugData.DrinkType == "Water")
            // {
            //     // Debug.Log("Water");
            //     LostAndFoundQuestion();
            //     //permission to move from mid
            //     mid = true;
            // }
        }  
    }
    //hit center/exit yet?
    bool Move(GameObject trigger)
    {
        var dist = trigger.transform.position - transform.position;
        return( dist.sqrMagnitude < triggerHitDist * triggerHitDist );
        // if (transform.position.z < trigger.transform.position.z - .01 || transform.position.z > trigger.transform.position.z + .01)
        // {
        //     return false;
        // }
        // else
        // {
        //     return true;
        // }
    }

    void AskForDrink()
    {
        //based on character rate, ask for drink
        var order = type.GenerateOrder();
        preferredDrink = order;

        // print( order + " pls" );
        speech.SpawnText( order + " pls",1.0f );

        // if (order == "Ale")
        // {
        //     //display ale drink text bubble
        //     Debug.Log("Ale Pls");
        // }
        // else if ( order == "Wine")
        // {
        //     //wine
        //     Debug.Log("Wine pls");
        // 
        // }
        // else if ( order == "Water")
        // {
        //     Debug.Log("Water pls");
        // }
    }
    void LostAndFoundQuestion()
    {
        bool askingTime = type.AskAboutLostAndFound();
        if(askingTime == true)
        {

        }
    }
}
