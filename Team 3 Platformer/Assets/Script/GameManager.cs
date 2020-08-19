using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
