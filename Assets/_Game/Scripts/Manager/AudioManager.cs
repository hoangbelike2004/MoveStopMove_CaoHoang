using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public GameObject Prefab;//prefab audioSource

    public AudioClip acClick;
    private AudioSource asClick;
    public AudioClip achit;
    private AudioSource ashit;
    public AudioClip acLose;
    private AudioSource asLose;
    public AudioClip acWin;
    private AudioSource asWin;
    public void PlaySound(AudioClip clip,float volum)
    {
        if(clip == this.acClick)
        {
            Play(clip,ref asClick,volum);
        }
        if (clip == this.achit)
        {
            Play(clip, ref ashit, volum);
        }
        if (clip == this.acLose)
        {
            Play(clip, ref asLose, volum);
        }
        if (clip == this.acWin)
        {
            Play(clip, ref asWin, volum);
        }
    }

    private void Play(AudioClip clip,ref AudioSource audioSource,float volum,bool isLoopBack = false)
    {
        if(audioSource != null && audioSource.isPlaying)
        {
            return;
        }
        audioSource = Instantiate(this.Prefab).GetComponent<AudioSource>();
        audioSource.loop = isLoopBack;
        audioSource.volume = volum;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
    
    public void StopSound(AudioClip clip)
    {
        if (clip == this.acClick)
        {
            asClick?.Stop();
            return;
        }
        if (clip == this.achit)
        {
            ashit?.Stop();
            return;
        }
        if (clip == this.acLose)
        {
            asLose?.Stop();
            return;
        }
        if (clip == this.acWin)
        {
            asWin?.Stop();
            return;
        }
    }
}
