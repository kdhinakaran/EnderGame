using UnityEngine;
using System.Collections;

public class ShipRadar : MonoBehaviour {
	
	public GameObject missile;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	int time = 0;
	int duration = 100;
	void OnTriggerEnter(Collider other) {
	}
	
	void OnTriggerStay(Collider other) {
		if (!(other.gameObject.tag.Equals("Ship")))
			return;
		if (time > duration) {
			Fire (other.gameObject);
			time = 0;
		}
		time++;
	}
	void OnTriggerExit(Collider other) {
	}

	void Fire(GameObject target) {
		GameObject newMissile = (GameObject)Instantiate (missile, transform.position, Quaternion.identity);
		Vector3 vec = Random.insideUnitSphere;
		Missile mis = (Missile)newMissile.GetComponent("Missile");
		mis.SetTarget (transform.parent.gameObject, target.transform.position);
	}
}
