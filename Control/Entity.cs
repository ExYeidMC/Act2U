using UnityEngine;
using System.Collections;

public delegate void onHitAction(ColliderData s);

public class Entity : MonoBehaviour {

	protected onHitAction PlayerHitAct;
	protected onHitAction EnemyHitAct;

	protected onHitAction LivingHitAct;
	protected onHitAction ObjectHitAct;

	protected onHitAction AnythingHitAct;
	
	public Collider[] hitBoxes;
	
	protected CharacterController t;

	protected float age = 0f;
	protected Vector3 Vel;
	public float lifeSpan = -1f;
	public int maxLife = 3;
	public int damagePoint = 0;
	
	protected float incredibleTime=0f;
	protected bool Damaged=false;
	protected bool AttackReady=false;

	// Use this for initialization
	protected void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {

	}

	protected virtual void CommonUpdate(){
		//Common Action
		age += Time.deltaTime;
	}

	void OnCollisionEnter(Collision s){onEnter (new ColliderData(s));}

	void OnCollisionStay(Collision s){onStay (new ColliderData (s));}

	void OnCollisionExit(Collision s){onExit (new ColliderData (s));}

	void OnTriggerEnter(Collider s){onEnter (new ColliderData(s));}

	void OnTriggerStay(Collider s){onStay (new ColliderData (s));}

	void OnTriggerExit(Collider s){onExit (new ColliderData (s));}

	void onEnter(ColliderData s){
		string tagName = s.Obj.tag;

		if (tagName == "Player" && 
		    PlayerHitAct!=null) {
			PlayerHitAct(s);
		}
		else if (tagName == "Enemy" && 
		         EnemyHitAct!=null) {
			EnemyHitAct(s);
		}
		else if ((tagName == "Player" || tagName == "Enemy") && 
		         LivingHitAct!=null) {
			LivingHitAct(s);
		}
		else if ((tagName == "Gimmick") && 
		         ObjectHitAct!=null) {
			ObjectHitAct(s);
		}
		else if (AnythingHitAct !=null) {
			AnythingHitAct(s);
		}
	}
	
	void onStay(ColliderData s){
	}

	void onExit(ColliderData s){
	}
	
	public virtual bool onAttacked(Vector3 D,int Damage){
		return false;
	}
	
	public bool Incredible{ get {return incredibleTime >0f;}}
	public int Life{
		get{
			int result = maxLife - damagePoint;
			return result < 0 ? 0 : result;
		}
	}

	public bool isDeath{get{return maxLife <= damagePoint;}}

	public void Damage(int d){damagePoint += d;}
	public void Recovery(int d){
		damagePoint -= d;
		if (damagePoint < 0) { damagePoint =0;}
	}

	public void DisableAllHitBoxes(){
		foreach (Collider c in hitBoxes) {
			c.enabled = false;
		}
	}
	
	public virtual void Kill(){
	}
}

