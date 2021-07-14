using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    Each stage of rock has the next stage. If this is the last stage, then destroy self.
*/

public class BreakableRock : MonoBehaviour
{
    public BreakableRock nextPrefab;
    public RockFragment rockFragmentPrefab;
    public LayerMask isAttack;
    private bool hit = false;
    private string ignore;

    // Update is called once per frame
    void Update()
    {
        //GameObject nextSprite = Instantiate(nextPrefab);
        if(hit)
        {
            if(nextPrefab != null)
            {
                BreakableRock next = Instantiate(nextPrefab, this.transform.position, Quaternion.identity);
                next.ignore = this.ignore;
                Destroy(this.gameObject);
            }
            else 
            {
                RockFragment rock1 = Instantiate(rockFragmentPrefab, this.transform.position + new Vector3(0.2f, -0.387f, 0), Quaternion.identity);
                RockFragment rock2 = Instantiate(rockFragmentPrefab, this.transform.position + new Vector3(-0.2f, 0.387f, 0), Quaternion.identity);
                RockFragment rock3 = Instantiate(rockFragmentPrefab, this.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
                Destroy(this.gameObject);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        if(isAttack.Contains(col.gameObject) && !col.gameObject.name.Equals(ignore)) 
        {
           hit = true;
           ignore = col.gameObject.name;
        }
    }
}
