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

	private DetectShips detectShips;
	private FireMissile fireMissile;

	private Color first = new Color(120f/255f, 75f/255f, 55f/255f, 255f/255f);
	private Color second = new Color(170f/255f, 100f/255f, 25f/255f, 255f/255f);
	// Use this for initialization
	void Start () {
		Vector3 position = transform.TransformPoint(new Vector3(0, -0.5f, 0));
		zone = (GameObject)Instantiate (firezone, position, transform.rotation);
		detectShips = (DetectShips)zone.GetComponent("DetectShips");
		fireMissile = (FireMissile)GetComponent ("FireMissile");

		lineRenderer = lineRender.GetComponent<LineRenderer>();
		lineRenderer.SetWidth(0.01F, 0.01F);
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.enabled = true;

		rotationValue = Random.value;
	}
	
	float period = 4;
	float time = 0;
	// Update is called once per frame
	void Update () {
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, zone.transform.position);
		bool inRange = Vector3.Distance (transform.position, zone.transform.position) < 5;
		if (!inRange) {
			lineRenderer.SetColors(Color.gray, Color.gray);
		}
		else
			lineRenderer.SetColors(c1, c2);

		zone.transform.Rotate(new Vector3(0.5f,0.3f,rotationValue) * Time.deltaTime * 10, Space.World);
		
		time += Time.deltaTime;
		if (detectShips.ShipsInside () && inRange && time > period) {
			fireMissile.Fire(zone.transform.position);
			time = 0;
		}
		// Color.Lerp(first, second, time/period)
		zone.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.white);
		zone.GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
		zone.GetComponent<Renderer> ().material.color = Color.white;
	}
}
