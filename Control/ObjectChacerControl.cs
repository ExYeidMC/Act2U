using UnityEngine;
using System.Collections;

enum CameraMode{
	Controlable,
	Chase,
	Fixed
}

public class ObjectChacerControl : MonoBehaviour {
	
	public GameObject TargetObj;
	public GameObject CameraObj;

	public Vector3 offset;
	public float distance=2f;
	public float minDistance=2f;
	public float maxDistance=10f;

	public float farAngle=20f;
	public float speedLimit=0f;

	public float RotateSpeed=720f;

	float targetRot;

	// Use this for initialization
	void Start () {
		targetRot = TargetObj.transform.rotation.eulerAngles.y;
	
	}

	// Update is called once per frame
	void Update () {

	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 bufAngle = transform.eulerAngles;
		float time = GameMaster.Instance.Timecount;

		if (GameMaster.isClear()) {
			bufAngle.y = TargetObj.transform.eulerAngles.y + 180f;
			transform.position = TargetObj.transform.position+offset;
			distance = minDistance;

			if(time>=1.8f){
				bufAngle.y += 10f*(time-1.8f);
				bufAngle.x = 30*(time-1.8f);
				distance += 12*(time-1.8f);
			}
		} else if (GameMaster.isFailed() && time >= 1f) {
		} else {
			targetRot += Input.GetAxis ("CamX");
			//distance -= Input.GetAxis ("Mouse ScrollWheel");

			if (Input.GetButtonDown ("CamY1") && distance > minDistance) {
				distance -= 1f;
			}
			if (Input.GetButtonDown ("CamY2") && distance < maxDistance) {
				distance += 1f;
			}

			float bufF;
			if (minDistance != maxDistance)
				bufF = (distance - minDistance) / (maxDistance - minDistance);
			else
				bufF = 1f;

		
			float rot = (targetRot % 360 - bufAngle.y % 360) + 360;
			rot %= 360;
			if (rot > 180) {
				rot -= 360;
			}
			
			bufAngle.y += 0.5f * rot;
			bufAngle.x += 0.5f * ((farAngle * bufF) - bufAngle.x);
		
			Vector3 target = TargetObj.transform.position + offset;

			Vector3 Move = 0.5f * (target - transform.position);
			if (Move.magnitude > speedLimit && speedLimit > 0) {
				Move = Move.normalized * speedLimit;
			}
		
			transform.position += Move;
		}
		
		transform.eulerAngles = bufAngle;

		CameraObj.transform.localPosition += 
			0.4f * (new Vector3 (0, 0, -distance) - CameraObj.transform.localPosition);
		
	}

	public void CameraFix(){
		targetRot = TargetObj.transform.eulerAngles.y;
	}
}
