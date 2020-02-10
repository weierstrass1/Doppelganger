using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introLoopMusic : MonoBehaviour
{
    public AudioClip Intro, Loop;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        if (Intro != null)
        {
            source.clip = Intro;
            source.loop = false;
        }
        else if (Loop != null)
        {
            source.clip = Loop;
            source.loop = true;
        }
        if (source.clip != null) source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Loop != null && !source.isPlaying && source.clip != Loop)
        {
            source.clip = Loop;
            source.loop = true;
            source.Play();
        }
    }
}
