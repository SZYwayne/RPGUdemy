using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float yInput;
    [SerializeField] private float xInput;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        Movement();
        CheckInput();
        AnimatorControllers();
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }        
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * 5, rb.velocity.y);
    }

    private void AnimatorControllers()
    {
        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void Jump()
    {
        rb.velocity += new Vector2(0, yInput);
    }
}
