using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that keeps track of whether or not the player is inside the current
public class CurrentTrigger : MonoBehaviour {

    [SerializeField] GameObject currentObject = null;
    public float fastSpeed;

    // when the player enters the current 
    void OnTriggerEnter(Collider other)
    {
        if (currentObject.CompareTag("current"))
        {
            // get the player objects
            Player1 p1 = other.GetComponent<Player1>();
            Player2 p2 = other.GetComponent<Player2>();
            // set their current booleans to true, their speed will increase 
            // inside their player class
            if (p1 != null)
            {
                p1.current = true;
            }
            else if (p2 != null)
            {
                p2.current = true;
            }
        }
    }

    // when the player exits the current
    void OnTriggerExit(Collider other)
    {
        if (currentObject.CompareTag("current"))
        {
            Player1 p1 = other.GetComponent<Player1>();
            Player2 p2 = other.GetComponent<Player2>();
            // set their current booleans to false, their speed will decrease
            // inside their player class
            if (p1 != null)
            {
                p1.current = false;
            }
            else if (p2 != null)
            {
                p2.current = false;
            }
        }
    }
}
