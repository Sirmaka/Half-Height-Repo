﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController playerController;
    private Rigidbody2D thisRigidbody;
    private SpriteRenderer thisSpriteRenderer;
    public float parryDuration;
    private float parryTimer;
    private bool isBlocking;
    private bool stopBlocking;
    private bool parryWindow;
    private bool canParry = true;
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
        thisRigidbody = this.GetComponent<Rigidbody2D>();
        parryTimer = parryDuration;        

    }

    // Update is called once per frame
    void Update()
    {
        isBlocking = false;
        if(Input.GetButton("Block") && !playerController.getAttacking()
            && !playerController.getDashing() && playerController.getGrounded()
            && !playerController.getInDialogue())
        {
            isBlocking = true;
        }
        if(Input.GetButtonUp("Block"))
        {
            stopBlocking = true;
        }
    }

    void FixedUpdate()
    {
        bool wasBlocking = playerController.getBlocking();
        bool curBlocking = false;
        if(isBlocking)
        {
            // tell PlayerController that we're blocking and we can't move
            playerController.setBlocking(true);
            playerController.setCanMove(false);
            curBlocking = true;
            // thisRigidbody.velocity = new Vector2(0,0);  //  Stop the player from sliding.
            if(canParry)
            {
                parryWindow = true;
                canParry = false;
            }
        }
        
        if(wasBlocking && !curBlocking)
        {
            stopBlocking = true;
        }

        //if was not blocking last frame, but ARE blocking this frame, play the block sound.
        if(!wasBlocking && playerController.getBlocking())
        {
            playerController.playSound("block");
        }

        if(parryWindow)
        {
            parryTimer -= Time.deltaTime;
            // thisSpriteRenderer.material.color = Color.black;
            playerController.setParrying(true);
        }
        if(parryTimer <= 0)
        {
            parryWindow = false;
            parryTimer = parryDuration;
        }
        if(!parryWindow)
        {
            // thisSpriteRenderer.material.color = Color.white;
            playerController.setParrying(false);
        }

        if(stopBlocking)
        {
            isBlocking = false;
            playerController.setBlocking(false);
            playerController.setCanMove(true);
            playerController.setParrying(false);
            stopBlocking = false;
            parryWindow = false;
            canParry = true; //reset it here so players can only parry again after they've stopped blocking
        }
    }

}
