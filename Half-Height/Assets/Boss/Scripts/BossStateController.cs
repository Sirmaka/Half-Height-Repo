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
    public PowerSwipeProjectile PSProjectilePrefab;
    private PowerSwipeProjectile PSProjectile;
    public float PSChargeDuration;
    public float PSSwingDuration;
    public float PSTravelDuration;
    public float PSTravelVelocity;
    public float PSMovementSmoothing;
    public float PSAttackDuration;
    public BoxCollider2D PSHitboxLeft;
    public BoxCollider2D PSHitboxRight;
    public Transform PSAttackPointLeft;
    public Transform PSAttackPointRight;
    private BossState currentState;
    private BossState idle;
    private BossState powerSwipe;
    private float timer = 1f;

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
            timer = 1f;
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

    /*
        Set Animation:
            Used for setting which main animation we are up to.
    */
    public void setAnimation(string animation)  //setting animations using strings is gross. 
    {
        //turn off all animations
        thisAnimator.SetBool("Idle", false);
        thisAnimator.SetBool("PowerSwipe", false);
        thisAnimator.SetInteger("AttackState", 0);  //  reset AttackState for the next animation
        
        //set the one we want to true
        thisAnimator.SetBool(animation, true);
    }
    /*
        Set Animation Transition:
            Throughout a main animation there are multiple steps. This controls which step we are up to.
    */
    public void setAnimationTransition(int state)
    {
        thisAnimator.SetInteger("AttackState", state);
    }  

    public Transform getTransform()
    {
        return this.transform;
    }

    public BoxCollider2D getPSHitbox(float direction)
    {
        if(direction < 0)
        {
            return PSHitboxLeft;
        }
        if(direction > 0)
        {
            return PSHitboxRight;
        }
        return null;
    }

    public void createPSProjectile()
    {
        PSProjectile = Instantiate(PSProjectilePrefab);
        PSProjectile.transform.SetParent(this.transform);
        Transform PSSpawn = PSAttackPointRight;
        float PSDirection = 1;
        if(thisSpriteRenderer.flipX)
        {
            PSSpawn = PSAttackPointLeft;
            PSDirection = -1;
        }
        PSProjectile.transform.position = PSSpawn.position;
        //Set direction
        PSProjectile.direction = PSDirection;
    }

    public void EndOfState()
    {
        //all states will call this when they are done.
        currentState = idle;
    }

}
