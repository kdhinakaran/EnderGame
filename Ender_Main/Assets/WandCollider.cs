using UnityEngine;
using System.Collections;

public class WandCollider : MonoBehaviour {
	// ADJUST THAT
	private float DISTANCE_FACTOR = 0.1f;
	// ADJUST THAT
	private float MOVMENT_FACTOR = 2f;

	public GameObject ghost;

	private GameObject selection = null;

	private Selector selector;
	private Indicator indicator;
	public RUISWand wand;

	private GameObject currentGhost;

	Vector3 posonpress;
	Vector3 startpos;

	void Start(){
		selector = (Selector)GameObject.Find("Selector Sphere").GetComponent("Selector");
		indicator = (Indicator)GameObject.Find("Indicator Sphere").GetComponent("Indicator");
	}

	void Update() {
		if (wand.SelectionButtonWasPressed () && selection != null) {
			if(currentGhost == null && selection.tag.Equals("Ship")){
			//	currentGhost = (GameObject)Instantiate (ghost, selection.transform.position, selection.transform.rotation);
			//	currentGhost.transform.parent = this.transform;
				posonpress = this.transform.position;
				startpos = posonpress;
			}

		} else if (wand.SelectionButtonIsDown() && selection != null) {
			if(currentGhost == null && selection.tag.Equals("Ship")){
				if(Vector3.Distance(transform.position, posonpress) > DISTANCE_FACTOR) {
					currentGhost = (GameObject)Instantiate (ghost, selection.transform.position, selection.transform.rotation);
					currentGhost.transform.position = this.transform.position;
					currentGhost.transform.rotation = this.transform.rotation;
				}
			}
			else if(currentGhost){
				float dist = Vector3.Distance(posonpress, this.transform.position)*MOVMENT_FACTOR;
				currentGhost.transform.position += (this.transform.position - startpos)*dist;
				currentGhost.transform.rotation = this.transform.rotation;
			}
			startpos = this.transform.position;
		} else if (wand.SelectionButtonWasReleased () && currentGhost != null) {
			// ship got probably destroyed during drag
			if(selection == null) {
				Destroy(currentGhost);
			} else {
				//MoveShip ship = (MoveShip)selection.GetComponent("MoveShip");
				MoveShipPhysics ship = (MoveShipPhysics)selection.GetComponent("MoveShipPhysics");
				ship.SetGhost(currentGhost);

				currentGhost.transform.parent = null;
				currentGhost = null;
			}
		}
	}
	
	
	void SetSelection(GameObject select){
		if (currentGhost != null) {
			return;
		}
		this.selection = select;
		selector.SetSelection(select);
	}

	void SetIndicator(GameObject indicator){
		this.indicator.indicator = indicator;
	}

	void OnTriggerEnter(Collider other) {
		if (!(other.gameObject.tag.Equals("Ship")))  // just allow ships 
			return;
		if(!wand.SelectionButtonWasPressed()) { // just select if pressed right now
			SetIndicator(other.gameObject);
			return;
		}
		if (selection != null) {
			float  ms = Vector3.Distance(gameObject.transform.position, selection.transform.position);
			float  mo = Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position);
			if(mo < ms)
				SetSelection(other.gameObject);
		}
		else {
			SetSelection(other.gameObject);
		}
	
	}

	void OnTriggerStay(Collider other) {
		if (!(other.gameObject.tag.Equals("Ship"))) // just allow ships
			return;
		if(!wand.SelectionButtonWasPressed()) { // just select if pressed right now
			SetIndicator(other.gameObject);
			return;
		}
		if(selection == null)
			SetSelection(other.gameObject);
		else if (other.gameObject != selection) {
			float  ms = Vector3.Distance(gameObject.transform.position, selection.transform.position);
			float  mo = Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position);
			if(mo < ms)
				SetSelection(other.gameObject);
		}
		
	}
	void OnTriggerExit(Collider other) {
		if (wand.SelectionButtonIsDown () || !(other.gameObject.tag.Equals("Ship")))
			return;
		if (other.gameObject == indicator.indicator)
			SetIndicator(null);
	}
}
