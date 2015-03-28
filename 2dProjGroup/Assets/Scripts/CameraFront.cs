using UnityEngine;
using System.Collections;

public class CameraFront : MonoBehaviour {
	public Transform player;
	private Transform target;
	private Vector3 targetPosition;
	private bool rotate;
	private float timestamp;
	float startRot = 0.0f;
	private GameRepository repository;
	
	// Use this for initialization
	void Start () {
		repository = GameRepository.getInstance();
		transform.position = new Vector3 (player.position.x, player.position.y, transform.position.z);
		//transform.LookAt (new Vector3(player.position.x, player.position.y,player.position.z), new Vector3(0,1,0));
		target = this.transform;
		rotate = false;
		repository.setCurrentDimension(Dimension.FRONT);
		timestamp = Time.deltaTime;
	}
	
	// Update is called once per frame
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
				//Debug.Log("A RotateSide = " + repository.getCurrentDimension() + " X = " + transform.position.x + " Y = " + transform.position.y + " Z = " + transform.position.z);
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
				//target.position = targetPosition;
				
				//transform.RotateAround(player.position, new Vector3 (0.0f, 1.0f, 0.0f), -90.0f); //ok
				
				
				repository.setCurrentDimension( (Dimension)(((int)repository.getCurrentDimension() + 1)%4));

				Debug.Log("Camera dimension: " + (int)repository.getCurrentDimension());

				rotate = true;
			}
		}

		float trAngles = transform.eulerAngles.y;
		if (rotate) {
			float angle = (Time.deltaTime - timestamp) / 0.1f * 90.0f;
			startRot = startRot + Mathf.Abs (angle);
			if(startRot < 90.0f){
				transform.RotateAround(player.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
			}
			if(startRot >= 90.0f){
				rotate = false;
				startRot = 0.0f;
			}
			
		}
		//transform.RotateAround(player.position, new Vector3 (0.0f, 1.0f, 0.0f), -90.0f * Time.deltaTime);
		//if (rotate) {
		//Debug.Log ("Rotate = " + rotate);
		//transform.rotation = Quaternion.Slerp (transform.rotation, target.transform.rotation, Time.time * 10.0f);
		//transform.position = Vector3.Slerp (transform.position, target.transform.position, Time.time * 10.0f);
		//}
	}
}
