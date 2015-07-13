using UnityEngine;
using System.Collections;

public class DebugPrint : MonoBehaviour {

	public UnityEngine.UI.Text text;

	public static string DebugText;

	public float rx,tx;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		text.text = DebugText;
		DebugText = null;
	}
}
