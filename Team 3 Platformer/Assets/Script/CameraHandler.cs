using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
	public CameraController cameracontroller;
	public Transform PlayerTransform;

	
	
    // Start is called before the first frame update
    void Start()
    {
        cameracontroller.Setup(() => PlayerTransform.position);		
		
    }


    public void SetupCamera() {

        cameracontroller.Setup(() => PlayerTransform.position);
    }


}