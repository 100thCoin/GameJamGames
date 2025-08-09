using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {


	public RibbonMover TwoDPlayer;
	public RibbonManager RibbonMan;
	public float Distance;
	public Vector3 Offset;
	public Vector3 SmoothPos;
	public float SmoothRot;

	public float tilt = 20;
	public float tValue;

	public int SmoothSamples;
	public int loopind;
	public Vector3 DebugNormal;
	public float DebugRot;

	public MovementThreeD ThreeDPlayer;

	public Vector3 ThreeDOffset;
	public float ThreeDHeight;

	public bool MirroredRibbonPoints;
	public bool FlipCameraForReduceMotionSickness;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (RibbonMan.PlayerIsInsideRibbon || Global.Dataholder.PlayerMov3D.Transition_EnteringRibbon) {

			MirroredRibbonPoints = Global.Dataholder.RibbonMan.RibbonIsOrientedLeft;

			if (MirroredRibbonPoints) {
				tValue = Mathf.Clamp01 (((TwoDPlayer.transform.position.x * 1.2f) / (RibbonMan.SliceCount * RibbonMan.DistanceBetweenSlices) + 0.5f));
			} else {
				tValue = Mathf.Clamp01 (1 - ((TwoDPlayer.transform.position.x * 1.2f) / (RibbonMan.SliceCount * RibbonMan.DistanceBetweenSlices) + 0.5f));
			}

			if (Global.Dataholder.PlayerMov3D.Transition_EnteringRibbon) {
					tValue = 0;
			}

			int ind = Mathf.FloorToInt (tValue * RibbonMan.SliceCount);
			if (ind < SmoothSamples + 1) {
				ind = SmoothSamples + 1;
			}
			if (ind > RibbonMan.SliceCount - SmoothSamples - 1) {
				ind = RibbonMan.SliceCount - SmoothSamples - 1;
			}

			Vector3 TotalNormal = Vector3.zero;
			Vector3 NodePos = RibbonMan.PointsTop [ind];

			int i = -(SmoothSamples / 2);
			loopind = i;
			while (i < SmoothSamples / 2) {



				Vector3 CheckPos = RibbonMan.PointsTop [ind + i];
				Vector3 Normal = (RibbonMan.PointsTop [ind + i] - RibbonMan.PointsTop [ind - 1 + i]).normalized;
				Normal = new Vector3 (-Normal.z, 0, Normal.x);
				TotalNormal += Normal;

				i++;
			}

			TotalNormal = new Vector3 (TotalNormal.x / SmoothSamples, 0, TotalNormal.z / SmoothSamples);
			DebugNormal = TotalNormal;
			if (MirroredRibbonPoints != FlipCameraForReduceMotionSickness) {
				TotalNormal *= -1;
			}

			Vector3 DestPos = NodePos + TotalNormal * Distance + Offset;





			SmoothPos = (SmoothPos * 16 + DestPos) / 17f;
			transform.position = SmoothPos;

			float Rot = Mathf.Atan2 (TotalNormal.z, TotalNormal.x) * Mathf.Rad2Deg;


			float PrevSmooth = SmoothRot;


			if (Rot < 0) {
				Rot += 360;
			}


			DebugRot = Rot;


			if (Rot > 180 && PrevSmooth < 90) {
				PrevSmooth = ((360 + PrevSmooth) % 360)+360;
				if (PrevSmooth > 360 + 90) {
					PrevSmooth -= 360;
				}
			}
			else if (Rot < 90 && PrevSmooth > 180) {
				PrevSmooth = ((360 + PrevSmooth) % 360)-360;
			}

			SmoothRot = (PrevSmooth * 10 + Rot) / 11F;


			transform.eulerAngles = new Vector3 (tilt, -SmoothRot - 90, 0);

		} else {

			Vector3 DestPos = new Vector3 (ThreeDPlayer.transform.position.x, ThreeDHeight, ThreeDPlayer.transform.position.z) + ThreeDOffset;

			SmoothPos = (SmoothPos * 6 + DestPos) / 7f;


			transform.position = SmoothPos;

			float Rot = -90;
			float PrevSmooth = SmoothRot;

			if (PrevSmooth > 180) {
				PrevSmooth -= 360;
			}
			if (PrevSmooth < -180) {
				PrevSmooth += 360;
			}

			if (Rot > 90 != PrevSmooth > 90) {

				PrevSmooth = ((360 + Rot) % 360);

			}
			SmoothRot = (PrevSmooth * 6 + Rot) / 7f;


			transform.eulerAngles = new Vector3 (tilt, -SmoothRot - 90, 0);

		}
	}
}
