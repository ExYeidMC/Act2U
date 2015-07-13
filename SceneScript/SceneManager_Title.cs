using UnityEngine;
using System.Collections;

public class SceneManager_Title : SceneManager_Base {
	
	public UnityEngine.UI.Button startbutton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void GameStart(){
		GameMaster.Instance.FullReset ();
		FadeManager.FadeStart (0,"Main");

		startbutton.interactable = false;
	}
}
