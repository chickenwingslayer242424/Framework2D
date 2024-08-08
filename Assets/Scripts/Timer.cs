using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
     public bool timerIsRunning = false;
     public float timeRemaining = 15f;
     public TMP_Text timerText;
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();;
        }
        else
        {
             timeRemaining = 0;
             Time.timeScale = 0f;
        }
    }
}
