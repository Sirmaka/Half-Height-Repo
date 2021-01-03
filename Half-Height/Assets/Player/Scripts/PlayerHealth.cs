using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    private SpriteRenderer thisSpriteRenderer;
    private Rigidbody2D thisRigidbody;
    public LayerMask isAttack;
    public float knockBackTimer;        //set value
    private float knockBackDuration;    // countdown
    private bool knockedBack = false;
    public int maxHP;
    private int hp;
    private float attackDirection;
    private Vector3 refVelocity = Vector3.zero;
    private float movementSmoothing = 0f;
    public Vector2 knockBackForce;
    public float knockBackSpeedX;
    public float knockBackSpeedY;
    // public LayerMask whatIsAttack;
    // private bool invincible;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
        thisRigidbody = this.GetComponent<Rigidbody2D>();
        hp = maxHP;   
        knockBackDuration = knockBackTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //code for when the player is being knocked back
        if(knockedBack)
        {
            playerController.setInvincible(true);   // cannot take damage in knockback
            knockBackDuration -= Time.deltaTime;
            playerController.setCanMove(false);
            /*
                KnockBack movement
            */
            Vector3 targetVelocity = new Vector2(-attackDirection * knockBackForce.x * knockBackSpeedX* Time.deltaTime, knockBackForce.y * knockBackSpeedY * Time.deltaTime);
            thisRigidbody.velocity = Vector3.SmoothDamp(thisRigidbody.velocity, targetVelocity, ref refVelocity, movementSmoothing);
        }
        if(knockBackDuration <= 0 && knockedBack)
        {
            knockedBack = false;
            knockBackDuration = knockBackTimer;
            if(playerController.getHurt())
            {
                playerController.setHurt(false);
            }
            playerController.setCanMove(true);
            playerController.setInvincible(false); 
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        
        //if collision happens with an attack AND the player is not invincible
        if(isAttack.Contains(col.gameObject)
            && !playerController.getInvincible())
        {
            Vector2 attackPosition = transform.InverseTransformPoint(col.transform.position);
            attackDirection = Mathf.Sign(attackPosition.x);
            //determine if player is facing right or left
            bool facingRight = true;
            if(thisSpriteRenderer.flipX)
            {
                facingRight = false;
            }
            //if player is not blocking or is blocking and the attack came from a different direction to where the player is facing, do damage
            if(!playerController.getBlocking() || (playerController.getBlocking() && facingRight && attackDirection < 0)
                || (playerController.getBlocking() && !facingRight && attackDirection > 0))
            {
                takeDamage();
                playerController.setHurt(true);
            }
            //knockback
            // thisRigidbody.AddForce(new Vector2(knockBackForce.x*-attackDirection, knockBackForce.y), ForceMode2D.Impulse);
            knockedBack = true;

        }
    }

    private void takeDamage()
    {
        hp -= 1;
        if(hp <= 0)
        {
            //die
            Debug.Log("Player is dead!");
        }
    }
}
