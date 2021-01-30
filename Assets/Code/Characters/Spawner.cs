using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //queue of customers
    Queue<GameObject> line = new Queue<GameObject>();

    //customer
    public GameObject customerPrefab;

    //So the customers dont spawn in each other
    private int offset = 10;

    private void Start()
    {
        // var curCustomer = Instantiate( customerPrefab, new Vector3(13, 0, 6), Quaternion.identity);
        // curCustomer.GetComponent<CharacterAI>().enabled = true;
        // line.Enqueue(curCustomer);
        SpawnCustomer();
        ActivateCustomer();
    }
    // Update is called once per frame
    void Update()
    {
        //until the queue is full, keep spawning.
        if(line.Count < 5)
        {
            //make a character, turn it off, put in line
            // Instantiate(customer, new Vector3(13 + (offset * line.Count), 0, 6), Quaternion.identity);
            // customer.GetComponent<CharacterAI>().enabled = false;
            // line.Enqueue(customer);
            SpawnCustomer();
        }
        //if the guy has been destroyed and removed, enable the next.
        if(line.Peek().GetComponent<CharacterAI>().exit == true)
        {
            Destroy(line.Peek().gameObject);
            line.Dequeue();
            line.Peek().GetComponent<CharacterAI>().enabled = true;
            ActivateCustomer();
        }
    }

    void SpawnCustomer()
	{
        var curCustomer = Instantiate( customerPrefab );
        curCustomer.transform.position = new Vector3( 13 + ( offset * line.Count ),-1.0f,6 );
        curCustomer.GetComponent<CharacterAI>().enabled = false;
        line.Enqueue( curCustomer );
    }

    void ActivateCustomer()
	{
        var theLine = line.ToArray();
        line.Peek().GetComponent<CharacterAI>().enabled = true;

        line.Peek().transform.position = new Vector3(13, -1.0f, 6);
    }
}
