using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public LayerMask isAttack;
    private Rigidbody2D thisRigidbody;
    public int hp;
    private GameObject[] attackObjects;
    private int pos = 0;                        //used for navigating attackObjects[]
    
    //Knockback variables
    private bool knockedBack;
    public float knockBackDuration = 0.2f;
    private float knockbackTimer;
    private float attackDirection;              // where the attack is coming from
    private Vector3 refVelocity = Vector2.zero;
    private float movementSmoothing = 0;
    public Vector2 knockBackForce;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("/Player");
        thisRigidbody = this.GetComponent<Rigidbody2D>();
        attackObjects = new GameObject[3];        //set to arbitrary size. Will only be a problem if the player can attack so quickly that 3+ attack objects appear at once.
        knockbackTimer = knockBackDuration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetVelocity = Vector2.zero;

        if(knockedBack)
        {
            targetVelocity = new Vector2(-attackDirection * knockBackForce.x *Time.deltaTime, knockBackForce.y * Time.deltaTime);

            knockbackTimer -= Time.deltaTime;
        }
        if(knockbackTimer <= 0)
        {
            knockedBack = false;
            knockbackTimer = knockBackDuration;
        }
        thisRigidbody.velocity = Vector3.SmoothDamp(thisRigidbody.velocity, targetVelocity, ref refVelocity, movementSmoothing);   
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(isAttack.Contains(col.gameObject))
        {
            bool canTakeDamage = true;
            foreach(GameObject attack in attackObjects)
            {
                if(col.gameObject.Equals(attack))
                {  
                    canTakeDamage = false;
                }
            }
            if(canTakeDamage)
            {
                Vector2 attackPosition = transform.InverseTransformPoint(player.transform.position);
                attackDirection = Mathf.Sign(attackPosition.x);
                takeDamage(col.gameObject.GetComponent<AttackObject>().getDamage());
                attackObjects[pos] = col.gameObject;
                //add this object to the array
                if(pos==2)
                {
                    pos = 0;
                }

                knockedBack = true;
            }
        }
    }

    private void takeDamage(int damage)
    {
        Debug.Log("Ouch!");
        hp -= damage;
        if(hp <= 0)
        {
            Debug.Log("Ouch! I'm dead.");
        }
    }
}
