using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject canonballPrefab;
    public float fireRate;
    private float fireTimer;
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
            GameObject canonball = Instantiate(canonballPrefab);
            canonball.transform.parent = transform;
            canonball.transform.position = transform.position;
            fireTimer = fireRate;
        }
    }
}
