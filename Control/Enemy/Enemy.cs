using UnityEngine;
using System.Collections;

enum EnemyRootuine{
	Wait,
	Chase,
	Back
}

public class Enemy : Entity {
	public int DeathBound = 3;
	protected int boundCnt = 0;
	// Use this for initialization
	protected void Start () {
		t = GetComponent<CharacterController> ();
	
	}

	protected void Update ()
	{
	}
}
