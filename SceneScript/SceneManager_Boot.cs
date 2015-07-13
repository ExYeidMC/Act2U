using UnityEngine;
using System.Collections;

public class SceneManager_Boot : SceneManager_Base {
	
	IEnumerator Start(){
		yield return null;
		//yield return new WaitForSeconds(0.1f);
		FadeManager.GoTitle ();
	}
}
