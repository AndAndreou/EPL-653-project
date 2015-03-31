using UnityEngine;
using System.Collections;

public class CameraFront : MonoBehaviour {
	public Transform player;
	private Vector3 targetPosition;
	private bool direction;
	private float timestamp;
	private float startRot;
	private float startRotX;
	private float startRotY;
	private float startRotZ;
	private GameRepository repository;
	private bool wait;
	private bool isUp;
	private int timer;
	
	void Start () {
		repository = GameRepository.getInstance();
		repository.setCurrentDimension(Dimension.FRONT);
		transform.position = new Vector3 (player.position.x, player.position.y, transform.position.z);
		timestamp = Time.deltaTime;
		startRot = 0.0f;
		startRotX = 0.0f;
		startRotY = 0.0f;
		startRotZ = 0.0f;
		direction = true;
		wait = false;
		isUp = false;
		timer = 0;
	}
	
	void Update () {
		if (wait) {
			if (timer == 90) {
				timer = 0;
				wait = false;
				//transform.Translate(new Vector3(-4.0f, -3.0f, -2.0f));
				//Debug.Log("Works Fine?");
				return;
			} else {
				timer++;
				//Debug.Log(timer);
				return;
			}
		}
		
		if (!repository.isRotating() && !repository.isRaised() && transform.position.y >= 0.0f) {
			if (repository.getCurrentDimension() == Dimension.FRONT) {
				transform.position = new Vector3 (player.position.x, player.position.y, Mathf.Round (player.position.z) - 100.0f);
			}
			if (repository.getCurrentDimension() == Dimension.BACK) {
				transform.position = new Vector3 (player.position.x, player.position.y, Mathf.Round (player.position.z) + 100.0f);
			}
			if (repository.getCurrentDimension() == Dimension.RIGHT) {
				transform.position = new Vector3 (Mathf.Round (player.position.x) + 100.0f, player.position.y, player.position.z);
			}
			if (repository.getCurrentDimension() == Dimension.LEFT) {
				transform.position = new Vector3 (Mathf.Round (player.position.x) - 100.0f, player.position.y, player.position.z);
			}
		}
		
		if (transform.position.y >= 0.0f) {
			transform.LookAt (new Vector3 (player.position.x, player.position.y, player.position.z), Vector3.up);
		}
		
		if (transform.position.y >= 0.0f) {
			if (Input.GetKeyDown (KeyCode.C) && (!repository.isRotating())) {
				
				repository.setCurrentDimension( (Dimension)(((int)repository.getCurrentDimension() + 1)%4));
				
				Debug.Log("Camera dimension: " + (int)repository.getCurrentDimension());
				
				repository.setRotate(true);
				direction = true;
			}
			
			if (Input.GetKeyDown (KeyCode.Z) && (!repository.isRotating())) {
				
				repository.setCurrentDimension( (Dimension)(((int)repository.getCurrentDimension() + 2 + 1)%4));
				
				Debug.Log("Camera dimension: " + (int)repository.getCurrentDimension());
				
				repository.setRotate(true);
				direction = false;
			}
			
			if (Input.GetKey (KeyCode.X)) {
				repository.setRaise(true);
			}
		}
		
		//float trAngles = transform.eulerAngles.y;
		if (repository.isRotating()) {
			if(direction){
				float angle = (Time.deltaTime - timestamp) / 0.1f * 90.0f;
				startRot = startRot + Mathf.Abs (angle);
				
				if(startRot < 90.0f){
					transform.RotateAround(player.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
				}
				if(startRot >= 90.0f){
					repository.setRotate(false);
					startRot = 0.0f;
				}
				//Debug.Log("aaa" + transform.eulerAngles);
			} else {
				float angle = (Time.deltaTime - timestamp) / 0.1f * -90.0f;
				startRot = startRot - Mathf.Abs (angle);
				
				if(startRot > -90.0f){
					transform.RotateAround(player.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
				}
				if(startRot <= -90.0f){
					repository.setRotate(false);
					startRot = 0.0f;
				}
			}
		}
		
		if (repository.isRaised() && !repository.isRotating()) {
			//transform.Translate(new Vector3(4.0f, 3.0f, 2.0f));
			//wait = true;
			//repository.setRaise(false);
			float timestamp2 = Time.deltaTime;
			float angleX = (Time.deltaTime - timestamp) / 0.1f * 45.0f;
			float angleY = (Time.deltaTime - timestamp) / 0.1f * 45.0f;
			float angleZ = (Time.deltaTime - timestamp) / 0.1f * 45.0f;
			startRotX = startRotX + Mathf.Abs (angleX);
			startRotY = startRotY + Mathf.Abs (angleY);
			startRotZ = startRotZ + Mathf.Abs (angleZ);
			
			Debug.Log(startRotX + " " + startRotY + " " + startRotZ);
			Debug.Log(transform.eulerAngles);
			
			if(repository.getCurrentDimension() == Dimension.FRONT) {
				if(startRotX < 45.0f){
					transform.RotateAround(player.position, new Vector3 (1.0f, 0.0f, 0.0f), -angleX);
					transform.RotateAround(player.position, new Vector3 (0.0f, 1.0f, 0.0f), angleY);
					transform.RotateAround(player.position, new Vector3 (0.0f, 0.0f, 1.0f), 0);
				}
			}
			
			if(repository.getCurrentDimension() == Dimension.RIGHT) {
				if(startRotX < 45.0f){
					transform.RotateAround(player.position, new Vector3 (1.0f, 0.0f, 0.0f), 0);
					transform.RotateAround(player.position, new Vector3 (0.0f, 1.0f, 0.0f), angleY);
					transform.RotateAround(player.position, new Vector3 (0.0f, 0.0f, 1.0f), -angleZ);
				}
			}
			
			if(repository.getCurrentDimension() == Dimension.BACK) {
				if(startRotX < 45.0f){
					transform.RotateAround(player.position, new Vector3 (1.0f, 0.0f, 0.0f), angleX);
					transform.RotateAround(player.position, new Vector3 (0.0f, 1.0f, 0.0f), angleY);
					transform.RotateAround(player.position, new Vector3 (0.0f, 0.0f, 1.0f), 0);
				}
			}
			
			if(repository.getCurrentDimension() == Dimension.LEFT) {
				if(startRotX < 45.0f){
					transform.RotateAround(player.position, new Vector3 (1.0f, 0.0f, 0.0f), 0);
					transform.RotateAround(player.position, new Vector3 (0.0f, 1.0f, 0.0f), angleY);
					transform.RotateAround(player.position, new Vector3 (0.0f, 0.0f, 1.0f), angleZ);
				}
			}
			
			if(startRotX >= 45.0f){
				repository.setRaise(false);
				startRotX = 0.0f;
				startRotY = 0.0f;
				startRotZ = 0.0f;
				wait = true;
			}
		}
	}
	
}
