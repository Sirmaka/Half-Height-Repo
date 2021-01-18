using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BossState
{
    void doState(Animator thisAnimator, Rigidbody2D thisRigidbody, SpriteRenderer thisSpriteRenderer, Vector2 playerPosition);
}
