using UnityEngine;
using System.Collections;

public class FireZoneMover : MonoBehaviour {
	// ADJUST THAT
	private float MOVMENT_FACTOR = 10f;

	public Selector selector;

	public RUISWand wand;

	private Vector3 startposition = Vector3.zero;
	private Vector3 firstposition = Vector3.zero;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (selector.selection) {
			FireZone firezone = selector.selection.GetComponentInChildren<FireZone>();
			if(wand.SelectionButtonWasPressed() && startposition == Vector3.zero){
				startposition = wand.transform.position;
				firstposition = startposition;
				if(!firezone.instantiated)
					firezone.EnableFirezone(startposition);
				//firezone.PlaySound();
			}
			else if(wand.SelectionButtonIsDown () && startposition != Vector3.zero && firezone.zone){
				float dist = Vector3.Distance(firstposition, wand.transform.position)*MOVMENT_FACTOR;
				firezone.zone.transform.position += (wand.transform.position - startposition)*dist;
				startposition = wand.transform.position;
			}
			else if(wand.SelectionButtonWasReleased()){
				startposition = Vector3.zero;
				firezone.PlaySound();
			}
		}
	}
}
