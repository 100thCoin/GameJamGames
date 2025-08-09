using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupText : MonoBehaviour {

	public string Text;
	public TextMesh[] TM;
	public bool DontMove;
	// Use this for initialization
	public void Start () {
		TM [0].text = Text;
		TM [1].text = Text;
		TM [2].text = Text;
		TM [3].text = Text;
		TM [4].text = Text;
		TM [5].text = Text;
		TM [6].text = Text;
		TM [7].text = Text;
		TM [8].text = Text;

	}
	
	// Update is called once per frame
	void Update () {
		if (!DontMove) {
			transform.position += new Vector3 (0, Time.deltaTime, 0);
		}
	}
}
