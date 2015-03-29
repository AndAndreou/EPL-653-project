using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneInit : MonoBehaviour {
	public Material cubeMaterial;
	private GameRepository repository;
	//
	public Transform Enemy;
	private Dimension prevDiv; //karata to proigoumeno dimension
	private int count=-15;	//metra ta cube p mpikan stin idia diastasi
	List<Vector3> listPosEnemy = new List<Vector3>();
	private int numOfEnemy; //arithmos ton exthron
	//

	// Use this for initialization
	void Start() {
		repository = GameRepository.getInstance();

		int x=0, y=0, z=0;

		//dimension FRONT

		Vector3 position = new Vector3(-5,0,0);
		Vector3 size = new Vector3(1,1,1);
		for (y=0; y<4; y++) {
			position.y = y;
			createStaticCube(position, size, Dimension.FRONT, Color.red);
		}

		position = new Vector3(0,0,0);
		for (x=-5; x<55; x++) {
			position.x = x;
			createStaticCube(position, size, Dimension.FRONT, Color.red);
		}

		position.x = 10;
		position.y = 1;
		createStaticCube (position, size, Dimension.FRONT, Color.red);

		//dimension RIGHT

		position = new Vector3(x,0,0);
		for (z=0; z<=15; z++) {
			position.z = z;
			createStaticCube(position, size, Dimension.RIGHT, Color.red);
		}

		position.y = 1;
		position.z = 5;
		createStaticCube (position, size, Dimension.RIGHT, Color.red);

		position.z = 10;
		createMovableCube(position, size, Dimension.RIGHT, Color.red);

		position.z = 15;
		createStaticCube(position, size, Dimension.RIGHT, Color.red);

		position.y = 2;
		createStaticCube(position, size, Dimension.RIGHT, Color.red);

		for (z=15; z<25; z++) {
			position.z = z;
			createStaticCube(position, size, Dimension.RIGHT, Color.red);
		}


		//dimension BACK

		for (;x>40;x--) {
			position.x = x;
			createStaticCube(position, size, Dimension.BACK, Color.red);
		}

		position.y--;
		createStaticCube (position, size, Dimension.BACK, Color.red);
		position.y--;
		createStaticCube (position, size, Dimension.BACK, Color.red);

		for (;x>40;x--) {
			position.x = x;
			createStaticCube(position, size, Dimension.BACK, Color.red);
		}

		//

		numOfEnemy = Random.Range (3, listPosEnemy.Count / 10); //tixeos arithmos exthron
		for (int i=0; i<numOfEnemy; i++) { //dimiourgia ekthron
			int p = Random.Range(0, listPosEnemy.Count);
			Transform  newEnemy = Instantiate(Enemy,listPosEnemy[p],Quaternion.identity ) as Transform;
			newEnemy.tag="Enemy";
			listPosEnemy.RemoveRange(p-2,5);
			/*listPosEnemy.RemoveAt(p-1);
			listPosEnemy.RemoveAt(p);
			listPosEnemy.RemoveAt(p+1);*/
		}
		Debug.Log("-----" + numOfEnemy);
		//
	}

	/*
	 * Function that creates a static cube in the scene
	 * 
	 * Parameters:
	 * - Position: the position of the cube defeined as a vector
	 * - Size: the size of the cube defined as a vector
	 * - Dimension: the dimension of the cube
	 * - Color: the color of the cube
	 */
	private void createStaticCube(Vector3 position, Vector3 size, Dimension dimension, Color color) {
		//
		if ((prevDiv != dimension)&&(count>=0)) { //se kathe alagi tou divension midenizete to count , to (count>=0) xrismopoite gia tin arxi, na min ginonte spawn enemy konta s stin arxi
			count=0;
		}
		prevDiv = dimension;
		count++;
		if (count >= 5) {
			listPosEnemy.Add(new Vector3(position.x,position.y+3,position.z));
		}
		//
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

		Vector3 scale = transform.localScale;
		scale.x = size.x;
		scale.y = size.y;
		scale.z = size.z;

		Rigidbody rigidBody = cube.AddComponent<Rigidbody>();
		rigidBody.isKinematic = true;
		rigidBody.useGravity = false;

		cube.transform.position = new Vector3(position.x, position.y, position.z);
		cube.transform.localScale = scale;
		cube.AddComponent<Cube>();
		cube.tag = "StaticCube";

		Renderer renderer = cube.GetComponent<Renderer> ();
		//renderer.material = cubeMaterial;
		renderer.material.color = new Color (10, 10, 10, 1.0f);

		if (dimension==Dimension.FRONT){
			cube.transform.Rotate(new Vector3(0.0f,0.0f,0.0f));
		}
		else if (dimension==Dimension.BACK){
			cube.transform.Rotate(new Vector3(0.0f,-180.0f,0.0f)); //180
		}
		else if (dimension==Dimension.RIGHT){
			cube.transform.Rotate(new Vector3(0.0f,-90.0f,0.0f)); //270
		}
		else if (dimension==Dimension.LEFT){
			cube.transform.Rotate(new Vector3(0.0f,270.0f,0.0f)); //90
		}
	}


	/*
	 * Function that creates a static moveable in the scene
	 * 
	 * Parameters:
	 * - Position: the position of the cube defeined as a vector
	 * - Size: the size of the cube defined as a vector
	 * - Dimension: the dimension of the cube
	 * - Color: the color of the cube
	 */
	private void createMovableCube(Vector3 position, Vector3 size, Dimension dimension, Color color) {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		
		Vector3 scale = transform.localScale;
		scale.x = size.x;
		scale.y = size.y;
		scale.z = size.z;
		
		Rigidbody rigidBody = cube.AddComponent<Rigidbody>();
		rigidBody.isKinematic = false;
		rigidBody.useGravity = true;

		Renderer renderer = cube.GetComponent<Renderer> ();
		//renderer.material = cubeMaterial;
		renderer.material.color = new Color (1, 1, 20, 1.0f);

		if (dimension == Dimension.FRONT || dimension == Dimension.BACK) {
			rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
		}
		else if (dimension == Dimension.RIGHT || dimension == Dimension.LEFT) {
			rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
		}

		cube.transform.position = new Vector3(position.x, position.y, position.z);
		cube.transform.localScale = scale;
		cube.tag = "MovableCube";


	}

	private void createText() {
		GameObject Text = new GameObject();

	}
	
	// Update is called once per frame
	void Update () {

	}
}
