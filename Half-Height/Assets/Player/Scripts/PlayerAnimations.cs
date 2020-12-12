using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        animator.SetBool("grounded", playerController.getGrounded());
        //changeOfState();
        // airborne animations happen automatically if grounded != true, just have to update y velocity
        animator.SetFloat("airSpeed", rb.velocity.y);
    }

    public void changeOfState()
    {
        //all grounded animations

        if(playerController.getNeutral())
        {
            setAnimation(0);
        }
        if(playerController.getMoving())
        {
            setAnimation(1);
        }
        if(playerController.getAttacking())
        {
            setAnimation(2);
        }
        if(playerController.getBlocking())
        {
            setAnimation(3);
        }
        if(playerController.getDashing())
        {
            setAnimation(4);
        }
    
        

    }

    // set all animations to false, then set the right animation. 
    private void setAnimation(int animation)
    {
        /*
        neutral     = 0;
        moving      = 1;
        attacking   = 2;
        blocking    = 3;
        dashing     = 4;
        airspeed
        */
        animator.SetBool("neutral", false);
        animator.SetBool("moving", false);
        animator.SetBool("attacking", false);
        animator.SetBool("blocking", false);
        animator.SetBool("dashing", false);
        
        switch(animation)
        {
            case 0:
                animator.SetBool("neutral", true);
                break;
            case 1:
                animator.SetBool("moving", true);
                break;
            case 2:
                animator.SetBool("attacking", true);
                break;
            case 3: 
                animator.SetBool("blocking", true);
                break;
            case 4: 
                animator.SetBool("dashing", true);
                break;
            default:
                animator.SetBool("neutral", true);
                break;
        }
    }
}
