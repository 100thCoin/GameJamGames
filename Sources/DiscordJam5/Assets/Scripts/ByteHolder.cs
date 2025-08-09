using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByteHolder : MonoBehaviour {

	public HexByte[] Bytes;

	// Use this for initialization
	void OnEnable () {
		DataHolder Main = GameObject.Find("Main").GetComponent<DataHolder>();
		Main.HexView.Bytes = Bytes;
		Main.HexView.Size = Bytes.Length;
		Main.CurrentLevelUI = gameObject;
	}
	

}
