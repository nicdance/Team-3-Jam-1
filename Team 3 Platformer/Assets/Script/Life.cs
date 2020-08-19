using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public GameObject filledLife;
    [SerializeField]
    private Image emptyLifeImage;

    private Color visible = Color.white;
    private Color invisible = Color.clear;

    private bool isHeartFilled = false;

    // Start is called before the first frame update
    void Start()
    {
        emptyLifeImage = gameObject.GetComponent<Image>();
        filledLife.SetActive(false);
        emptyLifeImage.color = invisible;
    }

    // This will show the placeholder image for the health heart,
    public void ActivateLife()
    {
        emptyLifeImage.color = visible;
    }

    // Activate the life
    public void FillLife()
    {
        filledLife.SetActive(true);
    }
    // deactivates the red heart
    public void DrainLife()
    {
        filledLife.SetActive(false);
    }
}
