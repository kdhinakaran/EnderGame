using UnityEngine;
using System.Collections;

public class FireZone : MonoBehaviour {
	public GameObject firezone;

	public GameObject lineRender;

	private GameObject zone;
	private LineRenderer lineRenderer;

	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	private float rotationValue;
	// Use this for initialization
	void Start () {
		Vector3 position = transform.TransformPoint(new Vector3(0, -0.5f, 0));
		zone = (GameObject)Instantiate (firezone, position, transform.rotation);

		lineRenderer = lineRender.GetComponent<LineRenderer>();
		lineRenderer.SetWidth(0.01F, 0.01F);
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.enabled = true;

		rotationValue = Random.value;
	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, zone.transform.position);
		if (Vector3.Distance (transform.position, zone.transform.position) > 5) {
			lineRenderer.SetColors(Color.gray, Color.gray);
		}
		else
			lineRenderer.SetColors(c1, c2);

		
		zone.transform.Rotate(new Vector3(0.5f,0.3f,rotationValue) * Time.deltaTime * 10, Space.World);
	}
}
