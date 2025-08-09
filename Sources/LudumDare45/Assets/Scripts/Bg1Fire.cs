using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg1Fire : MonoBehaviour {


	public float ConstTimer;
	public float Timer;

	public bool Initial;

	public float DotPos;

	public DataHolder Main;

	void Start()
	{
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
	}

	void Update () {
		if(Main.FireBG)
		{
			Timer += Time.deltaTime;
			ConstTimer+= Time.deltaTime;
			if(Timer < 8)
			{
				Initial = true;
			}
			if(Initial)
			{
				float durationDOT = 8f;
				float prevscaleDOT = -11f;
				float targestscaleDOT = 0f;
				DotPos = (((prevscaleDOT - targestscaleDOT) * Mathf.Pow(Timer,2))/Mathf.Pow(durationDOT,2) - ((2*prevscaleDOT - 2*targestscaleDOT) * Timer)/durationDOT + prevscaleDOT);
				if(Timer > 8)
				{
					Initial = false;
					DotPos = 0;
				}

			}

			float bonus = Mathf.Sin(ConstTimer * 5)*0.15f + Mathf.Sin(ConstTimer * 2.37f)*0.15f;

			transform.localPosition = new Vector3(0,DotPos + bonus*2,0);
		}


	}
}
