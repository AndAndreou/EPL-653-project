using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	private Image healthBar;

	// Use this for initialization
	void Start () {
		healthBar = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.fillAmount = GameRepository.getPlayerLife() / 1000.0f;
	}
}
