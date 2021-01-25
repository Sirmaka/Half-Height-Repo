using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private PlayerController playerController;
    private Rigidbody2D thisRigidbody;
    private SpriteRenderer thisSpriteRenderer;  //used to know which direction the player is facing;
    public float dashSpeed;      
    private float direction = 1;            //will be changed by rigidbody.FlipX
    private bool amDashing = false;
    private bool dashing = false;
    public float dashTime;
    private float dashTimeReset;
    private Vector3 refVelocity = Vector3.zero;
    private float movementSmoothing = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
        thisRigidbody = this.GetComponent<Rigidbody2D>();
        dashTimeReset = dashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Dash") && playerController.getCanMove() 
            && !playerController.getBlocking() && !playerController.getAttacking()
            && !playerController.getInDialogue())
        {
            dashing = true;  
        }
    }

    void FixedUpdate()
    {
        //direction will be left by default. If flipX is true, then dash will go right.
        direction = -1;
        if(thisSpriteRenderer.flipX) 
        {
            direction = 1;
        }

        if(dashing)
        {   
            amDashing = true;
            playerController.setCanMove(false); //set canMove to false. Must reset when dash is over
            playerController.setDashing(true);  //alert the playerController that the player is dashing   
        }
        dashing = false;
        
        /*
            Code for dashing
        */
        if(amDashing)
        {
            Vector3 targetVelocity = new Vector2(direction * dashSpeed * Time.deltaTime, thisRigidbody.velocity.y);
            thisRigidbody.velocity = Vector3.SmoothDamp(thisRigidbody.velocity, targetVelocity, ref refVelocity, movementSmoothing);
            dashTime--;
        }

        if(dashTime <= 0 && amDashing)
        {
            amDashing = false;
            playerController.setCanMove(true);  //tell the playerController that the player is allowed to move now.
            playerController.setDashing(false); //tell the playerController that dashing is over
            dashTime = dashTimeReset;
        }
    }
}
