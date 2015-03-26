using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	public GameObject selection;

	// Use this for initialization
	void Start () {
		renderer.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * 10, Space.World);
		renderer.enabled = false;

		if(selection != null){
			renderer.enabled = true;
			transform.position = selection.transform.position;
		}
	}
}
