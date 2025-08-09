using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public DataHolder Main;
	public GameObject ItemGet;

	public bool LevelGoal;
	public bool Key;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.name == "Player")
		{
			if(LevelGoal)
			{
				Main.ItemStar.transform.position = new Vector3(GameObject.Find("Main Camera").transform.position.x,0,0);
				Main.ItemStar.SetActive(true);
				Instantiate(ItemGet,Main.ItemStar.transform.position,transform.rotation,transform.parent);
				other.GetComponent<PlayerController>().CutsceneNoMove = true;
				Main.ItemPickupGoalLevel = GetComponent<SpriteRenderer>().sprite;

				GameObject.Find("Main Camera").transform.parent.GetComponent<CameraControl>().Player = other.gameObject;
				GameObject.Find("Main Camera").transform.parent.GetComponent<CameraControl>().Target = other.gameObject;

				Main.LockCamera = true;
				Main.SwooshControl.Step[0] = true;
				Main.SwooshControl.LevelClear = true;
				Main.SwooshControl.RingSize = 10;
				Main.SwooshControl.DotPos = 10;
				Main.Aud.clip = Main.Clear;
				Main.Aud.enabled = false; Main.Aud.enabled = true;
				Destroy(gameObject);
			}
			else
			{
				if(Key)
				{
					Main.HasKey = true;
					Instantiate(ItemGet,transform.position,transform.rotation,transform.parent);

					Destroy(gameObject);

				}




			}
		}


	}


}
