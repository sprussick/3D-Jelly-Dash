using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for JellyFish enemies that cause the player to bounce on collision
public class BounceEnemy : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip bounceSound;

    BoxCollider boxCollider; 

	void Start () 
    {
        // get the BoxCollider of the jellyfish
        boxCollider = GetComponent<BoxCollider>();
    }
	
    // When something collides with the jellyfish
    IEnumerator OnTriggerEnter(Collider other)
    {
        // get the audio clip 
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bounceSound;
        // play the audio
        audioSource.Play();

        // if object colliding into the bounce enemy is player 1
        if (other.gameObject.tag == "player1")
        {
            // get player 1 
            Player1 p1 = other.GetComponent<Player1>();
            if (p1 != null)
            {
                // set p1's bounce boolean to true, it will call it's bounce coroutine
                p1.bounce = true;
            }
        }
        // if object colliding into the bounce enemy is player 2
        if (other.gameObject.tag == "player2")
        {
            // get player 2
            Player2 p2 = other.GetComponent<Player2>();
            if (p2 != null)
            {
                // set p2's bounce boolean to true, it will call it's bounce coroutine
                p2.bounce = true;
            }
        }

        // temporarily disable the boxcollider so the player cannot immediately collide again
        boxCollider.enabled = false;
        yield return new WaitForSeconds(2);
        boxCollider.enabled = true;

    }

    // when the bounce enemy becomes invisible, it will be deleted  
    private void OnBecameInvisible()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("bounce");
        foreach (GameObject jelly in gameObjects)
        {
            if (!jelly.GetComponent<Renderer>().isVisible)
            {
                Destroy(jelly);
            }
        }
    }
}
