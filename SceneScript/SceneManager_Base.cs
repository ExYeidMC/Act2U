using UnityEngine;
using System.Collections;

public class SceneManager_Base : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected virtual void Awake(){
		if(FadeManager.Instance!=null)FadeManager.FadeRemove ();
	}
	
	public virtual void orderSceneChange(string next){
		FadeManager.FadeStart(0,next);
	}
}

