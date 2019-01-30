using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportVaisseau : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Téléportation dans le vaisseau");
        SceneManager.LoadScene("NomSceneVaisseau");/*Modifier le nom de la scène*/
    }
}
