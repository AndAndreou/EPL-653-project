using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	GameRepository repository;

	private int dimension;

	// Use this for initialization
	void Start () {
		repository = GameRepository.getInstance();
	}
	
	// Update is called once per frame
	void Update () {

	}


	//setters and getters

	public int getDimension() {
		return this.dimension;
	}

	public void setDimension(int dimension) {
		this.dimension = dimension;
	}
}
