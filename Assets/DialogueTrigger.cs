using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    //lancement
    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    //Lancer le dialogue à l'entrée du trigger
    void OnTriggerEnter(){
        TriggerDialogue();
    }
}
