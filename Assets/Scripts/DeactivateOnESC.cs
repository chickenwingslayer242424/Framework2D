using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Interactions;

public class DeactivateOnESC : MonoBehaviour
{
    public UnityEvent onEscapePressed;

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
            // Invoke the UnityEvent
            onEscapePressed.Invoke();
        }
    }

}
