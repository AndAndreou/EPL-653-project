using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private float pos;
	private Transform tr;
	private GameRepository repository;
	private Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		tr = GetComponent<Rigidbody> ().transform;
		repository = GameRepository.getInstance();
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)) {
			if (repository.getCurrentDimension() == Dimension.FRONT) {
				tr.position = new Vector3( tr.position.x - 0.1f, tr.position.y, tr.position.z);
			}
			if (repository.getCurrentDimension() == Dimension.RIGHT) {
				tr.position = new Vector3( tr.position.x, tr.position.y, tr.position.z - 0.1f);
			}
			if (repository.getCurrentDimension() == Dimension.BACK) {
				tr.position = new Vector3( tr.position.x + 0.1f, tr.position.y, tr.position.z);
			}
			if (repository.getCurrentDimension() == Dimension.LEFT) {
				tr.position = new Vector3( tr.position.x, tr.position.y, tr.position.z + 0.1f);
			}
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			if (repository.getCurrentDimension() == Dimension.FRONT) {
				tr.position = new Vector3( tr.position.x + 0.1f, tr.position.y, tr.position.z);
			}
			if (repository.getCurrentDimension() == Dimension.RIGHT) {
				tr.position = new Vector3( tr.position.x, tr.position.y, tr.position.z + 0.1f);
			}
			if (repository.getCurrentDimension() == Dimension.BACK) {
				tr.position = new Vector3( tr.position.x - 0.1f, tr.position.y, tr.position.z);
			}
			if (repository.getCurrentDimension() == Dimension.LEFT) {
				tr.position = new Vector3( tr.position.x, tr.position.y, tr.position.z - 0.1f);
			}
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			tr.position = new Vector3 (Mathf.Round(tr.position.x) , tr.position.y, Mathf.Round(tr.position.z));
			tr.Rotate(new Vector3( 0.0f, -90.0f, 0.0f));

			if (repository.getCurrentDimension() == Dimension.FRONT || repository.getCurrentDimension() == Dimension.BACK) {
				rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
			}
			if (repository.getCurrentDimension() == Dimension.RIGHT || repository.getCurrentDimension() == Dimension.LEFT) {
				rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
			}
		}
		
		//new code
		//Debug.Log (repository.getPlayerLife());
		if (repository.getPlayerLife() <= 0.0f) {
			Destroy(this);
			//end game
		}
	}
	
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			//pos += Time.deltaTime;
			if (repository.getCurrentDimension() == Dimension.FRONT || repository.getCurrentDimension() == Dimension.BACK) {
				tr.position = new Vector3(tr.position.x, tr.position.y + 1.5f, tr.position.z);
			}
			if (repository.getCurrentDimension() == Dimension.RIGHT || repository.getCurrentDimension() == Dimension.LEFT) {
				tr.position = new Vector3(tr.position.x, tr.position.y + 1.5f, tr.position.z);
			}
		}
	}
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.name == "Cube 8") {
			//Debug.Log("Collides");
		}
	}
}