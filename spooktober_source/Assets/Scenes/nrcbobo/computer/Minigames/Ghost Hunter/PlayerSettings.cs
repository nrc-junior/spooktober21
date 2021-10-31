using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour {
    bool inCombat; // Cannot pause while on.
    
    [HideInInspector] public bool freezed;
    RawImage line_color; 
    
    [Range(0,10)] public float velocity = 2;
    public float cameraSpeed = 2 ;
    public GameObject tips;
    public Button preset;
    Transform cam_obj;
    GameObject main;
    GameObject wireframe;
    
    bool paused = true;
    bool wasd_move = true;

    Rigidbody rb;
    CharacterController cc;
    float gravity = 0;
    Vector2 rot;


    void Awake() {
        mixer.GetFloat("volume", out GlobalMixer.volume );
    }
    
    void Start() {
        cam_obj = Camera.main.transform.parent;
        wireframe = cam_obj.GetChild(1).gameObject;
        main = Camera.main.gameObject;
        main.SetActive(false);
        rb = gameObject.GetComponent<Rigidbody>();
        cc = gameObject.GetComponent<CharacterController>();
        wireframe.SetActive(true);
        line_color = tips.GetComponent<RawImage>();
      
        audios = GetComponent<AudioCollection>();
        idle_source = GetComponent<AudioSource>();
        IdleAudio();
    }
    
    void Update() {
        cc.Move(Vector3.down);
        if (Input.GetKeyDown(KeyCode.Escape) && !inCombat) {
            paused = !paused;
            wireframe.SetActive(paused);
            if (paused) {
                Cursor.lockState = CursorLockMode.None;
                
            }else {
                
            }
            Cursor.visible = paused;
            tips.SetActive(paused);
            main.SetActive(!paused);
            preset.Select();
        }
        if (paused || freezed) return;
        if (!wasd_move) { return;}

        gravity = cc.isGrounded ? 0 : -3f;
        rot.x -= Input.GetAxis("Mouse X") * cameraSpeed;
        rot.y += Input.GetAxis("Mouse Y") * cameraSpeed;
        rot.y = Mathf.Clamp(rot.y, -60, 89);
        cam_obj.eulerAngles = new Vector3(-rot.y,-rot.x, 0);
        
        Vector3 forward =  cam_obj.forward;
        Vector3 right =  cam_obj.right;
        forward.y = 0;
        forward.y = 0;
        forward.Normalize();
        right.Normalize();
        Vector3 axis = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        Vector3 dir = forward * axis.y + right * axis.x;
        dir.y = gravity;
        cc.Move(dir * velocity * Time.deltaTime);
    }

    public void setMovementMouse(bool set) {
        wasd_move = set;
        paused = false;
        tips.SetActive(false);
        main.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        wireframe.SetActive(false);
    }

    public void Look(Vector3 pos) {
        cam_obj.LookAt(pos);
    }

    public void setCombatCamera(bool on) {
            inCombat = on;
        if (on) {
            wireframe.transform.parent = null;
            wireframe.transform.position = new Vector3(1,6,31);
            wireframe.transform.rotation = new Quaternion(0.00569213834f,-0.967265368f,0.252854884f,-0.0207314882f);
            main.SetActive(false);
            audio_src = wireframe.AddComponent<AudioSource>();
            tips.SetActive(true);
            for (int i = 0; i < tips.transform.childCount; i++) {
                tips.transform.GetChild(i).gameObject.SetActive(false);
            }
            wireframe.SetActive(true);
        }else {
            wireframe.transform.parent = cam_obj;
            wireframe.transform.localPosition = Vector3.zero;
            wireframe.transform.localRotation = main.transform.localRotation;
            Destroy(audio_src);
            main.SetActive(true);
            wireframe.SetActive(false);
            for (int i = 0; i < tips.transform.childCount; i++) {
                tips.transform.GetChild(i).gameObject.SetActive(true);
            }
            tips.SetActive(false);
        }
    }


    public void setCombatColor(Color c) => line_color.color = c;

    #region audio

    public AudioMixer mixer;
    AudioSource audio_src;
    
    public void playSound(AudioClip ac) {
        playSound(ac, audio_src);
    }
    
    public void playSound(AudioClip ac, AudioSource sr) {
        sr.PlayOneShot(ac, GlobalMixer.volume);
    }

    AudioCollection audios;
    AudioSource idle_source;

    public void CombatAudio() {
        StartCoroutine(MusicFade(idle_source, 1.5f,  0));
        idle_source.Stop();
        idle_source.clip = audios.music_combat;
        idle_source.Play();
        StartCoroutine(MusicFade(idle_source, 1.5f,  GlobalMixer.volume));
    }
    
    public void IdleAudio() {
        idle_source.volume = 0;
        idle_source.clip = audios.music_idle;
        idle_source.Play();
        StartCoroutine(MusicFade(idle_source, 5,  GlobalMixer.volume  ));
    } 
        
    public IEnumerator MusicFade(AudioSource audioSource, float duration, float targetVolume) {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    #endregion
    
}
