﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource opening;
    public AudioSource loop;
    private float openingTimer;
    private bool looping = false;
    // Start is called before the first frame update
    void Start()
    {
        openingTimer = opening.clip.length;

        //play the song
        opening.Play();
    }

    // Update is called once per frame
    void Update()
    {
        openingTimer -= Time.deltaTime;

        if(openingTimer <= 0 && !looping)
        {
            loop.Play();
            opening.Stop();
            looping = true;
        }
    }
}
