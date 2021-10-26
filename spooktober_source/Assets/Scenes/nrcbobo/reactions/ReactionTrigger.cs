using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionTrigger : MonoBehaviour {
    public Sprite display_image;
    public bool repeatable;
    bool played;

    void OnTriggerEnter(Collider col) {
        if (!played) {
            played = !repeatable;
            Animator anim = col.transform.GetChild(0).GetComponent<Animator>();
            anim.transform.GetChild(anim.transform.childCount - 1).GetComponent<Image>().sprite = display_image;
            StartCoroutine(PlayAnim(anim));
        }
    }

    IEnumerator PlayAnim(Animator anim) {
            anim.SetBool("active",true);
        yield return new WaitForSeconds(6);
            anim.SetBool("active",false);
    }
}
