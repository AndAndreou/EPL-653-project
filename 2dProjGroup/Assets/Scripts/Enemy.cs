using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private GameObject target; // metavliti p krata ton pekti mas
	private GameRepository GameR; //GameRepository
	private float vision= 6.0f; // apostasi oratotitas
	private Dimension dimension ; //metavliti p krata se pia diastasi ine o enemy
	private float speed=0.05f;
	private float life=100;
	private float damage=30;
	private float jump=300.0f;
	private bool findDimension= true;
	private string lookAt; //metavliti p krato p vlepi o enemy left or right
	private bool isJump=false;


	
	// Use this for initialization
	void Start () {
		int lookLeft;
		GameR = GameRepository.getInstance();
		target = GameObject.FindGameObjectWithTag("Player");
		damage = Random.Range (10.0f, 80.0f);
		Color c = new Color ((damage/100.0f),0.0f,0.0f,1.0f);
		GetComponent<SpriteRenderer>().color = c;
		lookLeft = Random.Range (0, 10);
		if (lookLeft < 5) { //vlepi aristera
			lookAt = "left";
		} 
		else { //vlepi deksia
			lookAt = "right";
			transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
		}

		//spriteRenderer.sprite = sp1 ;
		//dimension=0;// dokimastika
	}
	
	// Update is called once per frame
	void Update () {

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

		//if(GameR.getCurrentDimension() == dimension){  //elenxos an vriskonte stin idia diastasi
			//if (Mathf.Abs(target.transform.position.y-transform.position.y)<=2){ //elenxos an vriskonte sto dio y
			if ((isVisionRayHit) && (tagVisonRayHit == "Player")) {
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
						if (visionRayDirection.x < 0) { //elenxos an o pektis ine aristera
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
							lookAt = "left";
						} else { //elenxos an o pektis ine deksia
							transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + speed); //kino ton ekthro deksia
							if (lookAt == "left") {
								transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
							}
							lookAt = "right";
						}
					} else {
						if (visionRayDirection.z < 0) { //elenxos an o pektis ine aristera
							transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + speed); //kino ton ekthro aristera

							if (lookAt == "right") {
								transform.Rotate(new Vector3(0.0f,1.0f,0.0f),-180.0f);
							}
							lookAt = "left";
						} else {
							transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - speed); //kino ton ekthro deksia
							if (lookAt == "left") {
								transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
							}
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
			
			isRayHit = Physics.Raycast (startPosRay, RayDirection, out RayHit, 1);
			
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
		}
	}

	void OnCollisionEnter(Collision other){  
		if ( (other.gameObject.tag == "Player") && (dimension==GameR.getCurrentDimension())) {
			GameR.losePlayerLife(damage);
		}
		if (findDimension){ //elexos gia na vro ti diastasi ke na kano to analogo rotation tou enemy elexo mono tin proti fora
			if((other.gameObject.tag=="StaticCube") || (other.gameObject.tag=="MovableCube")){ 
				
				if(other.transform.localEulerAngles.y==0){ //dimension cube = 0
					transform.Rotate(new Vector3(0.0f,0.0f,0.0f));
					dimension=Dimension.FRONT;
				}
				else if(other.transform.localEulerAngles.y==180){ //dimension cube = 1
					//Debug.Log("test");
					transform.Rotate(new Vector3(0.0f,-180.0f,0.0f));
					//Debug.Log("test1");
					dimension=Dimension.BACK;
				}
				else if(other.transform.localEulerAngles.y==270){ //dimension cube = 2
					transform.Rotate(new Vector3(0.0f,-90.0f,0.0f));
					dimension=Dimension.RIGHT;
				}
				else if(other.transform.localEulerAngles.y==90){ //dimension cube = 3
					transform.Rotate(new Vector3(0.0f,-270.0f,0.0f));
					dimension=Dimension.LEFT;
				}
			}
			findDimension=false;
		}

		// elexos gia jump tou enemy
		/*if (((other.gameObject.tag == "StaticCube") || (other.gameObject.tag == "MovableCube")) && (Mathf.Abs(other.transform.position.y-transform.position.y)>0.0f)&& (Mathf.Abs(other.transform.position.y-transform.position.y)<1.0f)) { //o ekthros pida an vriski empodio 
			//transform.position = new Vector3(transform.position.x, transform.position.y + jump, transform.position.z);
			if (!isJump ) {
				GetComponent<Rigidbody>().AddForce(Vector3.up * jump);
			}*/
		if ((other.gameObject.tag == "StaticCube") || (other.gameObject.tag == "MovableCube")) {

			if (GetComponent<Rigidbody> ().velocity.y == 0.0f) {
				//Debug.Log("*****************");
				isJump = false;
			}
		}
		//}
	}
	
	void OnTriggerEnter(Collider other){
		
		
	}
	
	
}
