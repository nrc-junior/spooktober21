using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public struct Combat {
    public readonly bool has_enemy;
    public readonly int life;
    public readonly int armor;
    public readonly float speed;
    public readonly bool epic;
    public readonly bool superepic;
    public bool enter_ending;
    
    public GameObject skin;

    public Combat(GameObject enemy, int life, int armor, float speed, bool epic, bool super_epic_combat = false, bool enterEnding = false) {
        has_enemy = true;
        skin = enemy;
        superepic = super_epic_combat;
        this.life = life;
        this.armor = armor;
        this.speed = speed;
        this.epic = epic;
        enter_ending = enterEnding;
    }   
}

public enum attack_type {
    normal,
    critical,
    miss
}

public class PlayerCombatSystem : MonoBehaviour {
    public bool has_secret_key = false;
    
    [HideInInspector] public bool on_trigger;
    [HideInInspector] public Animator door;
    [HideInInspector] public int points;
    
    public AudioCollection audios;
    PlayerSettings configs;
    
    Combat combat_info = new Combat();
    bool on_animation;

    void Start() {
        configs = GetComponent<PlayerSettings>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)|| Input.GetKeyDown(KeyCode.Space)) {
            if (on_trigger && !on_animation) {
                on_animation = true;
                configs.freezed = true;
                StartCoroutine(Knocks());
            } else if (running_combat && !running_animation ) {
                StartCoroutine(ClickedOnTime(hit(value)));
            }
            
        }        
    }

    public void setCombat(Combat cb, bool enter_ending = false) {
        combat_info = cb;
        if (enter_ending) {
            combat_info.enter_ending = true;
        }
    } 
    
    IEnumerator Knocks() {
        configs.Look(door.transform.position);
        door.SetBool("knocks", true);
        if (combat_info.has_enemy && combat_info.epic) {
            configs.CombatAudio();
            if (combat_info.superepic) {
                configs.SuperCombatAudio();
            }
        }
        configs.playSound(audios.knocking,  door.GetComponent<AudioSource>());
        yield return new WaitForSeconds(0.1f);
        
        door.SetBool("knocks", false);
        yield return new WaitForSeconds(3f);
        if (combat_info.enter_ending) {
            StartCoroutine(FadeToBlack(true));
            
        }
        bool verify = combat_info.has_enemy ? enterCombat() : endCombat();
        if (verify) points++;
    }

    [SerializeField] GameObject enemy_object;
    [Range(0.01f, 5)] [SerializeField] float animation_time = 1;
    [SerializeField] Slider slider;
    [HideInInspector] public GameObject old_trigger;
    bool running_combat;
    bool running_animation;

    public Animator anim;
    float result;
    
    float value;
    float my_time;
    
    public Ghost_Enemy enemy;
    
    bool enterCombat() {
        StartCoroutine(FadeToBlack());
        StartCoroutine(StartClock());
        StartCoroutine(UpdateSlider());
        return true;
    }

    IEnumerator StartClock(){
        running_combat = true;
        yield return new WaitForSeconds(2);
        configs.setCombatCamera(true);
        
        enemy_object.SetActive(true);
        for (int i = 0; i < enemy.actor.transform.childCount; i++) {
            Destroy(enemy.actor.transform.GetChild(i).gameObject);
        }
        Instantiate(combat_info.skin, enemy.actor.transform);
        enemy = new Ghost_Enemy(combat_info.life, combat_info.armor, enemy.actor, enemy.chance_to_flee);
        configs.setCombatColor(enemy.getStatus());


        slider.onValueChanged.AddListener((v) =>{value = v;});
        StartCoroutine(BackToNormal());

        while(running_combat) {
            if(running_animation) {
                yield return new WaitForSeconds(1.2f);
                running_animation = false;
            }
            my_time += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    
    IEnumerator UpdateSlider() {
        while(running_combat) {
            slider.value = Mathf.PingPong(my_time*combat_info.speed,100);
            yield return new WaitForSeconds(0.0003f);
        }
    }

    static attack_type hit(float pos) {
        const int x1 = 42;
        const int x2 = 52;
        const int x3 = 79;
        const int x4 = 82;
        
        attack_type attack;
        if (pos >= x1 && pos <= x2) {
            attack = attack_type.normal;
        }else if (pos >= x3 && pos <= x4) {
            attack = attack_type.critical;
        }else {
            attack = attack_type.miss;
        }
        return attack;
    }
    
    IEnumerator ClickedOnTime(attack_type attack) {
        running_animation = true;
        bool killed = false;
        bool flee = false;
        int accuracy = 0;
        
        switch (attack){
            case attack_type.normal:
                killed = enemy.dealDamage(1);
                accuracy = 1;
            break;
            case attack_type.critical:  
                killed = enemy.dealDamage(2);
                accuracy = 1;
                break;
            case attack_type.miss:
                flee = enemy.heal();
                accuracy = -1;
                break;
        }
        
        configs.playSound(audios.attack);
        anim.SetInteger("accuracy",accuracy);
        yield return new WaitForSeconds(.2f);
        anim.SetInteger("accuracy",0);        
        yield return new WaitForSeconds(1);
        
        configs.setCombatColor(enemy.getStatus());

        if (flee) {
            running_combat = false;
            enemy_object.GetComponent<Animator>().Play("flee");
            configs.playSound(audios.dying);
            configs.setCombatColor(Color.green);
            StartCoroutine(FadeToBlack());
            yield return new WaitForSeconds(3);
            endCombat(); 
        }
        if (killed) {
            if (!has_secret_key) {
                has_secret_key = true;
            }
            
            running_combat = false;
            enemy_object.GetComponent<Animator>().Play("die");
            configs.playSound(audios.dying);
            configs.setCombatColor(Color.green);
            StartCoroutine(FadeToBlack());
            yield return new WaitForSeconds(3);
            endCombat();
        }
        
        running_animation = false;
    }
    
    
    bool endCombat() {
        on_trigger = false;
        Destroy(old_trigger.gameObject);

        enemy_object.SetActive(false);
        configs.setCombatCamera(false);
        if (combat_info.has_enemy) {
            StartCoroutine(BackToNormal());
        }
        
        on_animation = false;
        configs.freezed = false;
        return true;
    }


    public Image fade;
    IEnumerator FadeToBlack(bool ending = false) {
        Color Black = Color.black;
        Black.a = 0;

        float a = 1;
        while (a++ < 100) {
            Black.a = a / 100;
            fade.color = Black;
            yield return new WaitForSeconds(.01f);
        }

        if (ending) {
            SceneManager.LoadScene(1);
        }
    }    
    
    IEnumerator BackToNormal() {
        yield return new WaitForSeconds(1);
        Color Black = Color.black;
        Black.a = 1;

        float a = 1;
        while (a > 0) {
            Black.a = a -= 0.01f;
            fade.color = Black;
            yield return new WaitForSeconds(.01f);
        }
    }
}


