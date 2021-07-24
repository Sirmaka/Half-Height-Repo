using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperDespawner : MonoBehaviour
{
    public GameObject helper;
    public LayerMask isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(isPlayer.Contains(collider.gameObject))
        {
            GameObject.Destroy(helper.gameObject);
        }
    }
}
