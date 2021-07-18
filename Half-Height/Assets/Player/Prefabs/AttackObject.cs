using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    // Start is called before the first frame update
    // private Animator thisAnimator;

    private float timer;
    public SpriteRenderer playerSpriteRenderer;
    private Transform attackPointRight;
    private Transform attackPointLeft;
    public Animator thisAnimator;       // set in unity
    public SpriteRenderer thisSpriteRenderer;
    public SFXManager sfx;
    private bool facingLeft;
    public int damage = 1;
    public LayerMask isHittable;
    void Start()
    {
        AnimationClip[] clips = thisAnimator.runtimeAnimatorController.animationClips;
        sfx = GameObject.FindObjectOfType<SFXManager>();
        foreach(AnimationClip clip in clips)
        {
            if(clip.name.Equals("attackAnim"))
            {
                timer = clip.length;
            }
        }

        /*
          Get whether the player is facing left or right and match it
        */
        facingLeft = transform.parent.GetComponent<SpriteRenderer>().flipX;

        if(facingLeft)
        {
            transform.position = attackPointLeft.position;
        }
        if(!facingLeft)
        {
            transform.position = attackPointRight.position;
        }
        thisSpriteRenderer.flipX = facingLeft;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(this.gameObject);
        }
        
       
    }

    public int getDamage()
    {
        return damage;
    }

    //  To be called on instantiation
    public void setAttackPoints(Transform left, Transform right)
    {
        attackPointLeft = left;
        attackPointRight = right;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(isHittable.Contains(collider.gameObject))
        {
            sfx.Play("attackHit");
        }
    }
}
