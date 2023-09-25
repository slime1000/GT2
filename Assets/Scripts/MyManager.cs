using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyManager : MonoBehaviour
{
    public static MyManager Instance;

    public int playerScore = 0;

    void Start()
    {
        playerScore = 0;
    }
    private void Awake()
    {
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
        if (Instance == this)
        {
            Instance = null;
        }
    }
    public void AddScore ()
    {
        playerScore += 1;
        Debug.Log("Score Added");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScore >= 5)
        {

        }
    }
}
