using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {
	private GameRepository repository;
	bool direction;
	bool rotate;
	private float startRot;
	private float startRotX;
	private float startRotY;
	private float startRotZ;
	private float timestamp;

	// Use this for initialization
	void Start () {
		repository = GameRepository.Instance;
		startRot = 0.0f;
		startRotX = 0.0f;
		startRotY = 0.0f;
		startRotZ = 0.0f;
		direction = false;
		rotate = false;
		timestamp = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		this.gameObject.transform.position = new Vector3 (player.transform.position.x, this.gameObject.transform.position.y, player.transform.position.z);
		if (Input.GetKeyDown (KeyCode.C)) {
			direction = true;
			rotate = true;
		}
		
		if (Input.GetKeyDown (KeyCode.Z)) {
			direction = false;
			rotate = true;
		}

		//float trAngles = transform.eulerAngles.y;
		if (rotate) {
			if(direction){
				float angle = (Time.deltaTime - timestamp) / 0.1f * 90.0f;
				startRot = startRot + Mathf.Abs (angle);
				
				if(startRot < 90.0f){
					transform.RotateAround(player.transform.position, new Vector3 (0.0f, 1.0f, 0.0f), angle);
				}
				if(startRot >= 90.0f){
					rotate = false;
					startRot = 0.0f;
				}
				
			} else {
				float angle = (Time.deltaTime - timestamp) / 0.1f * -90.0f;
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
