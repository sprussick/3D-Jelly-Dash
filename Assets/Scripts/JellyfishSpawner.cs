using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to spawn the jelly fish enemies 
public class JellyfishSpawner : MonoBehaviour
{
    //instantiate the variables
    public GameObject[] enemies;

    // the locations where the jelly fish will be spawned between
    public Vector3 spawnValuesFirst;
    public Vector3 spawnValuesSecond;

    // time period to wait between spawning jellyfish
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;

    // boolean that will control when the spawner is spawning
    public static bool stopSpawning;

    // randomly choose between shock and bounce jellyfish
    int randEnemy;

    void Start()
    {
        StartCoroutine(JellySpawner());
    }

    void Update()
    {
        //vary the spawn times
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator JellySpawner()
    {
        //wait to begin spawning until 321 countdown
        yield return new WaitForSeconds(startWait);

        // while jelly fish should be spawning
        while (!stopSpawning)
        {
            //choose randomly between shock and bounce jelly fish
            randEnemy = Random.Range(0, 2);

            //randomize the spawn postions relative to x and z axis 
            Vector3 spawnPosition = new Vector3(Random.Range(spawnValuesFirst.x, spawnValuesSecond.x), -1, Random.Range(spawnValuesFirst.z, spawnValuesSecond.z));

            //pull the positions of the players
            Vector3 p1Position = GameObject.FindGameObjectWithTag("player1").transform.position;
            Vector3 p2Position = GameObject.FindGameObjectWithTag("player2").transform.position;
            Vector3 leaderPos;
        
            //find which player is in the lead, as that is where we want to spawn the jelly fish
            if (p1Position.z >= p2Position.z)
            {
                leaderPos = p1Position;
            }
            else
            {
                leaderPos = p2Position;
            }

            //move the spawn point further out ahead of the players, but substract leader's X position so that each spawner
            //does not follow the player
            leaderPos = leaderPos + new Vector3(transform.position.x-leaderPos.x, 0, 70);
            // create the jelly fish
            Instantiate(enemies[randEnemy], spawnPosition + leaderPos, gameObject.transform.rotation);
           
            //wait to spawn the next one 
            yield return new WaitForSeconds(spawnWait);
        }
    }
}