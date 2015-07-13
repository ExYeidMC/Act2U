using UnityEngine;
using System.Collections;

public class CoinNumberAnimation : MonoBehaviour {


	float animTime = 0.10f;
	float animCnt;

	string pretext;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (animCnt > 0) {
			animCnt -= Time.deltaTime;
			if (animCnt<0) animCnt = 0f;
		}

		float buf = (animCnt - 0.5f * animTime) / animTime * 2;

		float y = 40 * (1-buf*buf);
		Vector3 v = transform.localPosition;
		v.y = y;
		transform.localPosition = v;

		string s = GetComponent<UnityEngine.UI.Text> ().text;
		if (pretext != s) {	animOrder();}
		pretext = s;
	}

	public void animOrder(){
		animCnt = animTime;
	}
}
