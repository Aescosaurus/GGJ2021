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
    public string nameOfDrink = "";
    bool lostAndFoundAnswer;

    public bool exit;
    bool mid;

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
        //after everything, move again
        else if (Move(exitTrigger) == false && mid == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitTrigger.transform.position, rate);
        }
        //give ok to die
        else if(Move(exitTrigger) == true)
        {
            exit = true;
        }
        /*If player is hit with bottle AND liar = true, answered = true, they move left
        If player is handed an item AND liar = false, answered = true, they move left.
        Else
        Game Over prints on screen.*/
    }
    void OnCollisionEnter(Collision collision)
    {
        //until drink given
        if(gameObject.name == "Ale")
        {
            LostAndFoundQuestion();
            //permission to move from mid
            mid = true;
        }
        if (gameObject.name == "Wine")
        {
            LostAndFoundQuestion();
            //permission to move from mid
            mid = true;
        }
        if (gameObject.name == "Water")
        {
            LostAndFoundQuestion();
            //permission to move from mid
            mid = true;
        }
        
    }
    //hit center/exit yet?
    bool Move(GameObject trigger)
    {
        if (transform.position.z < trigger.transform.position.z - .01 || transform.position.z > trigger.transform.position.z + .01)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void AskForDrink()
    {
        //based on character rate, ask for drink
        if (type.GenerateOrder() == "Ale")
        {
            //display ale drink text bubble
            Debug.Log("Ale Pls");
        }
        else if (type.GenerateOrder() == "Wine")
        {
            //wine
            Debug.Log("Wine pls");

        }
        else if (type.GenerateOrder() == "Water")
        {
            Debug.Log("Water pls");
        }
    }
    void LostAndFoundQuestion()
    {
        bool askingTime = type.AskAboutLostAndFound();
        if(askingTime == true)
        {

        }
    }
}
