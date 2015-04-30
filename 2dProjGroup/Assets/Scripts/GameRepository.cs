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
public sealed class GameRepository : MonoBehaviour {  
	//Singleton specific attributes
	private static readonly GameRepository instance = new GameRepository();

	//common game variables
	private Dimension currentDimension = Dimension.FRONT;
	private float playerLife = 100000.0f;

	//camera specific variables
	private bool rotate = false;
	private bool raise = false;
	private bool pause = false;
	private bool sounds = true;
	private bool music = true;

	//background specific variables
	private float backgroundSpeed = 0.0f;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static GameRepository()
	{
	}

	private GameRepository () { }

	public static GameRepository Instance
	{
		get
		{
			return instance;
		}
	}

//	public static GameRepository getInstance() {
//		if (instance == null) {
//			instance = new GameRepository();
//			instance.playerLife = 100000.0f;
//			instance.currentDimension = Dimension.FRONT;
//			instance.rotate = false;
//			instance.raise = false;
//		}
//		return instance;
//	}


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

	public void setRotate(bool rotateInput){
		instance.rotate = rotateInput;
	}

	public bool isRotating(){
		return instance.rotate;
	}

	public void setRaise(bool raiseInput){
		instance.raise = raiseInput;
	}
	
	public bool isRaised(){
		return instance.raise;
	}

	public float getBackgroundSpeed(){
		return instance.backgroundSpeed;
	}

	public void setBackgroundSpeed(float speed){
		instance.backgroundSpeed = speed;
	}

	public void setPause(bool pauseInput){
		instance.pause = pauseInput;
	}
	
	public bool isPaused(){
		return instance.pause;
	}

	public void setMusic(bool musicInput){
		instance.music = musicInput;
	}
	
	public bool isMusicOn(){
		return instance.music;
	}

	public void setSounds(bool soundsInput){
		instance.sounds = soundsInput;
	}
	
	public bool isSoundsOn(){
		return instance.sounds;
	}
}
