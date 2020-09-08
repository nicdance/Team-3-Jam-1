using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isActive = true;
    public GameObject collectedState;
    public GameObject idleState;
    public BoxCollider myCollider;

    public void SetCollected()
    {
        idleState.SetActive(false);
        collectedState.SetActive(true);
        myCollider.enabled = false;
        isActive = false;
    }
}
