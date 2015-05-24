using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
	private float playerLife = 1000.0f;
	private int playerLives = 3;
	private float timestamp = Time.deltaTime;

	//camera specific variables
	private bool rotate = false;
	private bool raise = false;
	private bool pause = true;
	private bool sounds = true;
	private bool music = true;
	private bool pauseScreen = false;
	private bool mainScreen = true;
	private bool gameOverScreen = false;

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

	public static Dimension getCurrentDimension() {
		return instance.currentDimension;
	}

	public static void setCurrentDimension(Dimension dimension) {
		instance.currentDimension = dimension;
	}

	public static float getPlayerLife() {
		return instance.playerLife;
	}

	public static void setPlayerLife(float plife) {
		instance.playerLife = plife;
		Image healthBar = GameObject.FindGameObjectWithTag ("HealthBar").GetComponent<Image>();
		healthBar.fillAmount = instance.playerLife / 1000.0f;
		Image deathBar = GameObject.FindGameObjectWithTag ("DeathBar").GetComponent<Image>();
		deathBar.fillAmount = 1.0f - instance.playerLife / 1000.0f;
	}

	public static void losePlayerLife(float lose) {
		instance.playerLife -= lose;
		Image healthBar = GameObject.FindGameObjectWithTag ("HealthBar").GetComponent<Image>();
		healthBar.fillAmount = instance.playerLife / 1000.0f;
		Image deathBar = GameObject.FindGameObjectWithTag ("DeathBar").GetComponent<Image>();
		deathBar.fillAmount = 1.0f - instance.playerLife / 1000.0f;
	}

	public static void winPlayerLife(float win) {
		instance.playerLife += win;
	}

	public static void setRotate(bool rotateInput){
		instance.rotate = rotateInput;
	}

	public static bool isRotating(){
		return instance.rotate;
	}

	public static void setRaise(bool raiseInput){
		instance.raise = raiseInput;
	}
	
	public static bool isRaised(){
		return instance.raise;
	}

	public static float getBackgroundSpeed(){
		return instance.backgroundSpeed;
	}

	public static void setBackgroundSpeed(float speed){
		instance.backgroundSpeed = speed;
	}

	public static void setPause(bool pauseInput){
		instance.pause = pauseInput;
	}
	
	public static bool isPaused(){
		return instance.pause;
	}

	public static void setMusic(bool musicInput){
		instance.music = musicInput;
	}
	
	public static bool isMusicOn(){
		return instance.music;
	}

	public static void setSounds(bool soundsInput){
		instance.sounds = soundsInput;
	}
	
	public static bool isSoundsOn(){
		return instance.sounds;
	}
	
	public static float getTimeStamp(){
		return instance.timestamp;
	}

	public static void setPauseScreen(bool pauseScreen){
		instance.pauseScreen = pauseScreen;
	}
	
	public static bool isPauseScreen(){
		return instance.pauseScreen;
	}

	public static void setMainScreen(bool mainScreen){
		instance.mainScreen = mainScreen;
	}
	
	public static bool isMainScreen(){
		return instance.mainScreen;
	}

	public static void setGameOverScreen(bool gameOverScreen){
		instance.gameOverScreen = gameOverScreen;
	}
	
	public static bool isGameOverScreen(){
		return instance.gameOverScreen;
	}
}
