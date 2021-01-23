using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGElemAnim : MonoBehaviour
{
    // Background elements are always 3 frames long 
    public Sprite frame1;
        public Sprite frame2;
            public Sprite frame3;

    public Sprite[] spriteArr;

    float count = 0;
    int arrayCount = 0;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        spriteArr = new Sprite[]{frame1, frame2, frame3};
        sr.sprite = spriteArr[0];
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime; 

        if (count >= 0.5) 
        {
            if (arrayCount > 2)
                arrayCount = 0;

            sr.sprite = spriteArr[arrayCount];
            arrayCount++;
            count = 0;
        }
            
    }
}
