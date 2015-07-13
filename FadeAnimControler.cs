using UnityEngine;
using System.Collections;

public class FadeAnimControler : MonoBehaviour {
	
	public enum FadeAnim{
		Off,
		Out,
		Wait,
		In
	}
	protected FadeAnim currentAnim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public FadeManager.FadePhase Phase{
		get{
			Animation anime =  GetComponent<Animation>();
			switch(currentAnim){
			case FadeAnim.Out:
				if (anime.isPlaying)return FadeManager.FadePhase.Out;
				else return FadeManager.FadePhase.OutEnd;
			case FadeAnim.Wait:
				return FadeManager.FadePhase.Wait;
			case FadeAnim.In:
				if (anime.isPlaying)return FadeManager.FadePhase.In;
				else return FadeManager.FadePhase.InEnd;
			default:
				return FadeManager.FadePhase.InEnd;
			}
			}
		}

	public void AnimStart(FadeAnim anim){
		Animation anime =  this.GetComponent<Animation>();
		if (anim != FadeAnim.Off) {
						SetChildrenActive (true);
		} else {
			SetChildrenActive (false);
				}
		switch(anim){
		case FadeAnim.Off:
			if (currentAnim!=anim){SetChildrenActive(false);}
			break;
		case FadeAnim.Out:
			anime.clip=anime.GetClip("OUT");
			anime.Play();
			break;
		case FadeAnim.Wait:
			anime.clip=anime.GetClip("WAIT");
			anime.Play();
			break;
		case FadeAnim.In:
			anime.clip=anime.GetClip("IN");
			anime.Play();
			break;
		}
		currentAnim = anim;
	}

	public void SetChildrenActive(bool enable){
		Transform[] children = GetComponentsInChildren<Transform>();
		foreach ( Transform n in children )
		{
			if (n==transform)continue;
			n.gameObject.SetActive(enable);
		}
	}
}
