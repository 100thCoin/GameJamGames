using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour {

	public int ID;
	public float Timer;

	public int E;
	public bool Enemy;


	public bool WillHitFlower;

	// Use this for initialization
	void Start () {
	
		E = Enemy ? -1 : 1;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Timer+= Time.fixedDeltaTime;

		if(ID == 0 || ID == 10)
		{
			transform.localPosition = new Vector3(Mathf.Sin(Timer*3),-Timer * E,0.1f);

		}

		else if(ID == 4 || ID == 14)
		{
			transform.localPosition = new Vector3(Mathf.Sin(Timer*6),Timer*5 * E,0.1f);

		}

		else if(ID == 2 || ID == 12)
		{
			transform.localPosition = new Vector3((0),Timer*5 * E,0.1f);
		}

		else if(ID == 3 || ID == 13)
		{
			transform.localPosition = new Vector3((-(Mathf.Pow(5*Timer-2,2)-4)*3),(-1)/(Timer+0.25f)+4 * E,0.1f);
			transform.localScale = new Vector3(1f + Timer*15,1f,1);
		}

		else if(ID == 7)
		{
			transform.localPosition = new Vector3(Mathf.Sin(Timer*3),Timer * E,0.1f);

		}

		else if(ID == 1 || ID == 11)
		{
			transform.localPosition = new Vector3((0),Timer*5 * E,0.1f);
		}

		if(WillHitFlower && ID != 3 && ID != 13 && ID != 2 && ID != 12)
		{
			if(Timer > 1)
			{
				Destroy(gameObject);

			}

		}


	}
}
