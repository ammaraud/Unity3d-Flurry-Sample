using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {
	

	public GameObject ClickHereButton;
	public GameObject ContinueHereButton;
	public GameObject Elephant;
	public GameObject Helloworld;

	private string FLURRY_API_KEY = "FN4YNJT59843V7JSG6YJ";	
	private NerdFlurry mNerdFlurry = null;

	// Use this for initialization
	void Start () {

		ContinueHereButton.SetActive(false);
		Elephant.SetActive(false);

		mNerdFlurry = new NerdFlurry();
		mNerdFlurry.StartSession(FLURRY_API_KEY);
		Debug.Log("Agent version is "+mNerdFlurry.GetAgentVersion());
		mNerdFlurry.LogEvent("App opened");
		//flurry.onPageView();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}

	public void clicked_ClickHereButton()
	{
		ClickHereButton.SetActive(false);
		Helloworld.SetActive(false);
	
		ContinueHereButton.SetActive(true);
		Elephant.SetActive(true);

		mNerdFlurry.LogEvent("clicked_ClickHereButton");
	}

	public void clicked_ElephantButton()
	{
		ContinueHereButton.SetActive(false);
		Elephant.SetActive(false);

		ClickHereButton.SetActive(true);
		Helloworld.SetActive(true);

		mNerdFlurry.LogEvent("clicked_ElephantButton");
	}

	public void OnApplicationPause(bool pause)
	{
		Application.Quit();
	}
	
	public void OnApplicationQuit()
	{
		mNerdFlurry.EndSession();
	}



}
