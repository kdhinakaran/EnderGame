using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	public GameObject selection;
	//public GameObject indicator;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * 10, Space.World);
		GetComponent<Renderer>().enabled = false;

		if(selection != null) {
			GetComponent<Renderer>().enabled = true;
			transform.position = selection.transform.position;
		}
	}
}
