using System;
using UnityEngine;
using UnityEngine.Audio;
/*
    This class is based on a script by Brackeys which can be found here: https://www.youtube.com/watch?v=6OT43pvUyfY
*/
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    //we want this to be awake so that we can play sounds in Start()
    void Awake()
    {
        //Singleton
        if(instance == null)
        {
            instance = this;
        }
        else{
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private Sound GetAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound:  " + name + " not found!");
            return null;
        }
        return s;
    }
    public void Play(string name)
    {
        Sound s = GetAudio(name);
        s.source.Play();
    }
    // Update is called once per frame
    void Start()
    {
        //on first frame, play the opening
        Play("TuteOpening");
    }

    void Update()
    {
        //TODO Change this so that it is bassed on what room the player is in, based on the WorldState object.
        if(!GetAudio("TuteOpening").source.isPlaying && !GetAudio("TuteLoop").source.isPlaying)
        {
            Play("TuteLoop");
        }
    }
}
