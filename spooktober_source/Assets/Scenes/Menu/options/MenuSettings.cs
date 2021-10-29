using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour {

    public AudioMixer widget_am;
    public TMP_Dropdown widget_res;
    public Toggle widget_fullscreen;
    
    Resolution[] resolutions;    
    void Start() {
        resolutions = Screen.resolutions;
        widget_res.ClearOptions();

        int picked = 0;
        List<string> list_res = new List<string>();
        for (int i = 0; i < resolutions.Length; i++) {
            Resolution res = resolutions[i]; 
            list_res.Add($"{res.width} x {res.height}");
            if (res.width == Screen.width && res.height == Screen.height) {
                picked = i;
            }
        }
        widget_res.AddOptions(list_res);
        widget_res.value = picked;
        widget_res.RefreshShownValue();
    }

    public void SetFullscreen() => Screen.fullScreen = widget_fullscreen;
    
    public void SetVolume( float volume) => widget_am.SetFloat("volume", volume);
}
