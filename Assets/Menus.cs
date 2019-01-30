using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void loadGame(){
        SceneManager.LoadScene("Scene_test");
    }

    public void returnToMenu(){
        SceneManager.LoadScene("MainMenuScene");
    }

    public void exit(){
        Application.Quit();
    }
}
