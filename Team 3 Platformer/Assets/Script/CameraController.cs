using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	protected Vector3 currentCameraOffset = Vector3.zero;
	[Header("Zoomed Out Settings")]
	[SerializeField]
	protected Vector3 startCameraOffset = Vector3.zero;
	[SerializeField]
	protected float zoomedOutPosition;
	[Header("Zoomed In Settings")]
	[SerializeField]
	protected Vector3 zoomCameraOffset = Vector3.zero;
	[SerializeField]
	protected float zoomedInPosition;


	[Header("other Camera Settings")]
	[SerializeField]
	protected float cameraMoveSpeed = 2f;
	[SerializeField]
	private Func<Vector3> setCamfollowposFunc;
	protected float currentZ;
    public void Start()
    {

		PlayerController.onZoomCamera += ToggleZoom;
	}

    public void Setup(Func<Vector3> setCamfollowposFunc)
	{
		this.setCamfollowposFunc = setCamfollowposFunc;
		currentCameraOffset = startCameraOffset;
		currentZ = zoomedOutPosition;
	}

	public void ToggleZoom() {
		float zPosition = transform.position.z;
		if (zPosition <= zoomedOutPosition+1)
        {
			currentZ = zoomedInPosition;
			currentCameraOffset = zoomCameraOffset;
		}
		else
		{
			currentZ = zoomedOutPosition;
			currentCameraOffset = startCameraOffset;
		}
	//	transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
	}
	void Update ()
	{
		Vector3 camfollowpos = setCamfollowposFunc();
		camfollowpos += currentCameraOffset;
		camfollowpos.z = currentZ;

		Vector3 camdirmov = (camfollowpos - transform.position).normalized;
		float distance = Vector3.Distance(camfollowpos, transform.position);
		
		
		if (distance > 0) 
		{
			Vector3 newCamPos = transform.position + camdirmov * distance * cameraMoveSpeed * Time.deltaTime;
			
			float distaftermoving = Vector3.Distance(newCamPos,camfollowpos);
			
			if (distaftermoving > distance) 
			{
				newCamPos = camfollowpos;
			}
			transform.position = newCamPos;
			
			
		}
		
		
	}
}