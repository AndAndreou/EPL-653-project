using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
	//private GameRepository repository;
	private GameObject player;
	private Renderer renderer;
	private GameObject camera;
	
	// Use this for initialization
	void Start () {
		//repository = GameRepository.Instance;
		renderer = this.GetComponent<Renderer> ();
		player = GameObject.FindGameObjectWithTag(("Player"));
		camera = GameObject.FindGameObjectWithTag(("MainCamera"));
	}
	
	// Update is called once per frame
	void Update () {
		if (GameRepository.isPaused()) {
			return;
		}
		
		if (GameRepository.isRaised() || GameRepository.isRotating() ) {
			renderer.enabled = true;
			return;
		}

		Vector3 playerPosition = new Vector3 (Mathf.RoundToInt (player.transform.position.x), 
		                                      Mathf.RoundToInt (player.transform.position.y), 
		                                      Mathf.RoundToInt (player.transform.position.z));
		if ((GameRepository.getCurrentDimension() == Dimension.FRONT) || (GameRepository.getCurrentDimension() == Dimension.BACK)) { //dimension cube = 0
			if (playerPosition.z == this.transform.position.z) {
				renderer.enabled = true;
			}
			else {
				renderer.enabled = false;
			}
		} 
		else  {
			if (playerPosition.x == this.transform.position.x) {
				renderer.enabled = true;
			}
			else {
				renderer.enabled = false;
			}
		}
	}
}
