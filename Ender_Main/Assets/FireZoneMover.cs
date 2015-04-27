using UnityEngine;
using System.Collections;

public class FireZoneMover : MonoBehaviour {
	public Selector selector;

	private RUISWand wand;

	private Vector3 startposition = Vector3.zero;
	// Use this for initialization
	void Start () {
		wand = GetComponent<RUISWand> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (selector.selection) {
			FireZone firezone = selector.selection.GetComponentInChildren<FireZone>();
			if(wand.SelectionButtonWasPressed() && startposition == Vector3.zero){
				startposition = wand.transform.position;
				if(!firezone.instantiated)
					firezone.EnableFirezone(startposition);
			}
			else if(wand.SelectionButtonIsDown () && startposition != Vector3.zero){
				firezone.transform.position += wand.transform.position - startposition;
				startposition = wand.transform.position;
			}
			else if(wand.SelectionButtonWasReleased()){
				startposition = Vector3.zero;
			}
		}
	}
}
