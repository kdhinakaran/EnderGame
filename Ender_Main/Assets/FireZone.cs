using UnityEngine;
using System.Collections;

public class FireZone : MonoBehaviour {
	public GameObject firezone;
	// Use this for initialization
	void Start () {
		Vector3 position = transform.position + transform.TransformPoint(new Vector3(-0.5f, 0, 0));
		GameObject fire = (GameObject)Instantiate (firezone, position, transform.rotation);
		fire.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
