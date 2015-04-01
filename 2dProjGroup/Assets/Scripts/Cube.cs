using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	private GameRepository repository;
	private GameObject player;
	private Renderer renderer;
	private GameObject camera;

	// Use this for initialization
	void Start () {
		repository = GameRepository.getInstance();
		renderer = this.GetComponent<Renderer> ();
		player = GameObject.FindGameObjectWithTag(("Player"));
		camera = GameObject.FindGameObjectWithTag(("MainCamera"));
	}
	
	// Update is called once per frame
	void Update () {

		if (repository.isRaised() || repository.isRotating() ) {
			renderer.enabled = true;
			return;
		}

		if ((repository.getCurrentDimension() == Dimension.FRONT) || (repository.getCurrentDimension() == Dimension.BACK)) { //dimension cube = 0
			if (player.transform.position.z == this.transform.position.z) {
				//Debug.Log("Z - Z");
				renderer.enabled = true;
			}
			else {
				renderer.enabled = false;
				//Debug.Log("Z !- Z");
			}
		} 
		else  {
			if (player.transform.position.x == this.transform.position.x) {
				renderer.enabled = true;
				//Debug.Log("X - X");
			}
			else {
				renderer.enabled = false;
				//Debug.Log("X !- X");
			}
		}

		/*
		int x = Mathf.RoundToInt (player.transform.position.x);
		int z = Mathf.RoundToInt(player.transform.position.z);
		if ( (x == this.transform.position.x) && ( z == this.transform.position.z)) {
			renderer.material.color = Color.black;
		} else if ( ( Mathf.Abs(x - this.transform.position.x) == 1 ) && ( z == this.transform.position.z) ){
			renderer.material.color = Color.gray;
		}
		else if ( ( Mathf.Abs(z - this.transform.position.z) == 1 ) && ( x == this.transform.position.x) ){
			renderer.material.color = Color.gray;
		}
		else if ( ( Mathf.Abs(x - this.transform.position.x) == 2 ) && ( z == this.transform.position.z) ){
			renderer.material.color = Color.white;
		}
		else if ( ( Mathf.Abs(z - this.transform.position.z) == 2 ) && ( x == this.transform.position.x) ){
			renderer.material.color = Color.white;
		}
	else {
			renderer.material.color = new Color (10, 10, 10, 1.0f);
		}*/

	}


	//setters and getters

}
