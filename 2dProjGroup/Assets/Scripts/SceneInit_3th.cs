using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneInit_3th : MonoBehaviour {

	public Material cubeMaterial;
	
	//Prefubs
	private GameObject Player;
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
	public void createScene() {
		Player = GameObject.FindGameObjectWithTag ("Player");
		Player.GetComponent<Renderer> ().enabled = true;

		totalEnemyNumber = 0;
		
		listOfList.Add(new List<Vector3>());
		
		int x=0, y=0, z=0;
		
		//CREATING THE SCENE
		
		//dimension FRONT
		
		prevDim = Dimension.FRONT;
		
		Vector3 size = new Vector3(1,1,1);
		Vector3 position = new Vector3(0,0,0);

		//
		//arxi stadiou
		position = new Vector3 (0,0,0);
		for (position.x = 0; position.x<=10; position.x++) {
				createStaticCube(position, size, Dimension.FRONT,1);
		}

		//dimiourgia skalas
		position = new Vector3 (12,0,0);
		createStaticCube(position, size, Dimension.FRONT,0);

		position = new Vector3 (12,1,2);
		createStaticCube(position, size, Dimension.FRONT,0);
	
		position = new Vector3 (14,2,2);
		createStaticCube(position, size, Dimension.FRONT,0);

		position = new Vector3 (14,2,3);
		for (position.z = 3; position.z<=10; position.z++) {
			createStaticCube (position, size, Dimension.RIGHT, 1);
		}

		position = new Vector3 (14,2,17);
		for (position.z = 17; position.z<=30; position.z++) {
			createStaticCube (position, size, Dimension.RIGHT, 1);
		}

		position = new Vector3 (14,3,0);
		createStaticCube(position, size, Dimension.FRONT,0);

		position = new Vector3 (16,3,0);
		createStaticCube(position, size, Dimension.FRONT,0);

		position = new Vector3 (16,4,1);
		for (position.z = 1; position.z<=20; position.z++) {
			createStaticCube (position, size, Dimension.RIGHT, 1);
		}

		position = new Vector3 (15,3,30);
		for (position.z = 30 ; position.z>=27; position.z--) {
			for (position.x = 15 ; position.x<=20; position.x++) {
				createStaticCube (position, size, Dimension.BACK, 1);
			}
		}

		//skala
		position = new Vector3 (21,4,27);
		for (position.x = 21 ; position.x<=26; position.x++,position.y++) {
			createStaticCube (position, size, Dimension.FRONT, 1);

		}

		position = new Vector3 (25,8,26);
		for (position.z = 26 ; position.z>=20; position.z--) {
			createStaticCube (position, size, Dimension.RIGHT, 1);
			
		}

		position = new Vector3 (25,9,20);
		createStaticCube (position, size, Dimension.RIGHT, 0);

		//revgr

		position = new Vector3 (24,15,21);
		for (position.x = 24 ; position.x<=32; position.x++) {
			createStaticCube (position, size, Dimension.FRONT, 1);
			
		}

		//revgr2
		
		position = new Vector3 (15,20,21);
		for (position.x = 15 ; position.x<=26; position.x++) {
			createStaticCube (position, size, Dimension.FRONT, 0);
			
		}

		position = new Vector3 (15,19,21);
		createStaticCube (position, size, Dimension.RIGHT, 0);
		position = new Vector3 (15,18,21);
		createStaticCube (position, size, Dimension.RIGHT, 0);

		position = new Vector3 (31,10,21);
		for (position.z = 21 ; position.z>=15; position.z--) {
			for (position.x = 31 ; position.x<=50; position.x++) {
				createStaticCube (position, size, Dimension.FRONT, 1);
			}			
		}

		position = new Vector3 (35,10,22);
		for (position.x = 35 ; position.x<=45; position.x++) {
			createStaticCube (position, size, Dimension.FRONT, 1);
			
		}

		//skala telefteou cube
		position = new Vector3 (50,10,15);
		for (position.x = 50 ; position.x>=31; position.x-- , position.y++) {
			if ((position.x>41) || (position.x<38)){
				createStaticCube (position, size, Dimension.BACK, 0);
			}
			
		}

		//patoma anamesa sta skalia
		position = new Vector3 (50,17,15);
		for (position.x = 40 ; position.x>=39; position.x-- ) {
				createStaticCube (position, size, Dimension.BACK, 1);
			
		}

		position = new Vector3 (31,22,15);
		for (position.z = 15 ; position.z<=19; position.z++ ) {
			createStaticCube (position, size, Dimension.LEFT, 1);
			
		}

		//cube ston area monos t
		position = new Vector3 (31,28,22);
		createStaticCube (position, size, Dimension.LEFT, 0);

		position = new Vector3 (31,22,25);
		for (position.z = 25 ; position.z<=29; position.z++ ) {
			createStaticCube (position, size, Dimension.LEFT, 1);
			
		}

		position = new Vector3 (30,22,29);
		for (position.x = 30 ; position.x>=26; position.x-- ) {
			createStaticCube (position, size, Dimension.BACK, 1);
			
		}

		position = new Vector3 (26,22,29);
		for (position.z = 29 ; position.z>=22; position.z-- ) {
			createStaticCube (position, size, Dimension.LEFT, 1);
			
		}

		//fin
		position = new Vector3 (14,18,21);
		createStaticCube (position, size, Dimension.BACK, 0);
		position = new Vector3 (13,18,21);
		createStaticCube (position, size, Dimension.BACK, 0);
		position = new Vector3 (12,18,21);
		createStaticCube (position, size, Dimension.BACK, 0);
		position = new Vector3 (11, 18, 21);
		createStaticCube (position, size, Dimension.BACK, 0);
		position = new Vector3 (11,19,21);
		createStaticCube (position, size, Dimension.BACK, 0);
		position = new Vector3 (11,20,21);
		createStaticCube (position, size, Dimension.BACK, 0);

		//coin
		position = new Vector3 (14,3,10);
		createCoinPowerUp(position, Dimension.LEFT);
		position = new Vector3 (14,3,9);
		createCoinPowerUp(position, Dimension.LEFT);
		position = new Vector3 (16,5,19);
		createCoinPowerUp(position, Dimension.LEFT);

		position = new Vector3 (24,16,21);
		createCoinPowerUp(position, Dimension.FRONT);
		position = new Vector3 (36,11,15);
		createCoinPowerUp(position, Dimension.FRONT);
		position = new Vector3 (34,11,15);
		createCoinPowerUp(position, Dimension.FRONT);
		position = new Vector3 (43,11,19);
		createCoinPowerUp(position, Dimension.FRONT);
		position = new Vector3 (12,19,21);
		createCoinPowerUp(position, Dimension.FRONT);

		//gravity
		position = new Vector3 (25,9,21);
		createGravityPowerUp (position, Dimension.RIGHT);
		position = new Vector3 (31,14,21);
		createGravityPowerUp (position, Dimension.FRONT);
		position = new Vector3 (26,19,21);
		createGravityPowerUp (position, Dimension.FRONT);
		position = new Vector3 (37,18,15);
		createGravityPowerUp (position, Dimension.FRONT);
		position = new Vector3 (31,28,15);
		createGravityPowerUp (position, Dimension.FRONT);
		position = new Vector3 (31,23,19);
		createGravityPowerUp (position, Dimension.RIGHT);
		position = new Vector3 (31,27,22);
		createGravityPowerUp (position, Dimension.RIGHT);

		//health
		position = new Vector3 (31,16,21);
		createHealthPowerUp (position, Dimension.FRONT);
		position = new Vector3 (39,11,22);
		createHealthPowerUp (position, Dimension.FRONT);
		position = new Vector3 (20,4,30);
		createHealthPowerUp (position, Dimension.FRONT);

		//
		position = new Vector3 (14,19,21);
		createExitPortal (position,Dimension.FRONT, "CreditLevel");



		//Debug.Log(listOfList[0][0].x + "," + listOfList[0][0].z + "," + listOfList[0].Count);
	//	if (/*(listOfList[0][0].x==0.0f)&&*/(listOfList[0][0].z==0.0f)&&(listOfList[0].Count>18)){//elexos an ime ontos sto proto diadromo ke an exi toulaxiston 7 cubes
			//Debug.Log(listOfList[0][0].x + "," + listOfList[0][0].z + "," + listOfList[0].Count);
	//		listOfList[0].RemoveRange(0,17); // stin arxi (tou stadiou) afinoume kapies thesis adies 
			//Debug.Log(listOfList[0][0].x + "," + listOfList[0][0].z + "," + listOfList[0].Count);
	//	}

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
			//Debug.Log("-----" + numOfEnemy);
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