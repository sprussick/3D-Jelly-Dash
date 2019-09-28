using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for the jelly fish enemies that shock the player upon collision
public class ShockEnemy : MonoBehaviour
{

    BoxCollider boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // on collision
    IEnumerator OnTriggerEnter(Collider other)
    {
        // if it is player 1
        if (other.gameObject.tag == "player1")
        {
            // get player 1
            Player1 p1 = other.GetComponent<Player1>();
            if (p1 != null)
            {
                // set its shock boolean to true, will call shock routine inside its class
                p1.shock = true;        
            }
        }
        // if it is player 2
        if (other.gameObject.tag == "player2")
        {
            // get player 2
            Player2 p2 = other.GetComponent<Player2>();
            if (p2 != null)
            {
                // set its shock boolean to true, will call shock routine inside its class
                p2.shock = true;
            }
        }

        // disable the collider for a few seconds so the player isn't getting shocked repeatedly
        boxCollider.enabled = false;
        yield return new WaitForSeconds(4f);
        boxCollider.enabled = true;


    }

    // when the shock enemy becomes invisible, it will be deleted
    private void OnBecameInvisible()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("shock");
        foreach (GameObject jelly in gameObjects)
        {
            if (!jelly.GetComponent<Renderer>().isVisible)
            {
                Destroy(jelly);
            }
        }
    }

}

