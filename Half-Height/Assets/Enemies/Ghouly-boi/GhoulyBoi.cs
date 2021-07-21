using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulyBoi : MonoBehaviour
{
    //faces left by default
    public float speed;
    public float chargingSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector3 targetVelocity;
    private Vector3 refVelocity = Vector3.zero;
    private float movementSmoothing = 0.05f;
    public Transform leftCheck;
    public Transform rightCheck;
    public Transform leftGroundCheck;
    public Transform rightGroundCheck;
    public float checkRadius;
    public float awareDistance;
    public LayerMask whatIsGround;
    public LayerMask isPlayer;
    public LayerMask raycastCheck;

    private float direction = -1;
    private bool canSee;
    private bool keepCharging;
    public float chargeTime = 2;
    private float chargeTimeReset;
    public float waitTime = 2;
    private float waitTimeReset;

    private enum States
    {
        patrolling,
        charging,
        waiting,
    }
    private States state;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        state = States.patrolling;
        chargeTimeReset = chargeTime;
        waitTimeReset = waitTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if the ghouly boi sees the player, go to charge state.
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.right * direction, awareDistance, raycastCheck);
        int player = LayerMask.NameToLayer("Player");
        
        //to check if the enemy could see the player last frame, but not this frame
        bool couldSee = canSee;
        canSee = false;

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.layer == player )
            {
                state = States.charging;
                canSee = true;
            }        
        }

        //if the enemy could see the player last frame, but not this frame, keep charging for a bit.
        if(couldSee && !canSee)
        {
            keepCharging = true;
            chargeTime = chargeTimeReset;
        }

        if(keepCharging)
        {
            chargeTime -= Time.deltaTime;
        }

        if(chargeTime <= 0)
        {
            state = States.waiting;
            keepCharging = false;
            chargeTime = chargeTimeReset;
        }



        switch(state)
        {
            case States.patrolling:
                checkCollision();
                move(false);
                break;

            case States.charging:
                move(true);
                break;
            case States.waiting:
                bool doneWaiting = wait();
                if(doneWaiting)
                {
                    state = States.patrolling;
                }
                break;
        }
    }

    private void move(bool charging)
    {
        if(!charging)
        {
            targetVelocity = new Vector3(speed * direction, rb.velocity.y, 0); 
        }
        else
        {
            targetVelocity = new Vector3(chargingSpeed * direction, 0, 0); 

        }
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, movementSmoothing);

    }

    private void checkCollision()
    {
        //if about to walk off a ledge, turn around
        Collider2D[] collidersLeft = Physics2D.OverlapCircleAll(leftCheck.position, checkRadius, whatIsGround);
        if(collidersLeft.Length != 0)
        {
            direction = 1;
            sprite.flipX = true;
        }

        Collider2D[] collidersRight = Physics2D.OverlapCircleAll(rightCheck.position, checkRadius, whatIsGround);
        if(collidersRight.Length != 0)
        {
            direction = -1;
            sprite.flipX = false;
        }
        //if about to walk into a ledge, turn around
        Collider2D[] collidersLeftGround = Physics2D.OverlapCircleAll(leftGroundCheck.position, checkRadius, whatIsGround);
        if(collidersLeftGround.Length == 0)
        {
            direction = 1;
            sprite.flipX = true;
        }

        Collider2D[] collidersRightGround = Physics2D.OverlapCircleAll(rightGroundCheck.position, checkRadius, whatIsGround);
        if(collidersRightGround.Length == 0)
        {
            direction = -1;
            sprite.flipX = false;

        }
    }

    private bool wait()
    {
        waitTime -= Time.deltaTime;
        if(waitTime <= 0)
        {
            waitTime = waitTimeReset;
            return true;
        }

        return false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(isPlayer.Contains(col.gameObject))
        {
            direction *= -1;
            if(sprite.flipX == true)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
            state = States.patrolling;
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(rightCheck.position, checkRadius);
        Gizmos.DrawWireSphere(leftCheck.position, checkRadius);
        Gizmos.DrawWireSphere(rightGroundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(leftGroundCheck.position, checkRadius);
    }

}
