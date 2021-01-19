using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossState
{
    /*
        Things the Boss can do in Idle:
            - Turn to face the player.
    */
    BossStateController controller;
    private float directionFacing;

    public BossIdle(BossStateController stateController)
    {
        controller = stateController;
    }
    //this method will run every frame the boss is in this state. Called in BossStateController
    public void doState()
    {
        controller.setAnimation("Idle");

        
        //get the players position
        Vector2 playerPosition = controller.getTransform().InverseTransformPoint(controller.getPlayerPosition());

        //get the direction the boss is facing
        if(controller.getSpriteRenderer().flipX)
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
            controller.getSpriteRenderer().flipX = true;
        }
        if(playerPosition.x > 0 && directionFacing < 0)
        {
            controller.getSpriteRenderer().flipX = false;
        }
    }
}
