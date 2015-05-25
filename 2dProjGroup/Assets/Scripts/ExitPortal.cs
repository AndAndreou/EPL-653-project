using UnityEngine;
using System.Collections;

public class ExitPortal : MonoBehaviour {
	private GameObject player;
	private Renderer renderer;
	private GameObject camera;
	private string portalTarget;
	
	// Use this for initialization
	void Start () {
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

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			GameRepository.setPauseScreen(false);
			GameRepository.setMainScreen(true);
			GameRepository.setGameOverScreen(false);
			GameRepository.setPause(true);
			if (GameRepository.isSoundsOn ()) {
				GetComponent<AudioSource>().Play();
			}
			if(this.portalTarget == "CreditsLevel") {
				GameRepository.setPauseScreen(false);
				GameRepository.setMainScreen(false);
				GameRepository.setGameOverScreen(false);
				GameRepository.setCreditsScreen(true);
			}
			AutoFade.LoadLevel(this.portalTarget, 2, 3, Color.black);
		}
	}

	//setters and getters
	public string getPortalTarget() {
		return this.portalTarget;
	}

	public void setPortalTarget(string portalTargetName) {
		this.portalTarget = portalTargetName;
	}

}
