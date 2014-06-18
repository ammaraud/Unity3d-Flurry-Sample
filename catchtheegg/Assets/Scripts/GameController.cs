using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class GameController2 : MonoBehaviour
{
/*	private FacebookController facebookController;
	private PlaneScrolling planeController;
	private PlayerScript playerScript;
	private GameOverScript gameOverScript;
	private MainMenuScript mainMenuScript;

	public float spawnWait;
	public bool isGameOver;
	public bool spawnElemets;

	public bool PlayGame = false;
	public GameObject obstacle;

	public Vector3 spawnValues;
	public float MaxY = -2.5f;
	private bool spawnStarted = false;

	public GUIText scoreText;

	private int highScore = 0;
	public static int score = 0;
	private bool restart = false;
	
	//public GameObject obstacle;
	public GameObject bird;
	public GameObject gameOver;
	public GameObject grassObject;
	public GameObject Mainmenu;
	public GameObject cameraScriptObject;

	private Vector3 birdSpawnPosition;
	private Quaternion birdSpawnRotation;
	private bool audioOn = true ;
	private bool alreadyShowingGameOverScreen = false;

	private AudioSource audio_source;
	public AudioClip sound;
	public AudioClip GameEndsound;
	public GameObject AudioBtnOn;
	public GameObject AudioBtnOff;

	private string LEADER_BOARD_API_KEY = "CgkItODBj9kREAIQBg";
	private string signInKey = "signed_into_google";
	private int isSignedIn = 0; //0 for not signed, 1 for signed

	public float time;
	public float currentTime; 


	void Start ()
	{
		PlayGamesPlatform.DebugLogEnabled = true;
		
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();

		isSignedIn = PlayerPrefs.GetInt (signInKey);
		if (isSignedIn == 1) {
			signIntoPlayServices ();
		}

//		AudioBtnOn = GameObject.FindGameObjectWithTag("soundOn");
//		AudioBtnOff = GameObject.FindGameObjectWithTag("soundOff");
		AudioBtnOff.SetActive (false);
		audio_source = (AudioSource)gameObject.AddComponent("AudioSource");
		//sound = (AudioClip)Resources.Load("flight-of-the-bumblebee");
		audio_source.clip = sound;
		gameOver.SetActive (false);
		playerScript = bird.GetComponent <PlayerScript>();

		gameOverScript = gameOver.GetComponent<GameOverScript> ();
		if (gameOverScript == null)
		{
			Debug.Log ("Cannot find 'GameOverScript' script");
		}

		facebookController = gameObject.GetComponent <FacebookController>();
		if (facebookController != null) {
			facebookController.initializeFacebook (this);
		} else {
			Debug.Log ("Cannot find 'FacebookController' script");
		}

		planeController = grassObject.GetComponent <PlaneScrolling>();
		if (planeController == null)
		{
			Debug.Log ("Cannot find 'PlaneScrolling' script");
		}

		mainMenuScript = Mainmenu.GetComponent <MainMenuScript>();
		if (mainMenuScript == null)
		{
			Debug.Log ("Cannot find 'MainMenuScript' script");
		}

		if (facebookController.isFacebookLoggedIn ()) {
			mainMenuScript.hideFacebookButton();
		}

		resetLabels ();

//		Debug.Log ("Before: x:" + bird.transform.position.x + "  y:" + bird.transform.position.y + "  z:" + bird.transform.position.z);

		birdSpawnPosition = new Vector3 ();
		birdSpawnPosition.x = bird.transform.position.x;
		birdSpawnPosition.y = bird.transform.position.y;
		birdSpawnPosition.z = bird.transform.position.z;

		birdSpawnRotation = bird.transform.rotation;
//		Debug.Log ("After: x:" + bird.transform.position.x + "  y:" + bird.transform.position.y + "  z:" + bird.transform.position.z);
//		Debug.Log ("x : " + birdSpawnPosition.x + "   y: " + birdSpawnPosition.y + "  z:" + birdSpawnPosition.z); 

		highScore = PlayerPrefs.GetInt("High Score");
//		Debug.Log("High Score is " + highScore );

	}

	void resetLabels() {
		scoreText.text = "";

	}

	void addBird() {
		Vector3 spawnPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		Quaternion spawnRotation = Quaternion.identity;
		bird = Instantiate (bird, spawnPosition, spawnRotation) as GameObject;
	}

	IEnumerator AddObstacles (){
		yield return new WaitForSeconds (spawnWait);
		while (true)
		{
			Vector3 spawnPosition = new Vector3 (spawnValues.x, Random.Range (MaxY, spawnValues.y), spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (obstacle, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (spawnWait);
	
			if (isGameOver) {
				restart = true;
				break;
			}
		}
	}


	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver() {
		if (audio_source.isPlaying) 
		{
			audio_source.Stop ();
			//audio_source.clip = GameEndsound;
			audio_source.PlayOneShot(GameEndsound);
		}
		if (!isGameOver) {
			isGameOver = true;
			GameObject[] gos = GameObject.FindGameObjectsWithTag ("Obstacle");
			if (cameraScriptObject != null)
			{
				CameraShake camScript = cameraScriptObject.GetComponent <CameraShake>();
				camScript.enabled = true;
			}
			
			foreach (GameObject go in gos) {
				Mover mover = go.GetComponent <Mover>();
				if ( mover != null ) {
					mover.stopMovingObstacles();
				}
			}

			saveHighScore ();
			planeController.stopMoving();
			gameOverScript.setScore (score);
			gameOverScript.setBestScore (highScore);
			gameOverScript.showMedal (score);
			StartCoroutine (showGameOverScreen ());

			#if UNITY_EDITOR
			Debug.Log("Unity Editor");
			#else
			AdMobPlugin.ShowBannerView ();
			#endif
		}
	}

	private IEnumerator showGameOverScreen() {
		Debug.Log ("showGameOverScreen");
		yield return new WaitForSeconds (1f);
		if (!alreadyShowingGameOverScreen) {
			alreadyShowingGameOverScreen = true;
			gameOver.SetActive (true);
		}

	}

	void saveHighScore(){
		int scorePublished = PlayerPrefs.GetInt("scorePublished");
		if ((score > highScore) || (scorePublished == 0)) {
			highScore = score > highScore?score:highScore;

			// post score 12345 to leaderboard ID "Cfji293fjsie_QA")
			Social.ReportScore(highScore, LEADER_BOARD_API_KEY, (bool success) => {
				if (success)
					PlayerPrefs.SetInt("scorePublished",1);
					Debug.Log("Score Posted");
			});


			PlayerPrefs.SetInt("High Score", highScore);
			Debug.Log("High Score is " + highScore );
			Debug.Log("Current Score is " + score );
		}
	}


	public void restartGame() {
		if (restart) {
			if(audio_source.isPlaying == false)
			{

				audio_source.Play();
			}
			restart = false;
			gameOver.SetActive(false);
			alreadyShowingGameOverScreen = false;
			GameObject[] gos = GameObject.FindGameObjectsWithTag ("Obstacle");
			
			foreach (GameObject go in gos) {
				Destroy (go);
			}

			isGameOver = false;
			playerScript.resetPosition();

			score = 0;
			resetLabels ();

			bird.rigidbody2D.velocity = Vector3.zero;
			bird.rigidbody2D.angularVelocity = 0f;
			bird.transform.position = birdSpawnPosition;
			bird.transform.Rotate(0,0,0);
			bird.transform.rotation = birdSpawnRotation;
			bird.SetActive (true);

			planeController.startMoving();
			playerScript.startAnimation ();
			StartCoroutine (AddObstacles ()); 
			#if UNITY_EDITOR
				Debug.Log("Unity Editor");
			#else
				AdMobPlugin.HideBannerView();
			#endif
		}
	}

	void Update () {
		if (Input.GetButton ("Fire1") && PlayGame==true) {
			if (spawnElemets && !spawnStarted) {
				spawnStarted = true;
				StartCoroutine (AddObstacles ()); 
			}
		}

		currentTime = Time.time;

		if (Input.GetKey(KeyCode.Escape) && (currentTime - time > 0.5f))
			
		{
			time = Time.time;

			if(isGameOver && alreadyShowingGameOverScreen){
				showMainMenu();
				return;
			} else if(!PlayGame ){
				Application.Quit();
				return;
			}

			
		}
	}

	public void showMainMenu() {

		gameOver.SetActive(false);
		audio_source.Stop();
		Mainmenu.SetActive(true);
		PlayGame = false;
		isGameOver = false;
	}

	public void HideMainMenu() {

		Mainmenu.SetActive(false);
		PlayGame = true;
		audio_source.Play();
		audio_source.loop = true;
		#if UNITY_EDITOR
		Debug.Log("Unity Editor");
		#else
		AdMobPlugin.HideBannerView ();
		#endif

	}

	public void AudioOn() {

		audio_source.Stop();
		if(audioOn)
		{

			AudioBtnOff.SetActive(true);
			AudioBtnOn.SetActive(false);
			audioOn = false;
			AudioListener.volume = 0.0f;
		}
		else
		{
			AudioBtnOff.SetActive(false);
			AudioBtnOn.SetActive(true);

			audioOn = true;
			AudioListener.volume = 1.0f;
			
		}
	}


	public void hideFacebookButton() {
		mainMenuScript.hideFacebookButton ();
	}

	private void signIntoPlayServices() {
		// authenticate user:
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			if (isSignedIn == 0 && success) {
				PlayerPrefs.SetInt(signInKey, 1);
				isSignedIn = 1;
				Debug.Log("signed in sccesfully");
			}
		});
	}

	public void showLeaderBoard() {
		Debug.Log("isSignedIn : " + isSignedIn);
		if (isSignedIn == 1) {
			Debug.Log("showing Leaderboard");
			((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (LEADER_BOARD_API_KEY);
		} else {
			Debug.Log("signing in");
			signIntoPlayServices();
		}
	}*/
}
