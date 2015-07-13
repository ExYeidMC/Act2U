using UnityEngine;
using System.Collections;

public enum ItemType{
	NoEffect,
	Recovery,
	Gain,
	GameClear,
	GameOver
}
public enum GainItemType{
	Coin,
	Dummy
}
public class Item_Available : Item {

	public ItemType type = ItemType.NoEffect;
	public GainItemType gtype = GainItemType.Dummy;
	public int number=1;

	public bool useGravity = false;
	

	bool got;

	// Use this for initialization
	protected void Start () {
		PlayerHitAct = onHitPlayer;
		got = false;
	}
	
	// Update is called once per frame
	protected void Update() {
		CommonUpdate();

		if (useGravity && !got) {
		}

		if (got) {
			transform.Rotate (0, 1080f * Time.deltaTime, 0f);
			if (age< lifeSpan/2) transform.position += new Vector3 (0, 6f * (lifeSpan*1.4f - age) / lifeSpan, 0) * Time.deltaTime;
		} else {
			transform.Rotate(0,45f*Time.deltaTime,0f);
		}

		if (age > lifeSpan && lifeSpan > 0f) {Kill ();}

	}
	public void Kill(){
		Destroy(this.gameObject);
	}

	void onHitPlayer(ColliderData s){
		if (GameMaster.isFailed()) {return;}
		got = true;

		switch (type) {
		case ItemType.NoEffect:
			break;
			
		case ItemType.Gain:
			age=0f;
			lifeSpan = 0.5f;
			Gain (s.Obj.GetComponent<PlayerControl>());
			break;
		case ItemType.Recovery:
			age=0f;
			lifeSpan = 0.5f;
			s.Obj.GetComponent<PlayerControl>().Recovery(number);
			break;
		case ItemType.GameClear:
			age=0f;
			lifeSpan = 0.5f;
			GameMaster.Instance.onClear();
			break;
		}
		PlayerHitAct = null;
		Debug.Log ("Clear");
	}

	void Gain(PlayerControl pc){
		switch (gtype) {
		case GainItemType.Coin:
			GameMaster.GetCoin(number);
			break;
		}
	}
}
