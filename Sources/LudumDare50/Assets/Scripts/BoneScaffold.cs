using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScaffold : MonoBehaviour {

	public SpriteRenderer SR;
	public SpriteRenderer SR1;
	public SpriteRenderer SR2;

	public Sprite SmallBones;

	public float AppearTimer;

	public int Level;

	// Use this for initialization
	void Start () {

		if(Level == 2)
		{

			SR.sprite = SmallBones;
			SR1.sprite = SmallBones;
			SR2.sprite = SmallBones;

		}


	}
	
	// Update is called once per frame
	void Update () {

		AppearTimer += Time.deltaTime;
		float dur = 3;
		float Spos = -170;
		float Dpos = -6;
		float height = -Mathf.Cos(3.141592f*AppearTimer*(1/dur))*0.5f*(Dpos-Spos) + 0.5f*(Spos +Dpos);

		transform.localPosition = new Vector3(transform.localPosition.x,height,0);

		if(AppearTimer > dur)
		{
			transform.localPosition = new Vector3(transform.localPosition.x,Dpos,0);
			Destroy(this);

		}

	}
}
