using UnityEngine;
using System.Collections;

public class EnergyBullet : MonoBehaviour {
	private float bulletSpeed;
	private Transform rigidBodyTransform;
	private GameObject player;
	private Rigidbody bulletRigidBody;
	private Rigidbody playerRigidBody;
	private GameRepository repository;
	private Player playerScript;

	// Use this for initialization
	void Start () {
		bulletSpeed = 0.2f;

		//repository = GameRepository.getInstance ();
			
		bulletRigidBody = this.GetComponent<Rigidbody> ();
		rigidBodyTransform = bulletRigidBody.transform;

		player = GameObject.FindGameObjectWithTag ("Player") as GameObject;
		playerRigidBody = player.GetComponent<Rigidbody> ();
		playerScript = player.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		float xDif = bulletRigidBody.position.x - playerRigidBody.position.x;
		float zDif = bulletRigidBody.position.z - playerRigidBody.position.z;

		Debug.Log (playerScript.getPlayerDimension());

		if (playerScript.getPlayerDimension() == Dimension.FRONT) {
			if (xDif > 0) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x + bulletSpeed, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
			else {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x - bulletSpeed, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
		} else if (playerScript.getPlayerDimension() == Dimension.BACK) {
			if (xDif < 0) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x - bulletSpeed, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
			else {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x + bulletSpeed, rigidBodyTransform.position.y, rigidBodyTransform.position.z);
			}
		} else if (playerScript.getPlayerDimension() == Dimension.RIGHT) {
			if (zDif > 0) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - bulletSpeed);
			}
			else {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + bulletSpeed);
			}
		} else if (playerScript.getPlayerDimension() == Dimension.LEFT) {
			if (zDif > 0) {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z - bulletSpeed);
			}
			else {
				rigidBodyTransform.position = new Vector3 (rigidBodyTransform.position.x, rigidBodyTransform.position.y, rigidBodyTransform.position.z + bulletSpeed);
			}
		}
	}
}
