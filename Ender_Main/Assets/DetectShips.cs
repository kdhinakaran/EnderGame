using UnityEngine;
using System.Collections;

public class DetectShips : MonoBehaviour {

	private int shipsInside = 0;
	private GameObject shipInside = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool ShipsInside() {
		return shipInside != null;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("EnemyShip"))
			shipInside = other.gameObject;
	}

	void OnTriggerStay(Collider other) {
		if(other.gameObject.tag.Equals("EnemyShip"))
			Debug.Log ("OnTriggerStay :" + other.gameObject.tag);
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag.Equals ("EnemyShip"))
			shipInside = null;
	}
}

