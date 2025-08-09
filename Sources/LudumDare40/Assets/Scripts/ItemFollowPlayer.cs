using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFollowPlayer : MonoBehaviour {

	public GameObject Follow;

	public Vector3 Point;
	public Vector3 FollowPoint;
	public Vector3 Distance;
	public int ID;

	// Use this for initialization
	void Start () {

		Point = gameObject.transform.position;

		if (gameObject.transform.parent.GetComponent<ItemHolder> ().Items == 0) {
			Follow = GameObject.Find ("Player");
		} 
		else 
		{
			Follow = GameObject.Find ("Item " + gameObject.transform.parent.GetComponent<ItemHolder> ().Items);

		}

		gameObject.transform.parent.GetComponent<ItemHolder> ().Items = gameObject.transform.parent.GetComponent<ItemHolder> ().Items + 1;
		ID = gameObject.transform.parent.GetComponent<ItemHolder> ().Items;
		gameObject.transform.name = "Item " + gameObject.transform.parent.GetComponent<ItemHolder> ().Items;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		FollowPoint = Follow.gameObject.transform.position;

		Distance = FollowPoint - Point;
		if(Mathf.Pow(Mathf.Pow(FollowPoint.x - Point.x,2) + (Mathf.Pow(FollowPoint.y - Point.y,2)),0.5f) > 1.5f)
		{ 
			Point = new Vector3 ((FollowPoint.x + Point.x * 50) / 51.0f, (FollowPoint.y + Point.y * 50) / 51.0f, 0);
			Point = new Vector3 ((FollowPoint.x + Point.x * 50) / 51.0f, (FollowPoint.y + Point.y * 50) / 51.0f, 0);
			Point = new Vector3 ((FollowPoint.x + Point.x * 50) / 51.0f, (FollowPoint.y + Point.y * 50) / 51.0f, 0);

		}
		if(Mathf.Pow(Mathf.Pow(FollowPoint.x - Point.x,2) + (Mathf.Pow(FollowPoint.y - Point.y,2)),0.5f) > 2)
			{ 
			Point = new Vector3 ((FollowPoint.x + Point.x * 50) / 51.0f, (FollowPoint.y + Point.y * 50) / 51.0f, 0);
			Point = new Vector3 ((FollowPoint.x + Point.x * 50) / 51.0f, (FollowPoint.y + Point.y * 50) / 51.0f, 0);

			}

		if(Mathf.Pow(Mathf.Pow(FollowPoint.x - Point.x,2) + (Mathf.Pow(FollowPoint.y - Point.y,2)),0.5f) > 3)
		{ 
			Point = new Vector3 ((FollowPoint.x + Point.x * 50) / 51.0f, (FollowPoint.y + Point.y * 50) / 51.0f, 0);

		}
		if(Mathf.Pow(Mathf.Pow(FollowPoint.x - Point.x,2) + (Mathf.Pow(FollowPoint.y - Point.y,2)),0.5f) > 4)
		{ 
			Point = new Vector3 ((FollowPoint.x + Point.x * 50) / 51.0f, (FollowPoint.y + Point.y * 50) / 51.0f, 0);
		}
		if(Mathf.Pow(Mathf.Pow(FollowPoint.x - Point.x,2) + (Mathf.Pow(FollowPoint.y - Point.y,2)),0.5f) > 5)
		{ 
			Point = new Vector3 ((FollowPoint.x + Point.x * 50) / 51.0f, (FollowPoint.y + Point.y * 50) / 51.0f, 0);
		}
		if(Mathf.Pow(Mathf.Pow(FollowPoint.x - Point.x,2) + (Mathf.Pow(FollowPoint.y - Point.y,2)),0.5f) > 2.5f)
		{ 
			Point = new Vector3 ((FollowPoint.x + Point.x * 50) / 51.0f, (FollowPoint.y + Point.y * 50) / 51.0f, 0);

		}
		if(Mathf.Pow(Mathf.Pow(FollowPoint.x - Point.x,2) + (Mathf.Pow(FollowPoint.y - Point.y,2)),0.5f) > 3.5f)
		{ 
			Point = new Vector3 ((FollowPoint.x + Point.x * 50) / 51.0f, (FollowPoint.y + Point.y * 50) / 51.0f, 0);
		}
		if(Mathf.Pow(Mathf.Pow(FollowPoint.x - Point.x,2) + (Mathf.Pow(FollowPoint.y - Point.y,2)),0.5f) > 4.5f)
		{ 
			Point = new Vector3 ((FollowPoint.x + Point.x * 50) / 51.0f, (FollowPoint.y + Point.y * 50) / 51.0f, 0);
		}

		gameObject.transform.position = Point;


	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Enemy" && !gameObject.transform.parent.GetComponent<ItemHolder>().Explode) {

			gameObject.transform.parent.GetComponent<ItemHolder> ().ExplodePos = ID;
			gameObject.transform.parent.GetComponent<ItemHolder> ().Explode = true;


		}




	}


}
