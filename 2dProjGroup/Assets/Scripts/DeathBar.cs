using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathBar : MonoBehaviour {
	Image deathBar;
	// Use this for initialization
	void Start () {
		deathBar = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		deathBar.fillAmount = 1.0f - GameRepository.getPlayerLife() / 1000.0f;
	}
}
