using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
	private Renderer renderer;
	private GameRepository repository; //GameRepository
	private Vector2 offset;
	// Use this for initialization
	void Start () {
		offset = new Vector2 (0.0f, 0.0f);
		repository = GameRepository.getInstance();
		renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (repository.getBackgroundSpeed () == 0.0f) {
			renderer.material.mainTextureOffset = new Vector2 (offset.x, offset.y);
		} else {
			if (this.gameObject.tag == "Background 1") {
				renderer.material.mainTextureOffset = new Vector2 (offset.x + repository.getBackgroundSpeed (), 0.0f);
			}
		}
		if (this.gameObject.tag == "Background 2") {
			renderer.material.mainTextureOffset = new Vector2 (Time.time * 0.02f, 0.0f);
		}
		offset = new Vector2(renderer.material.mainTextureOffset.x, renderer.material.mainTextureOffset.y);

	}
}
