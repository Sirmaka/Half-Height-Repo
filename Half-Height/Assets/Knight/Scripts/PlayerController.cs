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
        return canMove;
    }

    public void setNeutral()
    {
        switchOffStates();
        neutral = true;
    }

    public bool getAttacking()
    {
        return attacking;
    }
    
    public void setAttacking()
    {
        switchOffStates();
        attacking = true;
    }

    public bool getDashing()
    {
        return dashing;
    }
    

    public void setDashing()
    {
        switchOffStates();
        dashing = true;
    }

    public bool getBlocking()
    {
        return blocking;
    }

    public void setBlocking()
    {
        switchOffStates();
        blocking = true;
    }

    public bool getGrounded()
    {
        return grounded;
    }
    
    public void setGrounded(bool set)
    {
        grounded = set;
    }

    // used to set all states to false so a new one can be identified.
    private void switchOffStates()
    {
        neutral = false;
        attacking = false;
        blocking = false;
        dashing = false;
        moving = false;   
    }
}
