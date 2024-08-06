using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        //input
        
    }

    void FixedUpdate()  //fixedupdate ist framerate unabhängig // 50 fps
    {
        rb.MovePosition(rb.position + movement * moveSpeed* Time.fixedDeltaTime);
        Movement();
    }

    public void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");//gibt dir -1 wenn nach links, 1 wenn nach rechts, 0 wenn nichts gedrückt wird zurück
        movement.y = Input.GetAxisRaw("Vertical"); //+1 oben, -1 unten, 0 nichts
        movement = movement.normalized; // x und y haben konstante geschwindigkeit, mit normalized ist es auch queer gleich schnell 
    }
}
