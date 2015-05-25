using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {
	//private GameRepository repository;
	//refrence for the pause menu panel in the hierarchy
	public GameObject pauseMenuPanel;
	private GameObject resumeButton;
	private GameObject endScore;
	private GameObject scoreText;
	private GameObject pauseButton;
	private GameObject pauseText;
	private GameObject logoImage;
	private GameObject healthBar;
	private GameObject deathBar;
	private GameObject audioOptions;
	private GameObject soundsToggle;
	private GameObject optionsToggle;
	private GameObject musicOnObj;
	private GameObject musicOffObj;
	private GameObject soundsOnObj;
	private GameObject soundsOffObj;
	private GameObject optionsOnObj;
	private GameObject optionsOffObj;
	private GameObject particles;
	private Image backgroundImage;
	private SceneInit_intro firstScecne;
	private ParticleSystem psBirds;
	//animator reference
	private Animator anim;
	//variable for checking if the game is paused 
	private bool isPaused = true;
	private bool isPauseScreen = false;
	// Use this for initialization

	void Start () {
		resumeButton = GameObject.FindGameObjectWithTag ("Resume");
		endScore = GameObject.FindGameObjectWithTag ("EndScore");
		scoreText = GameObject.FindGameObjectWithTag ("ScoreText");
		pauseText = GameObject.FindGameObjectWithTag ("PauseText");
		logoImage = GameObject.FindGameObjectWithTag ("Logo");
		healthBar = GameObject.FindGameObjectWithTag ("HealthBar");
		deathBar = GameObject.FindGameObjectWithTag ("DeathBar");
		audioOptions = GameObject.FindGameObjectWithTag ("AudioOptions");
		musicOnObj = GameObject.FindGameObjectWithTag ("MusicOnObj");
		musicOffObj = GameObject.FindGameObjectWithTag ("MusicOffObj");
		soundsOnObj = GameObject.FindGameObjectWithTag ("SoundsOnObj");
		soundsOffObj = GameObject.FindGameObjectWithTag ("SoundsOffObj");
		optionsOnObj = GameObject.FindGameObjectWithTag ("OptionsOnObj");
		optionsOffObj = GameObject.FindGameObjectWithTag ("OptionsOffObj");
		particles = GameObject.FindGameObjectWithTag ("ParticleBirds");
		firstScecne = GameObject.Find("SceneInit").GetComponent<SceneInit_intro> ();
		//repository = GameRepository.Instance;
		//unpause the game on start
		//Time.timeScale = 1;
		//get the animator component
		pauseMenuPanel = GameObject.FindGameObjectWithTag ("BackGroundImage");
		backgroundImage = GameObject.FindGameObjectWithTag ("BackGroundImage").GetComponent<Image> ();
		anim = pauseMenuPanel.GetComponent<Animator>();
		//disable it on start to stop it from playing the default animation
		anim.enabled = false;

		pauseButton = GameObject.FindGameObjectWithTag ("PauseButton");
		if (GameRepository.isMainScreen ()) {
			pauseButton.SetActive (false);
			deathBar.SetActive (false);
			healthBar.SetActive (false);
			//backgroundImage.color = new Color( backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0f);
			pauseMenuPanel.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0.0f, 25.0f);

			backgroundImage.sprite = Resources.Load<Sprite> ("images/background");
			//backgroundImage.color = new Color(30f, 50f, 50f);
			backgroundImage.CrossFadeColor (new Color (30f, 50f, 50f, 180f), 2.0f, false, true);
			//backgroundImage.CrossFadeAlpha(0.8f, 1.0f, false);
			Debug.Log ("Pauses Start");
			logoImage.SetActive (true);
			pauseText.SetActive (false);
			scoreText.SetActive (false);
			//PauseGame ();
		}

		audioOptions.SetActive (false);
		musicOnObj.SetActive(false);
		musicOffObj.SetActive(true);
		soundsOnObj.SetActive(false);
		soundsOffObj.SetActive(true);
		optionsOnObj.SetActive(false);
		optionsOffObj.SetActive(true);
		endScore.SetActive (false);
		
		psBirds = particles.gameObject.GetComponent<ParticleSystem> ();
		psBirds.Stop ();
	}
	
	// Update is called once per frame
	public void Update () {
		if(Input.GetKeyUp(KeyCode.P) && !GameRepository.isPaused() && GameRepository.isPauseScreen ()){
			PauseGame();
			psBirds.Pause();
		} else if(Input.GetKeyUp(KeyCode.P) && GameRepository.isPaused() && GameRepository.isPauseScreen ()){
			UnpauseGame();
			psBirds.Play();
		}

		if (Input.GetKey (KeyCode.LeftArrow) && (!GameRepository.isRotating())){
			//psBirds.Simulate(0.03f, true, false);
			psBirds.startSpeed = 8;
		}
		if (Input.GetKey (KeyCode.RightArrow) && (!GameRepository.isRotating())){
			psBirds.startSpeed = 4;
			//psBirds.Simulate(0.01f, true, false);
		}
		psBirds.startSpeed = 4;
		//psBirds.Simulate(0.01f, true, false);
	}
	
	//function to pause the game
	public void PauseGame(){
		if (GameRepository.isGameOverScreen ()) {
			logoImage.SetActive(false);
			pauseText.SetActive(true);

			Image resumeImage = resumeButton.GetComponent<Image>();
			resumeImage.sprite = Resources.Load<Sprite>("images/replay");

			pauseText.GetComponent<Text>().text = "Game Over";
			backgroundImage.sprite = Resources.Load<Sprite>("images/backgroundTransparent");
			//backgroundImage.color = new Color(0f, 150f, 0f);
			//backgroundImage.CrossFadeAlpha(1.0f, 2.0f, false);
			backgroundImage.CrossFadeColor(new Color(150f, 0f, 0f, 255f), 2.0f, false, true);
			GameRepository.setPause (true);
			anim.Play ("pauseAnimDown");
			isPaused = true;
			Debug.Log ("Pauses GameOver");
			endScore.SetActive(true);
			endScore.GetComponent<Text> ().text = "Score\n" + GameRepository.getScore();

			audioOptions.SetActive (false);
			musicOnObj.SetActive(false);
			musicOffObj.SetActive(false);
			soundsOnObj.SetActive(false);
			soundsOffObj.SetActive(false);
			optionsOnObj.SetActive(false);
			optionsOffObj.SetActive(false);
		}
		if (GameRepository.isPauseScreen ()) {
			logoImage.SetActive(false);
			pauseText.SetActive(true);
			backgroundImage.sprite = Resources.Load<Sprite>("images/backgroundTransparent");
			//backgroundImage.color = new Color(0f, 150f, 0f);
			//backgroundImage.CrossFadeAlpha(1.0f, 2.0f, false);
			backgroundImage.CrossFadeColor(new Color(0f, 150f, 0f, 255f), 2.0f, false, true);
			GameRepository.setPause (true);
			anim.Play ("pauseAnimDown");
			isPaused = true;
			Debug.Log ("Pauses Pause");
			//backgroundImage.CrossFadeColor (new Color (0f, 150f, 0f, 255f), 2.0f, false, true);
		}
		if (GameRepository.isMainScreen ()) {
			GameRepository.setPause (true);
			anim.enabled = true;
			Debug.Log ("Pauses Main");
		}
		pauseButton.SetActive (false);
		deathBar.SetActive (false);
		healthBar.SetActive (false);
		scoreText.SetActive (false);
	}
	//function to unpause the game
	public void UnpauseGame(){
		if (GameRepository.isMainScreen ()) {
			SceneInit_intro scene_intro = GameObject.Find("SceneInit").GetComponent<SceneInit_intro>();
			SceneInit_first scene_first = GameObject.Find("SceneInit").GetComponent<SceneInit_first>();
			SceneInit_3th scene_second = GameObject.Find("SceneInit").GetComponent<SceneInit_3th>();
			if (scene_intro != null) {
				scene_intro.createScene();
			}
			else if (scene_first != null) {
				scene_first.createScene();
			}
			else if (scene_second != null) {
				scene_second.createScene();
			}
			GameRepository.setPauseScreen (true);
			GameRepository.setMainScreen (false);
			anim.enabled = true;
			//backgroundImage.color = new Color(30f, 50f, 50f);
			//backgroundImage.CrossFadeColor (new Color (30f, 50f, 50f, 30f), 2.0f, false, true);
			backgroundImage.CrossFadeColor (new Color (30f, 50f, 50f, 30f), 2.0f, false, true);
			//backgroundImage.CrossFadeAlpha(0.1f, 2.0f, false);
			GameRepository.setPause (false);
			//set the isPaused flag to false to indicate that the game is not paused
			isPaused = false;
			//play the SlideOut animation
			anim.Play ("pauseAnimUp");
			//set back the time scale to normal time scale
			//Time.timeScale = 1;
			pauseButton.SetActive (true);
			deathBar.SetActive (true);
			healthBar.SetActive (true);
			scoreText.SetActive (true);
		} else if (GameRepository.isPauseScreen ()) {

			//backgroundImage.color = new Color(30f, 50f, 50f);
			//backgroundImage.CrossFadeColor (new Color (0f, 150f, 0f, 30f), 2.0f, false, true);
			backgroundImage.CrossFadeColor (new Color (0f, 150f, 0f, 30f), 2.0f, false, true);
			//backgroundImage.CrossFadeAlpha(0.1f, 2.0f, false);
			GameRepository.setPause (false);
			//set the isPaused flag to false to indicate that the game is not paused
			isPaused = false;
			//play the SlideOut animation
			anim.Play ("pauseAnimUp");
			//set back the time scale to normal time scale
			//Time.timeScale = 1;
			pauseButton.SetActive (true);
			deathBar.SetActive (true);
			healthBar.SetActive (true);
			scoreText.SetActive (true);
		} else if (GameRepository.isGameOverScreen ()) {
			GameRepository.resetScore ();
			GameRepository.resetPlayerLife ();
			GameRepository.setMainScreen (true);
			GameRepository.setPauseScreen (false);
			GameRepository.setGameOverScreen (false);
			//GameRepository.setPlayerLife(1000.0f);
			//backgroundImage.color = new Color(30f, 50f, 50f);
			//backgroundImage.CrossFadeColor (new Color (0f, 150f, 0f, 30f), 2.0f, false, true);
			backgroundImage.CrossFadeColor (new Color (150f, 0f, 0f, 30f), 2.0f, false, true);
			//backgroundImage.CrossFadeAlpha(0.1f, 2.0f, false);
			GameRepository.setPause (true);
			//set the isPaused flag to false to indicate that the game is not paused
			isPaused = false;
			//play the SlideOut animation
			anim.Play ("pauseAnimUp");

			//Application.LoadLevel(Application.loadedLevel);
			AutoFade.LoadLevel(Application.loadedLevel, 2, 2, Color.black);
		}
		//backgroundImage.CrossFadeAlpha(0.1f, 2.0f, false);
		psBirds.Play ();
	}

	public void onOptionsClick(bool on) {
		if (on) {
			audioOptions.SetActive (false);
			optionsOnObj.SetActive(false);
			optionsOffObj.SetActive(true);
		} else {
			audioOptions.SetActive (true);
			optionsOnObj.SetActive(true);
			optionsOffObj.SetActive(false);
		}
	}

	public void toggleMusic(bool on) {
		if (on) {
			GameRepository.setMusic(true);
			musicOnObj.SetActive(false);
			musicOffObj.SetActive(true);
		} else {
			GameRepository.setMusic(false);
			musicOnObj.SetActive(true);
			musicOffObj.SetActive(false);
		}
	}

	public void toggleSounds(bool on) {
		if (on) {
			GameRepository.setSounds(true);
			soundsOnObj.SetActive(false);
			soundsOffObj.SetActive(true);
			Debug.Log("Sounds on");
		} else {
			GameRepository.setSounds(false);
			soundsOnObj.SetActive(true);
			soundsOffObj.SetActive(false);
			Debug.Log("Sounds off");
		}
	}

	public void exitGame(){
		Application.Quit ();
	}
}