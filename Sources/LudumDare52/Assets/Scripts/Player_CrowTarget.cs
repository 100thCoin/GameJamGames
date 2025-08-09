using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CrowTarget : MonoBehaviour {

	public float Timer;
	public float Speed;
	public float DimX;
	public float DimY;
	public float VOff;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime * Speed;
		transform.localPosition = new Vector3 (Mathf.Cos (Timer) * DimX, Mathf.Sin (Timer) * DimY + VOff, 0);


	}
}
