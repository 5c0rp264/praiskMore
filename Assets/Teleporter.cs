using UnityEngine;
using UnityEngine.SceneManagement;

// Check that we have a collider
[RequireComponent(typeof(Collider))]
public class Teleporter : MonoBehaviour
{
    public int scene = 1;
    public bool changePosition = false;
    public Vector3 newPosition = Vector3.zero;

    private void Start()
    {
        if (!GetComponent<Collider>().isTrigger)
        {
            // if collider is not set as trigger, the dialogue will never display
            Debug.Log("Teleporter cannot trigger because collider is not set as trigger", gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(scene);
            if (changePosition)
            {
                other.transform.position = newPosition;
            }
        }
    }
}
