using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private Transform rigidBodyTransform;
	//private GameRepository repository;
	private Rigidbody rigidBody;
	private float startRot;
	private float timestamp;
	private bool rotate;
	private bool cameraDirection; //true = right false = left
	private bool playerDirection;
	private Dimension playerDimension;
	private bool jump;
	private Animator animator;
	private float gravity;
	private bool isGravity;
	private bool touchesCube;
	private float startPos;
	
	//bullet
	public Transform energyBullet;
	private float bulletSpeed;
	
	void Start () {
		GetComponent<Renderer> ().enabled = false;
		rigidBodyTransform = GetComponent<Rigidbody> ().transform;
		//repository = GameRepository.Instance;
		rigidBody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		timestamp = Time.deltaTime;
		startRot = 0.0f;
		rotate = false;
		cameraDirection = true;
		jump = true;
		playerDirection = false;
		playerDimension = Dimension.FRONT;
		GameRepository.setPlayerLife (GameRepository.getPlayerLife ());
		bulletSpeed = 10.0f;
		gravity = -9.81f;
		isGravity = false;
		touchesCube = true;
		startPos = 1.34f;
	}
	
	void Update () {
		if (GameRepository.isPaused ()) {
			rigidBody.isKinematic = true;
			animator.SetBool ("walkBool", false);
			return;
		} else {
			rigidBody.isKinematic = false;
		}

		if (transform.position.y < 1.0f) {
			float distanceFromLoss = Vector3.Distance(new Vector3(0.0f, transform.position.y, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
			Debug.Log ("Distance = " + distanceFromLoss);
			Color playerColor = this.gameObject.GetComponent<SpriteRenderer> ().material.color;
			float newTransparencyValue = 1.0f - (distanceFromLoss/3.0f);
			Debug.Log("Transparency = " + newTransparencyValue);
			playerColor.a =  newTransparencyValue;
			this.gameObject.GetComponent<SpriteRenderer> ().material.color = playerColor;
			if(transform.position.y < -3.0f){
				GameRepository.setGameOverScreen(true);
				GameRepository.setPauseScreen(false);
				GameObject.FindGameObjectWithTag("CameraBackGround").GetComponent<PauseScript>().PauseGame();
				//Destroy(this.gameObject);
				//gameover
			}
		}
		
		if (GameRepository.isRaised ()) {
			if (isGravity) {
				reverseGravity();
			}
			return;
		}
		rigidBody.isKinematic = false;

		if (isGravity) {
			reverseGravity();
		}
		
		/* Fix camera misposition */
		if (!GameRepository.isRotating() && !GameRepository.isRaised() && transform.position.y >= 0.0f) {
			if ( GameRepository.getCurrentDimension() == Dimension.FRONT ) {
				transform.localEulerAngles  = new Vector3 (0.0f, 0.0f, 0.0f);
			}
			else if (GameRepository.getCurrentDimension() == Dimension.RIGHT) {
				transform.localEulerAngles  = new Vector3 (0.0f, 270.0f, 0.0f);
			}
			else if (GameRepository.getCurrentDimension() == Dimension.BACK) {
				transform.localEulerAngles  = new Vector3 (0.0f, 180.0f, 0.0f);
			}
			else if (GameRepository.getCurrentDimension() == Dimension.LEFT) {
				transform.localEulerAngles  = new Vector3 (0.0f, 90.0f, 0.0f);
			}
		}
		
		
		/* Walking & shooting */
		bool isWalking = false; 
		if (Input.GetKey (KeyCode.LeftArrow) && (!rotate)){// && !Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKey(KeyCode.RightArrow)) {
			animator.SetBool ("walkBool", true);
			GameRepository.setBackgroundSpeed (-0.0002f);
			if (GameRepository.getCurrentDimension () == Dimension.FRONT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x - 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (GameRepository.getCurrentDimension () == Dimension.RIGHT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - 0.1f);
			} else if (GameRepository.getCurrentDimension () == Dimension.BACK) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x + 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (GameRepository.getCurrentDimension () == Dimension.LEFT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + 0.1f);
			}
			isWalking = true;
		} else if (Input.GetKey (KeyCode.RightArrow) && (!rotate)){// && !Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
			animator.SetBool ("walkBool", true);
			GameRepository.setBackgroundSpeed (0.0002f);
			if (GameRepository.getCurrentDimension () == Dimension.FRONT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x + 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (GameRepository.getCurrentDimension () == Dimension.RIGHT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + 0.1f);
			} else if (GameRepository.getCurrentDimension () == Dimension.BACK) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x - 0.1f, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			} else if (GameRepository.getCurrentDimension () == Dimension.LEFT) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - 0.1f);
			}
			isWalking = true;
		} else if (Input.GetKeyDown (KeyCode.Space) && (!rotate)) {
			animator.SetBool("shoot", true);
			createBullet();
		} else {
			GameRepository.setBackgroundSpeed(0.00f);
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

		if (GameRepository.getCurrentDimension() == Dimension.FRONT || GameRepository.getCurrentDimension() == Dimension.BACK) {
			rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
		}
		else if (GameRepository.getCurrentDimension() == Dimension.RIGHT || GameRepository.getCurrentDimension() == Dimension.LEFT) {
			rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
		}
		
		/* Camera rotation */
		if (Input.GetKeyDown (KeyCode.C) && (!rotate)) {
			rigidBodyTransform.position = new Vector3 (Mathf.Round(rigidBodyTransform.position.x) , rigidBodyTransform.position.y, Mathf.Round(rigidBodyTransform.position.z));
			rotate = true;
			cameraDirection = true;
		}
		
		if (Input.GetKeyDown (KeyCode.Z) && (!rotate)) {
			rigidBodyTransform.position = new Vector3 (Mathf.Round(rigidBodyTransform.position.x) , rigidBodyTransform.position.y, Mathf.Round(rigidBodyTransform.position.z));
			rotate = true;
			cameraDirection = false;
		}

		if (Input.GetKeyDown (KeyCode.G) && (!rotate)) {
			if(isGravity) {
				isGravity = false;
			} else {
				isGravity = true;
			}
			//reverseGravity();
		}
		
		if (rotate) {
			if(cameraDirection){
				float angle = (Time.deltaTime - GameRepository.getTimeStamp()) / 0.1f * 90.0f;
				startRot = startRot + Mathf.Abs (angle);
				
				if(startRot < 90.0f){
					transform.RotateAround(transform.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
				}
				else if(startRot >= 90.0f){
					rotate = false;
					startRot = 0.0f;
				}
			} else {
				float angle = (Time.deltaTime - GameRepository.getTimeStamp()) / 0.1f * -90.0f;
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
		//Debug.Log ("Gravity = " + Physics.gravity + " rigidBody.velocity.y = " + rigidBody.velocity.y);
		/* Jumping */
		if (jump && transform.position.y >= 0.0f && Input.GetKey (KeyCode.UpArrow)) {

			if(!isGravity) {
				rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
				rigidBody.AddForce(Vector3.up * 300.0f);
			} else {Debug.Log ("Gravity = " + Physics.gravity + " rigidBody.velocity.y = " + rigidBody.velocity.y);
				rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
				rigidBody.AddForce(Vector3.down * 300.0f);
			}
			jump = true;
			touchesCube = false;
		}
		
		if (!touchesCube) { //(rigidBody.velocity.y != 0.0f) {
			jump = false;
		} else {
			jump = true;
		}
		
		if (GameRepository.getPlayerLife() <= 0.0f) {
			Destroy(this.gameObject);
		}
	}
	
	
	private void reflectPlayer() {
		if(Input.GetKeyDown(KeyCode.LeftArrow) && (!rotate)){// && !Input.GetKeyDown (KeyCode.RightArrow) && !Input.GetKey (KeyCode.RightArrow)) {
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			playerDirection = false;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow) && (!rotate)){// && !Input.GetKeyDown (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.LeftArrow)) {
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			playerDirection = true;
		}
		if (isGravity) {
			transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z);
		} else {
			transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
		}
	}
	
	
	void FixedUpdate () {
		
	}

	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "StaticCube") {
			Vector3 hit = other.contacts [0].normal;
			
			float angle = Vector3.Angle (hit, Vector3.up);
			//Debug.Log(Vector3.Dot (hit, Vector3.up));
			if (Vector3.Dot (hit, Vector3.up) > 0) { // top
				touchesCube = true;
				//rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
				//Debug.Log("Top" + hit);
			} else if (Vector3.Dot (hit, Vector3.up) < 0) {
				touchesCube = true;
				//rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
				//Debug.Log ("Bottom" + hit);
			} else if (Vector3.Dot (hit, Vector3.up) == 0) {
				touchesCube = true;
				//Debug.Log ("Sides" + hit);
				//rigidBody.isKinematic = true;
				//rigidBody.velocity = Vector3.zero;
			}
		}
		if (other.gameObject.tag == "MovableCube") {
			Vector3 hit = other.contacts [0].normal;
			
			float angle = Vector3.Angle (hit, Vector3.up);
			//Debug.Log(Vector3.Dot (hit, Vector3.up));
			if (Vector3.Dot (hit, Vector3.up) > 0) { // top
				touchesCube = true;
				//rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
				//Debug.Log("Top" + hit);
			} else if (Vector3.Dot (hit, Vector3.up) < 0) {
				touchesCube = true;
				//rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
				//Debug.Log ("Bottom" + hit);
			} else if (Vector3.Dot (hit, Vector3.up) == 0) {
				//Debug.Log ("Sides" + hit);
				//rigidBody.isKinematic = true;
				//rigidBody.velocity = Vector3.zero;
			}
		}
		if (other.gameObject.tag == "Enemy") {
			Vector3 hit = other.contacts [0].normal;

			float angle = Vector3.Angle (hit, Vector3.up);

			if (Vector3.Dot (hit, Vector3.up) > 0) {

			} else if (Vector3.Dot (hit, Vector3.up) < 0) {

			} else if (Vector3.Dot (hit, Vector3.up) == 0) {
				Vector3 norm; 
				Vector3 push = new Vector3(0.0f, 0.0f, 0.0f);

				norm = (transform.position - other.transform.position).normalized;
				if(norm.x > 0) {
					push = new Vector3(1.0f, 0.0f, 0.0f);
					rigidBody.AddForce(transform.up * 45.0f);
					rigidBody.AddForce(transform.right * 150.0f);
				}
				if(norm.x < 0) {
					push = new Vector3(-1.0f, 0.0f, 0.0f);
					rigidBody.AddForce(transform.up * 45.0f);
					rigidBody.AddForce(-transform.right * 150.0f);
				}
				if(norm.z > 0) {
					push = new Vector3(0.0f, 0.0f, 1.0f);
				}
				if(norm.z < 0) {
					push = new Vector3(0.0f, 0.0f, -1.0f);
				}
				//Debug.Log ("hit " + norm);


			}
		}
	}



	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Coin") {
			GameRepository.setScore(10.0f);
			other.gameObject.SetActive (false);
		} else if (other.gameObject.tag == "Gravity") {
			if(isGravity) {
				isGravity = false;
			} else {
				isGravity = true;
			}
			reverseGravity ();
			other.gameObject.SetActive (false);
		} else if (other.gameObject.tag == "Health") {
			//TODO add health
			other.gameObject.SetActive (false);
			GameRepository.setPlayerLife(GameRepository.getPlayerLife() + 200.0f);
		}
	}

	private void createBullet() {
		Vector3 position = this.gameObject.transform.position;
		float xShift = 0.0f;
		float zShift = 0.0f;
		float yShift = 0.2f;
		
		if (playerDirection) {
			if (GameRepository.getCurrentDimension () == Dimension.FRONT) {
				xShift = 1.2f;
			} else if (GameRepository.getCurrentDimension () == Dimension.BACK) {
				xShift = -1.2f;
			} else if (GameRepository.getCurrentDimension () == Dimension.RIGHT) {
				zShift = 1.2f;
			} else if (GameRepository.getCurrentDimension () == Dimension.LEFT) {
				zShift = -1.2f;
			}
		} else {
			if (GameRepository.getCurrentDimension () == Dimension.FRONT) {
				xShift = -1.2f;
			} else if (GameRepository.getCurrentDimension () == Dimension.BACK) {
				xShift = 1.2f;
			} else if (GameRepository.getCurrentDimension () == Dimension.RIGHT) {
				zShift = -1.2f;
			} else if (GameRepository.getCurrentDimension () == Dimension.LEFT) {
				zShift = 1.2f;
			}
		}
		Vector3 bulletPosition = new Vector3(position.x + xShift, position.y + yShift, position.z+zShift);
		
		Transform newBullet = Instantiate (energyBullet, bulletPosition, Quaternion.identity) as Transform;
		newBullet.tag = "EnergyBullet";
	}

	public void reverseGravity()
	{
		gravity = -gravity;



		rigidBody.AddForce(-1.8f * Physics.gravity);
		//Physics.gravity = new Vector3(0f, gravity, 0f);
	}
}