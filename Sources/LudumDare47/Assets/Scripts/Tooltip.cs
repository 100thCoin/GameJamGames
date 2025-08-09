using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour {

	public float Timer;
	public bool Walk;
	public bool Click;
	public bool Vertical;

	public DataHolder Main;

	public TextMesh TM;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Walk)
		{
			if(Main.SendTip1)
			{
				Timer += Time.deltaTime;

				if(Timer > 4)
				{
					TM.gameObject.SetActive(true);

				}

				if(Main.HaltTip1)
				{
					TM.gameObject.SetActive(false);
					Destroy(gameObject);
				}
			}
		}


		if(Click)
		{
			if(Main.SendTip2)
			{
				Timer += Time.deltaTime;

				if(Timer > 10)
				{
					TM.gameObject.SetActive(true);

				}

				if(Main.HaltTip2)
				{
					TM.gameObject.SetActive(false);
					Destroy(gameObject);
				}
			}
		}

		if(Vertical)
		{
			if(Main.SendTip3)
			{
				Timer += Time.deltaTime;

				if(Timer > 4)
				{
					TM.gameObject.SetActive(true);

				}

				if(Main.HaltTip3)
				{
					TM.gameObject.SetActive(false);
					Destroy(gameObject);
				}
			}
		}

	}
}
