using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGrass : MonoBehaviour {

	public Material Grass1;
	public Material Grass2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[ContextMenu("Set Mat")]
	void Change()
	{

		int ra = Random.Range (0, 2);
		if (ra == 1) {
			GetComponent<Renderer> ().material = Grass2;
		} else {
			GetComponent<Renderer> ().material = Grass1;

		}
		float R = 38;

		float r = R * Mathf.Sqrt(Random.Range(0f,1f));
		float theta = Random.Range(0f,1f)* 2 * Mathf.PI;
		float x = r * Mathf.Cos(theta);
		float y = r * Mathf.Sin(theta);
		transform.position = new Vector3 (x, transform.position.y, y);
		float rot = Random.Range (0f, 360f);
		transform.eulerAngles = new Vector3 (0, rot, 0);
	}

}
