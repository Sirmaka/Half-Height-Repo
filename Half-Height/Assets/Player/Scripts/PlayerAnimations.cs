using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    private Rigidbody2D rb;
    private AnimationClip parryClip; //  required for animation length
    private float parryAnimDuration;
    private float parryAnimTimer;
    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            if(clip.name.Equals("parryAnim"))
            {
                parryAnimDuration = clip.length;
            }
            //do this for attack anims
        }

    }

    // Update is called once per frame
    void Update()
    {   
        // airborne animations happen automatically if grounded != true, just have to update y velocity
        animator.SetBool("grounded", playerController.getGrounded());
        animator.SetFloat("airSpeed", rb.velocity.y);

        //TODO: move attacking animation code here

        //Parry animation must play to end, but move on once that's done
        if(playerController.getSuccessfulParry())
        {
            parryAnimTimer -= Time.deltaTime;
        }
        if(parryAnimTimer <= 0)
        {
            playerController.setSuccessfulParry(false);
            parryAnimTimer = parryAnimDuration;
        }
    }

    public bool changeOfState()
    {
        //all grounded animations

        //attacking must come first here as it takes priority over all other states.
        if(playerController.getSuccessfulParry())
        {
            setAnimation(6);
            return true;
        }
        if(playerController.getHurt())
        {
            setAnimation(5);
            return true;
        }
        if(playerController.getAttacking())
        {
            setAnimation(2);
            return true;
        }
        if(playerController.getNeutral())
        {
            setAnimation(0);
            return true;
        }
        if(playerController.getMoving())
        {
            setAnimation(1);
            return true;
        }
        if(playerController.getBlocking())
        {
            setAnimation(3);
            return true;
        }
        if(playerController.getDashing())
        {
            setAnimation(4);
            return true;
        }
        setAnimation(-1);
        return false;
    }

    // set all animations to false, then change to the right animation. 
    private void setAnimation(int animation)
    {
        /*
        neutral     = 0;
        moving      = 1;
        attacking   = 2;
        blocking    = 3;
        dashing     = 4;
        hurt        = 5;
        airspeed
        */
        animator.SetBool("neutral", false);
        animator.SetBool("moving", false);
        animator.SetBool("attacking", false);
        animator.SetBool("blocking", false);
        animator.SetBool("dashing", false);
        animator.SetBool("hurt", false);
        animator.SetBool("parrying", false);
        
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
            case 5:
                animator.SetBool("hurt", true);
                break;
            case 6:
                animator.SetBool("parrying", true);
                break;
            default:
                animator.SetBool("neutral", true);
                break;
        }
    }
}
