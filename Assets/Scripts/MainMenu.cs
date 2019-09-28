using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class to control buttons on the Main Menu
public class MainMenu : MonoBehaviour {

    [SerializeField] GameObject UIPanel = null;

    // loads a new scene 
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
