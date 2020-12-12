﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController pc;
    private SpriteRenderer sr;
    //grounded variables
    public Transform groundCheck;
    public float groundedRadius = 0.1f;
    public LayerMask whatIsGround;
    private float movementSmoothing = 0.05f;
    private Vector3 refVelocity = Vector3.zero;
    public float speed = 1f;

    // movement variables
    private float horizontalMove = 0;
    private bool facingRight = true;

    // jumping variables
    public float jumpSpeed = 1f;
    private bool jump;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pc = gameObject.GetComponent<PlayerController>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        // grounded calculations
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                pc.setGrounded(true);
            }
        }

        // movement
        Vector3 targetVelocity = new Vector2(horizontalMove * 10f * Time.deltaTime, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, movementSmoothing);
        
        // jumping
        if(pc.getGrounded() && jump)
        {
            pc.setGrounded(false);
            rb.velocity += Vector2.up * jumpSpeed;
        }
        jump = false;
        
        // the following code was sourced from Board To Bits Games at https://www.youtube.com/watch?v=7KiK0Aqtmzc&t=399s 
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump")) 
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        //end of sourced code

        //set state values in PlayerController
        setStates();
        //flip to face right way
        flip();
    }

    private void flip()
    {
        //flipping
        if(horizontalMove > 0 && !facingRight)
        {
            sr.flipX = false;
            facingRight = true;
        }
        if(horizontalMove < 0 && facingRight)
        {
            sr.flipX = true;
            facingRight = false;
        }
    }

    private void setStates()
    {  
        if(horizontalMove == 0)
        {
            pc.setNeutral();
        }
        else
        {
            pc.setMoving();
        }
    }

}
