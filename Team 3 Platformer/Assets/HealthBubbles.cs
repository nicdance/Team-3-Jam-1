using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBubbles : MonoBehaviour
{
    public GameObject filledHeart;
    [SerializeField]
    private Image emptyHeartImage;

    private Color visible = Color.white;
    private Color invisible = Color.clear;

    private bool isHeartFilled = false;

    // Start is called before the first frame update
    void Start()
    {
        emptyHeartImage = gameObject.GetComponent<Image>();
        filledHeart.SetActive(false);
        emptyHeartImage.color = invisible;
    }

    // This will show the placeholder image for the health heart,
    public void ActivateHealth() {
        emptyHeartImage.color = visible;
    }

    // Activate sthe red heart
    public void FillHeart() {
        filledHeart.SetActive(true);
    }
    // deactivates the red heart
    public void DrainHeart()
    {
        filledHeart.SetActive(false);
    }


}
