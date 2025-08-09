using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengthText : MonoBehaviour {

	public DataHolder Main;
	public TextMesh TM;
	public Color TMCol;
	public bool DoOnce;
	public Collider Gate;

	public bool LowerGate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		TM.text = "Length: " + Main.Length + "m / 25.6m";
		if(Main.Length > 20 && !DoOnce)
		{
			DoOnce = true;
			TMCol = new Vector4(0,1,0,TMCol.a);
		}

		TMCol = new Vector4(TMCol.r,TMCol.g,TMCol.b,Main.ScaleTimer);
		TM.color = TMCol;

		if(Main.Length > 20 && Main.ScaleTimer > 0.5f)
		{
			Gate.enabled = false;
			LowerGate = true;
		}

		if(LowerGate)
		{
			Gate.transform.position -= new Vector3(0,Time.deltaTime*5,0);
		}


	}
}
