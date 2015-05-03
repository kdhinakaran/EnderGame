﻿using UnityEngine;
using System.Collections;

public class WandCollider : MonoBehaviour {

	public GameObject ghost;

	private GameObject selection = null;

	private Selector selector;
	public RUISWand wand;

	private GameObject currentGhost;

	void Start(){
		selector = (Selector)GameObject.Find("Selector Sphere").GetComponent("Selector");
	}

	void Update() {
		if (wand.SelectionButtonWasPressed () && selection != null) {

			if(currentGhost == null && selection.tag.Equals("Ship")){
				currentGhost = (GameObject)Instantiate (ghost, selection.transform.position, selection.transform.localRotation);
				currentGhost.transform.parent = this.transform;
			} 
		} else if (wand.SelectionButtonWasReleased () && currentGhost != null) {
			
			//MoveShip ship = (MoveShip)selection.GetComponent("MoveShip");
			MoveShipPhysics ship = (MoveShipPhysics)selection.GetComponent("MoveShipPhysics");
			ship.SetGhost(currentGhost);

			currentGhost.transform.parent = null;
			currentGhost = null;
		}
	}
	
	
	void SetSelection(GameObject select){
		if (currentGhost != null) {
			return;
		}
		this.selection = select;
		selector.selection = select;
	}

	void OnTriggerEnter(Collider other) {
		if (wand.SelectionButtonIsDown () || !(other.gameObject.tag.Equals("Ship")))
			return;
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
		if (wand.SelectionButtonIsDown () || !(other.gameObject.tag.Equals("Ship")))
			return;
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
		if (other.gameObject == selection)
			SetSelection(null);
		
	}
}
