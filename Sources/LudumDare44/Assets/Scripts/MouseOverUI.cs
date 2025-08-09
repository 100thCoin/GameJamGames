using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverUI : MonoBehaviour {


	public MouseOverWindow MouseOver;
	public ItemStat Stats;
	public TextMesh Count;
	public SpriteRenderer Sp;

	// Use this for initialization
	void Start () {
		MouseOver = GameObject.Find("MouseOverText").gameObject.GetComponent<MouseOverWindow>();
		Sp.sprite = Stats.Icon;
	}

	void Update () {

		Count.text = "x" + Stats.Count;

	}

	// Update is called once per frame
	void OnMouseOver () {
		Vector2 MousePos = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);
		MouseOver.T.text = Stats.Desc;
		MouseOver.transform.position = new Vector3(MousePos.x,MousePos.y,MouseOver.transform.position.z);
	}
	void OnMouseExit () 
	{

		MouseOver.transform.position = new Vector3(-10000,-10000,MouseOver.transform.position.z);
	}

}
