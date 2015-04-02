using UnityEngine;
using System.Collections;


public class Selector : MonoBehaviour {

	public GameObject selection;
	public float sphereScale;

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
			Vector3 selectionSize = selection.GetComponent<Renderer>().bounds.size;
			float maxSize = Mathf.Max(selectionSize.x, selectionSize.y, selectionSize.z);
			//Vector3 sphereSize = GetComponent<Renderer>().bounds.size;
			//float spehereMaxSize = Mathf.Max(selectionSize.x, selectionSize.y, selectionSize.z);
			transform.localScale = new Vector3(5*sphereScale*maxSize, 5*sphereScale*maxSize, sphereScale*maxSize);
		}
	}
}
