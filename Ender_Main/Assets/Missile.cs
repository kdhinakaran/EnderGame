using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public GameObject explosion;
	public AudioClip explosionSound;

	public GameObject implosion;
	public AudioClip implosionSound;
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
		GetComponent<Rigidbody> ().AddForce (vec.normalized*10, ForceMode.Acceleration);

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
	}

	void Implode(){
		GameObject bloob = (GameObject)Instantiate (implosion, transform.position, transform.rotation);
		Destroy(gameObject);
		Destroy (bloob, 2.0f);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Missile")) {
			AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.2f);
			Explode ();
			Destroy (other.gameObject);
		} else if (!other.gameObject.Equals (origin) && isShip (other.gameObject)) {
			Debug.Log("hit " + other.gameObject.name);
			HitTaker hittaker = other.gameObject.GetComponent<HitTaker>();
			if(hittaker != null)
				hittaker.hit(30);
			AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.2f);
			Explode ();
		} else if (!other.gameObject.Equals (origin) && other.gameObject.tag == "EnemyBase") {
			Debug.Log("hit " + other.gameObject.name);
			AudioSource.PlayClipAtPoint(implosionSound, transform.position, 0.2f);
			Implode ();
		}
	}

	bool isShip(GameObject target) {
		return (target.tag == "Ship" || target.tag == "EnemyShip"); 
	}
}
