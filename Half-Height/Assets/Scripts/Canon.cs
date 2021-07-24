using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public Canonball canonballPrefab;
    public float fireRate;
    private float fireTimer;
    public bool fireRight;
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime;
        if(fireTimer <= 0)
        {
            Canonball canonball = Instantiate(canonballPrefab);
            canonball.transform.parent = transform;
            canonball.transform.position = transform.position;
            if(fireRight)
            {
                canonball.direction = 1;
            }
            else
            {
                canonball.direction = -1;
            }
            fireTimer = fireRate;
        }
    }
}
