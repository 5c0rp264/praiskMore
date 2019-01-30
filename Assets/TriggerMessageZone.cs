using UnityEngine;

// Check that we have a collider
[RequireComponent(typeof(Collider))]
public class TriggerMessageZone : MonoBehaviour
{
    public Dialogue dialogue;
    public bool triggerOnce = true;

    protected bool triggered = false;

    private void Start()
    {
        if (!GetComponent<Collider>().isTrigger)
        {
            // if collider is not set as trigger, the dialogue will never display
            Debug.Log("Dialogue cannot trigger because collider is not set as trigger", gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If never triggered or not trigger once
        if (!triggered || !triggerOnce)
        {
            // Start the dialogue
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Option to remove the dialogue
    }
}
