using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip lightswitch_on;
    public AudioClip lightswitch_off;
    public AudioClip lightswitch_all_off;
    public AudioClip footstep;
    public AudioSource soundController;
    
    // Start is called before the first frame update
    public void Start()
    {
        soundController = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, float pitch = 1,  float volume = 1f) {
        soundController.pitch = pitch;
        soundController.PlayOneShot(clip, volume);
    }

}