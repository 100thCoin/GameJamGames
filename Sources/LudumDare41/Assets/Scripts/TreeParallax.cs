using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeParallax : MonoBehaviour {

	public float speed;
	public bool DontLoop;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(gameObject.transform.position.x - speed * Time.deltaTime * GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ParallaxMult,gameObject.transform.position.y,gameObject.transform.position.z);

		if(!DontLoop)
		{
		if(gameObject.transform.position.x <gameObject.transform.parent.gameObject.transform.position.x -38.4f && !DontLoop)
		{
				gameObject.transform.position = new Vector3(gameObject.transform.position.x + (38.4f*2),gameObject.transform.position.y,gameObject.transform.position.z);

		}
		}
		//if(gameObject.transform.position.x <gameObject.transform.parent.gameObject.transform.position.x -64 && DontLoop)
		//{
		//	Destroy(gameObject);

		//}

	}
}
