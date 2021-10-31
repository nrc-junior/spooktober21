using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventController2 : MonoBehaviour {

    public IInteractable Interactable {get; set;}
    public DialogueUI dialogueUI;
    //public DialogueObject[] dialogueObject_player;  ||| Não vai precisar dessa array pq nesse diálogo o player não vai falar nada
    public DialogueObject[] dialogueObject_diabo; // A mesma coisa, mas agora são os textos de fala do capetinha


    public void Run()
    {
        StartCoroutine(StartInSequence());
    }
    IEnumerator StartInSequence()
    {
        yield return new WaitForSeconds(3f);
        for(int i=0; i < 1; i++)
        {
            dialogueUI.ShowDialogue(dialogueObject_diabo[i]);
            while(dialogueUI.IsOpen)
            {
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
}