using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	public float Depth;
	public float Offset;
	public Material[] MaterialsForThisDepth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float off = (Global.Dataholder.CamMov.transform.position.x * (1 - Depth));
		transform.localPosition = new Vector3(off,transform.localPosition.y,transform.localPosition.z);
		int i = 0;
		while (i < MaterialsForThisDepth.Length) {
			MaterialsForThisDepth [i].SetFloat ("_NoiseXOffset", -off + Offset);
			i++;
		}
	}
}
