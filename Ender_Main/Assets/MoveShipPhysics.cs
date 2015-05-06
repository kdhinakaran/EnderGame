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
		STOP_BEFORE,
		MOVE_FAR,
		MOVE_CLOSE,
		MOVE_FINETUNE,
		STOP_AFTER,
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

		rb = GetComponent<Rigidbody>();
	}

	// Old stuff
	Vector3 startPosition, endPosition;
	Quaternion startRotation, endRotation;
	float period, time;

	private float ROTATION_TIME = 10.0f;

	// New stuff
	private float ROTATE_SPEED = 1.0f;

	private float MAX_POWER = 0.05f;
	private float DECELERATE_FACTOR = -1.0f;
	private float BREAK_ENGINE = 0.1f;
	private float MOVE_TOWARDS = 0.025f;

	private float MAX_VELOCITY = 0.25f;

	private Vector3 targetPosition;
	private float originalDistance;

	private Rigidbody rb;

	// Accelerate
	void accelerate(float power) {
		Vector3 forward = transform.up * -1.0f;
		rb.AddForce(forward * MAX_POWER * power, ForceMode.Acceleration);
	}

	// Decelerate
	void decelerate(float power) {
		accelerate(power * DECELERATE_FACTOR);
	}

	// Ship velocity
	void stopShip() {
		rb.velocity = Vector3.zero;
	}

	// Ship velocity
	Vector3 getShipVelocity() {
		return rb.velocity;
	}

	// Slow down velocity
	void slowDown(float power) {
		Vector3 direction = (getShipVelocity() / getShipVelocity().magnitude) * -1.0f;
		rb.AddForce(direction * BREAK_ENGINE * power, ForceMode.Acceleration);
	}

	void moveTowardsTarget() {
		Vector3 direction = targetPosition - transform.position;
		direction /= direction.magnitude;

		rb.AddForce(direction * MOVE_TOWARDS, ForceMode.Acceleration);
	}

	// Ship velocity towards target position
	Vector3 getShipVelocityTowardsTarget() {
		return new Vector3();
	}

	//
	float getAngleOffset() {
		return 0.0f;
	}

	void rotateTowardsTarget() {
		
	}

	// Distance to target position
	float distanceToTarget() {
		Vector3 heading = targetPosition - transform.position;

		return heading.magnitude;
	}

	// Update is called once per frame
	void FixedUpdate () {
		switch (state) {
		case State.IDLE:
			break;
		case State.STOP_BEFORE:
			if(getShipVelocity().magnitude > 0.05f) {
				slowDown(1.0f);
			} else {
				stopShip();

				moveTo = ghost.transform.position;
				startRotation = transform.rotation;
				endRotation = Quaternion.LookRotation (ghost.transform.position - transform.position);
				endRotation *= Quaternion.Euler(-90, 0, 0);
				startPosition = transform.position;
				endPosition = ghost.transform.position;
				
				move = true;
				time = 0f;
				period = (Quaternion.Angle(startRotation, endRotation) / 180.0f) * ROTATION_TIME;

				state = State.START_ROTATE;
			}

			break;
		case State.START_ROTATE:
		{
			time += Time.fixedDeltaTime;

			if (time < period) {
				transform.rotation = Quaternion.Slerp (startRotation, endRotation, time / period);
			} else {
				state = State.MOVE_FAR;

				afterburnerParticle.enableEmission = true;
			}

			break;
		}
		// Far away from target
		case State.MOVE_FAR:
		{
			Debug.Log ("Move far. Velocity: " + getShipVelocity().magnitude);

			// Change to proximity moving, when we get close
			if(distanceToTarget() < 0.75f) {
				state = State.MOVE_CLOSE;
			}

			// Accelerate, if velocity is low
			if(getShipVelocity().magnitude < MAX_VELOCITY) {
				accelerate(1.0f);
			}

			break;
		}
		// Close to the target
		case State.MOVE_CLOSE:
			Debug.Log ("Move close. Velocity: " + getShipVelocity().magnitude);

			// Almost at the target
			if(distanceToTarget() < 0.1f) {
				afterburnerParticle.enableEmission = false;

				state = State.MOVE_FINETUNE;
			}

			if(getShipVelocity().magnitude < 0.1f) {
				moveTowardsTarget();
				afterburnerParticle.enableEmission = true;
			}

			if(getShipVelocity().magnitude > 0.1f) {
				decelerate(1.0f);
				afterburnerParticle.enableEmission = false;
			}

			break;
		// Stop, when we are very close to the target
		case State.MOVE_FINETUNE:
		{
			Debug.Log ("Move finetune. Velocity: " + getShipVelocity().magnitude);

			if(getShipVelocity().magnitude > 0.1f) {
				decelerate(1.0f);
			}

			if(getShipVelocity().magnitude > 0.05f) {
				slowDown(1.0f);
			}

			if(distanceToTarget() < 0.05f || getShipVelocity().magnitude < 0.05f) {
				afterburnerParticle.enableEmission = false;

				state = State.STOP_AFTER;
			}

			break;
		}
		case State.STOP_AFTER:
			Debug.Log ("Move stop. Velocity: " + getShipVelocity().magnitude);

			slowDown(1.0f);
			
			if(getShipVelocity().magnitude < 0.1f) {
				stopShip();
				
				state = State.END_ROTATE;
				
				lineRenderer.SetPosition(0, transform.position);
				lineRenderer.SetPosition(1, ghost.transform.position);
				startRotation = transform.rotation;
				endRotation = ghost.transform.rotation;
				
				period = (Quaternion.Angle(startRotation, endRotation) / 180.0f) * ROTATION_TIME;
				time = 0;
				
				Debug.Log ("Starting end rotate");
			}

			break;
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
		//Debug.Log ("SetGhost");

		DestroyGhost();

		ghost = newGhost;
		state = State.STOP_BEFORE;
		afterburnerParticle.enableEmission = false;

		targetPosition = ghost.transform.position;
	}

	public void DestroyGhost(){
		if (ghost != null) {
			Destroy(ghost);
			ghost = null;
		}
	}

}
 