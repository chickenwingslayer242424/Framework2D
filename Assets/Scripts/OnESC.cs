using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Interactions;

public class OnESC : MonoBehaviour
{
    public UnityEvent onEscapePressed;

    public UnityEvent onPause; // Event triggered when the game is paused
    public UnityEvent onResume;

    // Update is called once per frame
    void Update()
    {
        CloseObjectOnESC();
    }
    public void CloseObjectOnESC()
    {
        // Check if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onEscapePressed.Invoke();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Stops the game

        onPause.Invoke(); // Invoke the pause event
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resumes the game

        onResume.Invoke(); // Invoke the resume event
    }

}
