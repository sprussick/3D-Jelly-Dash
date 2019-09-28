using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Class to control the UI on players' screen
public class UIManager : MonoBehaviour
{
    [SerializeField] WinnerText winnerPanel = null;
    [SerializeField] ProgressBar firstProgress = null;
    [SerializeField] ProgressBar secondProgress = null;
    //[SerializeField] PauseMenu pauseMenu = null;

    static UIManager instance;

    // get instance of UIManager
    void Awake()
    {
        instance = this;
    }

    // set the winner inside WinnerText class
    public static void SetWinner(int number)
    {
        instance.winnerPanel.SetWinner(number);
    }

    // update Player 1 slider inside ProgressBar class
    public static void FirstUpdateSlider(float distance)
    {
        instance.firstProgress.FirstUpdateSlider(distance);
    }

    // updates Player 2 slider inside ProgressBar class
    public static void SecondUpdateSlider(float distance)
    {
        instance.secondProgress.SecondUpdateSlider(distance);
    }

    // Load a new scene
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}