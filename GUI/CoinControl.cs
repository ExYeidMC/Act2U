using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum WindowState{
	Appear,
	Wait,
	Close,
	Off
}

public class CoinControl : MonoBehaviour {

	public int maxDigit=3;
	public List<GameObject> digit;
	public List<UnityEngine.UI.Text> digitText;
	public Font numberFont;

	public float waitTime=2f;
	float waitCount=0f;

	int viewNumber=0;
	float timeSpan = 0f;
	float progress;
	float originalY;

	WindowState ws = WindowState.Off;
	// Use this for initialization
	void Start () {
		for (int i=0; i<maxDigit; i++) {
			GameObject obj = new GameObject("Digit"+(maxDigit-i));
			obj.transform.parent = gameObject.transform;

			obj.AddComponent<CanvasRenderer>();

			//Debug.Log (obj.GetComponent<Rect>());

			digitText.Add(obj.AddComponent<UnityEngine.UI.Text>());
			digitText[i].font = numberFont;
			digitText[i].fontSize = 80;
			digitText[i].alignment = TextAnchor.MiddleCenter;
			obj.transform.localPosition = Vector3.zero + new Vector3(65*i,0,0);
			obj.transform.localScale = Vector3.one;

			obj.AddComponent<CoinNumberAnimation>();

			digit.Add(obj);
		}
		originalY = transform.localPosition.y;
		viewNumber = GameMaster.Instance.Coin;
	}
	
	// Update is called once per frame
	void Update () {
		int val = GameMaster.Instance.Coin;

		switch(ws){
		case WindowState.Appear:
			progress += 15f*Time.deltaTime;
			if(progress >= 1f){
				ws = WindowState.Wait;
				progress = 1f;
				waitCount = waitTime;
				timeSpan = 0f;
			}
			break;
		case WindowState.Wait:
			if (timeSpan > 0f){
				timeSpan-= Time.deltaTime;
			}else if (timeSpan <= 0f && viewNumber != val){
				viewNumber++;
				timeSpan = 0.05f;
			}else{
				waitCount -= 2f*Time.deltaTime;
				if (waitCount<=0f){
					ws = WindowState.Close;
				}
			}
			break;
		case WindowState.Close:
			progress -= Time.deltaTime;
			if(progress <= 0f){
				ws = WindowState.Off;
				progress = 0f;
			}
			break;
		case WindowState.Off:
			break;
		}
		
		Vector3 bufv = transform.localPosition;
		bufv.y = originalY - (110f * progress);
		transform.localPosition = bufv;

		if (viewNumber != val &&(ws == WindowState.Off || ws == WindowState.Close)) {
			ws =  WindowState.Appear;
		}

		int buf = viewNumber;
		for (int i=maxDigit-1; i>=0; i--) {
			digitText[i].text = (buf % 10).ToString();
			buf /= 10;
		}
	}
}
