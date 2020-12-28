using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    // Start is called before the first frame update
    // private Animator thisAnimator;

    public float timer;

    void Start()
    {
        //thisAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)//this.thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("attackAnim"))
        {
            Destroy(this.gameObject);
        }
    }
}
