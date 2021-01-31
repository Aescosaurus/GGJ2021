using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkAI
    :
    MonoBehaviour
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
    bool poisioned;

    public bool exit;
    bool mid;

    // bool liar = false;

    [SerializeField] float triggerHitDist = 0.8f;

    SpeechBubble speech;

    WeaponStats preferredWeapon = null;

    [SerializeField] Timer drinkRefire = new Timer( 10.0f );

    // Update is called once per frame
    void Start()
    {
        RandomizeRace();

        type = this.gameObject.GetComponent<CharStats>();
        if( type.LiarChance() == false )
        {
            itemLookingFor = GameObject.FindGameObjectWithTag( "Weapons" );
            //create a type of person based on the weapon.)
        }
        drinkGiven = false;

        centerTrigger = GameObject.Find( "CenterTrigger" ).gameObject;
        exitTrigger = GameObject.Find( "ExitTrigger" ).gameObject;

        lostAndFoundAnswer = false;
        /*
        If player wants lost and found item behind counter, show text bubble asking for it.
        Wait for answer
        */

        speech = GetComponent<SpeechBubble>();

        poisioned = false;

        drinkRefire.Update( Random.Range( 0.0f,drinkRefire.GetDuration() ) );
    }
    private void Update()
    {
        if( drinkRefire.Update( Time.deltaTime ) && !drinkAsked )
		{
            AskForDrink();
            drinkAsked = true;
		}

        // if( !drinkGiven )
        // {
        //     //hit center?
        //     if( Move( centerTrigger ) == false )
        //     {
        //         // transform.position = Vector3.MoveTowards(transform.position, centerTrigger.transform.position, rate);
        //         MoveTowards( centerTrigger.transform.position );
        //     }
        //     //stop
        //     else if( Move( centerTrigger ) == true && drinkAsked == false )
        //     {
        //         AskForDrink();
        //         //check if want item
        //         drinkAsked = true;
        //     }
        // }
        // else
        // {
        //     //after everything, move again
        //     if( Move( exitTrigger ) == false && mid == true )
        //     {
        //         // transform.position = Vector3.MoveTowards( transform.position,exitTrigger.transform.position,rate );
        //         MoveTowards( exitTrigger.transform.position );
        //     }
        //     //give ok to die
        //     else if( Move( exitTrigger ) == true )
        //     {
        //         exit = true;
        //     }
        //     /*If player is hit with bottle AND liar = true, answered = true, they move left
        //     If player is handed an item AND liar = false, answered = true, they move left.
        //     Else
        //     Game Over prints on screen.*/
        // }
    }
    void OnCollisionEnter( Collision collision )
    {
        //until drink given
        //If the object that hit the customer is not a mug
        var mugData = collision.gameObject.GetComponent<MugData>();
        if( mugData != null )
        {
            //Check based off of what drink type the mug is, ask question, then attach the mug to the customer. 
            if( drinkAsked && mugData.DrinkType == preferredDrink )
            {
                drinkAsked = false;
                drinkRefire.Reset();
                speech.DestroyText();
                // drinkGiven = true;
                // // speech.DestroyText();
                // LostAndFoundQuestion();
                // // mid = true;
                mugData.transform.SetParent( transform,true );
                Destroy( mugData.GetComponent<Rigidbody>() );
            }
            else if( mugData.DrinkType == "Poison" && !poisioned )
            {
                if( !drinkGiven )
                {
                    if( !( type.IsLiar() ) ) speech.SpawnText( "AAH WHYD U DO THAT I DIDNT EVEN ASK FOR AN ITEM YET" );
                    else speech.SpawnText( "HOW DID YOU KNOW WHO TALKED AHHHHHHHHHHHH" );
                }
                else
                {
                    if( type.IsLiar() ) speech.SpawnText( "AGH U CAUGHT ME OWOWOW" );
                    else speech.SpawnText( "WTF I WASNT EVEN LYING" );
                }

                Destroy( mugData.gameObject );

                drinkGiven = true; // lol
                mid = true;
                poisioned = true;
            }
        }
        var wepData = collision.gameObject.GetComponent<WeaponStats>();
        if( drinkGiven && wepData != null )
        {
            bool correctWep = ( wepData == preferredWeapon );
            if( correctWep )
            {
                speech.SpawnText( type.IsLiar() ? "hehe i was a liar" : "yay ty" );
            }
            else
            {
                speech.SpawnText( type.IsLiar() ? "thats not what i asked for but i was lying so thx anyway" : "thats not my item" );
            }

            if( correctWep || type.IsLiar() )
            {
                wepData.transform.SetParent( transform );
                Destroy( wepData.GetComponent<Rigidbody>() );
            }


            mid = true;
        }
    }
    //hit center/exit yet?
    bool Move( GameObject trigger )
    {
        var dist = trigger.transform.position - transform.position;
        return ( dist.sqrMagnitude < triggerHitDist * triggerHitDist );
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
        speech.SpawnText( order + " pls" );

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
        askingTime = true;
        // making it so all customers ask if possible, unless they are not a liar and have no matching weapons
        // if(askingTime == true)
        {
            var wepList = GameObject.FindGameObjectsWithTag( "Weapon" );
            if( !type.IsLiar() )
            {
                foreach( var wep in wepList )
                {
                    var wepStats = wep.GetComponent<WeaponStats>();
                    if( CheckMatch( wepStats,type ) )
                    {
                        preferredWeapon = wepStats;
                    }
                }
            }
            else
            {
                int chosen = Random.Range( 0,wepList.Length - 1 );
                preferredWeapon = wepList[chosen].GetComponent<WeaponStats>();
            }
            // int chosen = Random.Range( 0,wepList.Length - 1 );
            // preferredWeapon = wepList[chosen].GetComponent<WeaponStats>();

            if( preferredWeapon != null )
            {
                speech.SpawnText( GenWepText( preferredWeapon ) );
                // print( GenWepText( preferredWeapon ) );
            }
            else
            {
                speech.SpawnText( "ty for the drink" );
                mid = true;
            }
        }
        // else
        {
            // move to exit
        }
    }

    string GenWepText( WeaponStats wep )
    {
        string result = "i lost my ";
        result += wep.GetTitle();
        result += " with ";
        result += wep.GetMagicType();
        result += " magic";

        result += "\ndo you have it?";

        return ( result );
    }

    // true if can be owner
    bool CheckMatch( WeaponStats wep,CharStats charInfo )
    {
        if( !charInfo.CheckPref( wep.GetWepType().ToLower() ) ) return ( false );

        if( !charInfo.CheckPref( wep.GetMagicType().ToLower() + "_magic" ) ) return ( false );

        return ( true );
    }

    void RandomizeRace()
    {
        int choice = Random.Range( 0,6 );
        if( choice == 0 ) gameObject.AddComponent<DragonCharStats>();
        else if( choice == 1 ) gameObject.AddComponent<ElfCharStats>();
        else if( choice == 2 ) gameObject.AddComponent<FishCharStats>();
        else if( choice == 3 ) gameObject.AddComponent<HumanCharStats>();
        else if( choice == 4 ) gameObject.AddComponent<LizardCharStats>();
        else gameObject.AddComponent<WizardCharStats>();
    }

    void MoveTowards( Vector3 dest )
    {
        // var diff = dest - transform.position;
        // diff.Normalize();
        // // transform.Translate( diff * rate * Time.deltaTime );
        // transform.position += diff * rate * Time.deltaTime;
    }
}
