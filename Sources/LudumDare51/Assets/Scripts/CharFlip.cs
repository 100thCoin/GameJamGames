using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharFlip : MonoBehaviour {

	//26 frames of rotation
	public float CurrentCamRotation;

	public float RotationTimer;
	public bool Right;
	public bool Forward;
	public bool Turning;
	public bool Done;

	public bool Abort;

	public bool isPlayer;
	public bool PlayerSpin;
	public float SpinTimer;

	public CamMovement CamMov;
	Vector2 Trig;

	public bool Stage;


	Vector3 R;
	float L;
	float Z;
	float D;
	float F; 
	Vector3 E;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if(Turning)	//calculate rotation
		{
			if(Done) //begin a new rotation
			{
				RotationTimer = 0;
				Done = false;
			}

			L = 0;
			Z = 0;
			D = 1;
			if(Right)
			{
				D = -1;
				L = 1;
			}
			RotationTimer += (Time.deltaTime * 50f * D);

			F = -1; // 1 = CW, -1 = CCW

			if(Right)
			{
				if(!Forward)
				{
					F= 1;
				}
				else
				{
					Z = 1;
				}
			}
			else if(Forward)
			{
				F = 1;
			}
			else
			{
				Z = 1;
			}




			R = transform.eulerAngles;
			E = new Vector3(R.x,180 * L -(F* Mathf.Pow(RotationTimer-(14*D),2)+ 160 + 30*Z) + CamMov.YRotation,R.z);
			transform.eulerAngles = E;

			if(Mathf.Abs(RotationTimer) >= 18  && !Done)
			{
				transform.eulerAngles = new Vector3(R.x,180 * (L*-1 + 1) + CamMov.YRotation,R.z);
				Turning = false;
				Done = true; 
				if(Right)
				{
					Right = false;
				}
				else
				{
					Right = true;
				}
			}

			if(Abort)
			{
				Abort = false;
				Turning = false;
				RotationTimer =0;
				float AngleMRot = transform.eulerAngles.y - CamMov.YRotation;
				if( AngleMRot >90 || AngleMRot <-90)
				{
					transform.eulerAngles = new Vector3(0,180 + CamMov.YRotation,transform.eulerAngles.z);
					Right = true;
				}
				else
				{
					transform.eulerAngles = new Vector3(0,0 + CamMov.YRotation,transform.eulerAngles.z);
					Right = false;
				}

			}


		}
		else //if not turning
		{
			if(PlayerSpin)
			{
				SpinTimer += Time.deltaTime;
				if(Right)
				{
					transform.eulerAngles = new Vector3(0,180 - SpinTimer * (360*3),transform.eulerAngles.z);
				}
				else
				{
					transform.eulerAngles = new Vector3(0,0 + SpinTimer * (360*3),transform.eulerAngles.z);
				}

			}
			else
			{
				bool DontDoIt = false;
				if(isPlayer)
				{
					if(CamMov.PopupTimer > 0)
					{
						DontDoIt = true;
					}
				}

				if(!DontDoIt && !Stage)
				{
					if(Right)
					{
						transform.eulerAngles = new Vector3(0,180 + CamMov.YRotation,transform.eulerAngles.z);
					}
					else
					{
						transform.eulerAngles = new Vector3(0,0 + CamMov.YRotation,transform.eulerAngles.z);
					}
				}
			}
		}
	}

	public void AbortRotation()
	{
		Turning = false;
		RotationTimer = 0;

		float YRotation = transform.localRotation.eulerAngles.y - CamMov.YRotation;

		if(transform.rotation.eulerAngles.y > 180)
		{
			YRotation -= 360;
		}


		if(YRotation > 90 || YRotation <-90)
		{
			transform.eulerAngles = new Vector3(0,180 + CamMov.YRotation,transform.eulerAngles.z);
			Right = true;
		}
		else
		{
			transform.eulerAngles = new Vector3(0,0 + CamMov.YRotation,transform.eulerAngles.z);
			Right = false;
		}
	}


}
