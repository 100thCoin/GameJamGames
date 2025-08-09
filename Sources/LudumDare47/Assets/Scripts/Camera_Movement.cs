using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour {

	public Char_Movement Char;
	public Camera Cam;

	public float CharPos;
	public Vector3 Pivot;
	public float Angle;
	public float Yoff;
	public float Dist;

	public GameObject SnakingFocus;
	public bool Snaking;
	public GameObject SnakingPivot;

	public bool PerfectRing;
	public GameObject[] Focuses;

	public LoopMain Loop;


	public float YRotation;
	public float YRotationGradual;
	public GameObject GradualPivot;
	public GameObject GradualPivot2;

	public Vector3 GradualPos;


	public QuadStrip TStrip;
	public QuadStrip NStrip;

	public float BonusX;
	public float BonusY;
	public float BonusFOV;
	public float BonusYRot;


	bool large;

	public bool SnakeLeft;
	public float SnakeXOff;
	public float SnakeYOff;
	public float SnakeFOVOff;
	public float SnakeYRotOff;
	public bool testing;

	public int T;
	public float Lvl2;
	public bool DramaticZoomOut;

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate()
	{
		if(!Snaking)
		{
			if(!testing)
			{
				if(Lvl2 == 1 & DramaticZoomOut)
				{
					BonusFOV *= 0.98f;
					BonusX *= 0.98f;
					BonusY *= 0.98f;
					BonusYRot *= 0.98f;
				}
				else
				{
					BonusFOV *= 0.8f;
					BonusX *= 0.8f;
					BonusY *= 0.8f;
					BonusYRot *= 0.8f;
				}
			}
		}
		else
		{
			if(SnakeLeft)
			{
				BonusX = -(Mathf.Abs(BonusX)*5f + SnakeXOff)/6f;
				BonusYRot = -(Mathf.Abs(BonusYRot)*5f + SnakeYRotOff)/6f;

			}
			else
			{
				BonusX = (Mathf.Abs(BonusX)*5f + SnakeXOff)/6f;
				BonusYRot = (Mathf.Abs(BonusYRot)*5f + SnakeYRotOff)/6f;

			}
			BonusY = (Mathf.Abs(BonusY)*5f + SnakeYOff)/6f;
			BonusFOV = (Mathf.Abs(BonusFOV)*5f + SnakeFOVOff)/6f;

		}
	}

	// Update is called once per frame
	void Update () {

		if(!Snaking && PerfectRing)
		{
			Angle = Mathf.Round((-((Char.transform.position.x)/Char.StripLength) * (Char.StripLength/32f)*512f))/((Char.StripLength/32f)*512f)*360f -(-80f*Lvl2);
			float X = Mathf.Sin(Angle * Mathf.Deg2Rad) *Dist + Pivot.x;
			float Z = Mathf.Cos(Angle * Mathf.Deg2Rad) *Dist + Pivot.z;
			float Y = Pivot.y + Yoff;
			transform.position = new Vector3(X,Y+ BonusY,Z);
			transform.eulerAngles = new Vector3(30,Angle,0);
		}
		else if(!PerfectRing)
		{
			T = Mathf.RoundToInt(((Char.transform.position.x + Char.StripLength*0.5f)/Char.StripLength)*Loop.TheLoop.Length);
			if(T >= Loop.TheLoop.Length)
			{
				T = T % Loop.TheLoop.Length;
			}

			bool larger = ((Char.transform.position.x + Char.StripLength*0.5f)/Char.StripLength)*Loop.TheLoop.Length> T;

			//T += Loop.SliceAt;


			if(T >= Loop.TheLoop.Length)
			{
				T= T % Loop.TheLoop.Length;
			}

			int N = 0;
			float MidAngle = 0;
			if(larger)
			{
				N = T+1;
				if(N == Loop.TheLoop.Length)
				{
					N=0;
				}
			}
			else
			{
				N = T-1;
				if(N == -1)
				{
					N=Loop.TheLoop.Length-1;
				}
			}

			if(Snaking)
			{
				T=3;
				N=2;
			}

			float TRot = 0;
			float NRot = 0;

			if(!Snaking)
			{
				TRot = Loop.OriginalLoop[T].transform.eulerAngles.y;
				NRot = Loop.OriginalLoop[N].transform.eulerAngles.y;

				TStrip = Loop.OriginalLoop[T];
				NStrip = Loop.OriginalLoop[N];
			}
			else
			{
				TRot = Loop.TheLoop[T].transform.eulerAngles.y;
				NRot = Loop.TheLoop[N].transform.eulerAngles.y;

				TStrip = Loop.TheLoop[T];
				NStrip = Loop.TheLoop[N];
			}
			GradualPivot.transform.parent = TStrip.transform;
			GradualPivot.transform.localPosition = new Vector3(0,0,Loop.Radius);
			GradualPivot2.transform.parent = NStrip.transform;
			GradualPivot2.transform.localPosition = new Vector3(0,0,Loop.Radius);


			while(TRot > 180)
			{
				TRot -= 360;
			}
			while(TRot < -180)
			{
				TRot += 360;
			}

			while(NRot > 180)
			{
				NRot -= 360;
			}
			while(NRot < -180)
			{
				NRot += 360;
			}

			if(TRot > NRot + 180)
			{
				NRot += 360;
			}
			else if(TRot < NRot - 180)
			{
				NRot -= 360;
			}

			Vector3 Midpos = Vector3.zero;

			if(larger)
			{
				float work = (((Char.transform.position.x + Char.StripLength*0.5f)/Char.StripLength)*Loop.TheLoop.Length % 1) / (1f);

				MidAngle = (NRot - TRot)*(work)+TRot+BonusYRot;
				Midpos = new Vector3((GradualPivot2.transform.position.x - GradualPivot.transform.position.x) *work +GradualPivot.transform.position.x,
					0,(GradualPivot2.transform.position.z - GradualPivot.transform.position.z) *work +GradualPivot.transform.position.z);
			}
			else
			{
				float work = (((Char.transform.position.x + Char.StripLength*0.5f)/Char.StripLength)*Loop.TheLoop.Length % 1) / (1f);

				MidAngle = (TRot - NRot)*(work)+NRot+BonusYRot;
				Midpos = new Vector3((GradualPivot.transform.position.x - GradualPivot2.transform.position.x) *work +GradualPivot2.transform.position.x,
					0,(GradualPivot.transform.position.z - GradualPivot2.transform.position.z) *work +GradualPivot2.transform.position.z);
			}

			Angle = MidAngle;
			Pivot = Midpos;
			float X = Mathf.Sin(Angle * Mathf.Deg2Rad) *Dist + Pivot.x;
			float Z = Mathf.Cos(Angle * Mathf.Deg2Rad) *Dist + Pivot.z;
			float Y = Pivot.y + Yoff;
			transform.position = new Vector3(X,Y,Z);
			//transform.eulerAngles = new Vector3(30,Angle,0);

			transform.eulerAngles = new Vector3(30,Angle,0);


			if(SnakeLeft)
			{
				transform.position += new Vector3(BonusX * Mathf.Cos(MidAngle*Mathf.Deg2Rad),BonusY, -BonusX * Mathf.Sin(MidAngle*Mathf.Deg2Rad));
			}
			else
			{
				transform.position += new Vector3(BonusX * Mathf.Cos(MidAngle*Mathf.Deg2Rad),BonusY,- BonusX * Mathf.Sin(MidAngle*Mathf.Deg2Rad));
			}

		}
		else
		{
			Angle = SnakingFocus.transform.eulerAngles.y;
			Pivot = SnakingPivot.transform.position;
			float X = Mathf.Sin(Angle * Mathf.Deg2Rad) *Dist + Pivot.x;
			float Z = Mathf.Cos(Angle * Mathf.Deg2Rad) *Dist + Pivot.z;
			float Y = Pivot.y + Yoff;
			transform.position = new Vector3(X,Y,Z);
			transform.eulerAngles = new Vector3(30,Angle,0);
		}

		Cam.fov = 20 + BonusFOV;



	}
}
