using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTotem : MonoBehaviour {


	public RuntimeAnimatorController Thunk;
	public bool Active;
	public float Timer;
	public GameObject Killzone;
	public Animator anim;

	public bool DoOnce;
	public bool DoOnce2;

	public GameObject Col1;
	public GameObject Col2;

	public GameObject Boomsfx;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if(Active)
		{
			if(Timer == 0)
			{
				anim.runtimeAnimatorController = Thunk;
				Col1.transform.position = new Vector3(0,0,-500);
				Col2.SetActive(true);
			}
			Timer+= Time.deltaTime;

			if(Timer > 0.2f && !DoOnce)
			{
				DoOnce = true;
				GameObject.Find("Main Camera").GetComponent<CamShake>().Intensity = 1;
				Killzone.SetActive(true);
				Instantiate(Boomsfx,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);
			}

			if(Timer > 0.4f && !DoOnce2)
			{
				DoOnce2 = true;
				Killzone.SetActive(false);

			}
				

		}


	}
}
