using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwipeProjectile : MonoBehaviour
{
    public float direction;
    public float speed;
    public float increment;
    private Rigidbody2D thisRigidbody;
    public LayerMask player;
    private SpriteRenderer thisSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = this.GetComponent<Rigidbody2D>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
        if(direction < 0)
        {
            thisSpriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(direction * speed, 0);
        thisRigidbody.AddForce(movement);
        speed += increment;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(player.Contains(other.gameObject))
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            if(!pc.getInvincible())
            {
                Destroy(this.gameObject);
            }
        }
    }
}
