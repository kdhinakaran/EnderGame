using UnityEngine;
using System.Collections;

public class DetectShips : MonoBehaviour {

	private int shipsInside = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool ShipsInside() {
		return shipsInside != 0;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("EnemyShip"))
			shipsInside++;
	}

	void OnTriggerStay(Collider other) {
		if(other.gameObject.tag.Equals("EnemyShip"))
			Debug.Log ("OnTriggerStay :" + other.gameObject.tag);
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag.Equals ("EnemyShip"))
			shipsInside--;
	}
}

