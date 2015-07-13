using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : Entity {

	public GameObject CameraObject,HoldObject;

	public float MoveSpeed = 1.0f;
	public float RotateSpeed = 1.0f;
	public float GravityLV = 1.0f;

	public Animator playerAnimator;

	bool disableControl=false;

	Transform lastGround;

	Vector3 Move;
	float fall;


	// Use this for initialization
	void Start () {
		EnemyHitAct = onHitEnemy;
		
		t = GetComponent<CharacterController> ();
		Vel = Vector3.zero;
		lastGround = null;
	}
	
	void FixedUpdate () {
		//			transform.parent = null;
		//Debug.Log ("fix"+t.transform.position.ToString());
	}
	// Update is called once per frame
	public void Update () {

		if (GameMaster.isClear() && !disableControl) {
			onClear();
		}

		if (!isAttackMotion && !AttackReady) {DisableAllHitBoxes();	}

		if (isAllowedMove ) {
			MoveControl();

		} else {
			Move = Vector3.zero;
			
			if (lastGround!=null){
				Vel.x *= 0.9f;
				Vel.z *= 0.9f;
			}
		}

		if (isAllowedControl && lastGround) {
				if(Input.GetButtonDown("Fire1")){
					AnimatorStateInfo buf= playerAnimator.GetCurrentAnimatorStateInfo(0);
				
				//	playerAnimator.SetTrigger ("Attack");/*

					if (buf.IsName("Attack3")){
				}else if (buf.IsName("Attack2")){
					Vel = transform.forward * 3;
					Vel.y = 6;
					lastGround=null;
					DisableAllHitBoxes();
					hitBoxes[2].enabled =true;
						playerAnimator.CrossFade("Attack3",0.01f);
					}else if (buf.IsName("Attack1")){
						DisableAllHitBoxes();
						hitBoxes[1].enabled =true;
						Vel = transform.forward * 3;
						playerAnimator.CrossFade("Attack2",0.01f);
					}else{
					AttackReady=true;
					hitBoxes[0].enabled =true;
					Debug.Log (GameMaster.Instance.Timecount);
						playerAnimator.CrossFade("Attack1",0.01f);
					}//*/
				}else if(Input.GetButtonDown("Jump")){
					Vel.y = 10;
					lastGround=null;
					playerAnimator.CrossFade("Jump",0.1f);
					//playerAnimator.SetBool ("Jump",true);
				}
		}


		if (lastGround)	{
			Vel.y = 0;
		} else {
			Vel.y -= GravityLV * Time.deltaTime;
		}

		fall = Vel.y;

		Vel.y = fall;
		//r.velocity = Vel;

		CollisionFlags flag = t.Move (Vel*Time.deltaTime);


		if ((flag & CollisionFlags.CollidedBelow) > 0) {
		//	playerAnimator.SetBool ("Jump",false);
		}

		if (isDamageMotion) {Damaged = false;}
		if (isAttackMotion) {AttackReady = false;}

		
		if (transform.position.y < -100 && !isDeath) {
			Damage(maxLife);
			GameMaster.Instance.onDeath();
		}

		if (incredibleTime > 0f && isAllowedControl) {
			incredibleTime -= Time.deltaTime;
		} else if (incredibleTime < 0f){
			incredibleTime = 0f;
		}

		Vector2 v = new Vector2 (Vel.x,Vel.z);
		playerAnimator.SetFloat ("Speed",v.magnitude);
		playerAnimator.SetBool ("Ground",lastGround!=null);
		int i = 0;

		//obj.transform.Rotate(x*RotateSpeed*Time.deltaTime * new Vector3(0,1,0));

	}
	void LateUpdate () {
		if (!lastGround) {	return;	}

		//Vector3 ExtraMove;
		
		
		RaycastHit rayhit;
		Vector3 plpos = transform.position + new Vector3 (0,t.radius,0);
		float move = fall;
		float seekLv = (move > t.stepOffset ? move : t.stepOffset) + t.radius;
		
		if (Physics.SphereCast (plpos, t.radius, new Vector3 (0, -1, 0),
		                        out rayhit, seekLv)) {
			if (lastGround == rayhit.transform){
				t.Move (new Vector3(0,-seekLv,0));
			}else{
				lastGround = null;
			}
		}else{
			lastGround = null;
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {

		lastGround = hit.gameObject.transform;
	}

	// New Funciton

	public bool isAllowedMove{
		get{
			return isAllowedControl && !isAttackMotion;
		}
	}
	public bool isAllowedControl{
		get{
			
			bool damageFrag = !Damaged && !isDamageMotion && !disableControl;
			return damageFrag;
		}
	}

	public bool isDamageMotion{
		get{
			AnimatorStateInfo buf= playerAnimator.GetCurrentAnimatorStateInfo(0);
			return buf.IsName("DamageS") ||
				   buf.IsName("DamageL");
		}
	}
	public bool isAttackMotion{
		get{
			AnimatorStateInfo buf= playerAnimator.GetCurrentAnimatorStateInfo(0);
			return	buf.IsName("Attack1") ||
					buf.IsName("Attack2") ||
					buf.IsName("Attack3");
		}
	}



	void MoveControl(){
		float x = Input.GetAxis ("Horizontal"), y = Input.GetAxis ("Vertical");
		
		Vector3 MoveF = CameraObject.transform.forward;
		MoveF.y = 0f;
		MoveF.Normalize ();
		Vector3 MoveR = CameraObject.transform.right;
		MoveR.y = 0f;
		MoveF.Normalize ();
		
		Move = (x * MoveR + y * MoveF) * MoveSpeed;
		
		float rot = 0f;
		if (Move.magnitude > 0.5f) {
			rot = Mathf.Atan2 (Move.x, Move.z) * 180 / Mathf.PI + 360;
			rot = (rot - transform.rotation.eulerAngles.y) % 360;
			if (rot > 180) {rot -= 360;	}

			if (rot > RotateSpeed * Time.deltaTime)
				rot = RotateSpeed * Time.deltaTime;
			else if (rot < -RotateSpeed * Time.deltaTime)
				rot = -RotateSpeed * Time.deltaTime;
			
			transform.Rotate (0, rot, 0);
		}
		
		Vel.x = Move.x;
		Vel.z = Move.z;
	}

	public override bool onAttacked(Vector3 D,int damage){
		if (Incredible)
			return false;

		incredibleTime = 1f;
		Damaged = true;
		
		Damage(damage);

		
		transform.eulerAngles = new Vector3(0,
		Mathf.Atan2 (D.x, D.z) * 180 / Mathf.PI + 180,0);

		Vel = transform.forward * -5;
		Vel.y += 5;

		Move = Vector3.zero;
		if (isDeath) {
			playerAnimator.CrossFade ("Down", 0.1f);
			GameMaster.Instance.onDeath();
		} else {
			playerAnimator.CrossFade ("DamageL", 0.1f);
		}

		lastGround = null;

		return true;
	}

	void onHitEnemy(ColliderData s){
		Debug.Log (s.Obj.name);
		Vector3 d = s.Obj.transform.position - transform.position;
		d.y = 0f;
		s.Obj.GetComponent<Enemy> ().onAttacked (d,1);
		
		transform.eulerAngles = new Vector3(0,
		         Mathf.Atan2 (d.x, d.z) * 180 / Mathf.PI,0);
		//e.onAttacked (d.normalized, 1);
	}

	public void onClear(){
		playerAnimator.CrossFade("CLEAR",0.1f);
		disableControl = true;
		HoldObject.SetActive (false);
	}

	public void EnableHitBox(HitBoxPoint h){
		return;
		switch (h) {
		case HitBoxPoint.ArmL:
			hitBoxes[1].enabled =true;
			break;
		case HitBoxPoint.ArmR:
			hitBoxes[1].enabled =true;
			break;
		case HitBoxPoint.Body:
			hitBoxes[1].enabled =true;
			break;
		case HitBoxPoint.Head:
			hitBoxes[1].enabled =true;
			break;
		case HitBoxPoint.LegL:
			hitBoxes[1].enabled =true;
			break;
		case HitBoxPoint.LegR:
			hitBoxes[1].enabled =true;
			break;
		case HitBoxPoint.WeaponL:
			hitBoxes[1].enabled =true;
			break;
		case HitBoxPoint.WeaponR:
			hitBoxes[1].enabled =true;
			break;
		}
	}
}
