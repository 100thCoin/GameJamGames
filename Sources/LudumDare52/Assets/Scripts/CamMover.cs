using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour {

	public float YDir;
	public float XDir;

	public float CamSpeed;
	public float CamSpeedXMult;
	public float CamSpeedYMult;
	public float CamMultHoldX;
	public float CamMultHoldY;

	public float YOff;
	public float XOff;
	public float OffMult;

	public Vector3 Offset;
	public PlayerMovement PMov;

	public Vector3 PlayerRecentVel;

	public float MaxY;
	public float MinY;
	public float MaxX;
	public float MinX;

	public Transform CaughtTarg;
	public bool CaughtCam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void ManualUpdate()
	{

		if (PMov.YVelRise) {
			YDir = PMov.VelY*1000;
		} else {
			CamMultHoldY = 0.3f;
		}
		if (PMov.XVelRise) {
			XDir = PMov.VelX*1000;
		}
		else
		{
			CamMultHoldX = 0.3f;
		}

		Vector2 DirNormalizer = new Vector2 (XDir, YDir).normalized;
		YDir = DirNormalizer.y;
		XDir = DirNormalizer.x;

			
		YOff = (YOff * (CamSpeed+CamSpeedYMult) + YDir) / ((CamSpeed+CamSpeedYMult) + 1);
		XOff = (XOff * (CamSpeed+CamSpeedXMult) + XDir) / ((CamSpeed+CamSpeedXMult) + 1);



		CamMultHoldX -= Time.fixedDeltaTime;
		CamMultHoldY -= Time.fixedDeltaTime;

		if (CamMultHoldX < 0) {
			CamSpeedXMult *= 0.9f;
		} else {
			CamSpeedXMult = 20;
		}
		if (CamMultHoldY < 0) {
			CamSpeedYMult *= 0.9f;
		} else {
			CamSpeedYMult = 20;
		}

		if (CaughtCam) {
			transform.position = (transform.position * 2 + new Vector3 (CaughtTarg.position.x, CaughtTarg.position.y, -100) + new Vector3 (XOff, YOff, 0) * OffMult) / 3;
		} else {
			transform.position = (transform.position * 2 + new Vector3 (PMov.transform.position.x, PMov.transform.position.y, -100) + new Vector3 (XOff, YOff, 0) * OffMult) / 3;
		}
		if (transform.position.x > MaxX) {
			transform.position = new Vector3 (MaxX, transform.position.y, transform.position.z);
		}
		if (transform.position.y > MaxY) {
			transform.position = new Vector3 (transform.position.x,MaxY, transform.position.z);
		}
		if (transform.position.x < MinX) {
			transform.position = new Vector3 (MinX, transform.position.y, transform.position.z);
		}
		if (transform.position.y < MinY) {
			transform.position = new Vector3 (transform.position.x,MinY, transform.position.z);
		}

		if (Main.Data.Day == 1 || Main.Data.Day == 2) {
			if (transform.position.y < 5) {
				Main.Data.Obj_CheckCrops = true;
			}

		}

	}


}
