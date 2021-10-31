using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuTheme : MonoBehaviour {

	public AudioMixer AudioMx;
	public void Awake() {
		AudioMx.GetFloat("volume", out GlobalMixer.volume);
	}
	public AudioSource src;
	
	public  void OnEnable() {
		if (gameObject.active) {
		src.volume = 0;
		StartCoroutine(MusicFade(src, 10,  GlobalMixer.volume / 4 ));
		src.Play();
		}
	}

	public void OnDisable() {
		src.Stop();
		src.volume = 0;

	}
	public static IEnumerator MusicFade(AudioSource audioSource, float duration, float targetVolume) {
		float currentTime = 0;
		float start = audioSource.volume;

		while (currentTime < duration) {
			currentTime += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
			yield return null;                                               
		}
		yield break;
	}

}
