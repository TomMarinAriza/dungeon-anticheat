using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement stats")]
    public float Speed = 5f;



    Rigidbody2D rb;

    [Header("Animation")]

    Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        
        float moveY = Input.GetAxis("Vertical");


        rb.velocity = new Vector2(moveX * Speed, moveY * Speed);
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
    
        
        if (moveX == 0 && moveY == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
    }
}
