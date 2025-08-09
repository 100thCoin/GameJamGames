using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryButton : MonoBehaviour {
	public SpriteRenderer SR;
	public Sprite On;
	public Sprite Off;
	int Grace;

	// Use this for initialization
	void Start () {

	}



	void Update () {
		Grace--;
		if (Grace < 0) {
			SR.sprite = Off;
		}

	}

	void OnMouseOver()
	{
			SR.sprite = On;
			Grace = 2;
			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				Super.Dataholder.ReturnToTitle();

			}
	}

}