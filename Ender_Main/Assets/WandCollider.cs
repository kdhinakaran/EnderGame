using UnityEngine;
using System.Collections;

public class WandCollider : MonoBehaviour {

	GameObject selection = null;

	Selector selector;

	void Start(){
		selector = (Selector)GameObject.Find("Selector_Sphere").GetComponent("Selector");
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger----------------------------------");
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

	void SetSelection(GameObject select){
		this.selection = select;
		selector.selection = select;
	}


	void OnTriggerStay(Collider other) {
		Debug.Log ("Trigger Stay----------------------------------");
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
		Debug.Log ("Trigger Stay----------------------------------");
		if (other.gameObject == selection)
			SetSelection(null);
		
	}
}
