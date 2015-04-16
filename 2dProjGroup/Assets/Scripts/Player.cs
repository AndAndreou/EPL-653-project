using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private Transform rigidBodyTransform;
	private GameRepository repository;
	private Rigidbody rigidBody;
	private float startRot;
	private float timestamp;
	private bool rotate;
	private bool cameraDirection; //true = right false = left
	private bool playerDirection;
	private Dimension playerDimension;
	private bool jump;
	private Animator animator;
	
	//bullet
	public Transform energyBullet;
	private float bulletSpeed = 10.0f;
	
	void Start () {
		rigidBodyTransform = GetComponent<Rigidbody> ().transform;
		repository = GameRepository.Instance;
		rigidBody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		timestamp = Time.deltaTime;
		startRot = 0.0f;
		rotate = false;
		cameraDirection = true;
		jump = true;
		playerDirection = false;
		playerDimension = Dimension.FRONT;
	}
	
	void Update () {
		/*
		if (repository.isPaused()) {
			return;
		}
		*/
		
		if (repository.isRaised ()) {
			return;
		}
		rigidBody.isKinematic = false;
		
		/* Fix camera misposition */
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
		
		
		/* Walking & shooting */
		bool isWalking = false; 
		if (Input.GetKey (KeyCode.LeftArrow) && (!rotate)) {
			animator.SetBool ("walkBool", true);
			repository.setBackgroundSpeed (-0.0002f);
			if (repository.getCurrentDimension () == Dimension.FRONT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x - 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (repository.getCurrentDimension () == Dimension.RIGHT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - 0.1f);
			} else if (repository.getCurrentDimension () == Dimension.BACK) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x + 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (repository.getCurrentDimension () == Dimension.LEFT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + 0.1f);
			}
			isWalking = true;
		} else if (Input.GetKey (KeyCode.RightArrow) && (!rotate)) {
			animator.SetBool ("walkBool", true);
			repository.setBackgroundSpeed (0.0002f);
			if (repository.getCurrentDimension () == Dimension.FRONT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x + 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (repository.getCurrentDimension () == Dimension.RIGHT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + 0.1f);
			} else if (repository.getCurrentDimension () == Dimension.BACK) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x - 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (repository.getCurrentDimension () == Dimension.LEFT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - 0.1f);
			}
			isWalking = true;
		} else if (Input.GetKeyDown (KeyCode.Space) && (!rotate)) {
			animator.SetBool("shoot", true);
			createBullet();
		} else {
			repository.setBackgroundSpeed(0.00f);
			animator.SetBool("walkBool", false);
			animator.SetBool("shoot", false);
		}
		
		if ((Input.GetKeyDown (KeyCode.Space)) && (isWalking) && (!rotate)) {
			animator.SetBool ("shalk", true);
			createBullet();
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			animator.SetBool ("shalk", false);
		}
		
		reflectPlayer ();
		
		
		/* Camera rotation */
		if (Input.GetKeyDown (KeyCode.C) && (!rotate)) {
			rigidBodyTransform.position = new Vector3 (Mathf.Round(rigidBodyTransform.position.x) , rigidBodyTransform.position.y, Mathf.Round(rigidBodyTransform.position.z));
			rotate = true;
			cameraDirection = true;
			
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
			cameraDirection = false;
			
			if (repository.getCurrentDimension() == Dimension.FRONT || repository.getCurrentDimension() == Dimension.BACK) {
				rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
			}
			else if (repository.getCurrentDimension() == Dimension.RIGHT || repository.getCurrentDimension() == Dimension.LEFT) {
				rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
			}
		}
		
		if (rotate) {
			if(cameraDirection){
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
		
		/* Jumping */
		if (jump && transform.position.y >= 0.0f && Input.GetKeyDown (KeyCode.UpArrow)) {
			rigidBody.AddForce(Vector3.up * 300.0f);
			jump = true;
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
	
	
	private void reflectPlayer() {
		if(Input.GetKeyDown(KeyCode.LeftArrow) && (!rotate)) {
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			playerDirection = false;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow) && (!rotate)) {
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			playerDirection = true;
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
	
	private void createBullet() {
		Vector3 position = this.gameObject.transform.position;
		float xShift = 0.0f;
		float zShift = 0.0f;
		float yShift = 0.2f;
		
		if (playerDirection) {
			if (repository.getCurrentDimension () == Dimension.FRONT) {
				xShift = 1.2f;
			} else if (repository.getCurrentDimension () == Dimension.BACK) {
				xShift = -1.2f;
			} else if (repository.getCurrentDimension () == Dimension.RIGHT) {
				zShift = 1.2f;
			} else if (repository.getCurrentDimension () == Dimension.LEFT) {
				zShift = -1.2f;
			}
		} else {
			if (repository.getCurrentDimension () == Dimension.FRONT) {
				xShift = -1.2f;
			} else if (repository.getCurrentDimension () == Dimension.BACK) {
				xShift = 1.2f;
			} else if (repository.getCurrentDimension () == Dimension.RIGHT) {
				zShift = -1.2f;
			} else if (repository.getCurrentDimension () == Dimension.LEFT) {
				zShift = 1.2f;
			}
		}
		Vector3 bulletPosition = new Vector3(position.x + xShift, position.y + yShift, position.z+zShift);
		
		Transform newBullet = Instantiate (energyBullet, bulletPosition, Quaternion.identity) as Transform;
		newBullet.tag = "EnergyBullet";
	}
	
}