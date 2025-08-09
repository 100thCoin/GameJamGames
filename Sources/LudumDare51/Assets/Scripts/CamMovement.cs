using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour {

	//Purpose: Smoothly moves the camera around.

	// "When you do things right, people won't be sure you've done anything at all"

	public CharMovement Player;

	public float Height;
	public float CurrentHeight;
	public float LastHeight;
	public float HeightMoveTimer;

	public float Duration;

	public float LocalXPos;
	public float TargetXpos;
	public float LastXpos;
	public float XTimer;
	public float XDelay;
	public float XDelayTimer;

	public float XDuration;
	public float Bonus;
	public int LookDir;
	public int LastDir;
	public Vector2 Movamajig;
	public Vector3 LastMovamajig;

	public bool PopupMode;
	public float PopupTimer;
	public float PopupZDisplacement;
	public bool PopupCam2;
	public float YRotation;
	public float YRotationGradual;
	public float YRotationBeforeCutscene;

	public Vector2 Trig;

	public float FOV;
	public float ManualYPosAdjustment;
	public float ManualZPosAdjustment;
	public float ManualAngleAdjustment;

	public bool LockX;
	public bool LockZ;
	public float LockXPos;
	public float LockZPos;

	public float RuleBoundsCurve;
	public float RuleBoundsCurveBonus;
	public bool RuleBoundsXMin;
	public bool RuleBoundsXMax;
	public bool RuleBoundsZMin;
	public bool RuleBoundsZMax;
	public float RuleBoundsMinX;
	public float RuleBoundsMaxX;
	public float RuleBoundsMinZ;
	public float RuleBoundsMaxZ;


	public bool DebugCameraRotation;

	public float BonusZoom;
	public float ZoomTimer;
	public float ZoomDelay;
	public float ZoomAmount;

	//public bool TEST;
	// Use this for initialization
	void Start () {
	}
	public float CamZ;

	public GameObject vis;

	// Update is called once per frame
	void LateUpdate () {

		if(DebugCameraRotation)
		{

			Vector3 RotMovamajig = new Vector3(Input.GetAxis("HorizontalAim"),0,-Input.GetAxis("VerticalAim"));

			YRotationGradual+= RotMovamajig.x * 2;

		}

		if(G.Main.CharMov.UpdateCameraFalling)
		{
			LastHeight = CurrentHeight;

			Height = Player.transform.position.y + Bonus;
		}


		while(YRotationGradual > 180)
		{
			YRotationGradual -= 360;
		}
		while(YRotationGradual < -180)
		{
			YRotationGradual += 360;
		}

		//smooth lerp from 180 to -180 without zooming all the way from 180 over zero and to -180
		if(YRotationGradual < -90 && YRotation > 90)
		{
			YRotation = (YRotation*7+ YRotationGradual + 360)/8;
			if(YRotation > 180)
			{
				YRotation -=360;
			}
		}
		else if(YRotationGradual > 90 && YRotation < -90)
		{
			YRotation = (YRotation*7+ YRotationGradual -360)/8;
			if(YRotation < -180)
			{
				YRotation +=360;
			}
		}
		else
		{
			YRotation = (YRotation*7+ YRotationGradual)/8;
		}

		Trig = new Vector2(Mathf.Sin(YRotation * Mathf.Deg2Rad),Mathf.Cos(YRotation * Mathf.Deg2Rad));

		if(PopupMode)
		{
			if(PopupTimer < 0.354f)
			{
				PopupTimer += Time.deltaTime;
				if(!PopupCam2)
				{
					PopupZDisplacement = (Mathf.Pow(PopupTimer,2) * 360*2)/9;
				}
				else
				{
					PopupZDisplacement = (Mathf.Pow(PopupTimer,2) * 360*2)/2;
				}
				if(PopupTimer >= 0.354f)
				{
					if(!PopupCam2)
					{
						PopupZDisplacement = 10;
					}
					else
					{
						PopupZDisplacement = 45;
					}
				}
			}
		}
		else
		{
			if(PopupTimer > 0)
			{
				PopupTimer -= Time.deltaTime;
				if(!PopupCam2)
				{
					PopupZDisplacement = (Mathf.Pow(PopupTimer,2) * 360*2)/8;
				}
				else
				{
					PopupZDisplacement = (Mathf.Pow(PopupTimer,2) * 360*2)/2;
				}				
				if(PopupTimer <= 0)
				{
					PopupZDisplacement = 0;
				}
			}
		}


		//TEST = (XDelayTimer > 0.1f);

			transform.eulerAngles = new Vector3(ManualAngleAdjustment + PopupZDisplacement*2f,YRotation,0);
	

		if(CurrentHeight != Height)
		{

			HeightMoveTimer += Time.deltaTime;
			//CurrentHeight = ((((LastHeight-2*((Height + 2 - (0.5f*LastHeight))-2)))/2)*Mathf.Pow((1/Duration)*HeightMoveTimer,2)-(LastHeight-2*((Height + 2 - (0.5f*LastHeight))-2))*(1/Duration)*HeightMoveTimer+(LastHeight));

			CurrentHeight = (((LastHeight - Height) * Mathf.Pow(HeightMoveTimer,2))/Mathf.Pow(Duration,2) - ((2*LastHeight - 2*Height) * HeightMoveTimer)/Duration + LastHeight);

			//	Y = (((a - h) * Mathf.Pow(timer,2))/Mathf.Pow(dur,2) - ((2*a - 2*h) * timer)/dur + a);

			if(HeightMoveTimer >= Duration)
			{
				CurrentHeight = Height;
			}
		}
		else
		{
			HeightMoveTimer = 0;
		}

		gameObject.GetComponent<Camera>().fieldOfView = FOV + PopupZDisplacement*1.5f;
		float ZFovSomething = (1 / ((1f/3000f)*gameObject.GetComponent<Camera>().fov));
		Vector3 Pos = new Vector3(vis.transform.position.x - ((ZFovSomething - PopupZDisplacement*2) * Trig.x) -(LocalXPos * Trig.y),CurrentHeight  + ManualYPosAdjustment,vis.transform.position.z - ((ZFovSomething - PopupZDisplacement*2) * Trig.y) + (LocalXPos * Trig.x) + ManualZPosAdjustment + ZoomAmount);



		transform.position = Pos;

		if(Movamajig.magnitude != 0)
		{
			LastMovamajig = Movamajig;
		}

		if(LastMovamajig.x > 0.2f)
		{
			LookDir = -1;
		}
		else if(LastMovamajig.x < -0.2f)
		{
			LookDir =1;
		}
		else if(Mathf.Abs(LastMovamajig.z) >=0.2f)
		{
			LookDir =0;
		}

		if(LastDir != LookDir)
		{
			LastXpos = LocalXPos;
			XTimer =0;
			XDelayTimer = 0;

		}


		LastDir = LookDir;

		TargetXpos = LookDir*0.333f;
		ZoomDelay+=Time.deltaTime;


		Movamajig = Vector2.zero;
		if(Input.GetKey(KeyCode.A))
		{
			Movamajig += new Vector2(-1,0);
		}
		if(Input.GetKey(KeyCode.D))
		{
			Movamajig += new Vector2(1,0);
		}
		if(Input.GetKey(KeyCode.W))
		{
			Movamajig += new Vector2(0,1);
		}
		if(Input.GetKey(KeyCode.S))
		{
			Movamajig += new Vector2(0,-1);
		}
		Movamajig = Movamajig.normalized;

		if(Mathf.Abs(Movamajig.x) >= 0.2f || Mathf.Abs(Movamajig.y) >= 0.2f)
		{
			if(LocalXPos != TargetXpos)
			{

				XDelayTimer += Time.deltaTime;

				if(XDelayTimer >= XDelay)
				{

					XTimer += Time.deltaTime;

					//LocalXPos = ((((LastXpos-2*((TargetXpos + 2 - (0.5f*LastXpos))-2)))/2)*Mathf.Pow((1/XDuration)*XTimer,2)-(LastXpos-2*((TargetXpos + 2 - (0.5f*LastXpos))-2))*(1/XDuration)*XTimer+(LastXpos));

					LocalXPos = (((LastXpos - TargetXpos) * Mathf.Pow(XTimer,2))/Mathf.Pow(XDuration,2) - ((2*LastXpos - 2*TargetXpos) * XTimer)/XDuration + LastXpos);

					//	Y = (((a - h) * Mathf.Pow(timer,2))/Mathf.Pow(dur,2) - ((2*a - 2*h) * timer)/dur + a);


					if(XTimer >= XDuration)
					{
						LocalXPos = TargetXpos;
					}
				}
			}
			else
			{
				XTimer = 0;
				XDelayTimer = 0;
			}

			ZoomDelay = 0;


		}

		float zoomspeed = 0.12f;

		if(ZoomDelay > 3 && ZoomTimer <1)
		{
			ZoomTimer+= Time.deltaTime * zoomspeed;
			if(ZoomTimer >3)
			{
				ZoomTimer =3;
			}

		}
		else if(ZoomTimer >0)
		{
			ZoomTimer-= Time.deltaTime*2;
			if(ZoomTimer <0)
			{
				ZoomTimer =0;
			}
		}
		if(ZoomTimer >0)
		{
			ZoomAmount = -Mathf.Cos(3.141592f*ZoomTimer)*0.5f*(BonusZoom)+0.5f*(BonusZoom);
			//this equation is missing the duration (because it's being multiplied by 1) and the start value (because it's zero)
			//don't copy it for other places, unless those values are also being left out intentionally.
		}
		else
		{
			ZoomAmount = 0;
		}

		if(RuleBoundsXMin)
		{
			if(transform.position.x < RuleBoundsMinX + RuleBoundsCurve)
			{
				float over = transform.position.x - RuleBoundsMinX;
				RuleBoundsCurveBonus = (1f/(2*RuleBoundsCurve))*(over*over)+2*RuleBoundsCurve*0.25f;
				if(over <0)	{RuleBoundsCurveBonus =RuleBoundsCurve*0.5f;}
				Pos =  new Vector3(RuleBoundsMinX + RuleBoundsCurveBonus,transform.position.y,transform.position.z);
				transform.position = Pos;
			}
		}
		if(RuleBoundsXMax)
		{
			if(transform.position.x > RuleBoundsMaxX - RuleBoundsCurve)
			{
				float over = (transform.position.x - RuleBoundsMaxX)*-1;
				RuleBoundsCurveBonus = (1f/(2*RuleBoundsCurve))*(over*over)+2*RuleBoundsCurve*0.25f;
				if(over <0)	{RuleBoundsCurveBonus =RuleBoundsCurve*0.5f;}
				Pos =  new Vector3(RuleBoundsMaxX - RuleBoundsCurveBonus,transform.position.y,transform.position.z);
				transform.position = Pos;
			}
		}
		float ZFOVConst = (1 / ((1f/3000f)*45));
		CamZ = vis.transform.position.z - ((ZFOVConst) * Trig.y) + (LocalXPos * Trig.x) + ManualZPosAdjustment;
		float PopupBonus = 	ZFOVConst - ((ZFovSomething - PopupZDisplacement*2) * Trig.y);
		if(RuleBoundsZMin)
		{
			float RuleZ = RuleBoundsMinZ - PopupZDisplacement;
			if(CamZ < RuleZ + RuleBoundsCurve)
			{
				float over = CamZ - RuleZ;
				RuleBoundsCurveBonus = (1f/(2*RuleBoundsCurve))*(over*over)+2*RuleBoundsCurve*0.25f;
				if(over <0)	{RuleBoundsCurveBonus =RuleBoundsCurve*0.5f;}
				Pos = new Vector3(transform.position.x,transform.position.y,RuleZ + RuleBoundsCurveBonus + PopupBonus);
				transform.position = Pos;
			}
		}
		if(RuleBoundsZMax)
		{
			if(CamZ > RuleBoundsMaxZ - RuleBoundsCurve)
			{
				float over = (CamZ - RuleBoundsMaxZ)*-1;
				RuleBoundsCurveBonus = (1f/(2*RuleBoundsCurve))*(over*over)+2*RuleBoundsCurve*0.25f;
				if(over <0)	{RuleBoundsCurveBonus =RuleBoundsCurve*0.5f;}
				Pos = new Vector3(transform.position.x,transform.position.y,RuleBoundsMaxZ - RuleBoundsCurveBonus);
				transform.position = Pos;
			}
		}
	}
}
