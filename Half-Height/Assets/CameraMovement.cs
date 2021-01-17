using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    private float zDistance = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("/Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Will follow the player around. This objects transform will be the camera's position
        Vector2 pos = new Vector2(player.position.x, player.position.y);
        this.transform.position = new Vector3(pos.x, pos.y, zDistance);
    }
}
