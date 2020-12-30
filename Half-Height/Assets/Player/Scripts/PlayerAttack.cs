using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPointRight;
    public Transform attackPointLeft;
    private PlayerController playerController;
    private SpriteRenderer thisSpriteRenderer;
    private Animator thisAnimator;  //  required for animation length
    private AnimationClip clip; //  required for animation length
    public GameObject attackPrefab;
    private bool attacking;
    private float attackAnimTime;
    private float airAttackAnimTime;
    private float animationDuration;
    // private float attackAnimTimeReset;
    public float attackCooldown;
    private bool onCooldown = false;   //  determines whether the attack is on cooldown
    private float attackCooldownReset;
    
    private bool amAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
        
        attackCooldownReset = attackCooldown;
        
        // get the length of the attack animation, store it in attackAnimTime
        thisAnimator = this.GetComponent<Animator>();
        AnimationClip[] clips = thisAnimator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            if(clip.name.Equals("attackAnim"))
            {
                attackAnimTime = clip.length;
            }
            if(clip.name.Equals("airAttackAnim"))
            {
                airAttackAnimTime = clip.length;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Attack"))
        {
            attacking = true;
        }
    }

    void FixedUpdate()
    {
        if(attacking && !playerController.getBlocking()
            && !playerController.getDashing() && !onCooldown)
        {
            // tell playerController that we're attacking
            playerController.setAttacking(true);
            //create attack prefab
            GameObject attack = Instantiate(attackPrefab);
            attack.transform.parent = transform;
            
            //position
            attack.transform.position = attackPointRight.position;
            if(thisSpriteRenderer.flipX)
            {
                attack.transform.position = attackPointLeft.position;
                attack.GetComponent<SpriteRenderer>().flipX = true;
            }
            if(playerController.getGrounded())
            {
                animationDuration = attackAnimTime;
            }
            else
            {
                animationDuration = airAttackAnimTime;
            }
            
            amAttacking = true;
            onCooldown = true;
            
        }
        attacking = false;

        //  cooldowns
        if(onCooldown)
        {
            attackCooldown -= Time.deltaTime;
        }

        if(attackCooldown <= 0)
        {
            attackCooldown = attackCooldownReset;
            onCooldown = false;
        }

        /*
            Animation alerts:
                Attacking is a unique case. All action scripts must alert the PlayerAnimation script
                when a change of animation state is required, but as attacking takes priority over all
                other animations, the PlayerAttack script must alert PlayerAnimation when the attack begins
                and ends.
        */
        //animation timer
        if(amAttacking)
        {
            animationDuration -= Time.deltaTime;
        }
        //if attack animation is over, then alert PlayerController
        if(animationDuration <= 0 && amAttacking)
        {
            amAttacking = false;
            playerController.setAttacking(false);
        }   
    }
}
