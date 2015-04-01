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
	private Animator animator;
	
	void Start () {
		rigidBodyTransform = GetComponent<Rigidbody> ().transform;
		repository = GameRepository.getInstance();
		rigidBody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		timestamp = Time.deltaTime;
		startRot = 0.0f;
		rotate = false;
		direction = true;
		jump = true;
	}
	
	void Update () {
		rigidBody.isKinematic = false;
		if (!repository.isRotating() && !repository.isRaised() && transform.position.y >= 0.0f) {
			if ( repository.getCurrentDimension() == Dimension.FRONT ) {
				transform.localEulerAngles  = new Vector3 (0.0f, 0.0f, 0.0f);
			}
			else if (repository.getCurrentDimension() == Dimension.RIGHT) {
				transform.localEulerAngles  = new Vector3 (0.0f, 270.0f, 0.0f);
			}
			else if (repository.getCurrentDimension() == Dimension.BACK) {
				transform.localEulerAngles  = new Vector3 (0.0f, 180.0f, 0.0f);
			}
			else if (repository.getCurrentDimension() == Dimension.LEFT) {
				transform.localEulerAngles  = new Vector3 (0.0f, 90.0f, 0.0f);
			}
		}
		
		if (Input.GetKey (KeyCode.LeftArrow) && (!rotate)) {
			animator.SetBool("walkBool", true);
			if (repository.getCurrentDimension () == Dimension.FRONT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x - 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (repository.getCurrentDimension () == Dimension.RIGHT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - 0.1f);
			} else if (repository.getCurrentDimension () == Dimension.BACK) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x + 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (repository.getCurrentDimension () == Dimension.LEFT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + 0.1f);
			}
		} else if (Input.GetKey (KeyCode.RightArrow) && (!rotate)) {
			animator.SetBool("walkBool", true);
			if (repository.getCurrentDimension () == Dimension.FRONT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x + 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (repository.getCurrentDimension () == Dimension.RIGHT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + 0.1f);
			} else if (repository.getCurrentDimension () == Dimension.BACK) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x - 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (repository.getCurrentDimension () == Dimension.LEFT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - 0.1f);
			}
		} else {
			animator.SetBool("walkBool", false);
		}

		reflectPlayer ();
		
		if (Input.GetKeyDown (KeyCode.C) && (!rotate)) {
			rigidBodyTransform.position = new Vector3 (Mathf.Round(rigidBodyTransform.position.x) , rigidBodyTransform.position.y, Mathf.Round(rigidBodyTransform.position.z));
			rotate = true;
			direction = true;
			
			if (repository.getCurrentDimension() == Dimension.FRONT || repository.getCurrentDimension() == Dimension.BACK) {
				rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
			}
			else if (repository.getCurrentDimension() == Dimension.RIGHT || repository.getCurrentDimension() == Dimension.LEFT) {
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
			else if (repository.getCurrentDimension() == Dimension.RIGHT || repository.getCurrentDimension() == Dimension.LEFT) {
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
				else if(startRot >= 90.0f){
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
			jump = false;
		}
		
		if (rigidBody.velocity.y != 0.0f) {
			jump = true;
		}// else {
		//	jump = true;
		//}
		
		if (repository.getPlayerLife() <= 0.0f) {
			Destroy(this);
		}
	}


	private void reflectPlayer() {
		if(Input.GetKeyDown(KeyCode.LeftArrow) && (!rotate)) {
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow) && (!rotate)) {
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
	}


	void FixedUpdate () {
		
	}
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "StaticCube") {
			Vector3 hit = other.contacts[0].normal;
			
			float angle = Vector3.Angle(hit, Vector3.up);
			
			if (Vector3.Dot(hit,Vector3.up) > 0) { // top
				//Debug.Log("Top" + hit);
			}else if(Vector3.Dot(hit,Vector3.up) < 0){
				//Debug.Log ("Bottom" + hit);
			}else if(Vector3.Dot(hit,Vector3.up) == 0){
				//Debug.Log ("Sides" + hit);
				rigidBody.isKinematic = true;
				//rigidBody.velocity = Vector3.zero;
			}
		}

		if (other.gameObject.tag == "Coin") {
			other.gameObject.SetActive(false);
		}
	}

}