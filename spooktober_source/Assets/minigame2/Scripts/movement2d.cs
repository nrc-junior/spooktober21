using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement2d : MonoBehaviour
{
    public DialogueUI dialogueUI;
    public DialogueObject dialogueObject;

    public float Speed ;

    [SerializeField] private SoundController soundController;
    public AudioClip boom;

    Rigidbody rb;
    Vector3 mov;
    Animator anime;
    
    //usar o rigidbody 3d para o vector3
    void Start() {
        rb = GetComponent<Rigidbody>();
        anime = GetComponent<Animator>();
    }

    bool running;
    void Update(){
        if (playing) {
            Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	    
            if(transform.position.x + dir.x > 16.88f || transform.position.x + dir.x < -6.25  ){
                dir.x = 0;
            }

            mov = new Vector3(dir.x,0,0);

            transform.localRotation =  dir.x < 0 ? transform.localRotation = Quaternion.Euler(new Vector3(0,180,0)) : Quaternion.Euler(new Vector3(0,0,0));
            anime.SetFloat ("speed", Mathf.Abs(dir.x));
        }
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + mov * Speed * Time.fixedDeltaTime);
    }

    public void Explode() {
        GetComponent<SpriteRenderer>().enabled = false;
        GameObject dog;
        (dog = transform.GetChild(0).gameObject).SetActive(true);
        StartCoroutine(ByeByeDog(dog));
    }

    bool playing = true;
    IEnumerator ByeByeDog(GameObject dog) {
        Speed = 0;
        yield return new WaitForSeconds(3);
        playing = false;
        yield return new WaitForSeconds(.5f);

        dialogueUI.ShowDialogue(dialogueObject);

        while (dialogueUI.IsOpen){
            yield return new WaitForSeconds(0.3f);
        }
        
        dog.GetComponent<Animator>().Play("explode");
        yield return new WaitForSeconds(0.3f);
        soundController.PlaySound(boom);

        yield return new WaitForSeconds(4);
        
        StaticDataLoader.event_minigame2_finished = true;
        SceneManager.LoadScene(1);
    }
}
