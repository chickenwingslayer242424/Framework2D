using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 _moveDirection;

    public InputActionReference move;
    private bool _isFacingRight = true;


    // Update is called once per frame
    void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
        _moveDirection = move.action.ReadValue<Vector2>();


        if (_moveDirection.x > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (_moveDirection.x < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()  //fixedupdate ist framerate unabhängig // 50 fps
    {
        rb.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);

    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
