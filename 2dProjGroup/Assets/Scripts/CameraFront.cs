using UnityEngine;
using System.Collections;

public class CameraFront : MonoBehaviour {
	public Transform player;
	private Vector3 targetPosition;
	private bool direction;
	//private bool rotate;
	private float timestamp;
	private float startRot;
	private GameRepository repository;

	void Start () {
		repository = GameRepository.getInstance();
		repository.setCurrentDimension(Dimension.FRONT);
		transform.position = new Vector3 (player.position.x, player.position.y, transform.position.z);
		timestamp = Time.deltaTime;
		startRot = 0.0f;
		direction = true;
		//rotate = false;
	}

	void Update () {
		if (!repository.isRotating() && transform.position.y >= 0.0f) {
			if (repository.getCurrentDimension() == Dimension.FRONT || repository.getCurrentDimension() == Dimension.BACK) {
				transform.position = new Vector3 (player.position.x, player.position.y, Mathf.Round (transform.position.z));
			}

			if (repository.getCurrentDimension() == Dimension.RIGHT || repository.getCurrentDimension() == Dimension.LEFT) {
				transform.position = new Vector3 (Mathf.Round (transform.position.x), player.position.y, player.position.z);
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

			if (Input.GetKey (KeyCode.X) && (!repository.isRotating())) {
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

		if (repository.isRaised () && !repository.isRotating ()) {
			float angle = (Time.deltaTime - timestamp) / 0.1f * 90.0f;
			startRot = startRot + Mathf.Abs (angle);
			
			if(startRot < 90.0f){
				transform.RotateAround(player.position, new Vector3 (1.0f, 0.0f, 0.0f), angle);
				transform.RotateAround(player.position, new Vector3 (0.0f, 0.0f, 1.0f), angle);
			}
			if(startRot >= 90.0f){
				repository.setRaise(false);
				startRot = 0.0f;
			}
		}
	}
}
