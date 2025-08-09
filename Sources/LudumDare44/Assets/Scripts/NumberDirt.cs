using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDirt : MonoBehaviour {

	public TextMesh T;
	public BlockMining L;

	// Update is called once per frame
	void Update () {
		T.text = "" + L.Ladders;
	}
}
