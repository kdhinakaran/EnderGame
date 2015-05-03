﻿using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	private Camera camera;

	// Use this for initialization
	void Start () {
		this.camera = GameObject.Find("CameraLeft").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (camera.transform);
	}
}
