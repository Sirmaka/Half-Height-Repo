using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateController: MonoBehaviour
{
    private Transform player;
    private Rigidbody2D thisRigidbody;
    private SpriteRenderer thisSpriteRenderer;
    private Animator thisAnimator;
    private BossState idle;

    // private BossState powerSwipe;

    void Start()
    {
        player = GameObject.Find("/Player").GetComponent<Transform>();
        idle = this.GetComponent<BossIdle>();
        thisRigidbody = this.GetComponent<Rigidbody2D>();
        thisAnimator = this.GetComponent<Animator>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //get the players position
        Vector2 playerPosition = transform.InverseTransformPoint(player.position);
        //TODO: Make the states swap
        idle.doState(thisAnimator, thisRigidbody, thisSpriteRenderer, playerPosition);
    }



}
