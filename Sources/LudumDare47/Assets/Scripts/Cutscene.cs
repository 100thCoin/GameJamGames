using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {

	public float[] timer;

	public bool[] step;

	public bool start;

	public float playerspeed;
	public GameObject player;
	public Animator PAnim;
	public RuntimeAnimatorController PRun;
	public RuntimeAnimatorController PIdle;
	public RuntimeAnimatorController PItem;
	public GameObject OpenVoid;
	public GameObject OpenWalls;

	public GameObject Stamp;
	public GameObject Restofworld;

	public Whoop Whoooop;

	public DataHolder Main;

	public GameObject EndCutscene;
	public GameObject player2;
	public Animator PAnim2;
	public GameObject Stamp2;
	public GameObject THEEND;
	public GameObject Everything2;
	public GameObject Map;
	public Whoop Whoooop1;

	public GameObject WahWah;

	public GameObject Loop;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(start)
		{
			timer[0] += Time.deltaTime;
			if(timer[0] > 0.5f)
			{
				step[0] = true;
				start = false;
			}
		}
		if(step[0])
		{
			PAnim.runtimeAnimatorController = PRun;
			player.transform.position += new Vector3(playerspeed,0,0);
			if(player.transform.position.x > -4)
			{
				step[1] = true;
				step[0] = false;
				OpenVoid.SetActive(false);
				OpenWalls.SetActive(true);
				PAnim.runtimeAnimatorController = PIdle;

			}
		}
		if(step[1])
		{
			playerspeed *= 0.8f;
			player.transform.position += new Vector3(playerspeed,0,0);

			if(playerspeed < 0.0001f)
			{
				step[2] = true;
				step[1] = false;
				PAnim.runtimeAnimatorController = PItem;
			}

		}

		if(step[2])
		{
			Stamp.transform.position -= new Vector3(0,2f,0);
			if(Stamp.transform.position.y <= 0)
			{
				// screenshake.
				step[3] = true;
				step[2] = false;
			}
		}

		if(step[3])
		{
			timer[3] += Time.fixedDeltaTime;
			if (timer[3] > 0.5f)
			{
				step[3] = false;
				step[4] = true;
				player.SetActive(false);
				Restofworld.SetActive(true);
				Loop.SetActive(true);
			}

		}

		if(step[4])
		{
			timer[4] += Time.fixedDeltaTime;
			if (timer[4] > 0.5f)
			{
				step[4] = false;
				step[5] = true;
			}

		}


		if(step[5])
		{
			Stamp.transform.position += new Vector3(0,2f,0);
			OpenWalls.transform.position += new Vector3(0,1f,0);

			if(Stamp.transform.position.y >= 80)
			{

				Stamp.SetActive(false);
				OpenWalls.SetActive(false);

				step[5] = false;
				GameObject.Find("Main").GetComponent<DataHolder>().SendTip1 = true;
				GameObject.Find("Main").GetComponent<DataHolder>().InsideCutscene = false;

			}

		}

		if(Main.Win)
		{
			step[6] = true;
			Main.Win = false;
			playerspeed = 1;
			Main.Move = false;
			Main.Calm = false;
		}

		if(step[6])
		{
			Whoooop.Close = true;
			timer[6] += Time.fixedDeltaTime;
			if (timer[6] > 1f)
			{
				step[6] = false;
				step[7] = true;
				EndCutscene.SetActive(true);
				Whoooop1.Close = false;
				Map.SetActive(false);
				if(!Main.Mute)
				{
					Instantiate(WahWah,transform.position,transform.rotation,transform);
				}
			}
		}

		if(step[7])
		{
			PAnim2.runtimeAnimatorController = PRun;
			player2.transform.position += new Vector3(playerspeed,0,0);
			if(player2.transform.position.x > -4)
			{
				step[8] = true;
				step[7] = false;
				PAnim2.runtimeAnimatorController = PIdle;

			}
		}
		if(step[8])
		{
			playerspeed *= 0.8f;
			player2.transform.position += new Vector3(playerspeed,0,0);

			if(playerspeed < 0.0001f)
			{
				step[9] = true;
				step[8] = false;
				PAnim2.runtimeAnimatorController = PItem;
			}

		}

		if(step[9])
		{
			Stamp2.transform.position -= new Vector3(0,2f,0);
			if(Stamp2.transform.position.y <= 0)
			{
				// screenshake.
				step[10] = true;
				step[9] = false;
			}
		}

		if(step[10])
		{
			timer[10] += Time.fixedDeltaTime;
			if (timer[10] > 0.5f)
			{
				step[10] = false;
				player.SetActive(false);
				THEEND.SetActive(true);
				Main.AnyKey = true;
			}

		}



	}
}
