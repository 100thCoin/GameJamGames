using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleOutline : MonoBehaviour {

	[TextArea(5, 10)]
	public string text;

	public TextMesh TM;
	public TextMesh[] Out;

	// Use this for initialization
	[ContextMenu("Test")]
	void Start () {
		for(int i = 0; i < 16; i++)
		{
			Out [i].transform.localPosition = new Vector3(Mathf.Cos(Mathf.PI*2 * (i/16f))*0.1f,Mathf.Sin(Mathf.PI*2 * (i/16f))*0.1f,0);
		}
		Update ();
	}
	
	// Update is called once per frame
	void Update () {

		TM.text = text;
		for(int i = 0; i < 16; i++)
		{
			Out [i].text = text;
		}

	}
}
