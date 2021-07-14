﻿using System.Collections;
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
    public AttackObject attackPrefab;
    private bool attacking;
    private float attackAnimTime;
    private float airAttackAnimTime;
    private float animationDuration;
    // private float attackAnimTimeReset;
    public float attackCooldown;
    private bool onCooldown = false;   //  determines whether the attack is on cooldown
    private float attackCooldownReset;    

    private int number = 0;
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
            && !playerController.getDashing() && !onCooldown && !playerController.getInDialogue())
        {
            // tell playerController that we're attacking
            playerController.setAttacking(true);

            //create attack prefab
            AttackObject attack = Instantiate(attackPrefab);
            attack.name = "attack " + number;
            number++; //this makes the name different each time, useful for making sure that hits don't get counted multiple times in other objects.
            attack.setAttackPoints(attackPointLeft, attackPointRight);
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
            
            // amAttacking = true;
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

    }
}
