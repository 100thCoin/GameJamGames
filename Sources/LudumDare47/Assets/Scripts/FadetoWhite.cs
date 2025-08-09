using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadetoWhite : MonoBehaviour {


	public bool Active;

	public float Timer;

	public LoopMain Loop;
	public Camera_Movement CamMov;

	public SpriteRenderer SR;
	public Level2AlwaysFacePlayer PreLoad;
	public GameObject Level2Tree;

	public bool DoOnce;
	public bool alsoDoOnce;

	public GameObject SnakeCol;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Active)
		{
			Timer+= Time.deltaTime*2;
			SR.color = new Vector4(1,1,1,Timer*2);
			if(Timer > 0.5f &&!DoOnce)
			{
				DoOnce = true;
				CamMov.DramaticZoomOut = true;
				Loop.CreateLevel2 = true;
				Level2Tree.SetActive(true);
			}
			if(Timer > 0.6f)
			{
				GameObject.Find("Main").GetComponent<DataHolder>().Move = false;
				GameObject.Find("Main").GetComponent<DataHolder>().Forest = true;
				if(!alsoDoOnce)
				{

					SnakeCol.transform.position = new Vector3(SnakeCol.transform.position.x+32000 %320,0,SnakeCol.transform.position.z+32000%320);
					alsoDoOnce = true;
					CamMov.BonusFOV = -10;
					CamMov.BonusY = -4;
					CamMov.BonusX = 0;
					CamMov.BonusYRot = 0;

					GameObject.Find("Main").GetComponent<DataHolder>().Move = false;
					GameObject.Find("Main").GetComponent<DataHolder>().Forest = true;

					Destroy(PreLoad.gameObject);

				}
			
				SR.color = new Vector4(1,1,1,0);
				SR.color = new Vector4(1,1,1,+1.8f-Timer*3);

			}
			if(Timer > 0.9f)
			{
				Destroy(gameObject);
			}
		}


	}
}
