﻿using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	private GameRepository repository;
	private GameObject player;
	private Renderer renderer;
	private GameObject camera;
	private Dimension cubeDimension;

	// Use this for initialization
	void Start () {
		repository = GameRepository.Instance;
		renderer = this.GetComponent<Renderer> ();
		player = GameObject.FindGameObjectWithTag(("Player"));
		camera = GameObject.FindGameObjectWithTag(("MainCamera"));
		//cubeDimension = Dimension.BACK;
		
		// Nicolas
		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh mesh = GetComponent<Mesh>();
		if (mf != null)
			mesh = mf.mesh;
		
		if (mesh == null || mesh.uv.Length != 24) {
			Debug.Log("Script needs to be attached to built-in cube");
			return;
		}
		
		Vector2[] uvs = mesh.uv;
		
		// Front
		uvs[0]  = new Vector2(0.0f, 0.0f);
		uvs[1]  = new Vector2(1f / 3f, 0.0f);
		uvs[2]  = new Vector2(0.0f, 1f / 3f);
		uvs[3]  = new Vector2(1f / 3f, 1f / 3f);
		
		// Top
		uvs[8]  = new Vector2(1f / 3f, 0.0f);
		uvs[9]  = new Vector2(2f / 3f, 0.0f);
		uvs[4]  = new Vector2(1f / 3f, 1f / 3f);
		uvs[5]  = new Vector2(2f / 3f, 1f / 3f);
		
		// Back
		uvs[10] = new Vector2(2f / 3f, 0.0f);
		uvs[11] = new Vector2(1.0f, 0.0f);
		uvs[6]  = new Vector2(2f / 3f, 1f / 3f);
		uvs[7]  = new Vector2(1.0f, 1f / 3f);
		
		// Bottom
		uvs[12] = new Vector2(0.0f, 1f / 3f);
		uvs[14] = new Vector2(1f / 3f, 1f / 3f);
		uvs[15] = new Vector2(0.0f, 2f / 3f);
		uvs[13] = new Vector2(1f / 3f, 2f / 3f);   
		// Left
		uvs[16] = new Vector2(1f / 3f, 1f / 3f);
		uvs[18] = new Vector2(2f / 3f, 1f / 3f);
		uvs[19] = new Vector2(1f / 3f, 2f / 3f);
		uvs[17] = new Vector2(2f / 3f, 2f / 3f);    
		
		// Right        
		uvs[20] = new Vector2(2f / 3f, 1f / 3f);
		uvs[22] = new Vector2(1.00f, 1f / 3f);
		uvs[23] = new Vector2(2f / 3f, 2f / 3f);
		uvs[21] = new Vector2(1.0f, 2f / 3f);    
		
		mesh.uv = uvs;
		
		renderer.material = Resources.Load<Material> ("cubeMaterial4");
		// Nicolas
	}
	
	// Update is called once per frame
	void Update () {
		if (repository.isPaused()) {
			return;
		}

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

	public Dimension getDimension() {
		return this.cubeDimension;
	}

	public void setDimension(Dimension cubeDimension) {
		this.cubeDimension = cubeDimension;
	}
}
