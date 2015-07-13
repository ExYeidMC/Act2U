using UnityEngine;
using System.Collections;

public class SceneManager_TestPlay : SceneManager_Base{

	public GameObject player;
	GameObject area;
	AreaMaster areamaster;

	public int thisStageNo;
	public int thisAreaNo;

	// Use this for initialization
	void Awake () {
		base.Awake ();
		GameMaster.Instance.Stage = thisStageNo; 
		GameMaster.Instance.Area = thisAreaNo; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AreaChange(int id,int sp){
		Destroy (area);
		area = null;

		int st = GameMaster.Instance.Stage + 1, ar = GameMaster.Instance.Area + 1;
		area = 
			Instantiate(Resources.Load ("Area/AreaMaster"+st+"-"+ar)) as GameObject;

		area.transform.SetParent(transform);
	}
}
