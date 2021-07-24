using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    private SpriteRenderer thisSpriteRenderer;
    private Rigidbody2D thisRigidbody;
    public LayerMask isAttack;
    public float knockBackDuration;     //set value
    private float knockBackTimer;        // countdown
    private bool knockedBack = false;
    public int maxHP;
    private int hp;
    private float attackDirection;
    private Vector3 refVelocity = Vector3.zero;
    private float movementSmoothing = 0f;
    public Vector2 knockBackForce;
    public float knockBackSpeedX;
    public float knockBackSpeedY;
    public float parryInvincibleDuration;   //how long the player will be invincible after successfully parrying
    private float parryInvincibleTimer;
    private bool parryInvincible;
    // public LayerMask whatIsAttack;
    // private bool invincible;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
        thisRigidbody = this.GetComponent<Rigidbody2D>();
        hp = maxHP;   
        knockBackTimer = knockBackDuration;
        parryInvincibleTimer = parryInvincibleDuration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //code for when the player is being knocked back
        if(knockedBack)
        {
            playerController.setInvincible(true);   // cannot take damage in knockback
            knockBackTimer -= Time.deltaTime;
            playerController.setCanMove(false);
            /*
                KnockBack movement
            */
            Vector3 targetVelocity = new Vector2(-attackDirection * knockBackForce.x * knockBackSpeedX* Time.deltaTime, knockBackForce.y * knockBackSpeedY * Time.deltaTime);
            thisRigidbody.velocity = Vector3.SmoothDamp(thisRigidbody.velocity, targetVelocity, ref refVelocity, movementSmoothing);
        }
        if(knockBackTimer <= 0 && knockedBack)
        {
            knockedBack = false;
            knockBackTimer = knockBackDuration;
            if(playerController.getHurt())
            {
                playerController.setHurt(false);
            }
            playerController.setCanMove(true);
            playerController.setInvincible(false); 
        }

        //if a successful parry occurs, the following code will run
        if(parryInvincible)
        {
            parryInvincibleTimer -= Time.deltaTime;
            // thisSpriteRenderer.material.color = Color.gray;
        }
        if(parryInvincibleTimer <= 0 && parryInvincible)
        {
            parryInvincible = false;
            playerController.setInvincible(false);
            parryInvincibleTimer = parryInvincibleDuration;

        }
        if(!parryInvincible)
        {
            // thisSpriteRenderer.material.color = Color.white;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //if collision happens with an attack AND the player is not invincible
        if(isAttack.Contains(col.gameObject))
        { 
            Vector2 attackPosition = transform.InverseTransformPoint(col.transform.position);
            attackDirection = Mathf.Sign(attackPosition.x);
            //determine if player is facing right or left
            bool facingRight = true;
            if(thisSpriteRenderer.flipX)
            {
                facingRight = false;
            }
            
            //if the player is parrying and is facing the right way, do the parry stuff.
            if((playerController.getParrying() && facingRight && attackDirection > 0)
                    || (playerController.getParrying() && !facingRight && attackDirection < 0))
            {
                successfulParry();
                playerController.stopSound("block");
                playerController.playSound("parry");
            }
            //if we're invincible, don't do any of the following code
            if(!playerController.getInvincible())
            {
               
                //if player is not blocking or is blocking and the attack came from a different direction to where the player is facing, do damage
                if(!playerController.getBlocking() || (playerController.getBlocking() && facingRight && attackDirection < 0)
                    || (playerController.getBlocking() && !facingRight && attackDirection > 0))
                {
                    takeDamage();
                }
                else
                {
                    playerController.playSound("blocked");
                }
                //knockback
                knockedBack = true;

            }
          
        }
    }

    private void takeDamage()
    {
        hp -= 1;
        playerController.setHurt(true);
        if(hp <= 0)
        {
            //die
            Debug.Log("Player is dead!");
        }
    }

    private void successfulParry()
    {
        playerController.setSuccessfulParry(true);
        playerController.setInvincible(true);
        parryInvincible = true;
    }   
}
