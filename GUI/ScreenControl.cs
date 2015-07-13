using UnityEngine;
using System.Collections;
public enum ScreenType{
	GameOver,
	GameClear
}
public class ScreenControl : MonoBehaviour {
	public ScreenType type;
	public GameObject Obj;
	public UnityEngine.UI.Image img;
	public UnityEngine.UI.Text img2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float time = GameMaster.Instance.Timecount;
		switch (type) {
		case ScreenType.GameClear:
			if (GameMaster.isClear()){
				Obj.SetActive(true);
			}
			break;
		case ScreenType.GameOver:
			if (GameMaster.isFailed()){
				if (time >= 1f){
					Obj.SetActive(true);
					Color a = img.color;
					Color aa = img2.color;
					float aaa = (time - 1f)*0.5f;
					if (aaa>1f){aaa=1f;}
					a.a = 0.4f * (aaa);
					aa.a = 1f*(aaa);
					img2.color=aa;
				}
			}
			break;
		}
	}
}
