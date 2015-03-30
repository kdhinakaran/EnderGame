using UnityEngine;
using System.Collections;

public class MoveShip : MonoBehaviour {

	private Vector3 moveTo;
	private GameObject ghost;

	private float speed = 1;
	private bool move = false;
	// Use this for initialization
	void Start () {
		moveTo = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(move) {
			transform.position = Vector3.Slerp (transform.position, moveTo, Time.deltaTime * speed);
			if(Mathf.Approximately(transform.position.x, moveTo.x) && 
			   Mathf.Approximately(transform.position.y, moveTo.y) && 
			   Mathf.Approximately(transform.position.z, moveTo.z)) {
				move = false;
				DestroyGhost();
			}
		}
	}

	public void SetGhost(GameObject newGhost) {
		DestroyGhost();
		ghost = newGhost;
		moveTo = newGhost.transform.position;
		move = true;
	}

	void DestroyGhost(){
		if (ghost != null) {
			Destroy(ghost);
			ghost = null;
		}
	}
}
