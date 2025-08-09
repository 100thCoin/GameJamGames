using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBar : MonoBehaviour {

	public float Percent;
	public int Level;

	public Sprite[] UILevel;

	public GameObject LevelUpBoosh;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Percent = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur / GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpReq;
		Percent = Percent * 100;
		gameObject.transform.localPosition = new Vector3(-3.4375f * (1 -(Percent/100)),0.5f,0);
		gameObject.transform.localScale = new Vector3(28.625f * (Percent/100),4.64f,1);

		if(Percent >= 100)
		{
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur - GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpReq;
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpReq = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpReq + 110;
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level +1;
			Level = Level +1;

			Instantiate(LevelUpBoosh,GameObject.Find("Player").gameObject.transform.position,gameObject.transform.rotation,GameObject.Find("Player").gameObject.transform);
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().HP += 1;
			gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite = UILevel[Level];
		}





	}
}
