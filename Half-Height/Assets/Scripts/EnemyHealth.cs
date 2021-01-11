using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public LayerMask isAttack;
    public int hp;
    private GameObject[] attackObjects;
    private int pos = 0;                        //used for navigating attackObjects[]
    // Start is called before the first frame update
    void Start()
    {
        attackObjects = new GameObject[3];        //set to arbitrary size. Will only be a problem if the player can attack so quickly that 3+ attack objects appear at once.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(isAttack.Contains(col.gameObject))
        {
            bool canTakeDamage = true;
            foreach(GameObject attack in attackObjects)
            {
                if(col.gameObject.Equals(attack))
                {  
                    canTakeDamage = false;
                }
            }
            if(canTakeDamage)
            {
                takeDamage(col.gameObject.GetComponent<AttackObject>().getDamage());
                attackObjects[pos] = col.gameObject;
                //add this object to the array
                if(pos==2)
                {
                    pos = 0;
                }

            }
        }
    }

    private void takeDamage(int damage)
    {
        Debug.Log("Ouch!");
        hp -= damage;
        if(hp <= 0)
        {
            Debug.Log("Ouch! I'm dead.");
        }
    }
}
