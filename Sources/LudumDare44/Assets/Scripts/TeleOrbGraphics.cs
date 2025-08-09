using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleOrbGraphics : MonoBehaviour {

	public float Disp;
	public GameObject[] B_0;
	public GameObject[] B_1;
	public GameObject[] G_0;
	public GameObject[] G_1;
	public GameObject[] F_0;
	public GameObject F_1;
	public Vector2 Pos;


	public Vector2 Dest;

	public float Duration;
	public float Delay;
	public float DelayTimer;
	public float Timer;

	public DataHolder Main;

	public GameObject Teleparts;
	public GameObject TelepartsArrive;

	// Use this for initialization
	void OnEnable () {

		Instantiate(Teleparts,new Vector3(Main.Player.transform.position.x,Main.Player.transform.position.y,Main.Player.transform.position.z - 1),gameObject.transform.rotation,gameObject.transform.parent.parent);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		int i =0;
		float D = 1;
		bool first = true;
		while( i < B_0.Length)
		{
			B_0[i].transform.position = new Vector3( (transform.position.x + B_0[i].transform.position.x * (Disp/D))/(1 + (Disp/D)),
				(transform.position.y + B_0[i].transform.position.y * (Disp/D))/(1 + (Disp/D)),
				B_0[i].transform.position.z);
			
			B_1[i].transform.position = new Vector3( (transform.position.x + B_1[i].transform.position.x * (Disp/D))/(1 + (Disp/D)),
				(transform.position.y + B_1[i].transform.position.y * (1/D))/(1 + (1/D)),
				B_1[i].transform.position.z);
			
			G_0[i].transform.position = new Vector3( (transform.position.x + G_0[i].transform.position.x * (Disp/D))/(1 + (Disp/D)),
				(transform.position.y + G_0[i].transform.position.y * (1/D))/(1 + (1/D)),
				G_0[i].transform.position.z);
			
			G_1[i].transform.position = new Vector3( (transform.position.x + G_1[i].transform.position.x * (Disp/D))/(1 + (Disp/D)),
				(transform.position.y + G_1[i].transform.position.y * (1/D))/(1 + (1/D)),
				G_1[i].transform.position.z);
			
			F_0[i].transform.position = new Vector3( (transform.position.x + F_0[i].transform.position.x * (Disp/D))/(1 + (Disp/D)),
				(transform.position.y + F_0[i].transform.position.y * (1/D))/(1 + (1/D)),
				F_0[i].transform.position.z);
			


			D *= 1/Disp;
			i++;
		}

		F_1.transform.position = new Vector3((transform.position.x + F_1.transform.position.x)/(2),(transform.position.y + F_1.transform.position.y)/(2),F_1.transform.position.z);



		DelayTimer += Time.fixedDeltaTime;

		if(DelayTimer >= Delay)
		{

			Timer += Time.fixedDeltaTime;

			//LocalXPos = ((((LastXpos-2*((TargetXpos + 2 - (0.5f*LastXpos))-2)))/2)*Mathf.Pow((1/XDuration)*XTimer,2)-(LastXpos-2*((TargetXpos + 2 - (0.5f*LastXpos))-2))*(1/XDuration)*XTimer+(LastXpos));

			float LocalXPos = (((Pos.x - Dest.x) * Mathf.Pow(Timer,2))/Mathf.Pow(Duration,2) - ((2*Pos.x - 2*Dest.x) * Timer)/Duration + Pos.x);
			float LocalYPos = (((Pos.y - Dest.y) * Mathf.Pow(Timer,2))/Mathf.Pow(Duration,2) - ((2*Pos.y - 2*Dest.y) * Timer)/Duration + Pos.y);

			//	Y = (((a - h) * Mathf.Pow(timer,2))/Mathf.Pow(dur,2) - ((a - h) * timer)/dur + a);


			if(Timer >= Duration)
			{
				LocalXPos = Dest.x;
				LocalYPos = Dest.y;

				Main.IsPlayerTele = false;
				Main.Player.transform.position = new Vector3(Dest.x,Dest.y,Main.Player.transform.position.z);
				Main.Player.GetComponent<CharMovement>().Velocity = Vector3.zero;
				Main.Player.GetComponent<CharMovement>().OnGround = false;
				Main.Player.GetComponent<CharMovement>().InAir = true;


				Main.Player.SetActive(true);

				Timer = 0;
				DelayTimer = 0;
				Instantiate(TelepartsArrive,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z - 1),gameObject.transform.rotation,gameObject.transform.parent.parent);

				gameObject.transform.parent.gameObject.SetActive(false);
			}

			gameObject.transform.position = new Vector3(LocalXPos,LocalYPos,gameObject.transform.position.z);

		}


	}


	public void GetReady()
	{
		transform.position= Pos;
		int i = 0;
		while( i < B_0.Length)
		{
			B_0[i].transform.position = new Vector3(Pos.x,Pos.y,B_0[i].transform.position.z);
			B_1[i].transform.position = new Vector3(Pos.x,Pos.y,B_1[i].transform.position.z);
			G_0[i].transform.position = new Vector3(Pos.x,Pos.y,G_0[i].transform.position.z);
			G_1[i].transform.position = new Vector3(Pos.x,Pos.y,G_1[i].transform.position.z);
			F_0[i].transform.position = new Vector3(Pos.x,Pos.y,F_0[i].transform.position.z);
			i++;
		}
		F_1.transform.position = new Vector3(Pos.x,Pos.y,F_1.transform.position.z);;


	}



}
