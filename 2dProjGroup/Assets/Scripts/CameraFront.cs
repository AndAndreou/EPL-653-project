using UnityEngine;
using System.Collections;

public class CameraFront : MonoBehaviour {
	public Transform player;
	private Vector3 targetPosition;
	private bool direction;
	//private float timestamp;
	private float startRot;
	private float startRotX;
	private float startRotY;
	private float startRotZ;
	//private GameRepository repository;
	private bool wait;
	private bool isUp;
	private int timer;
	private AudioSource sound;
	private int musicCounter;
	
	void Start () {
		sound = gameObject.GetComponent<AudioSource>();
		//repository = GameRepository.Instance;
		GameRepository.setCurrentDimension(Dimension.FRONT);
		transform.position = new Vector3 (player.position.x, player.position.y, transform.position.z);
		//timestamp = Time.deltaTime;
		startRot = 0.0f;
		startRotX = 0.0f;
		startRotY = 0.0f;
		startRotZ = 0.0f;
		direction = true;
		wait = false;
		isUp = false;
		timer = 0;
		musicCounter = 0;
		Debug.Log ("Started");
	}

	void Update () {
		if (GameRepository.isMusicOn () && (musicCounter % 2 == 0)) {
			sound.Play ();
			musicCounter++;
		} else if (!GameRepository.isMusicOn () && (musicCounter % 2 == 1)) {
			sound.Pause ();
			musicCounter++;
		}

		if (GameRepository.isPaused()) {
			return;
		}

		if (wait) {
			if (timer == 90) {
				timer = 0;
				wait = false;
				isUp = true;
				//transform.Translate(new Vector3(-4.0f, -3.0f, -2.0f));
				//Debug.Log("Works Fine?");
				return;
			} else {
				timer++;
				//Debug.Log(timer);
				return;
			}
		}
		
		if (isUp) {
			//transform.Translate(new Vector3(4.0f, 3.0f, 2.0f));
			//wait = true;
			//repository.setRaise(false);
			//float timestamp2 = Time.deltaTime;
			//float angle = (Time.deltaTime - timestamp) / 0.1f * 45.0f;
			//startRotX = startRotX + Mathf.Abs (angle);
			
			//Debug.Log("WWW" + startRotX + " " + startRotY + " " + startRotZ);
			//Debug.Log("WWW" + transform.localEulerAngles);
			//Debug.Log(" " + startRotX + " " + isUp + " " + repository.isRaised());
			if(GameRepository.getCurrentDimension() == Dimension.FRONT) {
				transform.RotateAround (player.position,new Vector3(-1.0f,1.0f,0.0f), 45 * Time.deltaTime * 2.0f);
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
				
				//if(Mathf.Approximately(Mathf.Round(transform.eulerAngles.y), 0.0f)){
				if(transform.eulerAngles.y > 0.0f && transform.eulerAngles.y < 315.0f) {
					transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
					transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
					transform.position = new Vector3 (player.position.x, player.position.y, Mathf.Round (player.position.z) - 100.0f);
					//Debug.Log("Is it? is it?");
					isUp = false;
					GameRepository.setRaise(false);
					//return;
				} else {
					return;
				}
			}
			
			if(GameRepository.getCurrentDimension() == Dimension.RIGHT) {
				transform.RotateAround (player.position,new Vector3(0.0f,1.0f,-1.0f), 45 * Time.deltaTime * 2.0f);
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
				
				//if(Mathf.Approximately(Mathf.Round(transform.eulerAngles.y), 270.0f)){
				if(transform.eulerAngles.y > 270.0f) {
					transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
					transform.localEulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
					transform.position = new Vector3 (Mathf.Round (player.position.x) + 100.0f, player.position.y, player.position.z);
					isUp = false;
					GameRepository.setRaise(false);
				} else {
					return;
				}
			}
			
			if(GameRepository.getCurrentDimension() == Dimension.BACK) {
				transform.RotateAround (player.position,new Vector3(1.0f,1.0f,0.0f), 45 * Time.deltaTime * 2.0f);
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
				
				//if(Mathf.Approximately(Mathf.Round(transform.eulerAngles.y), 180.0f)){
				if(transform.eulerAngles.y > 180.0f) {
					transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
					transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
					transform.position = new Vector3 (player.position.x, player.position.y, Mathf.Round (player.position.z) + 100.0f);
					isUp = false;
					GameRepository.setRaise(false);
				} else {
					return;
				}}
			
			if(GameRepository.getCurrentDimension() == Dimension.LEFT) {
				transform.RotateAround (player.position,new Vector3(0.0f,1.0f,1.0f), 45 * Time.deltaTime * 2.0f);
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
				
				//if(Mathf.Approximately(Mathf.Round(transform.eulerAngles.y), 90.0f)){
				if(transform.eulerAngles.y > 90.0f) {
					transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
					transform.localEulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
					transform.position = new Vector3 (Mathf.Round (player.position.x) - 100.0f, player.position.y, player.position.z);
					isUp = false;
					GameRepository.setRaise(false);
				} else {
					return;
				}
			}
		}
		//
		//Debug.Log ("isRunning " + repository.isRaised() + " " + isUp);
		if (!GameRepository.isRotating() && !GameRepository.isRaised() && transform.position.y >= 0.0f) {
			if (GameRepository.getCurrentDimension() == Dimension.FRONT) {
				transform.position = new Vector3 (player.position.x, player.position.y, Mathf.Round (player.position.z) - 100.0f);
			}
			if (GameRepository.getCurrentDimension() == Dimension.BACK) {
				transform.position = new Vector3 (player.position.x, player.position.y, Mathf.Round (player.position.z) + 100.0f);
			}
			if (GameRepository.getCurrentDimension() == Dimension.RIGHT) {
				transform.position = new Vector3 (Mathf.Round (player.position.x) + 100.0f, player.position.y, player.position.z);
			}
			if (GameRepository.getCurrentDimension() == Dimension.LEFT) {
				transform.position = new Vector3 (Mathf.Round (player.position.x) - 100.0f, player.position.y, player.position.z);
			}
		}
		
		if (transform.position.y >= 0.0f) {
			transform.LookAt (new Vector3 (player.position.x, player.position.y, player.position.z), Vector3.up);
		}
		
		if (transform.position.y >= 0.0f) {
			if (Input.GetKeyDown (KeyCode.C) && (!GameRepository.isRotating())) {
				
				GameRepository.setCurrentDimension( (Dimension)(((int)GameRepository.getCurrentDimension() + 1)%4));
				
				//Debug.Log("Camera dimension: " + (int)repository.getCurrentDimension());
				
				GameRepository.setRotate(true);
				direction = true;
			}
			
			if (Input.GetKeyDown (KeyCode.Z) && (!GameRepository.isRotating())) {
				
				GameRepository.setCurrentDimension( (Dimension)(((int)GameRepository.getCurrentDimension() + 2 + 1)%4));
				
				//Debug.Log("Camera dimension: " + (int)repository.getCurrentDimension());
				
				GameRepository.setRotate(true);
				direction = false;
			}
			
			if (Input.GetKey (KeyCode.X)) {
				GameRepository.setRaise(true);
			}
		}
		
		//float trAngles = transform.eulerAngles.y;
		if (GameRepository.isRotating()) {
			if(direction){
				float angle = (Time.deltaTime - GameRepository.getTimeStamp()) / 0.1f * 90.0f;
				startRot = startRot + Mathf.Abs (angle);
				
				if(startRot < 90.0f){
					transform.RotateAround(player.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
				}
				if(startRot >= 90.0f){
					GameRepository.setRotate(false);
					startRot = 0.0f;
				}
				
			} else {
				float angle = (Time.deltaTime - GameRepository.getTimeStamp()) / 0.1f * -90.0f;
				startRot = startRot - Mathf.Abs (angle);
				
				if(startRot > -90.0f){
					transform.RotateAround(player.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
				}
				if(startRot <= -90.0f){
					GameRepository.setRotate(false);
					startRot = 0.0f;
				}
			}
		}
		
		if (GameRepository.isRaised() && !GameRepository.isRotating()) {
			//transform.Translate(new Vector3(4.0f, 3.0f, 2.0f));
			//wait = true;
			//repository.setRaise(false);
			float timestamp2 = Time.deltaTime;
			float angle = (Time.deltaTime - GameRepository.getTimeStamp()) / 0.1f * 45.0f;
			startRotX = startRotX + Mathf.Abs (angle);
			
			//Debug.Log("EEE" + startRotX + " " + startRotY + " " + startRotZ);
			//Debug.Log("EEE" + transform.localEulerAngles + "----"+ transform.eulerAngles);
			//Debug.Log("EEE" + startRotX + " " + isUp + " " + repository.isRaised());
			
			if(GameRepository.getCurrentDimension() == Dimension.FRONT) {
				transform.RotateAround (player.position,new Vector3(1.0f,-1.0f,0.0f), 45 * timestamp2 * 2.0f);
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
				
				//if(Mathf.Approximately(Mathf.Round(transform.eulerAngles.y), 315.0f)){
				if(transform.eulerAngles.y < 315.0f) {
					wait = true;
				}
			}
			
			if(GameRepository.getCurrentDimension() == Dimension.RIGHT) {
				transform.RotateAround (player.position,new Vector3(0.0f,-1.0f,1.0f), 45 * timestamp2 * 2.0f);
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
				
				//if(Mathf.Approximately(Mathf.Round(transform.eulerAngles.y), 225.0f)){
				if(transform.eulerAngles.y < 225.0f) {
					wait = true;
				}
			}
			
			if(GameRepository.getCurrentDimension() == Dimension.BACK) {
				transform.RotateAround (player.position,new Vector3(-1.0f,-1.0f,0.0f), 45 * timestamp2 * 2.0f);
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
				
				//if(Mathf.Approximately(Mathf.Round(transform.eulerAngles.y), 135.0f)){
				if(transform.eulerAngles.y < 135.0f) {
					wait = true;
				}
			}
			
			if(GameRepository.getCurrentDimension() == Dimension.LEFT) {
				transform.RotateAround (player.position,new Vector3(0.0f,-1.0f,-1.0f), 45 * timestamp2 * 2.0f);
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
				
				//if(Mathf.Approximately(Mathf.Round(transform.eulerAngles.y), 45.0f)){
				if(transform.eulerAngles.y < 45.0f) {
					wait = true;
				}
			}
		}
	}
	
}
