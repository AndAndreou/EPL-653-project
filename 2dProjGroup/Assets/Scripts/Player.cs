using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private Transform rigidBodyTransform;
	private GameRepository repository;
	private Rigidbody rigidBody;
	private float startRot;
	private float timestamp;
	private bool rotate;
	private bool direction; //true = right false = left
	private bool jump;

	void Start () {
		rigidBodyTransform = GetComponent<Rigidbody> ().transform;
		repository = GameRepository.getInstance();
		rigidBody = GetComponent<Rigidbody> ();
		timestamp = Time.deltaTime;
		startRot = 0.0f;
		rotate = false;
		direction = true;
		jump = true;
	}

	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow) && (!rotate)) {
			if (repository.getCurrentDimension() == Dimension.FRONT) {
				rigidBodyTransform.position = new Vector3( rigidBodyTransform.position.x - 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
			if (repository.getCurrentDimension() == Dimension.RIGHT) {
				rigidBodyTransform.position = new Vector3( rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - 0.1f);
			}
			if (repository.getCurrentDimension() == Dimension.BACK) {
				rigidBodyTransform.position = new Vector3( rigidBodyTransform.position.x + 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
			if (repository.getCurrentDimension() == Dimension.LEFT) {
				rigidBodyTransform.position = new Vector3( rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + 0.1f);
			}
		}

		if (Input.GetKey(KeyCode.RightArrow) && (!rotate)) {
			if (repository.getCurrentDimension() == Dimension.FRONT) {
				rigidBodyTransform.position = new Vector3( rigidBodyTransform.position.x + 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
			if (repository.getCurrentDimension() == Dimension.RIGHT) {
				rigidBodyTransform.position = new Vector3( rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + 0.1f);
			}
			if (repository.getCurrentDimension() == Dimension.BACK) {
				rigidBodyTransform.position = new Vector3( rigidBodyTransform.position.x - 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
			if (repository.getCurrentDimension() == Dimension.LEFT) {
				rigidBodyTransform.position = new Vector3( rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - 0.1f);
			}
		}

		if (Input.GetKeyDown (KeyCode.C) && (!rotate)) {
			rigidBodyTransform.position = new Vector3 (Mathf.Round(rigidBodyTransform.position.x) , rigidBodyTransform.position.y, Mathf.Round(rigidBodyTransform.position.z));
			rotate = true;
			direction = true;

			if (repository.getCurrentDimension() == Dimension.FRONT || repository.getCurrentDimension() == Dimension.BACK) {
				rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
			}
			if (repository.getCurrentDimension() == Dimension.RIGHT || repository.getCurrentDimension() == Dimension.LEFT) {
				rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
			}
		}

		if (Input.GetKeyDown (KeyCode.Z) && (!rotate)) {
			rigidBodyTransform.position = new Vector3 (Mathf.Round(rigidBodyTransform.position.x) , rigidBodyTransform.position.y, Mathf.Round(rigidBodyTransform.position.z));
			rotate = true;
			direction = false;
			
			if (repository.getCurrentDimension() == Dimension.FRONT || repository.getCurrentDimension() == Dimension.BACK) {
				rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
			}
			if (repository.getCurrentDimension() == Dimension.RIGHT || repository.getCurrentDimension() == Dimension.LEFT) {
				rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
			}
		}

		if (rotate) {
			if(direction){
				float angle = (Time.deltaTime - timestamp) / 0.1f * 90.0f;
				startRot = startRot + Mathf.Abs (angle);

				if(startRot < 90.0f){
					transform.RotateAround(transform.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
					
				}
				if(startRot >= 90.0f){
					rotate = false;
					startRot = 0.0f;
				}
			} else {
				float angle = (Time.deltaTime - timestamp) / 0.1f * -90.0f;
				startRot = startRot - Mathf.Abs (angle);

				if(startRot > -90.0f){
					transform.RotateAround(transform.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
				}
				if(startRot <= -90.0f){
					rotate = false;
					startRot = 0.0f;
				}
			}
		}

		if (jump && transform.position.y >= 0.0f && Input.GetKeyDown (KeyCode.UpArrow)) {
			rigidBody.AddForce(Vector3.up * 300.0f);
		}

		if (rigidBody.velocity.y != 0.0f) {
			jump = false;
		} else {
			jump = true;
		}

		if (repository.getPlayerLife() <= 0.0f) {
			Destroy(this);
		}
	}
	
	void FixedUpdate () {

	}

	void OnCollisionEnter(Collision other){

	}
}