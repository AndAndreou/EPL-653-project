using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	private Rigidbody bulletRigidBody;
	private float bulletSpeed=10.0f;
	private GameObject target;
	private GameRepository repository;
	private float damage = 10.0f; //damage sferas 
	private Vector3 tarpos;
	private Vector3 norm; 
	
	// Use this for initialization
	void Start () {

		target = GameObject.FindGameObjectWithTag("Player");
		repository = GameRepository.Instance;

		tarpos=target.transform.position ;
		norm = (tarpos - transform.position).normalized;

		bulletRigidBody = this.GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (repository.isPaused()) {
			bulletRigidBody.Sleep();
			return;
		}

		transform.position += norm * bulletSpeed * Time.deltaTime;

	}
	
	void OnCollisionEnter(Collision other){ 
		if (other.gameObject.tag == "Player") {
			repository.losePlayerLife(damage);
		}
		if (other.gameObject.tag!= "Enemy")
		Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider other){

	}
}
