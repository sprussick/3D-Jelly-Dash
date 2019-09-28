using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to control Player 1
public class Player1 : MonoBehaviour {

    private Rigidbody thisRigid; // the player game object's rigidbody
    public float slowSpeed; //the speed not in the current
    public float fastSpeed; //the speed in the current
    public float horizontalSpeed; //the horizontal speed
    public float enemyAffectTime; //how long a certain affect lasts after colliding with an enemy
    public bool current = false; //if the player is in the current or not
    public bool bounce = false; //if the player hit a bounce enemy 
    public bool shock = false; //if the player hit a shock enemy
    public bool countdown = true; //for the first 3 seconds of countdown time
    float speedZ; //the Z coordinate of the velocity vector for the player
    bool cantMove = false; //set to true when the player is not allowed to move

    // for shock routine
    MeshRenderer rend;
    Color originalCol;

    // set to true if player wins
    public static bool playerOneWon = false;
   
    // store the player's original rotation
    Quaternion originalRotation;

    // player's speed in the X direction
    float speedX;
   
    // player's box collider
    BoxCollider boxCollider;

    // player's bubble particle system
    GameObject bubbleSystem;
    ParticleSystem bubbles;

    void Start () 
    {
        // make sure the jelly fish spawner is spawning
        JellyfishSpawner.stopSpawning = false;
        // get all the components from the scene
        thisRigid = GetComponent<Rigidbody>();
        rend = GetComponent<MeshRenderer>();
        originalCol = rend.materials[0].color;
        originalRotation = transform.rotation;
        bubbleSystem = GameObject.FindGameObjectWithTag("bubbles1");
        bubbles = bubbleSystem.GetComponent<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = 0;

        //updating the X speed
        if (Input.GetKey(KeyCode.RightArrow))
        {
            speedX = horizontalSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speedX = -horizontalSpeed;
        }

        //if the character did not just hit an enemy
        if (!bounce && !shock)
        {
            // if the player is inside the current
            if (current)
            {
                // increase speed to fast speed
                speedZ = fastSpeed;
            }
            else
            {
                // use the slow speed
                speedZ = slowSpeed;
            }
        }

        //if the character has hit a bounce enemy
        if (bounce)
        {
            // start bounce routine
            StartCoroutine(BounceForSeconds(enemyAffectTime));
            StopCoroutine(BounceForSeconds(enemyAffectTime));
        }

        //if the character has hit a shock enemy
        if (shock)
        {
            // start shock routine
            StartCoroutine(ShockForSeconds(enemyAffectTime));
            StopCoroutine(ShockForSeconds(enemyAffectTime));
        }

        // wait 3 seconds before doing anything
        if (countdown)
        {
            StartCoroutine(CountdownWait(3.0f));
            StopCoroutine(CountdownWait(3.0f));
        }

        // player just hit a shock enemy, prevent their movement
        if (cantMove)
        {
            speedX = 0;
            speedZ = 0;
        }

        // update velocity
        thisRigid.velocity = new Vector3(speedX, 0, speedZ);

        // send position to update progress slider
        UIManager.FirstUpdateSlider(transform.position.z);
    }


    // return true is player 1 has won
    public static bool CheckStatus()
    {
        return playerOneWon;
    }

    // gets called if player 1 won
    public void Winner()
    {
        // set to true
        playerOneWon = true;
        // stop the bubbles
        bubbles.Stop();
        // stop spawning new jelly fish
        JellyfishSpawner.stopSpawning = true;
        // display winner on UI
        UIManager.SetWinner(1);
        // stop all movement
        StartCoroutine(WaitAndStop(1.5f));
    }

    // if player 1 lost 
    public void Loser()
    {
        // stop the bubbles
        bubbles.Stop();
        // stop movement
        StartCoroutine(WaitAndStop(1.5f));
    }

    // after crossing the finish line, movement should stop
    IEnumerator WaitAndStop(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        slowSpeed = 0;
        fastSpeed = 0;
        horizontalSpeed = 0;
        speedZ = 0;
    }

    //coroutine when player hits a bounce enemy
    public IEnumerator BounceForSeconds(float seconds)
    {
        // push back the player by 40 z coordinates
        speedZ = -speedZ - 40;
        yield return new WaitForSeconds(0.5f);
        bounce = false;
    }

    //coroutine when player hits a shock enemy
    public IEnumerator ShockForSeconds(float seconds)
    {
        // set cant move to true so player movement pauses
        cantMove = true;
        // start shock routine to blink colors
        StartCoroutine(ShockRoutine());
        // set shock back to false
        shock = false;
        // wait 
        yield return new WaitForSeconds(seconds);
        // set cant move back to false
        cantMove = false;
    }

    //coroutine to flash player's color
    IEnumerator ShockRoutine()
    {
        Color flashColor = Color.white;
        bool blink = false;
        float startTime = Time.time;
        // repeatedly switch between white and orange until enemyAffectTime
        while (startTime + enemyAffectTime > Time.time)
        {
          blink = !blink;
          if (blink)
          {
             foreach (Material mat in rend.materials)
             {
                mat.color = flashColor;
             }
           }
           else
           {
             foreach (Material mat in rend.materials)
             {
                mat.color = originalCol;
             }
            }
            yield return new WaitForSeconds(0.1f);
         }//end while
        // make sure player is back to original color
        foreach (Material mat in rend.materials)
        {
            mat.color = originalCol;
        }
        // set blink to false so it stops
        blink = false;
    }

    // wait seconds until game starts
    IEnumerator CountdownWait(float seconds) 
    {
        // pause the bubbles until game starts
        bubbles.Pause();
        // prevent player from moving
        speedX = 0;
        speedZ = 0;
        // wait 
        yield return new WaitForSeconds(seconds);
        // set count down to false
        countdown = false;
        // start bubbles again
        bubbles.Play();
    }

    // on collision
    IEnumerator OnCollisionEnter(Collision collision)
    {
        // if colliding with player 2
        if (collision.gameObject.tag == "player2")
        {
            // set to kinematic
            thisRigid.isKinematic = true;
            // move back
            speedZ = -speedZ;
            // very quick 
            yield return new WaitForSeconds(0.01f);
            // set back to not kinematic
            thisRigid.isKinematic = false;
            // put back to original rotation
            transform.rotation = originalRotation;
        }
    }

}
