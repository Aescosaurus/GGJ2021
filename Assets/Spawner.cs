using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //queue of customers
    Queue<GameObject> line = new Queue<GameObject>();

    //customer
    public GameObject customer;

    private void Start()
    {
        Instantiate(customer, new Vector3(13, 0, 6), Quaternion.identity);
        customer.GetComponent<CharacterAI>().enabled = true;
        line.Enqueue(customer);
    }
    // Update is called once per frame
    void Update()
    {
        //until the queue is full, keep spawning.
        if(line.Count < 5)
        {
            //make a character, turn it off, put in line
            Instantiate(customer, new Vector3(13, 0, 6), Quaternion.identity);
            customer.GetComponent<CharacterAI>().enabled = false;
            line.Enqueue(customer);
        }
        if(line.Peek().GetComponent<CharacterAI>().exit == true)
        {
            Destroy(line.Peek().gameObject);
            line.Dequeue();
        }
    }
}
