using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController playerController;
    private Rigidbody2D thisRigidbody;
    private SpriteRenderer thisSpriteRenderer;
    public float parryDuration;
    private bool isBlocking;
    private bool stopBlocking;
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
        thisRigidbody = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Block"))
        {
            isBlocking = true;
        }
        if(Input.GetButtonUp("Block"))
        {
            stopBlocking = true;
        }
    }

    void FixedUpdate()
    {
        if(isBlocking && !playerController.getAttacking()
            && !playerController.getDashing() && playerController.getGrounded())
        {
            // tell PlayerController that we're blocking and we can't move
            playerController.setBlocking(true);
            playerController.setCanMove(false);
            thisRigidbody.velocity = new Vector2(0,0);  //  Stop the player from sliding.
        }

        //  This is what I got up to. Write the code to stop blocking
        if(stopBlocking)
        {
            isBlocking = false;
            playerController.setBlocking(false);
            playerController.setCanMove(true);
            stopBlocking = false;
        }
    }

}
