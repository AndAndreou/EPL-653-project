using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	//private GameRepository repository;
	private GameObject player;
	private Renderer renderer;
	private GameObject camera;
	private Dimension cubeDimension;


	// Use this for initialization
	void Start () {

		renderer = this.GetComponent<Renderer> ();
		player = GameObject.FindGameObjectWithTag(("Player"));
		camera = GameObject.FindGameObjectWithTag(("MainCamera"));

		MeshFilter meshFilter = GetComponent<MeshFilter>();
		Mesh mesh = GetComponent<Mesh>();
		if (meshFilter != null) {
			mesh = meshFilter.mesh;
		}
		
		if (mesh == null || mesh.uv.Length != 24) {
			Debug.Log("Script needs to be attached to built-in cube");
			return;
		} 
		
		mesh.uv = setUVS(mesh);

		if (this.tag == "StaticCube") {
			renderer.material = Resources.Load<Material> ("cubeMaterial_purple");
		}
		else {
			renderer.material = Resources.Load<Material> ("cubeMaterial4");
		}

	}

	private Vector2[] setUVS(Mesh mesh) {
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

		return uvs;
	}

	// Update is called once per frame
	void Update () {
		if (GameRepository.isPaused()) {
			return;
		}

		Vector3 playerPosition = new Vector3 (Mathf.RoundToInt (player.transform.position.x), 
		                                      Mathf.RoundToInt (player.transform.position.y), 
		                                      Mathf.RoundToInt (player.transform.position.z));

		if (GameRepository.isRaised() || GameRepository.isRotating() ) {
			renderer.enabled = true;
			/*if (playerPosition.y < this.transform.position.y) {
				Color textColor = this.gameObject.GetComponent<Renderer> ().material.color;
				textColor.a = 0.4f;
				this.gameObject.GetComponent<Renderer>().material.color = textColor;
			}
			else {
				Color textColor = this.gameObject.GetComponent<Renderer> ().material.color;
				textColor.a = 1f;
				this.gameObject.GetComponent<Renderer>().material.color = textColor;
			}*/
			return;
		}


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
