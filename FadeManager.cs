using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum SceneChange{
	Access,
	Happenning,
	Success,
	Canceled
}


delegate SceneChange onSceneChange();
public delegate void FadeEndEvent();

public class FadeManager : SingletonBase<FadeManager> {
	static onSceneChange SCEvent;
	public FadeEndEvent onFadeEnd;

	protected static FadeAnimControler[] f_con;

	//アニメーションID
	public static int fadeNo=0;
	public static string nextScene;

	public static bool Loading;

	public enum FadePhase{
		Out,
		OutEnd,
		Wait,
		Error,
		Reject,
		In,
		InEnd
	}

	public FadePhase fadePhase;

	public enum FadeStyle
		{
		Active,
		Freeze
		}

	public enum FadeType
		{
		BlackOut,
		Animated,
		CrossFade
		}

	protected override void Awake ()
	{
		if (CheckInstance()) {
			f_con = GetComponentsInChildren<FadeAnimControler>();
		}
	}

	// Use this for initialization
	void Start () {
		fadePhase = FadePhase.InEnd;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (fadeNo >= 0) {
			switch(fadePhase){
			case FadePhase.Out:
			case FadePhase.OutEnd:
				if (f_con [fadeNo].Phase == FadePhase.OutEnd){
					fadePhase = FadePhase.Wait;
					f_con [fadeNo].AnimStart (FadeAnimControler.FadeAnim.Wait);
				}
				break;
			case FadePhase.Wait:

				fadePhase = FadePhase.In;
				if (nextScene!=null){
					Debug.Log(nextScene);
					Application.LoadLevel(nextScene);
				}else{
					onFadeEnd();
				}

				break;
			case FadePhase.Error:

				break;
			case FadePhase.In:
				if (f_con [fadeNo].Phase == FadePhase.InEnd){
					fadePhase = FadePhase.InEnd;
					fadeNo=-1;
				}
				break;
			case FadePhase.InEnd:
				break;
			default :
				break;
			}

		} else {
			switch(fadePhase){
			case FadePhase.Out:
			case FadePhase.OutEnd:
					fadePhase = FadePhase.Wait;
				break;
			case FadePhase.Wait:
				if (nextScene!=null){
					Application.LoadLevel(nextScene);
				}else{
					onFadeEnd();
				}
				break;
			case FadePhase.Error:
				break;
			case FadePhase.In:
					fadePhase = FadePhase.InEnd;
					fadeNo=-1;
				break;
			case FadePhase.InEnd:
				break;
			default :
				break;
			}
		}
	}

	public static void FadeStart(int no,string next){
		if (no >= 0) {
			fadeNo = no;
			Instance.fadePhase = FadePhase.Out;
			f_con [fadeNo].AnimStart (FadeAnimControler.FadeAnim.Out);
		}
		nextScene = next;
	}
	public static void GoTitle(){
		if (Instance.fadePhase != FadePhase.Wait) {
						FadeStart (0, "Title");
						f_con [fadeNo].AnimStart (FadeAnimControler.FadeAnim.Out);
		} else {
			nextScene = "Title";
		}

	}
	public static void FadeRemove(){
		if (fadeNo < 0 || f_con==null) {return;}
		f_con [fadeNo].AnimStart (FadeAnimControler.FadeAnim.In);
	}

	public void OnGUI(){
		//this.color.a = this.fadeAlpha;
		//GUI.color = this.fadeColor;
		//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);

	}
}
