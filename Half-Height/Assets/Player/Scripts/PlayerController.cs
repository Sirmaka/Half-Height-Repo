using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //This class holds all state information for the player-object. All classes refer to this for state information.

    //states for movement and animation
    private bool canMove = true; // determines whether movement is currently possible.
    private bool grounded = false; //determines whether the player is on the ground

    //states for animation
    private bool neutral = true; // true by default
    private bool moving = false;
    private bool attacking = false;
    private bool dashing = false;
    private bool blocking = false;
    private bool invincible = false;
    private bool hurt = false;
    private bool parrying = false;        //if done right, player may parry an attack
    private bool successfulParry = false; //parry has been successful
    private bool inDialogue = false;
    private bool canEnterDialogue = false;
    // reference to animations
    private PlayerAnimations playerAnimations;

    void Start()
    {
        playerAnimations = gameObject.GetComponent<PlayerAnimations>();
    }
    
    public bool getCanMove()
    {
        return canMove;
    }

    public void setCanMove(bool set)
    {
        canMove = set;
    }

    public bool getNeutral()
    {
        return neutral;
    }

    public void setNeutral()
    {
        if(grounded && !neutral)
        {
            switchOffStates();
            neutral = true;
            alertAnimations();
        }
    }

    public bool getMoving()
    {
        return moving;
    }
    public void setMoving()
    {
        if(grounded && !moving)
        {
            switchOffStates();
            moving = true;
            alertAnimations();
        }
    }

    public bool getAttacking()
    {
        return attacking;
    }
    
    public void setAttacking(bool set)
    {
        switchOffStates();
        attacking = set;
        alertAnimations();
    }

    public bool getDashing()
    {
        return dashing;
    }

    public void setDashing(bool set)
    {
        switchOffStates();
        invincible = set;
        dashing = set;
        alertAnimations();
    }

    public bool getBlocking()
    {
        return blocking;
    }

    public void setBlocking(bool set)
    {
        if(!blocking)
        {
            switchOffStates();
            blocking = set;
            alertAnimations();
        }
    }

    public bool getGrounded()
    {
        return grounded;
    }
    
    public void setGrounded(bool set)
    {
        grounded = set;
        alertAnimations();
    }

    public bool getInvincible()
    {
        return invincible;
    }
    public void setInvincible(bool set)
    {
        // Debug.Log(set);
        invincible = set;
    }

    public bool getHurt()
    {
        return hurt;
    }
    public void setHurt(bool set)
    {
        switchOffStates();
        hurt = set;
        alertAnimations();
    }

    //minor state. Important for when taking damage, but not for animations.
    public bool getParrying()
    {
        return parrying;
    }
    public void setParrying(bool set)
    {
        parrying = set;
    }

    public bool getSuccessfulParry()
    {
        return successfulParry;
    }
    public void setSuccessfulParry(bool set)  
    {
        switchOffStates();
        successfulParry = set;
        if(successfulParry)
        {   //parry was successful, so stop parrying
            parrying = false;
        }
        alertAnimations();
    }

    public bool getInDialogue()
    {
        return inDialogue;
    }

    public void enterDialogue()
    {
        if(canEnterDialogue)
        {
            switchOffStates();
            inDialogue = true;
            invincible = true;
            canMove = false;
        }

    }
    public void exitDialogue()
    {
        inDialogue = false;
        canMove = true;
        invincible = false;
        canEnterDialogue = false;
    }

    public bool getCanEnterDialogue()
    {
        return canEnterDialogue;
    }
    public void setCanEnterDialogue(bool set)
    {
        canEnterDialogue = set;
    }


    // used to set all states to false so a new one can be identified.
    private void switchOffStates()
    {
        neutral = false;
        //attacking = false;
        blocking = false;
        dashing = false;
        moving = false;   
    }

    // tell PlayerAnimations that a change of state has occured. 
    private void alertAnimations()
    {
        playerAnimations.changeOfState();
    }
}
