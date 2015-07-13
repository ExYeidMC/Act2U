using UnityEngine;
using System.Collections;


public interface IResult{
};

public interface IValue{
};

public class ColliderData : IValue{
	Collision Col;
	Collider Tri;
	GameObject master;
	bool isCol;

	public ColliderData(Collision c){
		Col = c;
		master = c.gameObject;
		isCol = true;
	}
	public ColliderData(Collider c){
		Tri = c;
		master = c.gameObject;
		isCol = false;
	}

	public GameObject Obj{	get{return master;}	}
	public Collider collider{	get{return Tri;}	}
	public Collision collision{	get{return Col;}	}
	public bool isCollision{get{return isCol;}}
	public bool isTriger{get{return !isCol;}}
}