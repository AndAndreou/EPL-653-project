using UnityEngine;
using System.Collections;

public class CameraFront : MonoBehaviour {
	public Transform player;
	private Transform target;
	private Vector3 targetPosition;
	private bool rotate;
	private bool direction;
	private float timestamp;
	private float startRot;
	private GameRepository repository;

	void Start () {
		repository = GameRepository.getInstance();
		repository.setCurrentDimension(Dimension.FRONT);
		transform.position = new Vector3 (player.position.x, player.position.y, transform.position.z);
		target = this.transform;
		timestamp = Time.deltaTime;
		startRot = 0.0f;
		direction = true;
		rotate = false;
	}

	void Update () {
		if (!rotate && transform.position.y >= 0.0f) {
			if (repository.getCurrentDimension() == Dimension.FRONT || repository.getCurrentDimension() == Dimension.BACK) {
				transform.position = new Vector3 (player.position.x, player.position.y, Mathf.Round (transform.position.z));
			}
			if (repository.getCurrentDimension() == Dimension.RIGHT || repository.getCurrentDimension() == Dimension.LEFT) {
				transform.position = new Vector3 (Mathf.Round (transform.position.x), player.position.y, player.position.z);
			}
		}
		if (transform.position.y >= 0.0f) {
			transform.LookAt (new Vector3 (player.position.x, player.position.y, player.position.z), new Vector3 (0, 1, 0));
		}
		
		if (transform.position.y >= 0.0f) {
			if (Input.GetKeyDown (KeyCode.C) && (!rotate)) {
				targetPosition = new Vector3 (0.0f, 0.0f, 0.0f);

				if (repository.getCurrentDimension() == Dimension.FRONT) {
					targetPosition = new Vector3 (transform.position.x + 10.0f, transform.position.y, transform.position.z + 10.0f);
				}
				if (repository.getCurrentDimension() == Dimension.RIGHT) {
					targetPosition = new Vector3 (transform.position.x - 10.0f, transform.position.y, transform.position.z + 10.0f);
				}
				if (repository.getCurrentDimension() == Dimension.BACK) {
					targetPosition = new Vector3 (transform.position.x - 10.0f, transform.position.y, transform.position.z - 10.0f);
				}
				if (repository.getCurrentDimension() == Dimension.LEFT) {
					targetPosition = new Vector3 (transform.position.x + 10.0f, transform.position.y, transform.position.z - 10.0f);
				}

				repository.setCurrentDimension( (Dimension)(((int)repository.getCurrentDimension() + 1)%4));

				Debug.Log("Camera dimension: " + (int)repository.getCurrentDimension());

				rotate = true;
			}
		}

		float trAngles = transform.eulerAngles.y;
		if (rotate) {
			if(direction) {
				float angle = (Time.deltaTime - timestamp) / 0.1f * 90.0f;
				startRot = startRot + Mathf.Abs (angle);
				if(startRot < 90.0f){
					transform.RotateAround(this.transform.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
				}
				if(startRot >= 90.0f){
					rotate = false;
					startRot = 0.0f;
				}
			} else {
				float angle = (Time.deltaTime - timestamp) / 0.1f * -90.0f;
				startRot = startRot + Mathf.Abs (angle);
				if(startRot < 90.0f){
					transform.RotateAround(this.transform.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
				}
				if(startRot >= 90.0f){
					rotate = false;
					startRot = 0.0f;
				}
			}
		}
	}

	bool isRotating () {
		return rotate;
	}
}
