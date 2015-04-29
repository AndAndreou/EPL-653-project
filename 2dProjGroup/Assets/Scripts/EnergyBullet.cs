﻿using UnityEngine;
using System.Collections;

public class EnergyBullet : MonoBehaviour {
	private float bulletSpeed;
	private Transform rigidBodyTransform;
	private Rigidbody bulletRigidBody;
	private Dimension bulletDimension;
	private GameRepository repository;
	private float damage = 50.0f; //damage sferas
	private AudioSource sound;
	
	float xDif, zDif, yDif;
	
	// Use this for initialization
	void Start () {
		Player playerScript;
		Rigidbody playerRigidBody;
		GameObject player;

		
		repository = GameRepository.Instance;
		
		bulletSpeed = 0.2f;
		bulletDimension = repository.getCurrentDimension ();
		bulletRigidBody = this.GetComponent<Rigidbody> ();
		rigidBodyTransform = bulletRigidBody.transform;
		
		player = GameObject.FindGameObjectWithTag ("Player") as GameObject;
		playerRigidBody = player.GetComponent<Rigidbody> ();
		
		//Get the direction of the bullet according to player
		xDif = bulletRigidBody.position.x - playerRigidBody.position.x;
		zDif = bulletRigidBody.position.z - playerRigidBody.position.z;
		
		float yRotate = (90 * (int)bulletDimension) % 180;
		transform.Rotate(new Vector3(0.0f,yRotate,0.0f));

		sound = gameObject.GetComponent<AudioSource>();
		sound.volume = 0.2f;
		sound.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (repository.isPaused()) {
			bulletRigidBody.Sleep();
			return;
		}

		if ( ( bulletRigidBody.position.x > 300 ) || (bulletRigidBody.position.x < -100) ||
		    (bulletRigidBody.position.z > 300) || (bulletRigidBody.position.z < -300) ) {
			Destroy(this.gameObject );
			this.GetComponent<Renderer>();
		}
		
		if (bulletDimension == Dimension.FRONT) {
			if (xDif > 0) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x + bulletSpeed, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
			else {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x - bulletSpeed, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
		} else if (bulletDimension == Dimension.BACK) {
			if (xDif < 0) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x - bulletSpeed, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
			else {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x + bulletSpeed, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
		} else if (bulletDimension == Dimension.RIGHT) {
			if (zDif > 0) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + bulletSpeed);
			}
			else {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - bulletSpeed);
			}
		} else if (bulletDimension == Dimension.LEFT) {
			if (zDif > 0) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + bulletSpeed);
			}
			else {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - bulletSpeed);
			}
		}
	}

	void OnCollisionEnter(Collision other){ 
		if ( (other.gameObject.tag == "Enemy") && (other.gameObject.GetComponent<Renderer>().enabled==true)) {
			other.gameObject.GetComponent<Enemy>().loseEnemyLife(damage);
			//Destroy(this.gameObject);
		}
		Destroy(this.gameObject);
	}
	
}
