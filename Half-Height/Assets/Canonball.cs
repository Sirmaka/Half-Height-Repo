using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canonball : MonoBehaviour
{
    public float speed;
    public LayerMask player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x - speed, transform.position.y);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(player.Contains(other.gameObject))
        {
            Destroy(this.gameObject);
        }
    }
}
