using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {
		
	public GameObject indicator;
		
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;
	}
		
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * 10, Space.World);
		GetComponent<Renderer>().enabled = false;
		
		if(indicator != null) {
			GetComponent<Renderer>().enabled = true;
			transform.position = indicator.transform.position;
		}
	}
}
