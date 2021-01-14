using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateController : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D thisRigidbody;
    private SpriteRenderer thisSpriteRenderer;
    private BossIdle idle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("/Player").GetComponent<Transform>();
        idle = this.GetComponent<BossIdle>();
        thisRigidbody = this.GetComponent<Rigidbody2D>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the players position
        Vector2 playerPosition = transform.InverseTransformPoint(player.position);
        idle.isIdle(playerPosition, thisRigidbody, thisSpriteRenderer);
    }



}
