using UnityEngine;
using System.Collections;

public class LifeBarControl : MonoBehaviour {

	public GameObject[] LifebarSprites;
	public Entity Player;
	int oldLife;
	// Use this for initialization
	void Start () {
		oldLife = Player.Life;
	}
	
	// Update is called once per frame
	void Update () {
		int life = Player.Life,i=0;
		if (oldLife != life) {
			foreach (GameObject obj in LifebarSprites) {
				obj.SetActive (life > i);
				i++;
			}
		}
		oldLife = Player.Life;

	}
}
