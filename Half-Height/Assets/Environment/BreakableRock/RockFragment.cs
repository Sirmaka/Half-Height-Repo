using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFragment : MonoBehaviour
{
    /*
        Launch upwards, spin, fall down.
    */
    private Rigidbody2D rb;
    private float rotation;
    private float force;
    private SpriteRenderer spriteRenderer;
    public float timeAlive;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        rotation = Random.Range(5f, 10f);
        force = Random.Range(5f, 15f);

        rb.AddForce(new Vector2(force, force));
        rb.AddTorque(rotation);

    }

    // Update is called once per frame
    void Update()
    {
        // spriteRenderer.color.a = 0.1f;
        timeAlive -= Time.deltaTime;
        Color color = spriteRenderer.color;
        color.a -= 0.01f;
        this.spriteRenderer.color = color;

        if(timeAlive <= 0) 
        {
            Destroy(this.gameObject);
        }
    }
}
