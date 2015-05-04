using UnityEngine;
using System.Collections;

public class HitTaker : MonoBehaviour {
	
	public GameObject explosion;
	public float health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void hit(int damage) {
		health -= damage;
		if(health <= 0) {
			Explode();
		}
		HealthBar hb = GetComponentInChildren<HealthBar> ();
		if (hb != null)
			hb.setBarLength (health / 100f);
	}

	void Explode(){
		Instantiate (explosion, transform.position, transform.rotation);

		
		FireZone firezone = GetComponent<FireZone> ();
		if (firezone != null)
			Destroy (firezone.zone);
		Destroy(gameObject);
		MoveShipPhysics ship = (MoveShipPhysics)gameObject.GetComponent("MoveShipPhysics");
		ship.DestroyGhost ();
	}
}
