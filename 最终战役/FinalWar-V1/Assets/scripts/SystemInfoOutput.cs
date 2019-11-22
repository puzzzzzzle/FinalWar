using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemInfoOutput : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(SystemInfo.deviceName);
        Debug.Log(SystemInfo.processorType);
        Debug.Log(SystemInfo.deviceType);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
