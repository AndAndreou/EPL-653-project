using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneInit : MonoBehaviour {
	public Material cubeMaterial;
	private GameRepository repository;
	//
	public Transform Enemy;
	public Transform coin;

	private Dimension prevDim; //karata to proigoumeno dimension
	private int count=-15;	//metra ta cube p mpikan stin idia diastasi
	private int countOfList=0; //counter gia to poses listes exoume
	List<Vector3> listPosEnemy = new List<Vector3>();
	List<List<Vector3>> listOfList = new List<List<Vector3>>();
	private int numOfEnemy; //arithmos ton exthron
	//

	// Use this for initialization
	void Start() {
		repository = GameRepository.getInstance();

		listOfList.Add(new List<Vector3>());

		int x=0, y=0, z=0;

		//CREATING THE SCENE

		//dimension FRONT

		prevDim = Dimension.FRONT;

		Vector3 size = new Vector3(1,1,1);
		Vector3 position = new Vector3(0,0,0);

		position = new Vector3 (-5,0,0);
		for (; position.y<4; position.y++) {
			createStaticCube(position, size, Dimension.FRONT, Color.red);
		}

		position = new Vector3(-5,0,0);
		for (; position.x<55; position.x++) {
			createStaticCube(position, size, Dimension.FRONT, Color.red);
		}

		position = new Vector3 (10,1,0);
		createStaticCube (position, size, Dimension.FRONT, Color.red);

		position = new Vector3 (30,1.5f,0);
		for (; position.x<40; position.x = position.x +2) {
			createCoin (position);
		}

		position = new Vector3 (50,0,1);
		for (; position.x<55; position.x++) {
			createStaticCube(position, size, Dimension.FRONT, Color.red);
		}

		position = new Vector3 (51,0,2);
		for (; position.x<55; position.x++) {
			createStaticCube(position, size, Dimension.FRONT, Color.red);
		}

		position = new Vector3 (52,0,3);
		for (; position.x<55; position.x++) {
			createStaticCube(position, size, Dimension.FRONT, Color.red);
		}

		position = new Vector3 (53,0,4);
		for (; position.x<55; position.x++) {
			createStaticCube(position, size, Dimension.FRONT, Color.red);
		}

		position = new Vector3 (54,0,5);
		createStaticCube(position, size, Dimension.FRONT, Color.red);


		//dimension RIGHT

		position = new Vector3(55,0,0);
		for (; position.z<=15; position.z++) {
			createStaticCube(position, size, Dimension.RIGHT, Color.red);
		}

		position = new Vector3(55,1,5);
		createStaticCube (position, size, Dimension.RIGHT, Color.red);

		position = new Vector3(55,1,10);
		createMovableCube(position, size, Dimension.RIGHT, Color.red);

		position = new Vector3(55,1,15);
		createStaticCube(position, size, Dimension.RIGHT, Color.red);

		position = new Vector3(55,2,15);
		createStaticCube(position, size, Dimension.RIGHT, Color.red);

		position = new Vector3(55,2,15);
		for (; position.z<25; position.z++) {
			createStaticCube(position, size, Dimension.RIGHT, Color.red);
		}

		position = new Vector3(55,2,15);
		for (; position.x>45; position.x--) {
			createStaticCube(position, size, Dimension.RIGHT, Color.red);
		}

		position = new Vector3(45,2,15);
		for (; position.z<25; position.z++) {
			createStaticCube(position, size, Dimension.RIGHT, Color.red);
		}


		//dimension BACK

		position = new Vector3(55,2,25);
		for (; position.x>35; position.x--) {
			createStaticCube(position, size, Dimension.BACK, Color.red);
		}

		position = new Vector3(50,3,25);
		createStaticCube(position, size, Dimension.BACK, Color.red);
		position.y++;
		createStaticCube(position, size, Dimension.BACK, Color.red);
		position.y++;
		createStaticCube(position, size, Dimension.BACK, Color.red);

		position = new Vector3(36,1,25);
		createStaticCube (position, size, Dimension.BACK, Color.red);
		position.y--;
		createStaticCube (position, size, Dimension.BACK, Color.red);

		position = new Vector3(36,0,25);
		for (;position.x>20;position.x--) {
			createStaticCube(position, size, Dimension.BACK, Color.red);
		}

		//create 'ladder' with a hole, going up
		position = new Vector3(20,0,25);
		for (; position.x>10; position.x--, position.y++) {
			for (position.z=23; position.z<=27; position.z++) {
				if ( (position.x==14 || position.x==15) && ( position.z>23 && position.z<27 )) {
					continue;
				}
				createStaticCube(position, size, Dimension.BACK, Color.red);
			}
		}

		//create platform connecting the ladders
		position.y = 10;
		for (position.x=8;position.x<=10;position.x++) {
			for (position.z=23; position.z<=27; position.z++) {
				createStaticCube(position, size, Dimension.BACK, Color.red);
			}
		}

		//create 'ladder' going down
		position = new Vector3(10,9,22);
		for (; position.y>0; position.y--, position.z--) {
			for (position.x=8; position.x<=12; position.x++) {
				createStaticCube(position, size, Dimension.LEFT, Color.red);
			}
		}
	
		position = new Vector3(10,0,14);
		for (; position.z>0; position.z--) {
			createStaticCube(position, size, Dimension.LEFT, Color.red);
		}
		
		
		/*
		//Debug.Log("-----" + listOfList.Count);
		List<int> zeroOrOne = new List<int>();// pithanotita 5/2 gia ton elaxisto aritmo ton exthron se mia grami 1:0
		zeroOrOne.Add(1);
		zeroOrOne.Add(1);
		zeroOrOne.Add(0);
		zeroOrOne.Add(1);
		zeroOrOne.Add(1);
		zeroOrOne.Add(0);
		zeroOrOne.Add(1);
		//random dimiourgia exthron
		for (int c=0 ; c<listOfList.Count ; c++){
			numOfEnemy = Random.Range (Random.Range(0,zeroOrOne.Count), (Mathf.CeilToInt(listOfList[c].Count / 40.0f))); //tixeos arithmos exthron
			for (int i=0; i<numOfEnemy; i++) { //dimiourgia ekthron
				if (listOfList[c].Count>0){
					int p = Random.Range(0, ((listOfList[c].Count)-1));
					Transform  newEnemy = Instantiate(Enemy,listOfList[c][p],Quaternion.identity ) as Transform;
					newEnemy.tag="Enemy";
					if (p+2<=listOfList[c].Count-1){
						listOfList[c].RemoveAt(p+2);
					}
					if (p+1<=listOfList[c].Count-1){
						listOfList[c].RemoveAt(p+1);
					}

					listOfList[c].RemoveAt(p);

					if (p-1>=0){ // p-1
						listOfList[c].RemoveAt(p-1);
					}
					if (p-2>=0){ //p-2
						listOfList[c].RemoveAt(p-2);
					}
				}
			}
		}
		/*Debug.Log("-----" + numOfEnemy);*/

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
		if ((prevDim != dimension)&&(count>=0)) { //se kathe alagi tou divension midenizete to count , to (count>=0) xrismopoite gia tin arxi, na min ginonte spawn enemy konta s stin arxi
			listOfList.Add(new List<Vector3>());
			countOfList++;
			count=0;
		}
		prevDim = dimension;
		count++;
		if (count >= 5) {
			listOfList[countOfList].Add(new Vector3(position.x,position.y+3,position.z));
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
		cube.AddComponent<Cube>();


	}

	/**
	 * Function that creates a coin in the given position
	 * */
	private void createCoin(Vector3 position) {
		Transform  newCoin = Instantiate(coin, position,Quaternion.identity ) as Transform;
		newCoin.tag = "Coin";
	}


	private void createText() {
		GameObject Text = new GameObject();

	}
	
	// Update is called once per frame
	void Update () {

	}
}
