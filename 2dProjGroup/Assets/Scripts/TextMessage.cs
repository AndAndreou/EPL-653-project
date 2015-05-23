using UnityEngine;
using System.Collections;

public class TextMessage : MonoBehaviour {
	//private GameRepository repository;
	private GameObject player;
	private Renderer renderer;
	private GameObject camera;

	private Dimension textDimension;
	private float appearanceDistance;
	
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

		//appearing when the player approaches
		if (renderer.enabled == true) {
			float distanceFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);

			if (distanceFromPlayer>appearanceDistance) {
				distanceFromPlayer = 20;
			}

			float newTransparencyValue = 0.9f - (distanceFromPlayer/10);
			if (newTransparencyValue < 0) {
				newTransparencyValue = 0;
			}

			Color textColor = this.gameObject.GetComponent<TextMesh> ().color;
			textColor.a = newTransparencyValue;
			this.gameObject.GetComponent<TextMesh>().color = textColor;
		}


		Vector3 playerPosition = new Vector3 (Mathf.RoundToInt (player.transform.position.x), 
		                                      Mathf.RoundToInt (player.transform.position.y), 
		                                      Mathf.RoundToInt (player.transform.position.z));

		if (GameRepository.getCurrentDimension () == this.textDimension) {
			if ((GameRepository.getCurrentDimension () == Dimension.FRONT) || (GameRepository.getCurrentDimension () == Dimension.BACK)) { //dimension cube = 0
				if (playerPosition.z == this.transform.position.z) {
					renderer.enabled = true;
				} else {
					renderer.enabled = false;
				}
			} else {
				if (playerPosition.x == this.transform.position.x) {
					renderer.enabled = true;
				} else {
					renderer.enabled = false;
				}
			}
		} else {
			renderer.enabled = false;
		}
	}


	//setters and getters
	
	public Dimension getDimension() {
		return this.textDimension;
	}
	
	public void setDimension(Dimension cubeDimension) {
		this.textDimension = cubeDimension;
	}

	
	public float getAppearanceDistance() {
		return this.appearanceDistance;
	}
	
	public void setAppearanceDistance(float appearanceDistance) {
		this.appearanceDistance = appearanceDistance;
	}
}
