using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController pc;
    
    //grounded variables
    public Transform groundCheck;
    public float groundedRadius = 0.1f;
    public LayerMask whatIsGround;
    private float movementSmoothing = 0.05f;
    private Vector3 refVelocity = Vector3.zero;
    public float speed = 1f;
    public float jumpSpeed = 1f;
    private float horizontalMove = 0;
    private bool jump;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pc = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        // grounded calculations
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                pc.setGrounded(true);
            }
        }

        // movement
        Vector3 targetVelocity = new Vector2(horizontalMove * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, movementSmoothing);
        
        // jumping
        if(pc.getGrounded() && jump)
        {
            pc.setGrounded(false);
            rb.velocity += Vector2.up * jumpSpeed;
        }
        jump = false;
        
    }

}
