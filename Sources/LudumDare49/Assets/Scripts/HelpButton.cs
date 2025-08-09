using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite Black;
	public Sprite Blue;
	public bool Buffer;


	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate()
	{
		if(!Buffer)
		{
			SR.sprite = Black;
		}
		else
		{
			Buffer = false;
		}

	}

	// Update is called once per frame
	void OnMouseOver () {
	
		SR.sprite = Blue;
		Buffer = true;
	}
}
