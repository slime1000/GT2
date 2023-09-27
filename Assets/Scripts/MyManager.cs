using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyManager : MonoBehaviour
{
    public static MyManager Instance;

    public int playerScore = 0;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        //always set score to 0
        playerScore = 0;
        
    }
    private void Awake()
    {
        //making manager, and managing if there already is one
        if (Instance == null)

        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this) 
        {
            Debug.Log("more than 1 instance of a manager", this);
        Destroy(this.gameObject);
        }
    }

    private void OnDisable()
    {
        //disabling instance
        if (Instance == this)
        {
            Instance = null;
        }
    }
    public void AddScore ()
    {
        //adding score on egg collect
        playerScore += 1;
        Debug.Log("Score Added");
    }

    // Update is called once per frame
    void Update()
    {
        //score ui update
        scoreText.text = playerScore.ToString() + "/5";
       
        
    }
}
