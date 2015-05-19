using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneInit : MonoBehaviour {
	public Material cubeMaterial;
	private GameRepository repository;

	//Prefubs
	public Transform Enemy;
	public Transform powerUp_coin;
	public Transform powerUp_health;
	public Transform powerUp_gravity;
	public Transform exitPortal;

	private Dimension prevDim; //karata to proigoumeno dimension
	private float prevY=0; //krata to  proigoumeno y
	private float prevX=0;
	private float prevZ=0;
	private int count=-15;	//metra ta cube p mpikan stin idia diastasi
	private int countOfList=0; //counter gia to poses listes exoume
	List<Vector3> listPosEnemy = new List<Vector3>();// listes apo pithana pos enemys
	List<List<Vector3>> listOfList = new List<List<Vector3>>(); //lista p krata tis pio pano listes
	private int numOfEnemy; //arithmos ton exthron
	Renderer renderer;
	private int totalEnemyNumber;
	//

	// Use this for initialization
	void Start() {
		repository = GameRepository.Instance;
		totalEnemyNumber = 0;

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

		createExitPortal (new Vector3(10,1,3), Dimension.RIGHT);

		position = new Vector3 (30,1f,0);
		for (; position.x<40; position.x = position.x +2) {
			createCoinPowerUp (position, Dimension.FRONT);
		}

		createHealthPowerUp (new Vector3 (4, 1f, 0), Dimension.FRONT);
		createGravityPowerUp (new Vector3 (5, 1f, 0), Dimension.FRONT);
		createCoinPowerUp (new Vector3 (6,1f,0), Dimension.FRONT);

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
			createStaticCube(position, size, Dimension.FRONT, Color.red);
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
				createStaticCube(position, size, Dimension.RIGHT, Color.red);
			}
		}

		//create platform connecting the ladders
		position.y = 10;
		for (position.x=8;position.x<=10;position.x++) {
			for (position.z=23; position.z<=27; position.z++) {
				createStaticCube(position, size, Dimension.RIGHT, Color.red);
			}
		}

		//create 'ladder' going down
		position = new Vector3(10,9,22);
		for (; position.y>0; position.y--, position.z--) {
			for (position.x=8; position.x<=12; position.x++) {
				createStaticCube(position, size, Dimension.FRONT, Color.red);
			}
		}
	
		position = new Vector3(10,0,14);
		for (; position.z>0; position.z--) {
			createStaticCube(position, size, Dimension.LEFT, Color.red);
		}
		
		
		//
		//Debug.Log(listOfList[0][0].x + "," + listOfList[0][0].z + "," + listOfList[0].Count);
		if (/*(listOfList[0][0].x==0.0f)&&*/(listOfList[0][0].z==0.0f)&&(listOfList[0].Count>10)){//elexos an ime ontos sto proto diadromo ke an exi toulaxiston 7 cubes
			//Debug.Log(listOfList[0][0].x + "," + listOfList[0][0].z + "," + listOfList[0].Count);
			listOfList[0].RemoveRange(0,10); // stin arxi (tou stadiou) afinoume kapies thesis adies 
			//Debug.Log(listOfList[0][0].x + "," + listOfList[0][0].z + "," + listOfList[0].Count);
		}
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
			if (listOfList[c].Count<=10)// gia ligotera apo 10 cubes epilego metaksi 0 kai 1
			{
				numOfEnemy = Random.Range (0, 2);// se ligoreto apo 10 cubes miono tin pithanotita na vgi ekthros
			}
			else if (listOfList[c].Count<=30)// gia ligotera apo 30 cubes epilego metaksi 0 i 1 me pithanotita na ine 1 5/7 kai ton arithmo to cubes/40
			{
				numOfEnemy = Random.Range (zeroOrOne[Random.Range(0,zeroOrOne.Count)], (Mathf.CeilToInt(listOfList[c].Count / 40.0f))+1); //tixeos arithmos exthron
			}
			else {// gia perisotera apo 30 cubes epilego metaksi 1 kai ton arithmo to cubes/40
				numOfEnemy = Random.Range (1, (Mathf.CeilToInt(listOfList[c].Count / 40.0f))+1); //tixeos arithmos exthron
			}
			for (int i=0; i<numOfEnemy; i++) { //dimiourgia ekthron
				if (listOfList[c].Count>0){
					int p = Random.Range(0, (listOfList[c].Count));
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
			/*Debug.Log("-----" + numOfEnemy);*/
			totalEnemyNumber+=numOfEnemy;
			Debug.Log("totalEnemyNumber:" + totalEnemyNumber);
		}


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

			if (listOfList[countOfList].Count!=0){	//elexos an i proigoumeni lista p dimiourgisa den ine adia , an ine pla midenizo to count kai den dimiourgo kenourgia														
				listOfList.Add(new List<Vector3>());
				countOfList++;
			}
			count=0;
		}
		if (prevY!=position.y){ //to prevY xrisimopoiite gia na elekso an vriskome sto idio y me to proigoumeno kivo p dimiourgisa
			count=0;
		}
		if ((dimension==prevDim)&&((dimension==Dimension.BACK) || (dimension==Dimension.FRONT))&& (prevZ!=position.z)){//an ime sti diastasi front i back kai alazi to z tote midenizete to count gt simeni dimiourgo platforma
			count=0;
		}
		if ((dimension==prevDim)&&((dimension==Dimension.LEFT) || (dimension==Dimension.RIGHT))&& (prevX!=position.x)){
			count=0;
		}
		prevDim = dimension;
		prevY=position.y;
		prevX=position.x;
		prevZ=position.z;
	
		count++;
		if (count >= 5) {
			listOfList[countOfList].Add(new Vector3(position.x,position.y+2,position.z));
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

		renderer = cube.GetComponent<Renderer> ();

		cube.GetComponent<Cube>().setDimension(dimension);
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

		renderer = cube.GetComponent<Renderer> ();

		cube.GetComponent<Cube>().setDimension(dimension);

		/*
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
		*/
	}

	/**
	 * Function that creates a coin in the given position
	 * */
	private void createCoinPowerUp(Vector3 position, Dimension dimension) {
		Transform  newCoin = Instantiate(powerUp_coin, position,Quaternion.identity ) as Transform;
		newCoin.tag = "Coin";
		newCoin.Rotate( dimensionToVector(dimension) );
	}

	/**
	 * Function that creates a gravity power up in the given position
	 * */
	private void createGravityPowerUp(Vector3 position, Dimension dimension) {
		Transform  newGravity = Instantiate(powerUp_gravity, position,Quaternion.identity ) as Transform;
		newGravity.tag = "Gravity";
		newGravity.Rotate( dimensionToVector(dimension) );
	}

	/**
	 * Function that creates a health power up in the given position
	 * */
	private void createHealthPowerUp(Vector3 position, Dimension dimension) {
		Transform  newHealth = Instantiate(powerUp_health, position,Quaternion.identity ) as Transform;
		newHealth.tag = "Health";
		newHealth.Rotate( dimensionToVector(dimension) );
	}

	/**
	 * Function that creates an exit portal in the given position
	 * */
	private void createExitPortal(Vector3 position, Dimension dimension) {
		Transform  newExitPortal = Instantiate(exitPortal, position,Quaternion.identity ) as Transform;
		newExitPortal.tag = "ExitPortal";
		newExitPortal.Rotate( dimensionToVector(dimension) );
	}

	/**
	 * Converst the dimension to a vector that correpsonds to the rotation degrees
	 * */
	private Vector3 dimensionToVector(Dimension dimension) {
		if (dimension == Dimension.FRONT) {
			return new Vector3 (0.0f, 0.0f, 0.0f);
		} else if (dimension == Dimension.BACK) {
			return new Vector3 (0.0f, 180.0f, 0.0f);
		} else if (dimension == Dimension.RIGHT) {
			return new Vector3 (0.0f, 90.0f, 0.0f);
		} else if (dimension == Dimension.LEFT) {
			return new Vector3 (0.0f, 270.0f, 0.0f);
		} else {
			return new Vector3 (0.0f, 0.0f, 0.0f);
		}
	}

	private void createText() {
		GameObject Text = new GameObject();

	}
	
	// Update is called once per frame
	void Update () {

	}
}
