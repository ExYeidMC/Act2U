using UnityEngine;
using System.Collections;

public class SceneManager_Main : SceneManager_Base{

	public GameObject player;
	GameObject area;
	AreaMaster areamaster;

	// Use this for initialization
	void Awake () {
		base.Awake ();
		GameMaster.ResetByStage ();

		int st = GameMaster.Instance.Stage + 1, ar = GameMaster.Instance.Area + 1;


		area = 
			Instantiate(Resources.Load ("Area/AreaMaster"+st+"-"+ar)) as GameObject;

		area.transform.SetParent(transform);

		areamaster = area.GetComponent<AreaMaster>();

		Resources.UnloadUnusedAssets ();

		//Debug.Log (obj.name);
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
