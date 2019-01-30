using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //toutes les informations à récupérer
    public GameObject message;
    public GameObject title;
    public GameObject dialogue;
    public Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        //création d'une nouvelle pile FIFO
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue){
        //supprime les anciennes phrases
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            //ajoute les phrases à la pile
            sentences.Enqueue(sentence);
        }
        //allume la fenêtre
        message.SetActive(true);
        DisplayNextSentence();
    }

    //change le texte du gameObject Message
    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            message.SetActive(false);
            return;
        }
        string sentence = sentences.Dequeue();
        dialogue.GetComponent<Text>().text = sentence;
    }
}
