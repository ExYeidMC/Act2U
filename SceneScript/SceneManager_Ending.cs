using UnityEngine;
using System.Collections;

public class SceneManager_Ending : SceneManager_Base {
	
	public UnityEngine.UI.Button button;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void GameStart(){
		FadeManager.FadeStart (0,"Title");

		button.interactable = false;
	}
}
