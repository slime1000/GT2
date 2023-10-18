using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        //main menu button to load game
        SceneManager.LoadScene("3D");
    }

    public void ExitGame()
    {
        //exits game
        Debug.Log("quit!");
        Application.Quit();
    }

    public void MainMenu()
    {
        //loads main menu
        SceneManager.LoadScene("MainMenu");
    }
}
