using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public Vector3 StartPos;

	public float TrackPos;

	public Transform Player;

	public int ExtraRoomSize;

	public float MidBuffer;

	public Camera Cam;

	public bool Inside;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Move () {

		TrackPos = 0;

		if(Player.transform.position.x > StartPos.x - MidBuffer)
		{
			float D = Player.transform.position.x - (StartPos.x - MidBuffer);

			if(D > 10)
			{
				TrackPos = D - 5;
			}
			else
			{
				TrackPos = Mathf.Pow(((D)/4.4725f),2);
			}
		}

		if(Player.transform.position.x > StartPos.x - MidBuffer + ExtraRoomSize)
		{
			float D = Player.transform.position.x - (StartPos.x - MidBuffer);
			float D2 = Player.transform.position.x - (StartPos.x - MidBuffer) - ExtraRoomSize;

			if(D2 > 10)
			{
				TrackPos -= (D2 - 5);
			}
			else
			{
				TrackPos -= Mathf.Pow(((D2)/4.4725f),2);
			}
		}


		transform.position = StartPos + new Vector3(TrackPos,0,0);
		transform.position *= 16;
		transform.position = new Vector3(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y),transform.position.z);
		transform.position /= 16;

		if(Inside)
		{
			transform.position = new Vector3(0,transform.position.y,transform.position.z);
		}

	}
}
