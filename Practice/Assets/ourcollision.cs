using UnityEngine;
using System.Collections;

public class ourcollision : MonoBehaviour {
	public GameObject particle;
	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
		//	Debug.DrawRay(contact.point, contact.normal, Color.white);
			Instantiate(particle, contact.point, new Quaternion());
		}
		if (collision.relativeVelocity.magnitude > 2)
			audio.Play();
		//Debug.Log ("AAAAAAAAAAAAAAAAAAAAAAAAAAA");
	}
}