using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {

	public GameObject Blinking;
	public float BlinkTimer;
	public GameObject Curtian1;
	public GameObject Curtian2;
	public float CurtianSpread;
	public bool ACTION;
	public GameObject Game;
	public GameObject ToDisable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		BlinkTimer += Time.deltaTime;
		if(BlinkTimer > 0.5f)
		{
			BlinkTimer -= 1;
		}
		Blinking.SetActive(BlinkTimer > 0);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			ACTION = true;
		}

		if(ACTION)
		{
			Game.SetActive(true);
			CurtianSpread+= Time.deltaTime*7;
			ToDisable.SetActive(false);

			Curtian1.transform.localPosition = new Vector3(-CurtianSpread,0,0);
			Curtian2.transform.localPosition = new Vector3(CurtianSpread,0,0);
			if(CurtianSpread > 16)
			{
				Destroy(gameObject);
			}

		}




	}
}
