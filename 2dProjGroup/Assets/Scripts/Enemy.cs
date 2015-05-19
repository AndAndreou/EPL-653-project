using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private GameObject target; // metavliti p krata ton pekti mas
	private GameRepository repository; //GameRepository
	private float vision; // apostasi oratotitas
	private Dimension dimension ; //metavliti p krata se pia diastasi ine o enemy
	private float speed;
	private float life;
	private float damage;
	private float jump;
	private bool findDimension= true;
	private string lookAt; //metavliti p krato p vlepi o enemy left or right
	private bool isJump=false;
	private Animator animator;
	private Renderer renderer;
	private Rigidbody rigidBody;
	private float ratefire;

	public Transform enemyBullet;
	private float creationTime;

	

	// Use this for initialization
	void Start () {
		//
		//player = GameObject.FindGameObjectWithTag(("Player"));
		renderer = this.GetComponent<Renderer> ();
		rigidBody = GetComponent<Rigidbody> ();
		//camera = GameObject.FindGameObjectWithTag(("MainCamera"));
		//
		int lookLeft;
		repository = GameRepository.Instance;
		target = GameObject.FindGameObjectWithTag("Player");
		damage = Random.Range (10.0f, 80.0f);
		//
		//this.GetComponent<BoxCollider>().size = new Vector3(0.01f,0.41f,0.2f);
		//this.transform.localScale = new Vector3(1.0f,3.0f,0.0f);
		//this.GetComponent<BoxCollider>().size=0.01f;
		//

		vision= 6.0f; // apostasi oratotitas
		speed=0.05f;
		life=100.0f;
		damage=30.0f;
		jump=300.0f;
		ratefire=1.0f;

		animator = GetComponent<Animator> ();

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

		//get creation time
		creationTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (repository.isPaused()) {
			rigidBody.Sleep();
			return;
		}

		if (life<=0.0f){
			Destroy(this.gameObject );
		}


		if (findDimension==false){ // monon otan o enemi topothetithi tin proti fora tha ginonte i pio kato elexi prin oxi
			// pote tha fenete kai pote oxi ekthors 
			if (repository.isRaised() || repository.isRotating() ) {
				renderer.enabled = true;
				return;
			}
			
			if ((repository.getCurrentDimension() == Dimension.FRONT) || (repository.getCurrentDimension() == Dimension.BACK)) { //dimension cube = 0
				if (target.transform.position.z == this.transform.position.z) {
					//Debug.Log("Z - Z");
					renderer.enabled = true;
				}
				else {
					//renderer.enabled = true; //prosorino gia na kano kapious elexous
					renderer.enabled = false;
					//Debug.Log("Z !- Z");
				}
			} 
			else  {
				if (target.transform.position.x == this.transform.position.x) {
					renderer.enabled = true;
					//Debug.Log("X - X");
				}
				else {
					//renderer.enabled = true; //prosorino gia na kano kapious elexous
					renderer.enabled = false;
					//Debug.Log("X !- X");
				}
			}
		}

		//
		if ((renderer.enabled==true)&&(findDimension==false)){ // mono an o ekthos in oratos kani tous elexous
			//vison ray
			bool isVisionRayHit = false;
			RaycastHit visionRayHit ;
			Vector3 startPosVisionRay= new Vector3 (transform.position.x,transform.position.y + 0.6f,transform.position.z); //exi sxesi me to ipsos tou enemy to 1.0f
			Vector3 visionRayDirection = target.transform.position - transform.position;
			if (((visionRayDirection.x < 0) && (lookAt=="right") && (Mathf.Abs(visionRayDirection.z)<=1)) || ((visionRayDirection.z < 0)&& (lookAt=="right") && (Mathf.Abs(visionRayDirection.x)<=1)) ) {

				isVisionRayHit = Physics.Raycast (startPosVisionRay, visionRayDirection, out visionRayHit, vision / 2);

			} 
			else if ((Mathf.Abs(visionRayDirection.z)<=1) || (Mathf.Abs(visionRayDirection.x)<=1))  {

				isVisionRayHit = Physics.Raycast (startPosVisionRay, visionRayDirection, out visionRayHit, vision);
			}

			string tagVisonRayHit="";

			if (isVisionRayHit) {
				tagVisonRayHit = visionRayHit.transform.tag;  

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
						//if (Mathf.Abs(target.transform.position.z-transform.position.z)<=1){ //elenxos an vriskonte sto dio z
						//	float distance=target.transform.position.x-transform.position.x;
						//	if (Mathf.Abs(distance)<=vision){ // vriskete entos oratotitas
						if (dimension == Dimension.FRONT) {
							if (visionRayDirection.x < 0) { //elenxos an o pektis ine aristera
								transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z); //kino ton ekthro aristera
								if (lookAt == "right") { //kano rotate to sprite an alakso katefthinsi
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),-180.0f);
								}
								lookAt = "left";
							} else { //elenxos an o pektis ine deksia
								transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z); //kino ton ekthro deksia
								if (lookAt == "left") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
								}
								lookAt = "right";
							}
						} else {
							if (visionRayDirection.x > 0) { //elenxos an o pektis ine aristera
								transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z); //kino ton ekthro aristera
								if (lookAt == "right") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),-180.0f);
								}
								lookAt = "left";
							} else {
								transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z); //kino ton ekthro deksia
								if (lookAt == "left") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
								}
								lookAt = "right";
							}
						}
						/*}
							}*/
					} else { //dimension==3 || dimension==4
						//if (Mathf.Abs(target.transform.position.x-transform.position.x)<=1){ //elenxos an vriskonte sto dio z
						//	float distance=target.transform.position.z-transform.position.z;
						//	if (Mathf.Abs(distance)<=vision){ // vriskete entos oratotitas
						if (dimension == Dimension.RIGHT) {
							if (visionRayDirection.z < 0) { //elenxos an o pektis ine aristera
								transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - speed); //kino ton ekthro aristera
								if (lookAt == "right") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),-180.0f);
								}
								//Debug.Log("test 1");
								lookAt = "left";
							} else { //elenxos an o pektis ine deksia
								transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + speed); //kino ton ekthro deksia
								if (lookAt == "left") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
								}
								//Debug.Log("test 2");
								lookAt = "right";
							}
						} else {
							if (visionRayDirection.z > 0) { //elenxos an o pektis ine aristera
								transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + speed); //kino ton ekthro aristera

								if (lookAt == "right") {
									transform.Rotate(new Vector3(0.0f,1.0f,0.0f),-180.0f);
								}
								//Debug.Log("test 3");
								lookAt = "left";
							} else {
								transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - speed); //kino ton ekthro deksia
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
						newEnemyBullet.tag = "enemyBullet";
					}
					creationTime = Time.time;
				}
			}
		}
		animator.SetBool("enemyWalk", false);
	}

	void OnCollisionEnter(Collision other){  

		if ( (other.gameObject.tag == "Player") && (dimension==repository.getCurrentDimension())) {
			repository.losePlayerLife(damage);
		}
		if (findDimension){ //elexos gia na vro ti diastasi ke na kano to analogo rotation tou enemy elexo mono tin proti fora
			if((other.gameObject.tag=="StaticCube") || (other.gameObject.tag=="MovableCube")){ 
				findDimension=false;
				//Debug.Log("-------");
				/*if(other.transform.localEulerAngles.y==0)*/
				//Debug.Log(other.gameObject.GetComponent<Cube>().getDimension());
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
				this.transform.localScale = new Vector3(3.0f,3.0f,0.0f);
				this.GetComponent<BoxCollider>().size = new Vector3(0.37f,0.41f,0.2f);
				Vector3 posi=other.gameObject.GetComponent<Transform>().transform.position; //epanatopotheto ton enemy sto kentro tou cube
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
			Destroy(this.gameObject );
		}
		//}
	}

	public void loseEnemyLife(float lose) { //aferi zoi tou enemy
		life -= lose;
	}



	
	
}
