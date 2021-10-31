using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusic : MonoBehaviour {
  public bool repeatable;
  bool played;
  public AudioClip audio_clip;
  [Range(0, 1)] public float volume = GlobalMixer.volume;
  
  AudioSource audio_source;

  void Start() {
    audio_source = Camera.main.GetComponent<AudioSource>();
  }
  
  void OnTriggerEnter() {
    if (!played) {
      audio_source.PlayOneShot(audio_clip, volume);
      played = !repeatable;
    }  
  }
}
