using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcapellaBG : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite Bar1;
	public Sprite Bar2;
	bool flag;
	float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if (timer > 4.363636363636f) {
			flag = !flag;
			timer -= 4.363636363636f;
		}
		SR.sprite = flag ? Bar2 : Bar1;
	}
}
