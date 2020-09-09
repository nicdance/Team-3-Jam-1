using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.SetDoubleJump(false);
        SceneManager.LoadScene("Level 1 (Jason3)");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() {
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }
}
