using UnityEngine;
using System.Collections;


public class Selector : MonoBehaviour {

	public GameObject selection;
	/// <summary>
	/// A float used to determine how big the selector sphere is in relation the
	/// selected object. Default 0.12.
	/// </summary>
	public float sphereScale;

	/// <summary>
	/// The selector sphere's x and y scales have to be 5 times
	/// to z scale for the sphere to be round.
	/// </summary>
	private readonly float zToXyScale = 5;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * 10, Space.World);
		GetComponent<Renderer>().enabled = false;

		if(selection != null){
			GetComponent<Renderer>().enabled = true;
			transform.position = selection.transform.position;
			scaleSelectorToObject();
		}
	}

	void scaleSelectorToObject() {
		Vector3 selectionSize = selection.GetComponent<Renderer>().bounds.size;
		float maxDimension = Mathf.Max(selectionSize.x, selectionSize.y, selectionSize.z);
		float zScale = maxDimension * sphereScale;
		transform.localScale = new Vector3(zToXyScale * zScale, zToXyScale * zScale,
		                                   sphereScale*maxDimension);
	}
}
