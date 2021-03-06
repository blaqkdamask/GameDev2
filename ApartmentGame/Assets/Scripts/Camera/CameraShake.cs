using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

[RequireComponent (typeof (HandHeldCam))]
public class CameraShake : MonoBehaviour {
	public float swaySpeed;
	public float swayAmount;

	HandHeldCam handCam;
	float saveSpeed;
	float saveSway;
	// Use this for initialization
	void Start () {
		
	}
	void OnEnable(){
		handCam = GetComponent<HandHeldCam> ();
		saveSpeed = handCam.m_SwaySpeed;
		saveSway = handCam.m_BaseSwayAmount;
	}

	
	// Update is called once per frame
	void Update () {
		handCam.m_SwaySpeed = swaySpeed;
		handCam.m_BaseSwayAmount = swayAmount;
	}

	void OnDisable(){
		handCam.m_SwaySpeed = saveSpeed;
		handCam.m_BaseSwayAmount = saveSway;
	}
}
