using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public GameObject explosion;
	float period, time;

	private State state = State.IDLE;
	
	enum State {
		IDLE,
		MOVE,
		EXPLODE
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Update is called once per frame
	void FixedUpdate () {
		switch (state) {
		case State.IDLE:
			break;
		case State.MOVE:
			{
				time += Time.fixedDeltaTime;
				if (time >= period) {
					state = State.EXPLODE;
					time = 0;
				}
				break;
			}
			case State.EXPLODE:
			{
			Explode();
				break;
			}	
		}
	}

	public void SetTarget(Vector3 target){
		state = State.MOVE;
		
		period = 20;
		time = 0;
		
		Vector3 vec = target - transform.position;
		GetComponent<Rigidbody> ().AddForce (vec*100, ForceMode.Acceleration);

		transform.LookAt (target);
	}

	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		Debug.Log ("OnCollisionEnter");
	}

	void Explode(){
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("EnemyShip"))
			Explode ();
		else if(other.gameObject.tag.Equals ("Missile")) {
			Explode();
			Destroy(other.gameObject);
		}
	}
}
