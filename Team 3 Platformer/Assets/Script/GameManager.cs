using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public CameraHandler cameraStart;
    public Image fadePanel;

    #region Singleton
    public static GameManager instance;

    public void Awake()
    {
        instance = this;
    }
    #endregion

    
    public GameObject gameOverMenu;
    public void GameOver() {
        gameOverMenu.SetActive(true);
    }
}
