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

    public void SetDoubleJump(bool state) {
        PlayerPrefs.SetInt("DoubleJump", boolToInt(state));
    }

    public bool GetDoubleJump()
    {

        if (PlayerPrefs.HasKey("DoubleJump") == true)
        { 
            int doubleJump = PlayerPrefs.GetInt("DoubleJump");
            return intToBool(doubleJump);
        }
        return false;

    }

    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}
