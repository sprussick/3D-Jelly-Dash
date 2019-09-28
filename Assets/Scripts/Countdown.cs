using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to control 3 2 1 count down at the beginning of game play
public class Countdown : MonoBehaviour
{
    // Holds the 3,2,1 images
    [SerializeField] GameObject imageObject = null;
    Coroutine startCountdown;

    // Call countdown
    void Start()
    {
        CountDown();
    }

    // starts the coroutine
    public void CountDown()
    {
        startCountdown = StartCoroutine(Count());
    }

    // Switches the raw image between 3, 2 and 1 
    IEnumerator Count()
    {
        Texture2D two = Resources.Load<Texture2D>("2gold");
        Texture2D one = Resources.Load<Texture2D>("1gold");

        yield return new WaitForSeconds(1.0f);
        imageObject.GetComponent<RawImage>().texture = two;
        yield return new WaitForSeconds(1.0f);
        imageObject.GetComponent<RawImage>().texture = one;
        // turn raw image to transparent
        yield return new WaitForSeconds(1.0f);
        imageObject.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);

    }
}