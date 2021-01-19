using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateController: MonoBehaviour
{
    private Transform player;
    private Rigidbody2D thisRigidbody;
    private SpriteRenderer thisSpriteRenderer;
    private Animator thisAnimator;
    [Header("Power Swipe Stats")]
    public float PSChargeDuration;
    public float PSSwingDuration;
    public float PSTravelDuration;

    public float PSTravelVelocity;
    public float PSMovementSmoothing;
    private BossState currentState;
    private BossState idle;
    private BossState powerSwipe;
    private float timer = 3f;

    void Start()
    {
        player = GameObject.Find("/Player").GetComponent<Transform>();

        thisRigidbody = this.GetComponent<Rigidbody2D>();
        thisAnimator = this.GetComponent<Animator>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();

        idle = new BossIdle(this);
        powerSwipe = new BossPowerSwipe(this);

        currentState = idle; // idle is the default state
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 3f;
            currentState = powerSwipe;
        }
        currentState.doState();
    }

    //  helper methods for the state objects
    public Animator getAnimator()
    {  
        return thisAnimator;
    }

    public SpriteRenderer getSpriteRenderer()
    {
        return thisSpriteRenderer;
    }
    public Rigidbody2D getRigidbody()
    {
        return thisRigidbody;
    }

    public Vector2 getPlayerPosition()
    {
        return player.position;
    }

    public void setAnimation(string animation)  //setting animations using strings is gross. 
    {
        //turn off all animations
        thisAnimator.SetBool("Idle", false);
        thisAnimator.SetBool("PowerSwipe", false);
        thisAnimator.SetBool("PSTravel", false);
        thisAnimator.SetBool("PSSwing", false);
        
        //set the one we want to true
        thisAnimator.SetBool(animation, true);
    }

    public Transform getTransform()
    {
        return this.transform;
    }

    public void EndOfState()
    {
        //all states will call this when they are done.
        currentState = idle;
    }

}
