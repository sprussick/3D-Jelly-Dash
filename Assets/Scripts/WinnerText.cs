using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to display "Winner" and "Loser" messages when the game is over
public class WinnerText : MonoBehaviour {

    [SerializeField] Text playerText1 = null;
    [SerializeField] Text playerText2 = null;

    // Set the messages based on which player won and lost
    public void SetWinner(int player)
    {
        if (player == 1) {
            playerText1.text = "Winner!";
            playerText2.text = "Loser!";
        }
        if (player == 2)
        {
            playerText2.text = "Winner!";
            playerText1.text = "Loser!";
        }
    }

}
