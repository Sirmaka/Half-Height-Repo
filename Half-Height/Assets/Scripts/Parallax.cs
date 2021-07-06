using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] bool localisedParallax; // if the parallax should only occur when the player is past a certain point, turn this on
    [SerializeField] Transform centrePoint;
    // [SerializeField] Transform rightLocal;
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private GameObject[] layers;
    [SerializeField] private float[] layerFractions;
    private GameObject player;
    private Rigidbody2D playerRigidbody;
    private float distanceX;
    private float distanceY;
    private float centrePointX;
    private float direction;
    private float playerPosInPoints;

    void Start()
    {
        if(layers.Length != layerFractions.Length)
        {
            Debug.LogError("Arrays were not the same length!");
            layerFractions = new float[layers.Length]; //avoid bugs
        }
        //check for no zeroes. If has been left as 0, assume not moving
        for(int i = 0; i < layerFractions.Length; i++)
        {
            layerFractions[i] = Mathf.Max(1f, layerFractions[i]);
        }
        player = GameObject.Find("/Player");
        if(player.GetComponent<Rigidbody2D>() != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody2D>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(localisedParallax)
        {
            distanceX = centrePoint.position.x - cameraPoint.transform.position.x; //distance between the player and the centrePoint
            distanceY = centrePoint.position.y - cameraPoint.transform.position.y;
    
            for(int curLayer = 0; curLayer < layers.Length; curLayer++)
            {
                //the position of the background is a fraction between the center of the area and the players position
                layers[curLayer].transform.position = new Vector2(cameraPoint.transform.position.x + (distanceX / layerFractions[curLayer]), cameraPoint.transform.position.y + (distanceY / layerFractions[curLayer]));
            }
                
        }
        //TODO: make this work
        if(!localisedParallax)
        {
            
            // int curLayer = 0;
            // foreach (GameObject layer in layers)
            // {
            //     layer.transform.localPosition = new Vector3(cameraPoint.position.x * -layerSpeeds[curLayer].x, cameraPoint.position.y * -layerSpeeds[curLayer].y, layer.transform.position.z);
            //     curLayer++;
            // }
        }
        
    }
}
