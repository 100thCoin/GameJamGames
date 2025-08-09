using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePlayer : MonoBehaviour {

	public bool FollowX;
	public bool FollowY;
	public bool PostSnap;
	public bool PostFFSnap;

	public PlayerMovement P;
	public float Speed;
	public Vector2 SnapPos;
	public GameObject Baton;
	public GameObject Whooop;
	public bool DoOnce;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(!PostSnap && !PostFFSnap)
		{
			
			if(FollowY)
			{
				gameObject.transform.position = new Vector3(gameObject.transform.position.x,P.gameObject.transform.position.y,gameObject.transform.position.z);
			}
			if(FollowX)
			{
				gameObject.transform.position = new Vector3(P.gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
			}

		}
		else if(PostSnap)
		{
			Speed += Time.fixedDeltaTime * 5;
			gameObject.transform.position = new Vector3(gameObject.transform.position.x + Speed,gameObject.transform.position.y,gameObject.transform.position.z);
			if(gameObject.transform.position.x >= SnapPos.x + 15)
			{
				NextLevel();
			}
		}
		else if(PostFFSnap)
		{
			Speed += Time.fixedDeltaTime * 0.5f;
			gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y - Speed,gameObject.transform.position.z);
			if(gameObject.transform.position.y <= SnapPos.x - 20)
			{
				NextLevel();
			}
		}

	}


	void NextLevel()
	{
		if(!DoOnce)
		{
		Instantiate(Whooop,P.gameObject.transform.position,gameObject.transform.rotation);
			DoOnce = true;
		}

	}


}
