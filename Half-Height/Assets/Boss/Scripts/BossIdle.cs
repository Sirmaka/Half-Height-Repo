using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : MonoBehaviour
{
    /*
        Things the Boss can do in Idle:
            - Turn to face the player.
    */
    private float directionFacing;

    //this method will run every frame the boss is in this state. Called in BossStateController
    public void isIdle(Vector2 playerPosition, Rigidbody2D thisRigidbody, SpriteRenderer thisSpriteRenderer)
    {
        //get the direction the boss is facing
        if(thisSpriteRenderer.flipX)
        {
            directionFacing = -1;
        }        
        else
        {
            directionFacing = 1;
        }
        
        
        //turn to face player
        if(playerPosition.x < 0 && directionFacing > 0)
        {
            thisSpriteRenderer.flipX = true;
        }
        if(playerPosition.x > 0 && directionFacing < 0)
        {
            thisSpriteRenderer.flipX = false;
        }
    }
}
