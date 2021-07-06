using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    private float zDistance = 0;
    public Camera cam;
    public float leftBounds;
    public float rightBounds;
    public float topBounds;
    public float botBounds;
    // Start is called before the first frame update
    void Start()
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        player = GameObject.Find("/Player").GetComponent<Transform>();
        //Get the bounds in more appropriate positions.
        leftBounds = leftBounds + width/2;
        rightBounds = rightBounds - width/2;
        topBounds = topBounds - height/2;
        botBounds = botBounds + height/2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = new Vector2();
        //Will follow the player around. This objects transform will be the camera's position
        if(player.position.x > leftBounds && player.position.x < rightBounds
            && player.position.y > botBounds && player.position.y < topBounds) 
        {
            pos = new Vector2(player.position.x, player.position.y);
        }
        else 
        {
            float posX = player.position.x;
            float posY = player.position.y;

            if(player.position.x < leftBounds)
            {
                posX = leftBounds;
            }
            if(player.position.x > rightBounds)
            {
                posX = rightBounds;
            }
            if(player.position.y < botBounds)
            {
                posY = botBounds;
            }
            if(player.position.y > topBounds)
            {
                posY = topBounds;
            }

            pos = new Vector2(posX, posY);
        }

        this.transform.position = new Vector3(pos.x, pos.y, zDistance);
    }
}
