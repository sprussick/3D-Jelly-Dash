using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to display winner/loser messages when the players cross the finish line
public class FinishLineCrossed : MonoBehaviour {

    // holds the finish line object
    [SerializeField] GameObject finishObject = null;

    // when a player crosses the finish line
    private void OnTriggerEnter(Collider col)
    {
        // if it is player 1
        if (col.GetComponent<Collider>().CompareTag("player1"))
        {
            if (finishObject.CompareTag("finish"))
            {
                // get player1
                Player1 player1 = col.transform.root.GetComponentInChildren<Player1>();
                // if neither player has already won, declare player 1 the winner
                if (Player1.CheckStatus() == false && Player2.CheckStatus() == false)
                {
                    player1.Winner();
                }
                // player 1 lost
                else
                {
                    player1.Loser();
                }
            }
        }
        // if it is player 2
        if (col.GetComponent<Collider>().CompareTag("player2"))
        {
            if (finishObject.CompareTag("finish"))
            {
                // get player 2
                Player2 player2 = col.transform.root.GetComponentInChildren<Player2>();
                // if neither player has already won, declare player 2 the winner
                if (Player1.CheckStatus() == false && Player2.CheckStatus() == false)
                {
                    player2.Winner();
                }
                // player 2 lost
                else
                {
                    player2.Loser();
                }
            }
        }
    }
}
