using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {
	//private GameRepository repository;
	//refrence for the pause menu panel in the hierarchy
	public GameObject pauseMenuPanel;
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
	private Image backgroundImage;
	private SceneInit_intro firstScecne;
	//animator reference
	private Animator anim;
	//variable for checking if the game is paused 
	private bool isPaused = true;
	private bool isPauseScreen = false;
	// Use this for initialization

	void Start () {
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
			pauseMenuPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 25.0f);

			backgroundImage.sprite = Resources.Load<Sprite>("images/background");
			//backgroundImage.color = new Color(30f, 50f, 50f);
			backgroundImage.CrossFadeColor(new Color(30f, 50f, 50f, 180f), 2.0f, false, true);
			//backgroundImage.CrossFadeAlpha(0.8f, 1.0f, false);
			Debug.Log ("Pauses Start");
			logoImage.SetActive(true);
			pauseText.SetActive(false);
			scoreText.SetActive(false);
			//PauseGame ();
		}

		audioOptions.SetActive (false);
		musicOnObj.SetActive(false);
		musicOffObj.SetActive(true);
		soundsOnObj.SetActive(false);
		soundsOffObj.SetActive(true);
		optionsOnObj.SetActive(false);
		optionsOffObj.SetActive(true);
		

	}
	
	// Update is called once per frame
	public void Update () {
		if(Input.GetKeyUp(KeyCode.P) && !GameRepository.isPaused() && GameRepository.isPauseScreen ()){
			PauseGame();
		} else if(Input.GetKeyUp(KeyCode.P) && GameRepository.isPaused() && GameRepository.isPauseScreen ()){
			UnpauseGame();
		}
	}
	
	//function to pause the game
	public void PauseGame(){
		if (GameRepository.isGameOverScreen ()) {
			logoImage.SetActive(false);
			pauseText.SetActive(true);

			pauseText.GetComponent<Text>().text = "Game Over";
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
			firstScecne.createScene ();
			GameRepository.setPauseScreen (true);
			GameRepository.setMainScreen (false);
			anim.enabled = true;
			//backgroundImage.color = new Color(30f, 50f, 50f);
			//backgroundImage.CrossFadeColor (new Color (30f, 50f, 50f, 30f), 2.0f, false, true);
			backgroundImage.CrossFadeColor(new Color(30f, 50f, 50f, 30f), 2.0f, false, true);
			//backgroundImage.CrossFadeAlpha(0.1f, 2.0f, false);
		} else if (GameRepository.isPauseScreen ()) {

			//backgroundImage.color = new Color(30f, 50f, 50f);
			//backgroundImage.CrossFadeColor (new Color (0f, 150f, 0f, 30f), 2.0f, false, true);
			backgroundImage.CrossFadeColor(new Color(0f, 150f, 0f, 30f), 2.0f, false, true);
			//backgroundImage.CrossFadeAlpha(0.1f, 2.0f, false);
		}
		GameRepository.setPause (false);
		//set the isPaused flag to false to indicate that the game is not paused
		isPaused = false;
		//play the SlideOut animation
		anim.Play("pauseAnimUp");
		//set back the time scale to normal time scale
		//Time.timeScale = 1;
		pauseButton.SetActive (true);
		deathBar.SetActive (true);
		healthBar.SetActive (true);
		scoreText.SetActive (true);

		//backgroundImage.CrossFadeAlpha(0.1f, 2.0f, false);
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