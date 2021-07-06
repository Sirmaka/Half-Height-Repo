using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public LayerMask isPlayer;
    public Scene moveSceneTo;
    public Animator transition;
    public float transitionTime = 1f;
    public Vector2 nextScenePosition;
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
            LoadNextLevel(collider.gameObject);
        }
    }

    public void LoadNextLevel(GameObject player) 
    {
        if(player.GetComponent<PlayerController>() != null)
        {
            StartCoroutine(LoadLevel(player));
        }
        else
        {
            Debug.LogError("Player does not have PlayerController!");
        }
    }

    IEnumerator LoadLevel(GameObject player)
    {
        //play animation
        transition.SetTrigger("Start");
        player.GetComponent<PlayerController>().setCanMove(false);
        //wait
        yield return new WaitForSeconds(transitionTime);
        //load scene
        SceneManager.LoadScene(moveSceneTo.handle);
        player.transform.position = new Vector3(nextScenePosition.x, nextScenePosition.y, 0);
        player.GetComponent<PlayerController>().setCanMove(true);
    }

}
