using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeldaCam : MonoBehaviour {

	public Vector3 LerpStart;
	public Vector3 LerpEnd;
	public float Tval;
	public bool Lerping;
	public float X;
	public float Y;
	int DDir;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Main.DataHolder.LinkDied)
		{
			return;
		}

		if(!Lerping)
		{
			if(Main.DataHolder.Link.transform.position.x > transform.position.x + 14.5f)
			{
				Main.DataHolder.Link.dir = 1;
				StartLerp(1);
				DDir= 1;
			}
			if(Main.DataHolder.Link.transform.position.x < transform.position.x - 14.5f)
			{
				Main.DataHolder.Link.dir = 2;
				StartLerp(2);
				DDir = 2;
			}
			if(Main.DataHolder.Link.transform.position.y < transform.position.y - 9.5f)
			{
				Main.DataHolder.Link.dir = 3;
				StartLerp(3);
				DDir =3;
			}
			if(Main.DataHolder.Link.transform.position.y > transform.position.y + 9.5f)
			{
				Main.DataHolder.Link.dir = 4;
				StartLerp(4);
				DDir=4;
			}
		}
		else
		{
			Tval+=Time.fixedDeltaTime*0.5f;
			if(Tval >=1)
			{
				Lerping = false;
				Tval = 1;
				Main.DataHolder.Link.ScreenTrans = false;
				Main.DataHolder.ZeldaCurrentRoomKilledEnemies =0;

				// Set ID?
				Main.DataHolder.ZeldaCalculateNextID(Main.DataHolder.CurrentRoomID,DDir);
				// spawn stuff
				Main.DataHolder.ZeldaSpawnEnemies();

			}
			X = Mathf.Lerp(LerpStart.x,LerpEnd.x,Tval);
			Y = Mathf.Lerp(LerpStart.y,LerpEnd.y,Tval);
			transform.position = new Vector3(X,Y,-50);
		}


	}

	void StartLerp(int dir)
	{
		if(dir == 1)
		{
			LerpStart = transform.position;
			LerpEnd = LerpStart + new Vector3(32,0,0);
		}
		else if(dir == 2)
		{
			LerpStart = transform.position;
			LerpEnd = LerpStart + new Vector3(-32,0,0);
		}
		else if(dir == 3)
		{
			LerpStart = transform.position;
			LerpEnd = LerpStart + new Vector3(0,-22,0);
		}
		else if(dir == 4)
		{
			LerpStart = transform.position;
			LerpEnd = LerpStart + new Vector3(0,22,0);
		}
		Main.DataHolder.Link.ScreenTrans = true;
		Lerping = true;
		Tval = 0;
		Main.DataHolder.ZeldaClearEnemyLeaveRoom();
	}



}
