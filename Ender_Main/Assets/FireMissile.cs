using UnityEngine;
using System.Collections;

public class FireMissile : MonoBehaviour {

	public GameObject missile;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fire(Vector3 target, float scale) {
		GameObject newMissile = (GameObject)Instantiate (missile, transform.position, Quaternion.identity);
		Vector3 vec = Random.insideUnitSphere;
		Missile mis = (Missile)newMissile.GetComponent("Missile");
		mis.SetTarget (target + vec * scale);
	}
}
