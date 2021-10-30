using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    //controls
    public float Speed = 10f;
    public Rigidbody rb;
    Vector3 mov;
    [HideInInspector] public bool sit;
    
    
    //triggers
    [HideInInspector] public GameObject trigger; 
    [HideInInspector] public bool on_trigger; 
    
    //animation
    public GameObject on_chair;
    bool walking;
    bool interacting;
    Material player_skin;

    //informations
    PlayerData _data;
    void Start() {
        _data = transform.GetComponent<PlayerData>();
        player_skin = transform.GetChild(0).GetComponent<Renderer>().material;
    }
    
    float offset;
    float time;
    void Update(){
        if (!sit) {
            #region moviment

            Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dir != Vector2.zero)
            {
                if (!(new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y)) == Vector2.one))
                {
                    dir =
                        dir.x > 0 && dir.y == 0 ? new Vector2(dir.x, dir.x) : // vai pra direita? 
                        dir.x < 0 && dir.y == 0 ? new Vector2(dir.x, dir.x) : // vai pra esquerda?  
                        new Vector2(-dir.y, dir.y); // cima & baixo
                }
                else
                {
                    dir =
                        dir.x > 0 && dir.y < 0 ? new Vector2(dir.x * 1.5f, -dir.y / 4) : // direita  baixo?
                        dir.x > 0 && dir.y > 0 ? new Vector2(-dir.x / 4, dir.y * 1.5f) : // direita cima ?  
                        dir.x < 0 && dir.y < 0 ? new Vector2(-dir.x / 4, dir.y * 1.5f) : // esquerda  baixo?  
                        new Vector2(dir.x * 1.5f, -dir.y / 5); // esquerda cima?
                }

            }

            mov = new Vector3(dir.x, 0, dir.y);

            #endregion

            #region animation

            walking = dir != Vector2.zero;
            if (walking)
            {
                //walks
                if (Time.time > time)
                {
                    time = Time.time + .25f;
                    offset = offset >= 0.75f ? 0 : offset + 0.25f;
                    player_skin.SetTextureOffset("_MainTex", new Vector2(offset, 0.333f));
                }
            }
            else if (interacting)
            {
                //interact
                if (Time.time > time)
                {
                    time = Time.time + .25f;
                    offset = offset >= 0.75f ? 0 : offset + 0.25f;
                    player_skin.SetTextureOffset("_MainTex", new Vector2(offset, 0));
                }
            }
            else if (Time.time > time)
            {
                // idle
                time = Time.time + .25f;
                offset = offset >= 0.75f ? 0 : offset + 0.25f;
                player_skin.SetTextureOffset("_MainTex", new Vector2(offset, 0.666f));
            }

            #endregion
        }

        #region triggers
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) {
                if (on_trigger) {
                    StartCoroutine(takeSeat());
                }

                if (on_blackout_trigger && blackout_event) {
                    blackout_event = false;
                    _data.TurnLights(true);
                    mov = Vector3.zero;
                    GetComponent<PlayerData>().pc.ConnectInternet();
                    StartCoroutine(RemoveEquips());
                    
                }
            }

        #endregion
        
    }

    public void disconect() {
        StartCoroutine(leaveSit());
    }
    IEnumerator leaveSit() {
        yield return new WaitForSeconds(1);
        sit = false;
        on_chair.SetActive(false);
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
    }
    
    IEnumerator takeSeat() {
        sit = true;
        Color c = Color.white;
        c.a = 0;
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = c;
        on_chair.SetActive(true);
        yield return new WaitForSeconds(1);
        trigger.SetActive(true);
    }
    
    void FixedUpdate(){
        rb.MovePosition(rb.position + mov * Speed * Time.fixedDeltaTime);
    }

    #region sator_blackout
    public GameObject blackout_equipament;
    public void Blackout() {
        on_trigger = false;
        blackout_event = true;
        StartCoroutine(EquipCandle());
    }
    
    IEnumerator EquipCandle() {
        sit = true;
        yield return new WaitForSeconds(2);
        blackout_equipament.SetActive(true);
        sit = false;
    }

    IEnumerator RemoveEquips() {
        sit = true;
        yield return new WaitForSeconds(3);
        blackout_equipament.SetActive(false);
        sit = false;
    }
    
    public bool on_blackout_trigger;
    bool blackout_event;

    #endregion
}
