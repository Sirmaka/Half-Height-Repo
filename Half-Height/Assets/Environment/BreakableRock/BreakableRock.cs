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
    private SFXManager sfx;
    public LayerMask isAttack;
    private bool hit = false;
    private string ignore;

    void Start() 
    {
        sfx = GameObject.FindObjectOfType<SFXManager>();
    }
    // Update is called once per frame
    void Update()
    {
        //GameObject nextSprite = Instantiate(nextPrefab);
        if(hit)
        {
            //if there is another phase to go
            if(nextPrefab != null)
            {
                BreakableRock next = Instantiate(nextPrefab, this.transform.position, Quaternion.identity);
                next.ignore = this.ignore;
                sfx.Play("rockBreak");
                Destroy(this.gameObject);
            }
            else //if this is the last phase
            {
                RockFragment rock1 = Instantiate(rockFragmentPrefab, this.transform.position + new Vector3(0.2f, -0.387f, 0), Quaternion.identity);
                RockFragment rock2 = Instantiate(rockFragmentPrefab, this.transform.position + new Vector3(-0.2f, 0.387f, 0), Quaternion.identity);
                RockFragment rock3 = Instantiate(rockFragmentPrefab, this.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
                sfx.Play("rockDestroy");
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
