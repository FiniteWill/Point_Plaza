using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioMixer))]
public class AudioManagerSingleton : MonoBehaviour
{
    private AudioMixer mixer = null;
    private AudioSource[] musicTracks = null;

    public static AudioManagerSingleton Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance and it is not me, destroy it.
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayAudio(AudioSource audio)
    {
        if(!audio.isPlaying)
        { audio.Play(); }
    }

    public void PauseAudio(AudioSource audio)
    {
        if(audio.isPlaying)
        { audio.Stop(); }
    }

    public void SetVol(string group, float val)
    {
        mixer.SetFloat(group, val);
    }
}
