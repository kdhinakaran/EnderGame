using UnityEngine;
using System.Collections;

public class MoveShipPhysics : MonoBehaviour {

	public GameObject lineRender;
	public GameObject afterburner;

	private Vector3 moveTo;
	private GameObject ghost;

	private float speed = 1;
	private bool move = false;

	private LineRenderer lineRenderer;
	private ParticleSystem afterburnerParticle;

	public Color c1 = Color.yellow;
	public Color c2 = Color.red;

	private State state = State.IDLE;

	enum State {
		IDLE,
		START_ROTATE,
		MOVE,
		END_ROTATE
	}
	// Use this for initialization
	void Start () {
		moveTo = transform.position;
		lineRenderer = lineRender.GetComponent<LineRenderer>();
		lineRenderer.SetWidth(0.01F, 0.01F);
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);

		afterburnerParticle = afterburner.GetComponent<ParticleSystem>();

		rigidbody = GetComponent<Rigidbody>();
	}

	// Get the current velocity of the ship
	Vector3 getVelocity() {
		return new Vector3();
	}

	// Old stuff
	Vector3 startPosition, endPosition;
	Quaternion startRotation, endRotation;
	float period, time;

	// New stuff
	public float MAX_POWER = 100.0f;
	private Vector3 targetPosition;

	private Rigidbody rigidbody;

	// Accelerate
	void accelerate(float power) {
		Vector3 direction = new Vector3(0.0f, 1.0f, 0.0f);
		rigidbody.AddForce(direction * MAX_POWER * power, ForceMode.Acceleration);

		return;
	}

	// Decelerate
	void decelerate(float power) {
		return;
	}

	// Update is called once per frame
	void FixedUpdate () {
		switch (state) {
		case State.IDLE:
			break;
		case State.START_ROTATE:
		{
			time += Time.fixedDeltaTime;

			if (time < period) {
				transform.rotation = Quaternion.Slerp (startRotation, endRotation, time / period);
			} else {
				state = State.MOVE;

				transform.LookAt (ghost.transform);
				transform.Rotate (new Vector3 (-90,0,0));

				afterburnerParticle.enableEmission = true;
			}

			break;
		}
		case State.MOVE:
		{
			accelerate(1.0f);

//			if (time >= period) {
//				state = State.END_ROTATE;
//				lineRenderer.SetPosition(0, transform.position);
//				lineRenderer.SetPosition(1, ghost.transform.position);
//				transform.position = endPosition;
//				startRotation = transform.rotation;
//				endRotation = ghost.transform.rotation;
//
//				afterburnerParticle.enableEmission = false;
//
//				period = (Quaternion.Angle(startRotation, endRotation)/180)*3;
//				time = 0;
//			} else {
//				transform.position = Vector3.Lerp (startPosition, endPosition, time / period);
//			}

			break;
		}
		case State.END_ROTATE:
		{
			time += Time.fixedDeltaTime;
			if (time >= period) {
				state = State.IDLE;
				DestroyGhost();
			} else {
				transform.rotation = Quaternion.Slerp (startRotation, endRotation, time / period);
			}
			break;
		}
		}
	}

	void Update() {
		if (ghost != null) {
			lineRenderer.enabled = true;
			lineRenderer.SetPosition (0, transform.position);
			lineRenderer.SetPosition (1, ghost.transform.position);
			float distance = Vector3.Distance(transform.position, ghost.transform.position);
			Vector3 heading = ghost.transform.position - transform.position;
			Vector3 direction = heading / heading.magnitude;
			if(Physics.Raycast(transform.position, direction, heading.magnitude)) {
				lineRenderer.SetColors(c1, c1);
			}
			else {
				lineRenderer.SetColors(c2, c2);
			}

		} else {
			lineRenderer.enabled = false;
		}
	}

	public void SetGhost(GameObject newGhost) {
		Debug.Log ("SetGhost");

		DestroyGhost();

		ghost = newGhost;
		moveTo = ghost.transform.position;
		startRotation = transform.rotation;
		endRotation = Quaternion.LookRotation (ghost.transform.position - transform.position);
		endRotation *= Quaternion.Euler(-90, 0, 0);
		startPosition = transform.position;
		endPosition = ghost.transform.position;

		move = true;
		time = 0f;
		period = (Quaternion.Angle(startRotation, endRotation)/180)*3; // it will take 3 sec to look

		state = State.START_ROTATE;

		targetPosition = ghost.transform.position;
	}

	void DestroyGhost(){
		if (ghost != null) {
			Destroy(ghost);
			ghost = null;
		}
	}

}
