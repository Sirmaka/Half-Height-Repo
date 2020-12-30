using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    private SpriteRenderer thisSpriteRenderer;
    private Rigidbody2D thisRigidbody;
    public int maxHP;
    private int hp;
    public Vector2 knockBackForce;
    // public LayerMask whatIsAttack;
    // private bool invincible;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
        thisRigidbody = this.GetComponent<Rigidbody2D>();
        hp = maxHP;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        //if collision happens with an attack AND the player is not invincible
        if(col.gameObject.layer == LayerMask.NameToLayer("Attack")
            && !playerController.getInvincible())
        {
            Debug.Log("got here");
            Vector2 attackPosition = transform.InverseTransformPoint(col.transform.position);
            float attackDirection = Mathf.Sign(attackPosition.x);
            //determine if player is facing right or left
            bool facingRight = true;
            if(thisSpriteRenderer.flipX)
            {
                facingRight = false;
            }
            //if player is not blocking or is and the attack came from a different direction to where the player is facing, do damage
            if(!playerController.getBlocking() || (playerController.getBlocking() && facingRight && attackDirection < 0)
                || (playerController.getBlocking() && !facingRight && attackDirection > 0))
            {
                hp -= 1;
            }
            //knockback
            thisRigidbody.AddForce(knockBackForce, ForceMode2D.Impulse);
        }
    }
}
