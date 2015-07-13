using UnityEngine;
using System.Collections;

public enum GameStatus{
	NA,
	GameOver,
	StageClear,
	Normal
}

public class GameMaster: SingletonBase<GameMaster>{
	public DataLinker data;
	public int Coin=0;
	public int Stage=0;
	public int MaxStage=2;
	public int Area=0;
	public GameStatus s;
	public float Timecount;
	bool togle;
	public static void GetCoin(int number){
		Instance.Coin += number;
	}

	public void FullReset(){
		Coin=0;
		Stage=0;
		Area=0;

	}

	public static void ResetByStage(){
		Instance.s = GameStatus.Normal;
		Instance.Timecount = 0f;
		Instance.togle = false;
	}
	public static bool isClear(){
		return Instance.s == GameStatus.StageClear;
	}
	public static bool isFailed(){
		return Instance.s == GameStatus.GameOver;
	}

	public void onDeath(){
		if (s != GameStatus.GameOver) {
			Timecount = 0f;
			togle=true;
			s=GameStatus.GameOver;
		}
	}
	public void onClear(){
		if (s != GameStatus.StageClear) {
			Timecount = 0f;
			togle=true;
			s=GameStatus.StageClear;
		}
	}

	void Update(){
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}

		Timecount += Time.deltaTime;

		if (isClear ()) {
			if (Timecount >3f && togle){
				GameMaster.Instance.Stage+=1;
				GameMaster.Instance.Area+=0;
				if (GameMaster.Instance.Stage >=GameMaster.Instance.MaxStage){
					FadeManager.FadeStart(0,"Ending");
				}else{
					FadeManager.FadeStart(0,"Main");
				}
				togle = false;
			}
		}
		if (isFailed ()) {
			if (Timecount >3f && togle){
				FadeManager.GoTitle();
				togle = false;
				}
		}
	}

}
