using UnityEngine;
using System.Collections;

public class Enemy_Zako1 : Enemy{

	// Use this for initialization
	void Start () {
		base.Start ();
		PlayerHitAct = onHitPlayer;
	}

	void Update(){

		base.Update ();

		
		Vel.y -= 20 * Time.deltaTime;
		CollisionFlags flag = t.Move (Vel*Time.deltaTime);
		
		
		if ((flag & CollisionFlags.CollidedBelow) > 0) {
			Vel.y = 0;
			Vel.x *= 0.9f;
			Vel.z *= 0.9f;
			if (isDeath){
				Debug.Log("Bound");
				boundCnt++;
				Vel = transform.forward * -5;
				Vel.y += 5 *(DeathBound - boundCnt) / DeathBound ;

				if (DeathBound <= boundCnt){
					Kill();
				}
			}
			//	playerAnimator.SetBool ("Jump",false);
		}
		if (transform.position.y < -100) {
			Kill ();
		}
	}
	
	void onHitPlayer(ColliderData s){
		Debug.Log ("Hit");
		Vector3 d = s.Obj.transform.position - transform.position;
		d.y = 0f;
		s.Obj.GetComponent<PlayerControl> ().onAttacked (d.normalized, 1);
	}
	
	public override bool onAttacked(Vector3 D,int damage){
		if (Incredible || isDeath)
			return false;
		
		incredibleTime = 0f;
		Damaged = true;
		Damage(damage);
		
		
		transform.eulerAngles = new Vector3(0,
		                                    Mathf.Atan2 (D.x, D.z) * 180 / Mathf.PI + 180,0);
		if (Life <= 0) {
			Vel = transform.forward * -5;
			Vel.y += 5;
		} else {
			Vel = transform.forward * -3;
			Vel.y += 3;
		}
		
		return true;
	}
	
	public void Kill(){
		if (isDeath) {
		}
		Destroy(this.gameObject);
	}

}
