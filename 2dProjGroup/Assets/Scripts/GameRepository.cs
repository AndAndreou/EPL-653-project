using UnityEngine;
using System.Collections;

/*
 * This class is the repository of the scene.
 * 
 * It holds information that is common for many components of the game
 * 
 * It is based on the Singleton Design Pattern
 * 
 */
public class GameRepository : MonoBehaviour {  
	//Singleton specific attributes
	private static GameRepository instance = null;
	bool created = false;

	//common game variables
	private Dimension currentDimension;
	private float playerLife;

	private GameRepository () { }


	public static GameRepository getInstance() {
		if (instance == null) {
			instance = new GameRepository();
			instance.playerLife = 100.0f;
			instance.currentDimension = Dimension.FRONT;
		}
		return instance;
	}


	//Game engine methods

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}


	//setters and getters

	public Dimension getCurrentDimension() {
		return instance.currentDimension;
	}

	public void setCurrentDimension(Dimension dimension) {
		instance.currentDimension = dimension;
	}

	public float getPlayerLife() {
		return instance.playerLife;
	}

	public void setPlayerLife(float plife) {
		instance.playerLife = plife;
	}

	public void losePlayerLife(float lose) {
		instance.playerLife -= lose;
	}

	public void winPlayerLife(float win) {
		instance.playerLife += win;
	}
}
