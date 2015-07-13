using UnityEngine;
using System.Collections;

public abstract class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T> {

	private static T instance;

	public static T Instance{
		get{
			if (instance == null) {
				instance = (T)FindObjectOfType(typeof(T));
				if (instance == null) {
					Debug.LogError ("No "+typeof(T) + " in this Scene.");
				}
			}
			return instance;
		}
	}
	
	virtual protected void Awake(){
		CheckInstance ();
	}

	protected bool CheckInstance(){
		if (instance == null) {
			instance = (T)this;
			DontDestroyOnLoad(this.gameObject);
			return true;
		}else if(instance == this){
			return true;
		}
		Destroy (this.gameObject);
		return false;
	}
}
