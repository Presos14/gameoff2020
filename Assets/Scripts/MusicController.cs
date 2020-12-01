using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public List<AudioClip> MusicClips;
    public AudioClip missionPassed;
    public AudioClip missionFailed;
    public static MusicController instance = null;
    private AudioSource audioSource;
    private int i= 0;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MusicClips[i];
        audioSource.Play();
        if(instance!=null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && WorldController.instance.getState() != WorldController.WorldState.LevelComplete)
        {
            i = Mathf.Clamp(-i+1,0,MusicClips.Count);
            audioSource.clip = MusicClips[i];
            audioSource.Play();
        }
    }

    public void PlayMissionPassed()
    {
        audioSource.clip = missionPassed;
        audioSource.Play();
    }

    public void PlayMissionFailed()
    {
        audioSource.clip = missionFailed;
        audioSource.Play();
    }

    public void StopAudio() {
        audioSource.Stop();
    }
}
