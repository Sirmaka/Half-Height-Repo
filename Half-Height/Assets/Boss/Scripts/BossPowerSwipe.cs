using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPowerSwipe : BossState
{
    BossStateController controller;
    private Transform transform;
    private float travelDistance;
    private Vector2 refVelocity = Vector3.zero;
    private float facedDirection;
    private float movementSmoothing;
    private float chargeTimer;
    private float travelTimer;
    private float swingTimer;
    private float swingAttackTimer;
    private float endTimer;
    private bool createdProjectile = false;
    private enum AttackState {charge, travel, swing};     //could be achieved using State pattern, but this will do as no more states will be added.
    private AttackState state;
    private PowerSwipeProjectile PSProjectilePrefab;
    // Start is called before the first frame update
    public BossPowerSwipe(BossStateController stateController)
    {
        controller = stateController;
        state = AttackState.charge;
        chargeTimer = controller.PSChargeDuration;
        travelTimer = controller.PSTravelDuration;
        swingTimer = controller.PSSwingDuration;
        transform = controller.getTransform();
        travelDistance = controller.PSTravelVelocity;
        movementSmoothing = controller.PSMovementSmoothing;
        swingAttackTimer = controller.PSAttackDuration;
        PSProjectilePrefab = controller.PSProjectilePrefab;
    }
    
    public void doState()
    {
        if(controller.getSpriteRenderer().flipX)
        {
            facedDirection = -1;
        }
        else
        {
            facedDirection = 1;
        }
        Vector3 targetPoint = transform.position;
        switch(state)
        {
            case AttackState.charge:    // do animation, hold until ready
                controller.setAnimation("PowerSwipe");
                chargeTimer -= Time.deltaTime;
                if(chargeTimer <= 0)
                {
                    chargeTimer = controller.PSChargeDuration;
                    state = AttackState.travel;
                }
                break;

            case AttackState.travel:    //do animation, slide forwards
                controller.setAnimationTransition(1);
                travelTimer -= Time.deltaTime;
                targetPoint = transform.TransformPoint(new Vector2(travelDistance * facedDirection, 0));
                if(travelTimer <= 0)
                {
                    travelTimer = controller.PSTravelDuration;
                    state = AttackState.swing;
                }
                break;

            case AttackState.swing:
                if(!createdProjectile)
                {
                    controller.createPSProjectile();
                    createdProjectile = true;
                }
                controller.setAnimationTransition(2);
                swingAttackTimer -= Time.deltaTime; //  length of the anim
                swingTimer -= Time.deltaTime;
                if(swingAttackTimer >= 0)
                {
                    controller.getPSHitbox(facedDirection).enabled = true;
                }
                else
                {
                    controller.getPSHitbox(facedDirection).enabled = false;
                }
                if(swingTimer <= 0)
                {
                    swingTimer = controller.PSSwingDuration;
                    swingAttackTimer = controller.PSAttackDuration;
                    state = AttackState.charge; //reset for next attack
                    createdProjectile = false;
                    controller.EndOfState();
                }
                break;
            default:
                break;
        }

        //make this happen every frame, just sometimes target Velocity will not = 0
        transform.position = Vector2.SmoothDamp(transform.position, targetPoint, ref refVelocity, movementSmoothing);

    }

}
