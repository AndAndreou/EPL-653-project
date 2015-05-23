using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {
	bool direction;
	bool rotate;
	private float startRot;
	private float startRotX;
	private float startRotY;
	private float startRotZ;
	private float timestamp;
	private float waterRot;

	// Use this for initialization
	void Start () {
		startRot = 0.0f;
		startRotX = 0.0f;
		startRotY = 0.0f;
		startRotZ = 0.0f;
		direction = false;
		rotate = false;
		timestamp = Time.deltaTime;
		waterRot = transform.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (!rotate && !GameRepository.isRaised ()) {
			if (GameRepository.getCurrentDimension () == Dimension.FRONT) {
				//if (transform.eulerAngles.y > 0.0f && transform.eulerAngles.y < 315.0f) {
					transform.eulerAngles = new Vector3 (waterRot, 0.0f, 0.0f);
					transform.localEulerAngles = new Vector3 (waterRot, 0.0f, 0.0f);
				//}
			}
			if (GameRepository.getCurrentDimension () == Dimension.RIGHT) {
				//if (transform.eulerAngles.y > 270.0f) {
					transform.eulerAngles = new Vector3 (waterRot, 270.0f, 0.0f);
					transform.localEulerAngles = new Vector3 (waterRot, 270.0f, 0.0f);
				//}
			}
			if (GameRepository.getCurrentDimension () == Dimension.BACK) {
				//if (transform.eulerAngles.y > 180.0f) {
					transform.eulerAngles = new Vector3 (waterRot, 180.0f, 0.0f);
					transform.localEulerAngles = new Vector3 (waterRot, 180.0f, 0.0f);
				//}
			}
			if (GameRepository.getCurrentDimension () == Dimension.LEFT) {
				//if (transform.eulerAngles.y > 90.0f) {
					transform.eulerAngles = new Vector3 (waterRot, 90.0f, 0.0f);
					transform.localEulerAngles = new Vector3 (waterRot, 90.0f, 0.0f);
				//}
			}
		}

		this.gameObject.transform.position = new Vector3 (player.transform.position.x, this.gameObject.transform.position.y, player.transform.position.z);
		if (!rotate && !GameRepository.isRaised ()) {
			if (Input.GetKeyDown (KeyCode.C)) {
				direction = true;
				rotate = true;
			}
		
			if (Input.GetKeyDown (KeyCode.Z)) {
				direction = false;
				rotate = true;
			}
		}

		//float trAngles = transform.eulerAngles.y;
		if (rotate) {
			if(direction){
				float angle = (Time.deltaTime - GameRepository.getTimeStamp()) / 0.1f * 90.0f;
				startRot = startRot + Mathf.Abs (angle);
				
				if(startRot < 90.0f){
					transform.RotateAround(player.transform.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
				}
				if(startRot >= 90.0f){
					rotate = false;
					startRot = 0.0f;
				}
				
			} else {
				float angle = (Time.deltaTime - GameRepository.getTimeStamp()) / 0.1f * -90.0f;
				startRot = startRot - Mathf.Abs (angle);
				
				if(startRot > -90.0f){
					transform.RotateAround(player.transform.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
				}
				if(startRot <= -90.0f){
					rotate = false;
					startRot = 0.0f;
				}
			}
		}
	}
}
