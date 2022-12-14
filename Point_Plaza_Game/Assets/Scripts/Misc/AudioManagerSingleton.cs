using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioMixer))]
public class AudioManagerSingleton : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer = null;
    public AudioMixer Mixer => mixer;
    [SerializeField] private List<AudioSource> musicTracks = null;
    [SerializeField] private List<AudioSource> sfx = null;

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
        if (musicTracks.Contains(audio) || sfx.Contains(audio))
        {
            if (!audio.isPlaying)
            { audio.Play(); }
        }
    }

    public void PauseAudio(AudioSource audio)
    {
        if (musicTracks.Contains(audio) || sfx.Contains(audio))
        {
            if (audio.isPlaying)
            { audio.Stop(); }
        }
    }

    public void PauseAllTracks()
    {
        foreach(AudioSource track in musicTracks)
        {
            track.Pause();
        }
    }

    public void PauseAllSFX()
    {
        foreach(AudioSource effect in sfx)
        {
            effect.Pause();
        }
    }
    public void SetVol(string group, float val)
    {
        mixer.SetFloat(group, val);
    }
}
