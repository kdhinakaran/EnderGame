using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public GameObject explosion;
	private AudioSource audio;
	float period, time;

	private State state = State.IDLE;
	
	enum State {
		IDLE,
		MOVE,
		EXPLODE
	}

	// holds the gameobject who shot this missile
	private GameObject origin;

	// Use this for initialization
	void Start () {
		this.audio = GetComponent<AudioSource> ();
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

	public void SetTarget(GameObject origin, Vector3 target) {
		this.origin = origin;
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
		GameObject kaboom = (GameObject)Instantiate (explosion, transform.position, transform.rotation);
		Destroy(gameObject);
		Destroy (kaboom, 2.0f);
		this.audio.Play ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Missile")) {
			Explode ();
			Destroy (other.gameObject);
		} else if (!other.gameObject.Equals (origin) && !other.gameObject.name.Equals ("Radar")) {
			Debug.Log("hit " + other.gameObject.name);
			HitTaker hittaker = other.gameObject.GetComponent<HitTaker>();
			if(hittaker != null)
				hittaker.hit(30);
			Explode ();
		}
	}
}
