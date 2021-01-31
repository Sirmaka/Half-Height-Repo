using UnityEngine;
using UnityEngine.Audio;

/*
    This class is based on a script by Brackeys which can be found here: https://www.youtube.com/watch?v=6OT43pvUyfY
*/
[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    [Range(0f, 1f)]
    public float volume;
    
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;

    [HideInInspector]    
    public AudioSource source;
}
