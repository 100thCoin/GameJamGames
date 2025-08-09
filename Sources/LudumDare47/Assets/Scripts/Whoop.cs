using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whoop : MonoBehaviour {

	public float Size;
	public float RealSize;
	public GameObject Loop;
	public GameObject[] Edge;

	public bool Close;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {


		if(Close)
		{
			Size -= Time.deltaTime;
			if(Size < -0.5f)
			{
				Size = -0.5f;
			}
		}
		else
		{
			Size += Time.deltaTime;
			if(Size > 1.1f)
			{
				Size = 1.1f;
			}
		}

		RealSize = Size / 6.6f;

		Edge[0].transform.localPosition = new Vector3(-32*RealSize-32,0,0);
		Edge[1].transform.localPosition = new Vector3(32*RealSize+32,0,0);
		Edge[2].transform.localPosition = new Vector3(0,-32*RealSize-32,0);
		Edge[3].transform.localPosition = new Vector3(0,32*RealSize+32,0);
		Loop.transform.localScale = Vector3.one * RealSize;


	}
}
