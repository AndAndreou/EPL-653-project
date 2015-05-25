using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneInit_first : MonoBehaviour {
	public Material cubeMaterial;
	
	//Prefubs
	public Transform Enemy;
	public Transform Enemystrong;
	public Transform powerUp_coin;
	public Transform powerUp_health;
	public Transform powerUp_gravity;
	public Transform exitPortal;
	public Transform text;
	
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
		totalEnemyNumber = 0;
		
		listOfList.Add(new List<Vector3>());
		
		int x=0, y=0, z=0;
		
		//CREATING THE SCENE
		
		//dimension FRONT
		
		prevDim = Dimension.FRONT;
		
		Vector3 size = new Vector3(1,1,1);
		Vector3 position = new Vector3(0,0,0);


		
		//proto epipedo

		for (position.x=0, position.y=0, position.z=0; position.z<=11; position.z++) {
			for (position.x=-1;position.x<=1;position.x++) {
				createStaticCube(position, size, Dimension.LEFT,1);
			}
		}

		//pili 1
		for (position.x=-2,position.y=0,position.z=5;position.y<=5;position.y++) {
			position.x=-2;
			createStaticCube(position, size, Dimension.LEFT,0);
			position.x=2;
			createStaticCube(position, size, Dimension.LEFT,0);
		}
		for (position.x=-1,position.y=5,position.z=5;position.x<2;position.x++) {
			createStaticCube(position, size, Dimension.LEFT,0);
		}

		//pili2
		for (position.x=-2,position.y=0,position.z=10;position.y<=5;position.y++) {
			position.x=-2;
			createStaticCube(position, size, Dimension.LEFT,0);
			position.x=2;
			createStaticCube(position, size, Dimension.LEFT,0);
		}
		for (position.x=-1,position.y=5,position.z=10;position.x<2;position.x++) {
			createStaticCube(position, size, Dimension.LEFT,0);
		}

		//single path
		for (position.x=0, position.y=0, position.z=11; position.z<=16; position.z++) {
			createStaticCube(position, size, Dimension.LEFT,1);
		}

		createGravityPowerUp (new Vector3(0,3, 20), Dimension.LEFT);

		//anapodos diadromos
		for (position.x=-2, position.y=6, position.z=22;position.x<=15;position.x++) {
			createStaticCube(position, size, Dimension.FRONT,1);
		}

		createStaticCube (new Vector3(5,5,22), size, Dimension.FRONT,1);
		createStaticCube (new Vector3(10,5,22), size, Dimension.FRONT,1);

		//anapodo tetragono
		for (position.x=19, position.y=6, position.z=22;position.x<=26;position.x++) {
			position.z=22;
			createStaticCube(position, size, Dimension.FRONT,1);
			position.z=32;
			createStaticCube(position, size, Dimension.FRONT,1);
		}

		for (position.y=6,position.z=23;position.z<=31;position.z++) {
			position.x=19;
			createStaticCube(position, size, Dimension.FRONT,1);
			position.x=26;
			createStaticCube(position, size, Dimension.FRONT,1);
		}

		//coins
		for (position.x=20,position.y=5,position.z=32;position.x<=25;position.x++) {
			createCoinPowerUp(position,Dimension.BACK);
		}

		for (position.x=20,position.y=7,position.z=32;position.x<=25;position.x++) {
			createCoinPowerUp(position,Dimension.BACK);
		}

		createGravityPowerUp (new Vector3(28,9, 22), Dimension.FRONT);

		//skala
		int cubeNumber = 1;
		for (position.y=6,position.z=22;cubeNumber<=5;cubeNumber++) {
			for (position.x =-3;position.x>=(-3-cubeNumber);position.x-- ) {
				createStaticCube(position, size, Dimension.FRONT,0);
			}
			position.y--;
		}


		for (position.x=-3, position.z=22,position.y=1;position.x>=-28;position.x-- ) {
			createStaticCube(position, size, Dimension.FRONT,1);
		}

		Debug.Log ("TOWER");
		//the tower
		for (position.y=1;position.y<=10;position.y++) {
			for (position.x=-15;position.x>=-23;position.x--) {
				position.z=26;
				createStaticCube(position, size, Dimension.FRONT,0);
				position.z=18;
				createStaticCube(position, size, Dimension.FRONT,0);
			}
			for (position.z=19;position.z<=25;position.z++) {
				if ( (position.z == 22) || (position.z == 23) || (position.z == 21)   ) continue;
				position.x=-15;
				createStaticCube(position, size, Dimension.FRONT,0);
				position.x=-23;
				createStaticCube(position, size, Dimension.FRONT,0);
			}
		}

		createStaticCube (new Vector3 (-19, 2, 22), size, Dimension.FRONT,0);

		/*
		int side = 0;
		for (position.y=3;position.y<=10;position.y+=2) {
			for (position.x=-16;position.x>=-22;position.x--) {
				if ((side%2) == 0) {
					for (position.z=19;position.z<=21;position.z++) {
						createStaticCube(position, size, Dimension.RIGHT,1);
					}
				}
				else {
					for (position.z=23;position.z<=25;position.z++) {
						createStaticCube(position, size, Dimension.RIGHT,1);
					}
				}
			}
			side++;
		}*/

		/*
		for (position.y=3,position.x=-16; position.x>=-22; position.x--) {
			for (position.z=19; position.z<=20; position.z++) {
				createStaticCube (position, size, Dimension.RIGHT, 1);
				if ( (position.x == -22) || (position.x == -16)) {
					createStaticCube (position, size, Dimension.RIGHT, 1);
					if ( (position.x == -22) || (position.x == -16)) {
						createStaticCube (new Vector3(position.x, position.y+1,position.z), size, Dimension.FRONT, 1);
					}
				}
			}
		}


		for (position.y=6,position.x=-16; position.x>=-22; position.x--) {
			for (position.z=24; position.z<=25; position.z++) {
				createStaticCube (position, size, Dimension.RIGHT, 1);
				if ( (position.x == -19) ) {
					createStaticCube (new Vector3(position.x, position.y+1,position.z), size, Dimension.FRONT, 1);
				}
			}
		}

		for (position.y=9,position.x=-16; position.x>=-22; position.x--) {
			for (position.z=19; position.z<=20; position.z++) {
				createStaticCube (position, size, Dimension.RIGHT, 1);
				if ( (position.x == -22) || (position.x == -16)) {
					createStaticCube (new Vector3(position.x, position.y+1,position.z), size, Dimension.FRONT, 1);
				}
			}
		}

		for (position.y=12,position.x=-16; position.x>=-22; position.x--) {
			for (position.z=23; position.z<=25; position.z++) {
				createStaticCube (position, size, Dimension.RIGHT, 1);
			}
		}*/

		for (position.y=1;position.y<=15;position.y++) {
			for (position.x=-18;position.x>=-20;position.x--) {
				position.z = 20;
				createStaticCube (position, size, Dimension.RIGHT, 0);
				position.z = 24;
				createStaticCube (position, size, Dimension.RIGHT, 0);
			}
		}

		for (position.z=25,position.y=15;position.z<=35;position.z++) {
			for (position.x=-18;position.x>=-20;position.x--) {
				createStaticCube (position, size, Dimension.RIGHT, 0);
			}
		}

		createExitPortal (new Vector3(-19,17,34),Dimension.RIGHT,"3thlevel");
			
			
			
			//text messages
		createTextMessage (new Vector3(-29, 4, 0), Dimension.FRONT, "Dead-end. Go back!",20f);
		createTextMessage (new Vector3(-29, 4, 0), Dimension.BACK, "Dead-end. Go back!",20f);
		
		//dimiourgia strong enemy gia testing
		Transform  newEnemy3 = Instantiate(Enemystrong,new Vector3(15.0f,2.0f,0.0f),Quaternion.identity ) as Transform;
		newEnemy3.tag="Enemy";
		//
		//Debug.Log("-----" + listOfList.Count);
		List<int> zeroOrOne = new List<int>();// pithanotita 5/7 gia ton elaxisto aritmo ton exthron se mia grami 1:0
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
					//random dimiourgia easy or strong enemy
					if(zeroOrOne[Random.Range(0,zeroOrOne.Count)]==1){
						Transform  newEnemy = Instantiate(Enemy,listOfList[c][p],Quaternion.identity ) as Transform;
						newEnemy.tag="Enemy";
					}
					else{
						Transform  newEnemy2 = Instantiate(Enemystrong,listOfList[c][p],Quaternion.identity ) as Transform;
						newEnemy2.tag="Enemy";
					}
					
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
			
		}
		Debug.Log("totalEnemyNumber:" + totalEnemyNumber);
		
	}
	
	
	/*
	 * Function that creates a static cube in the scene
	 * 
	 * Parameters:
	 * - Position: the position of the cube defeined as a vector
	 * - Size: the size of the cube defined as a vector
	 * - Dimension: the dimension of the cube
	 */
	private void createStaticCube(Vector3 position, Vector3 size, Dimension dimension, int setenemy) { //setenemy otan ine 1 tote ine ipopsifia thesi gia enemy
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
		if ((count >= 5) && (setenemy==1)) {
			listOfList[countOfList].Add(new Vector3(position.x,position.y+2,position.z));
		}
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
		
		//renderer = cube.GetComponent<Renderer> ();
		
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
	private void createMovableCube(Vector3 position, Vector3 size, Dimension dimension) {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		
		Vector3 scale = transform.localScale;
		scale.x = size.x;
		scale.y = size.y;
		scale.z = size.z;
		
		Rigidbody rigidBody = cube.AddComponent<Rigidbody>();
		rigidBody.isKinematic = false;
		rigidBody.useGravity = true;
		
		//Renderer renderer = cube.GetComponent<Renderer> ();
		
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
	private void createExitPortal(Vector3 position, Dimension dimension, string portalTargetName) {
		Transform  newExitPortal = Instantiate(exitPortal, position,Quaternion.identity ) as Transform;
		newExitPortal.tag = "ExitPortal";
		newExitPortal.Rotate( dimensionToVector(dimension) );
		newExitPortal.GetComponent<ExitPortal> ().setPortalTarget (portalTargetName);
	}
	
	/**
	 * Function that creates an exit portal in the given position
	 * */
	private void createTextMessage(Vector3 position, Dimension dimension, string message, float appearanceDistance) {
		Transform  newTextMessage = Instantiate(text, position,Quaternion.identity ) as Transform;
		newTextMessage.Rotate( dimensionToVector(dimension) );
		
		TextMesh textMesh = newTextMessage.gameObject.GetComponent<TextMesh> ();
		textMesh.text = message;
		
		newTextMessage.gameObject.GetComponent<TextMessage> ().setDimension (dimension);
		newTextMessage.gameObject.GetComponent<TextMessage> ().setAppearanceDistance (appearanceDistance);
	}
	
	
	/**
	 * Convert the dimension to a vector that correpsonds to the rotation degrees
	 * */
	private Vector3 dimensionToVector(Dimension dimension) {
		if (dimension == Dimension.FRONT) {
			return new Vector3 (0.0f, 0.0f, 0.0f);
		} else if (dimension == Dimension.BACK) {
			return new Vector3 (0.0f, 180.0f, 0.0f);
		} else if (dimension == Dimension.RIGHT) {
			return new Vector3 (0.0f, 270.0f, 0.0f);
		} else if (dimension == Dimension.LEFT) {
			return new Vector3 (0.0f, 90.0f, 0.0f);
		} else {
			return new Vector3 (0.0f, 0.0f, 0.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
