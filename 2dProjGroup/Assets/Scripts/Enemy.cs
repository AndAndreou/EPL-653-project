using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private GameObject target; // metavliti p krata ton pekti mas
	//private GameRepository repository; //GameRepository
	public float vision; // apostasi oratotitas
	private Dimension dimension ; //metavliti p krata se pia diastasi ine o enemy
	public float speed;
	public float life;
	private float damage;
	private float jump=300.0f;
	private bool findDimension= true;
	public string lookAt; //metavliti p krato p vlepi o enemy left or right
	private bool isJump=false;
	private Animator animator;
	private Renderer renderer;
	private Rigidbody rigidBody;
	public float ratefire;
	public float mindam;
	public float maxdam;

	//public RuntimeAnimatorController enemyeasy;
	//public RuntimeAnimatorController enemystrong;

	public Transform enemyBullet;
	private float creationTime;

	//
	public float[] scale = new float[3]; //scale[0]=x ,scale[1]=y , scale[2]=z 
	//public float scaley;
	//public float scalez;
	public float[] col = new float[3];
	//public float coly;
	//public float colz;
	public float rotanim;
	public float points; //points p dini kathe enemy
	private float epsilon = 0.4f; //timi gia dimiourgia orion stin oratotita
	//private int isRaisedORisRotating=0;
	//metavlites gia floor testing
	private int floorray ;
	private int floorOnx ;
	private int floorExists=0;
	

	// Use this for initialization
	void Start () {
		//
		//player = GameObject.FindGameObjectWithTag(("Player"));
		renderer = this.GetComponent<Renderer> ();
		rigidBody = GetComponent<Rigidbody> ();
		//camera = GameObject.FindGameObjectWithTag(("MainCamera"));
		//
		int lookLeft;
		//repository = GameRepository.Instance;
		target = GameObject.FindGameObjectWithTag("Player");
		damage = Random.Range (mindam, maxdam);
		//
		//this.GetComponent<BoxCollider>().size = new Vector3(0.01f,0.41f,0.2f);
		//this.transform.localScale = new Vector3(1.0f,3.0f,0.0f);
		//this.GetComponent<BoxCollider>().size=0.01f;
		//

		/*
		vision= 6.0f; // apostasi oratotitas
		speed=0.05f;
		life=100.0f;
		damage=30.0f;
		jump=300.0f;
		ratefire=1.0f;
		*/
		animator = GetComponent<Animator> ();
		//animator.runtimeAnimatorController = enemystrong;

		transform.Rotate(new Vector3(0.0f,1.0f,0.0f),rotanim);
		//Color c = new Color ((damage/100.0f),0.0f,0.0f,1.0f);
		//GetComponent<SpriteRenderer>().color = c;
		lookLeft = Random.Range (0, 10);
		if (lookLeft < 5) { //vlepi aristera
			lookAt = "left";
		}
		else { //vlepi deksia
			lookAt = "right";
			transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
		}
		renderer.enabled = false; // false etsi oste na min fenete o enemy mexri na mpi sti sosti thesi p prepi
		//spriteRenderer.sprite = sp1 ;
		//dimension=0;// dokimastika

		renderer.enabled=false;
		//get creation time
		creationTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if ((GameRepository.isPaused()) && (findDimension==false)) {
			rigidBody.Sleep();
			return;
		}

		if ((life<=0.0f) || (this.transform.position.y<=0)){
			GameRepository.setScore(points);
			Destroy(this.gameObject );
			return;
		}

		if (findDimension==false){ // monon otan o enemy topothetithi tin proti fora tha ginonte i pio kato elexi prin oxi
			// pote tha fenete kai pote oxi ekthors 
			if (GameRepository.isRaised() || GameRepository.isRotating() ) {
				renderer.enabled = true;
				return;
			}
			else if (((GameRepository.getCurrentDimension() == Dimension.FRONT) || (GameRepository.getCurrentDimension() == Dimension.BACK)) && ((dimension == Dimension.FRONT) || (dimension == Dimension.BACK))) { //dimension cube = 0
				//Debug.Log("------");
				if (Mathf.Abs(target.transform.position.z - this.transform.position.z)<epsilon) {
					//Debug.Log("Z - Z");
					renderer.enabled = true;
				}
				else  {
					//renderer.enabled = true; //prosorino gia na kano kapious elexous
					renderer.enabled = false;
					//Debug.Log("Z !- Z");
				}
			} 
			else if (((GameRepository.getCurrentDimension() == Dimension.RIGHT) || (GameRepository.getCurrentDimension() == Dimension.LEFT)) && ((dimension == Dimension.RIGHT) || (dimension == Dimension.LEFT))) {
				if (Mathf.Abs(target.transform.position.x - this.transform.position.x)<epsilon) {
					renderer.enabled = true;
					//Debug.Log("X - X");
				}
				else {
					//renderer.enabled = true; //prosorino gia na kano kapious elexous
					renderer.enabled = false;
					//Debug.Log("X !- X");
				}
			}
			else {
				renderer.enabled = false;
			}
		}

		//
		if ((renderer.enabled==true)&&(findDimension==false)){ // mono an o ekthos in oratos kani tous elexous
			//vison ray
			bool isVisionRayHit = false;
			RaycastHit visionRayHit ;
			Vector3 startPosVisionRay= new Vector3 (transform.position.x,transform.position.y + 0.6f,transform.position.z); //exi sxesi me to ipsos tou enemy to 1.0f
			Vector3 visionRayDirection = target.transform.position - transform.position;
			if (((visionRayDirection.x < 0) && (lookAt=="left") && (Mathf.Abs(visionRayDirection.z)<=0.5f)) || ((visionRayDirection.z < 0)&& (lookAt=="right") && (Mathf.Abs(visionRayDirection.x)<=0.5)) || ((visionRayDirection.z > 0)&& (lookAt=="left") && (Mathf.Abs(visionRayDirection.x)<=0.5)) || ((visionRayDirection.x > 0) && (lookAt=="right") && (Mathf.Abs(visionRayDirection.z)<=0.5f)) ) {
			//if (((target.transform.position.x < transform.position.x) && (lookAt=="left") && (visionRayDirection.z<=0.5f)) ||((target.transform.position.z < transform.position.z) && (lookAt=="left") && (Mathf.Abs(visionRayDirection.z)<=0.5f))){
				isVisionRayHit = Physics.Raycast (startPosVisionRay, visionRayDirection, out visionRayHit, vision);


			} 
			else if ((Mathf.Abs(visionRayDirection.z)<=0.5) || (Mathf.Abs(visionRayDirection.x)<=0.5))  {

				isVisionRayHit = Physics.Raycast (startPosVisionRay, visionRayDirection, out visionRayHit, (vision-1.0f));
			}

			string tagVisonRayHit="";

			if (isVisionRayHit) {
				tagVisonRayHit = visionRayHit.transform.tag;  
				//Debug.Log(target.transform.position.z - transform.position.z);
			}
			//Debug.DrawLine (startPosVisionRay, visionRayHit.transform.position, Color.green);
			/*Debug.Log(isVisionRayHit);
			Debug.Log(tagVisonRayHit);*/

			if ( ((isVisionRayHit) && (tagVisonRayHit != "Player")) || (!isVisionRayHit) ) {
				animator.SetBool("enemyWalk", false);
			}

			//if(GameR.getCurrentDimension() == dimension){  //elenxos an vriskonte stin idia diastasi
				//if (Mathf.Abs(target.transform.position.y-transform.position.y)<=2){ //elenxos an vriskonte sto dio y
				if ((isVisionRayHit) && (tagVisonRayHit == "Player")) {
					animator.SetBool("enemyWalk", true);
					if (dimension == Dimension.FRONT || dimension == Dimension.BACK) {
						floorOnx=1;
						//if (Mathf.Abs(target.transform.position.z-transform.position.z)<=1){ //elenxos an vriskonte sto dio z
						//	float distance=target.transform.position.x-transform.position.x;
						//	if (Mathf.Abs(distance)<=vision){ // vriskete entos oratotitas
						if (dimension == Dimension.FRONT) {
							if (visionRayDirection.x < 0) { //elenxos an o pektis ine aristera
								floorray=0;
								if (floorExists==1){
							   		transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z); //kino ton ekthro aristera
								}
								if (lookAt == "right") { //kano rotate to sprite an alakso katefthinsi
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),-180.0f);
								}
								lookAt = "left";
							} else { //elenxos an o pektis ine deksia
								floorray=1;
								if (floorExists==1){
									transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z); //kino ton ekthro deksia
								}
								if (lookAt == "left") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
								}
								lookAt = "right";
							}
						} else {
							if (visionRayDirection.x > 0) { //elenxos an o pektis ine aristera
								floorray=1;
								if (floorExists==1){
									transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z); //kino ton ekthro aristera
								}
								if (lookAt == "right") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),-180.0f);
								}
								lookAt = "left";
							} else {
								floorray=0;
								if (floorExists==1){
									transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z); //kino ton ekthro deksia
								}
								if (lookAt == "left") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
								}
								lookAt = "right";
							}
						}
						/*}
							}*/
					} else { //dimension==3 || dimension==4
						floorOnx=0;
						//if (Mathf.Abs(target.transform.position.x-transform.position.x)<=1){ //elenxos an vriskonte sto dio z
						//	float distance=target.transform.position.z-transform.position.z;
						//	if (Mathf.Abs(distance)<=vision){ // vriskete entos oratotitas
						if (dimension == Dimension.RIGHT) {
							if (visionRayDirection.z < 0) { //elenxos an o pektis ine aristera
								floorray=0;
								if (floorExists==1){
									transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - speed); //kino ton ekthro aristera
								}
								if (lookAt == "right") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),-180.0f);
								}
								//Debug.Log("test 1");
								lookAt = "left";
							} else { //elenxos an o pektis ine deksia
								floorray=1;
								if (floorExists==1){
									transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + speed); //kino ton ekthro deksia
								}
								if (lookAt == "left") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
								}
								//Debug.Log("test 2");
								lookAt = "right";
							}
						} else {
							if (visionRayDirection.z > 0) { //elenxos an o pektis ine aristera
								floorray=1;
								if (floorExists==1){
									transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + speed); //kino ton ekthro aristera
								}
								if (lookAt == "right") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),-180.0f);
								}
								//Debug.Log("test 3");
								lookAt = "left";
							} else {
								floorray=0;
								if (floorExists==1){
									transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - speed); //kino ton ekthro deksia
								}
								if (lookAt == "left") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
								}
								//Debug.Log("test 4");
								lookAt = "right";
							}
						}
					}
					/*}
						}
					}
				}*/
					//Debug.Log("Damage percentage: " + damage/100.0f);

				//empodio
				bool isRayHit = false;
				RaycastHit RayHit ;
				Vector3 startPosRay= new Vector3 (transform.position.x,transform.position.y - 0.6f,transform.position.z); //exi sxesi me to ipsos tou enemy to 1.0f
				Vector3 RayDirection = target.transform.position - transform.position;
				
				isRayHit = Physics.Raycast (startPosRay, RayDirection, out RayHit, 1.5f);
				
				string tagRayHit="";
				
				if (isRayHit) {
					tagRayHit = RayHit.transform.tag;  
					//Debug.Log("-------------");
				}
				//Debug.DrawLine (startPosRay, RayHit.transform.position, Color.red);
				//Debug.Log(isJump);
				//Debug.Log(tagRayHit);
				//
				if ((!isJump) && ((tagRayHit == "StaticCube") || (tagRayHit == "MovableCube"))) {
					GetComponent<Rigidbody>().AddForce(Vector3.up * jump);
					isJump = true;
				}


				//elexos an iparxi edafos mprosta
				float floorRayDireectionX;
				float floorRayDireectionZ;
				bool isFloorRayHit = false;
				RaycastHit floorRayHit ;
				Vector3 startPosFloorRay= new Vector3 (transform.position.x,transform.position.y + 0.6f,transform.position.z); //exi sxesi me to ipsos tou enemy to 1.0f

				if (floorray==1){
					floorRayDireectionX=(transform.position.x+1.0f);
					floorRayDireectionZ=(transform.position.z+1.0f);
				}
				else{
					floorRayDireectionX=(transform.position.x-1.0f);
					floorRayDireectionZ=(transform.position.z-1.0f);
				}

				Vector3 floorRayDirection;
				if (floorOnx==1){
					floorRayDirection = (-(transform.position- new Vector3 (floorRayDireectionX,(transform.position.y - 1.0f),transform.position.z))).normalized;
				}
				else{
					floorRayDirection = (-(transform.position- new Vector3 (transform.position.x,(transform.position.y - 1.0f),floorRayDireectionZ))).normalized;
				}

				//floorRayDirection =  new Vector3 (0.0f,1.0f,0.0f);

				//Debug.Log("////////////////////");
				//Debug.Log(floorRayDirection);
				isFloorRayHit = Physics.Raycast (startPosFloorRay, floorRayDirection, out floorRayHit, 5.0f);
				
				string tagFloorRayHit="";
				
				if (isFloorRayHit) {
					tagFloorRayHit = floorRayHit.transform.tag;
					//Debug.DrawLine (startPosFloorRay, floorRayHit.transform.position, Color.red);
					//Debug.Log("-------------");
				}
				//Debug.DrawLine (startPosFloorRay, floorRayDirection, Color.green);
				//Debug.DrawLine (startPosRay, RayHit.transform.position, Color.red);
				//Debug.Log(isJump);
				//Debug.Log(tagRayHit);
				//
				if (isFloorRayHit) {
					floorExists=1;
					//Debug.Log("************");
					//Debug.Log(isFloorRayHit);
					//Debug.Log(tagFloorRayHit);
					//Debug.Log("testray");
				}
				else{
					floorExists=0;
					animator.SetBool("enemyWalk", false);
					//Debug.Log("++++++++++++++");
					//Debug.Log(isFloorRayHit);
					//Debug.Log(tagFloorRayHit);
				}

				//
				/*if (GetComponent<Rigidbody>().velocity.y != 0.0f) {
					isJump = true;
				} else {
					isJump = false;
				}*/
				//dimiourgia sferas
				//if (Input.GetKeyDown (KeyCode.V))
				if ( (Time.time - creationTime) > ratefire) { //pirovolima enemy
					if (Random.Range (0, 2)==1){ //pithanotita an tha pirovolisi i oxi
						Transform newEnemyBullet = Instantiate (enemyBullet, transform.position, Quaternion.identity) as Transform;
						if ((dimension==Dimension.LEFT) || (dimension==Dimension.RIGHT)){
							newEnemyBullet.Rotate(new Vector3(0.0f,1.0f,0.0f),90.0f);
						}
						newEnemyBullet.tag = "enemyBullet";
					}
					creationTime = Time.time;
				}
			}
		}
		//animator.SetBool("enemyWalk", false);
	}

	void OnCollisionEnter(Collision other){  

		if ( (other.gameObject.tag == "Player") && (dimension==GameRepository.getCurrentDimension())) {
			GameRepository.losePlayerLife(damage);
		}
		if (findDimension){ //elexos gia na vro ti diastasi ke na kano to analogo rotation tou enemy elexo mono tin proti fora
			if((other.gameObject.tag=="StaticCube")/* || (other.gameObject.tag=="MovableCube")*/){ 
				findDimension=false;
				//Debug.Log("-------");
				/*if(other.transform.localEulerAngles.y==0)*/
				//Debug.Log(other.gameObject.GetComponent<Cube>().getDimension());
				if (rigidBody == null){ //apofigi enemy se lathos simia
					Debug.Log("******* totalEnemyNumber_by_rigidBody.constraints  - 1 *******");
					Destroy(this.gameObject );
					return;
				}

				if(other.gameObject.GetComponent<Cube>().getDimension()==Dimension.FRONT){ //dimension cube = 0
					//Debug.Log("test_front");
					transform.Rotate(new Vector3(0.0f,0.0f,0.0f));
					rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
					dimension=Dimension.FRONT;
				}
				else if(other.gameObject.GetComponent<Cube>().getDimension()==Dimension.BACK){ //dimension cube = 1
					//Debug.Log("test_back");
					//Debug.Log("test");
					transform.Rotate(new Vector3(0.0f,-180.0f,0.0f));
					rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
					//Debug.Log("test1");
					dimension=Dimension.BACK;
				}
				else if(other.gameObject.GetComponent<Cube>().getDimension()==Dimension.RIGHT){ //dimension cube = 2
					//Debug.Log("test_right");
					//Debug.Log(other.transform.position);
					//Debug.Log(other.transform.name);
					transform.Rotate(new Vector3(0.0f,-90.0f,0.0f));
					rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
					dimension=Dimension.RIGHT;
				}
				else if(other.gameObject.GetComponent<Cube>().getDimension()==Dimension.LEFT){ //dimension cube = 3
					//Debug.Log("test_left");
					transform.Rotate(new Vector3(0.0f,-270.0f,0.0f));
					rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
					dimension=Dimension.LEFT;
				}
				//Debug.Log(other.transform.localEulerAngles.y);
				//this.transform.localScale = new Vector3(3.0f,3.0f,0.0f);
				Vector3 posi=other.gameObject.GetComponent<Transform>().transform.position; //epanatopotheto ton enemy sto kentro tou cube
				this.transform.localScale = new Vector3(scale[0],scale[1],scale[2]);
				//this.GetComponent<BoxCollider>().size = new Vector3(0.37f,0.41f,0.2f);
				this.GetComponent<BoxCollider>().size = new Vector3(col[0],col[1],col[2]);

				posi.y+=1.2f;
				this.transform.position = posi;
				renderer.enabled = true;
				//transform.Rotate(Mathf.Round(transform.rotation.x),Mathf.Round(transform.rotation.y),Mathf.Round(transform.rotation.z));
				//transform.position=new Vector3(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y),Mathf.Round(transform.position.z));
				//this.GetComponent<BoxCollider>().size.x=0.37f;
				//this.GetComponent<BoxCollider>().size.y=0.41f;

				//
				/*if (dimension == Dimension.FRONT || dimension == Dimension.BACK) {
					rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
				}
				else if (dimension == Dimension.RIGHT || dimension == Dimension.LEFT) {
					rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
				}*/
				//
			}

		}

		// elexos gia jump tou enemy
		/*if (((other.gameObject.tag == "StaticCube") || (other.gameObject.tag == "MovableCube")) && (Mathf.Abs(other.transform.position.y-transform.position.y)>0.0f)&& (Mathf.Abs(other.transform.position.y-transform.position.y)<1.0f)) { //o ekthros pida an vriski empodio 
			//transform.position = new Vector3(transform.position.x, transform.position.y + jump, transform.position.z);
			if (!isJump ) {
				GetComponent<Rigidbody>().AddForce(Vector3.up * jump);
			}*/
		if ((other.gameObject.tag == "StaticCube") || (other.gameObject.tag == "MovableCube")) {

			if (GetComponent<Rigidbody> ().velocity.y == 0.0f) {//elexos gia na minpida ksana otan ine idi ston aera
				//Debug.Log("*****************");
				isJump = false;
			}
		}

		if (findDimension == true) {
			Debug.Log("******* totalEnemyNumber_by_findDimension - 1 *******");
			Destroy(this.gameObject );
			return;
		}
		//}
	}

	public void loseEnemyLife(float lose) { //aferi zoi tou enemy
		life -= lose;
	}



	
	
}
