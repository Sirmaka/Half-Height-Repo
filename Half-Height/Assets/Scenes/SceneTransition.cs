using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public LayerMask isPlayer;
    private int room1 = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(isPlayer.Contains(collider.gameObject))
        {
            SceneManager.LoadScene(room1);
        }
    }
}
