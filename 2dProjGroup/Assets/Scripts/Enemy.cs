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
	private float jump=1.5f;
	private bool findDimension= true;
	private string lookAt; //metavliti p krato p vlepi o enemy left or right
	private int lookLef;


	
	// Use this for initialization
	void Start () {
		GameR = GameRepository.getInstance();
		target = GameObject.FindGameObjectWithTag("Player");
		damage = Random.Range (10.0f, 80.0f);
		Color c = new Color ((damage/100.0f),0.0f,0.0f,1.0f);
		GetComponent<SpriteRenderer>().color = c;
		lookLef = Random.Range (0, 10);
		if (lookLef < 5) { //vlepi aristera
			lookAt = "left";
		} 
		else {
			lookAt = "right";
			transform.Rotate(new Vector3(0.0f,1.0f,0.0f),180.0f);
		}

		//spriteRenderer.sprite = sp1 ;
		//dimension=0;// dokimastika
	}
	
	// Update is called once per frame
	void Update () {

		bool isRayHit = false;
		RaycastHit rayHit ;
		Vector3 startPosRay= new Vector3 (transform.position.x,transform.position.y,transform.position.z);
		Vector3 rayDirection = target.transform.position - transform.position;
		Debug.Log("*****" + lookAt);
		if (((rayDirection.x < 0) && (lookAt=="right") && (Mathf.Abs(rayDirection.z)<=1)) || ((rayDirection.z < 0)&& (lookAt=="right") && (Mathf.Abs(rayDirection.x)<=1)) ) {
			isRayHit = Physics.Raycast (startPosRay, rayDirection, out rayHit, vision / 2);
			Debug.Log("-----test1");

		} 
		else if ((Mathf.Abs(rayDirection.z)<=1) || (Mathf.Abs(rayDirection.x)<=1))  {
			isRayHit = Physics.Raycast (startPosRay, rayDirection, out rayHit, vision);
			Debug.Log("-----test2");
		}

		string tagRayHit="";

		if (isRayHit) {
			tagRayHit = rayHit.transform.tag;  
			Debug.DrawLine (startPosRay, target.transform.position, Color.green);
			Debug.Log (rayDirection);
		}


		//if(GameR.getCurrentDimension() == dimension){  //elenxos an vriskonte stin idia diastasi
			//if (Mathf.Abs(target.transform.position.y-transform.position.y)<=2){ //elenxos an vriskonte sto dio y
			if ((isRayHit) && (tagRayHit == "Player")) {
				if (dimension == Dimension.FRONT || dimension == Dimension.BACK) {
					//if (Mathf.Abs(target.transform.position.z-transform.position.z)<=1){ //elenxos an vriskonte sto dio z
					//	float distance=target.transform.position.x-transform.position.x;
					//	if (Mathf.Abs(distance)<=vision){ // vriskete entos oratotitas
					if (dimension == Dimension.FRONT) {
						if (rayDirection.x < 0) { //elenxos an o pektis ine aristera
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
						if (rayDirection.x < 0) { //elenxos an o pektis ine aristera
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
						if (rayDirection.z < 0) { //elenxos an o pektis ine aristera
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
						if (rayDirection.z < 0) { //elenxos an o pektis ine aristera
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
		}
	}

	void OnCollisionEnter(Collision other){  
		if ( (other.gameObject.tag == "Player") && (dimension==GameR.getCurrentDimension())) {
			GameR.losePlayerLife(damage);
		}
		if (findDimension){ //elexos gia na vro ti diastasi ke na kano to analogo rotation tou enemy elexo mono tin proti fora
			if(other.gameObject.tag=="StaticCube"){ 
				
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
		
		if (((other.gameObject.tag == "StaticCube") || (other.gameObject.tag == "MovableCube")) && (Mathf.Abs(other.transform.position.y-transform.position.y)>0.0f)&& (Mathf.Abs(other.transform.position.y-transform.position.y)<1.0f)) { //o ekthros pida an vriski empodio 
			transform.position = new Vector3(transform.position.x, transform.position.y + jump, transform.position.z);
		}
	}
	
	void OnTriggerEnter(Collider other){
		
		
	}
	
	
}
