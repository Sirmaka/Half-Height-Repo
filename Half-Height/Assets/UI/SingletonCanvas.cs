using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public static SingletonCanvas instance;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
