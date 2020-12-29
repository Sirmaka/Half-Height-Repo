using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPointRight;
    public Transform attackPointLeft;
    private PlayerController playerController;
    private SpriteRenderer thisSpriteRenderer;
    public GameObject attackPrefab;
    private bool attacking;
    public float attackAnimTime;
    public float attackCooldown;
    private bool onCooldown = false;   //  determines whether the attack is on cooldown
    private float attackCooldownReset;
    private float attackAnimTimeReset;
    private bool amAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
        attackAnimTimeReset = attackAnimTime;
        attackCooldownReset = attackCooldown;
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
            
            attackAnimTime = attackAnimTimeReset;
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

        //animation timer
        if(amAttacking)
        {
            attackAnimTime -= Time.deltaTime;
        }

        //if attack animation is over, then alert PlayerController
        if(attackAnimTime <= 0 && amAttacking)
        {
            amAttacking = false;
            playerController.setAttacking(false);
        }   
    }
}
