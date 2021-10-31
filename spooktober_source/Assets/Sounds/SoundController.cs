using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour {
    public AudioMixer am;
    AudioSource a;
    
    void Awake() {
        soundController = GetComponent<AudioSource>();
        am.GetFloat("volume", out GlobalMixer.volume);
        //a = transform.GetChild(0).GetComponent<AudioSource>();
    }
    
    public void PlaySound(AudioClip clip, float pitch = 1,int channel = 0,   float volume = 1) {

        print("tocando"+ clip.name);
        volume = GlobalMixer.volume;
        soundController.pitch = pitch;
    }
}
