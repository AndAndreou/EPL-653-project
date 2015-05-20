using UnityEngine;
using System.Collections;

public class TextMessage : MonoBehaviour {
	private GameRepository repository;
	private GameObject player;
	private Renderer renderer;
	private GameObject camera;

	private Dimension textDimension;
	
	// Use this for initialization
	void Start () {
		repository = GameRepository.Instance;
		renderer = this.GetComponent<Renderer> ();
		player = GameObject.FindGameObjectWithTag(("Player"));
		camera = GameObject.FindGameObjectWithTag(("MainCamera"));
	}
	
	// Update is called once per frame
	void Update () {
		if (repository.isPaused()) {
			return;
		}
		
		/*if (repository.isRaised() || repository.isRotating() ) {
			renderer.enabled = true;
			return;
		}*/

		if (repository.getCurrentDimension () == this.textDimension) {
			if ((repository.getCurrentDimension () == Dimension.FRONT) || (repository.getCurrentDimension () == Dimension.BACK)) { //dimension cube = 0
				if (player.transform.position.z == this.transform.position.z) {
					renderer.enabled = true;
				} else {
					renderer.enabled = false;
				}
			} else {
				if (player.transform.position.x == this.transform.position.x) {
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
}
