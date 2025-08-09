using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCart : MonoBehaviour {

	public bool DetectPlayer;
	public GameObject Mary;

	public GameObject Explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {




		if(DetectPlayer)
		{

			Vector2 Dir = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1).normalized;

			gameObject.GetComponent<Rigidbody>().velocity = Dir * 4;



		}

		//  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH
		gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,0 + ((gameObject.transform.position.y ) * 0.0001f));

	}

	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.name == "Mary" || other.gameObject.name.Contains("SheepType") || other.gameObject.name == "ThrowBox")
		{

			GameObject badsplosion =	Instantiate(Explosion,gameObject.transform.position,gameObject.transform.rotation) as GameObject;
			badsplosion.name = "SheepHurtbox";

			Destroy(gameObject.transform.parent.gameObject);
		}


	}


}
