using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	protected Vector3 cameraOffset = Vector3.zero;

    private Func<Vector3> setCamfollowposFunc;
	
	public void Setup(Func<Vector3> setCamfollowposFunc)
	{
		this.setCamfollowposFunc = setCamfollowposFunc;
	
	}
	
	void Update ()
	{
		Vector3 camfollowpos = setCamfollowposFunc();
		camfollowpos += cameraOffset;
		camfollowpos.z = transform.position.z;

		Vector3 camdirmov = (camfollowpos - transform.position).normalized;
		float distance = Vector3.Distance(camfollowpos, transform.position);
		float cammovspd = 2f;
		
		if (distance > 0) 
		{
			Vector3 newCamPos = transform.position + camdirmov * distance * cammovspd * Time.deltaTime;
			
			float distaftermoving = Vector3.Distance(newCamPos,camfollowpos);
			
			if (distaftermoving > distance) 
			{
				newCamPos = camfollowpos;
			}
			transform.position = newCamPos;
			
			
		}
		
		
	}
}