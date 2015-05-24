using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {
	//private GameRepository repository;
	//refrence for the pause menu panel in the hierarchy
	public GameObject pauseMenuPanel;
	private GameObject pauseButton;
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
	//animator reference
	private Animator anim;
	//variable for checking if the game is paused 
	private bool isPaused = true;
	private bool isPauseScreen = false;
	// Use this for initialization

	void Start () {
		healthBar = GameObject.FindGameObjectWithTag ("HealthBar");
		deathBar = GameObject.FindGameObjectWithTag ("DeathBar");
		audioOptions = GameObject.FindGameObjectWithTag ("AudioOptions");
		musicOnObj = GameObject.FindGameObjectWithTag ("MusicOnObj");
		musicOffObj = GameObject.FindGameObjectWithTag ("MusicOffObj");
		soundsOnObj = GameObject.FindGameObjectWithTag ("SoundsOnObj");
		soundsOffObj = GameObject.FindGameObjectWithTag ("SoundsOffObj");
		optionsOnObj = GameObject.FindGameObjectWithTag ("OptionsOnObj");
		optionsOffObj = GameObject.FindGameObjectWithTag ("OptionsOffObj");
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
		}

		audioOptions.SetActive (false);
		musicOnObj.SetActive(false);
		musicOffObj.SetActive(true);
		soundsOnObj.SetActive(false);
		soundsOffObj.SetActive(true);
		optionsOnObj.SetActive(false);
		optionsOffObj.SetActive(true);

		PauseGame ();
	}
	
	// Update is called once per frame
	public void Update () {
		//pause the game on escape key press and when the game is not already paused
		if(Input.GetKeyUp(KeyCode.P) && !isPaused){
			PauseGame();
		}
		//unpause the game if its paused and the escape key is pressed
		else if(Input.GetKeyUp(KeyCode.P) && isPaused){
			UnpauseGame();
		}
	}
	
	//function to pause the game
	public void PauseGame(){
		if (GameRepository.isPauseScreen ()) {
			backgroundImage.sprite = Resources.Load<Sprite>("images/backgroundTransparent");
			GameRepository.setPause (true);
			//play the Slidein animation
			anim.Play ("pauseAnimDown");
			//set the isPaused flag to true to indicate that the game is paused
			isPaused = true;
			//freeze the timescale
			//Time.timeScale = 0;
		} if (GameRepository.isMainScreen ()) {
			GameRepository.setPause (true);
			//enable the animator component
			anim.enabled = false;
			//play the Slidein animation

			//anim.Play ("pauseAnimDown");
			//set the isPaused flag to true to indicate that the game is paused
			isPaused = true;
			//freeze the timescale
			//Time.timeScale = 0;
		}
		pauseButton.SetActive (false);
		deathBar.SetActive (false);
		healthBar.SetActive (false);
	}
	//function to unpause the game
	public void UnpauseGame(){
		if (GameRepository.isMainScreen ()) {
			GameRepository.setPauseScreen(true);
			GameRepository.setMainScreen(false);
			anim.enabled = true;
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