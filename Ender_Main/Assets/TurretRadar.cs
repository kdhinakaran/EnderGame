using UnityEngine;
using System.Collections;

public class TurretRadar : MonoBehaviour {
	
	public GameObject missile;

	public GameObject gun;
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
		Vector3 angles = Quaternion.LookRotation(other.gameObject.transform.position - transform.position).eulerAngles;
		int updown = (int)angles.x;
		updown %= 360;
		int leftright = (int)angles.y;
		leftright %= 360;
		if ((updown < 15 || updown > 335) && 
		    leftright > 120 && leftright < 240) {
			gun.transform.localRotation = Quaternion.Euler (new Vector3 (angles.x - 90, angles.y - 180, 0));
			if (time > duration) {
				Fire (other.gameObject);
				time = 0;
			}
		}
		time++;
	}
	void OnTriggerExit(Collider other) {
	}
	
	void Fire(GameObject target) {
		GameObject newMissile = (GameObject)Instantiate (missile, gun.transform.TransformPoint(new Vector3(0f, -3.36f, 0.76f)), Quaternion.identity);
		Vector3 vec = Random.insideUnitSphere;
		Missile mis = (Missile)newMissile.GetComponent("Missile");
		mis.SetTarget (transform.parent.gameObject, target.transform.position);
	}
}
