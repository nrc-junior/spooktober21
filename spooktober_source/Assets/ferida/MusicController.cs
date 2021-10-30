using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource _audioSource;



    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayLoop());
    }
    IEnumerator PlayLoop()
    {
        yield return new WaitForSeconds(11.3f);
        _audioSource.Play();
    }

    IEnumerator FadeOut()
    {
        while (_audioSource.volume > 0) {
            _audioSource.volume -= 0.3f * Time.deltaTime / 0.5f;
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void Stop()
    {
        StartCoroutine(FadeOut());
    }
}


