using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to control Progress Bar showing player distance to finish line
public class ProgressBar : MonoBehaviour {

    [SerializeField] GameObject finishLine = null;
    [SerializeField] RectTransform firstPlayerProgress = null;
    [SerializeField] RectTransform secondPlayerProgress = null;
    [SerializeField] Player1 player1 = null;

    // holds the place of the finish line
    float finishPosition;
    // holds the place where the first player starts
    float firstStartPosition;
    // holds the length from player starting position to finish line
    float courseLength;

    // Use this for initialization
    void Start () 
    {  
        finishPosition = finishLine.transform.position.z;
        firstStartPosition = player1.transform.position.z;
        courseLength = finishLine.transform.position.z - firstStartPosition;
    }

    // update player 1's slider
    public void FirstUpdateSlider(float distance)
    {
        // if player is past finish line, ignore
        if (distance > finishPosition)
        {
            return;
        }
        if (firstPlayerProgress == null)
        {
            return;
        }
        // calculate proportion to finish line
        float relativeScale = 1 - (finishPosition - distance) / courseLength;
        // scale it to full progress bar
        Vector3 scale = firstPlayerProgress.transform.localScale;
        scale.x = relativeScale;
        firstPlayerProgress.transform.localScale = scale;
    }

    // update player 2's slider
    public void SecondUpdateSlider(float distance)
    {
        // if player is past finish line, ignore
        if (distance > finishPosition)
        {
            return;
        }
        if (secondPlayerProgress == null)
        {
            return;
        }
        // calculate proportion to finish line
        float relativeScale = 1 - (finishPosition - distance) / courseLength;
        // scale it to full progress bar
        Vector3 scale = secondPlayerProgress.transform.localScale;
        scale.x = relativeScale;
        secondPlayerProgress.transform.localScale = scale;
    }


}
