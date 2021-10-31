using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventController1 : MonoBehaviour {

    public IInteractable Interactable {get; set;}
    public DialogueUI dialogueUI;
    public DialogueObject[] dialogueObject_player; // Uma array com todos os textos de fala do personagem player
    public DialogueObject[] dialogueObject_tomate; // A mesma coisa, mas agora são os textos de fala do tomate fofo


    public void Run(Animator anim)
    {
        StartCoroutine(StartInSequence(anim));
    }
    IEnumerator StartInSequence(Animator anim)
    {
        yield return new WaitForSeconds(3f);
        for(int i=0; i < 13; i++)
        {
            dialogueUI.ShowDialogue(dialogueObject_tomate[i]);
            while(dialogueUI.IsOpen)
            {
                yield return null;
            }
            if(i <= dialogueObject_player.Length && i != 10){
                if (i == 11){ 
                    anim.Play("cry");
                    dialogueUI.ShowDialogue(dialogueObject_player[10]);
                }
                else if (i == 12) dialogueUI.ShowDialogue(dialogueObject_player[11]);
                else dialogueUI.ShowDialogue(dialogueObject_player[i]);
            }
            while(dialogueUI.IsOpen)
            {
                yield return null;
            }
            yield return null;
        }
        GetComponent<TriggerOlhoMagico>().ReleasePlayer();
        yield return null;
    }
}