using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W4PropBlocks : MonoBehaviour {

	public Vector2 Dimensions;
	public SpriteRenderer[] SRs;
	public BoxCollider Col;
	Vector2 Dimensions2;
	Vector2 Dimensions3;

	public int Depth;

	public bool Pink;

	// Use this for initialization
	void Start () {

		//EditorUpdate();


	}
	
	// Update is called once per frame
	public void Update () {

		Dimensions3 = Dimensions * 2 - new Vector2(1,1);
		//POSITIONS
		SRs[1].transform.localPosition = new Vector3(Dimensions3.x - 1f,0,0); //Up Right Corner
		SRs[2].transform.localPosition = new Vector3(Dimensions3.x - 1f,-Dimensions3.y + 1,0); //Down Right Corner
		SRs[3].transform.localPosition = new Vector3(0f,-Dimensions3.y + 1,0); //Down Left Corner
		SRs[4].transform.localPosition = new Vector3(Dimensions3.x*0.5f-0.5f,0,0); //Up Edge
		SRs[5].transform.localPosition = new Vector3(Dimensions3.x - 1f,-Dimensions3.y*0.5f+0.5f,0); //Right Edge
		SRs[6].transform.localPosition = new Vector3(Dimensions3.x *0.5f-0.5f,-Dimensions3.y + 1f,0); //Down Edge
		SRs[7].transform.localPosition = new Vector3(0,-Dimensions3.y*0.5f+0.5f,0); //Left Edge
		SRs[8].transform.localPosition = new Vector3(Dimensions3.x *0.5f-0.5f,-Dimensions3.y*0.5f+0.5f,0); //Interior
		SRs[9].transform.localPosition = new Vector3(Dimensions3.x,-1,0); //Up Right Corner Shadow
		SRs[10].transform.localPosition = new Vector3(Dimensions3.x,-Dimensions3.y,0); //Down Right Corner Shadow
		SRs[11].transform.localPosition = new Vector3(1,-Dimensions3.y,0); //Down Left Corner Shadow
		SRs[12].transform.localPosition = new Vector3(Dimensions3.x,-Dimensions3.y*0.5f-0.5f,0); //Right Edge Shadow
		SRs[13].transform.localPosition = new Vector3(Dimensions3.x *0.5f+0.5f,-Dimensions3.y,0); //Down Edge Shadow

		//SCALE
		Dimensions2 = Dimensions3*0.5f;

		SRs[4].transform.localScale = new Vector3(Dimensions2.x-1,1,0); //Up Edge
		SRs[5].transform.localScale = new Vector3(1,Dimensions2.y-1,0); //Right Edge
		SRs[6].transform.localScale = new Vector3(Dimensions2.x-1,1,0); //Down Edge
		SRs[7].transform.localScale = new Vector3(1,Dimensions2.y-1,0); //Left Edge
		SRs[8].transform.localScale = new Vector3(Dimensions2.x-1,Dimensions2.y-1,0); //Interior
		SRs[12].transform.localScale = new Vector3(1,Dimensions2.y-1,0); //Right Edge Shadow
		SRs[13].transform.localScale = new Vector3(Dimensions2.x-1,1,0); //Down Edge Shadow

		//COL
		Col.transform.localPosition = new Vector3(Dimensions3.x*0.5f-0.5f,0,0);
		Col.size = new Vector3(Dimensions.x*2,1,1);

		SRs[0].sortingOrder = -Depth*2;
		SRs[1].sortingOrder = -Depth*2;
		SRs[2].sortingOrder = -Depth*2;
		SRs[3].sortingOrder = -Depth*2;
		SRs[4].sortingOrder = -Depth*2;
		SRs[5].sortingOrder = -Depth*2;
		SRs[6].sortingOrder = -Depth*2;
		SRs[7].sortingOrder = -Depth*2;
		SRs[8].sortingOrder = -Depth*2;
		SRs[9].sortingOrder = -Depth*2-1;
		SRs[10].sortingOrder = -Depth*2-1;
		SRs[11].sortingOrder = -Depth*2-1;
		SRs[12].sortingOrder = -Depth*2-1;
		SRs[13].sortingOrder = -Depth*2-1;

	}
}
