using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() {
        Application.Quit();
    } 
}
