using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

	public GameObject Curtain1;
	public GameObject Curtain2;
	public GameObject Stage;
	public float CurtainSpread;


	public GameObject Hand1Pivot;
	public GameObject Hand2Pivot;
	public GameObject Hand1;
	public GameObject Hand2;
	public GameObject Jaw;
	public float Timer;
	public float Timer2;

	float hand1eulerz;
	float hand2eulerz;
	float hand1Offset;
	float hand2Offset;
	float JawOffset;

	public bool NecroDied;

	public bool HandsReady;

	public GameObject Music;
	public float VictoryTimer;

	public GameObject VictoryMusic;
	public GameObject VictoryScreen;
	public bool playeedVctiryMusic;
	public bool PlayerDied;
	public float PlayerDiedTimer;
	public GameObject PlayerDiedScreen;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if(PlayerDied)
		{
			PlayerDiedTimer += Time.deltaTime;
			if(PlayerDiedTimer > 3)
			{
				PlayerDiedScreen.SetActive(true);
			}
		}


		Curtain1.transform.localPosition = new Vector3(-CurtainSpread - 6,0,0);
		Curtain2.transform.localPosition = new Vector3(CurtainSpread + 6,0,0);
		if(!NecroDied)
		{
		Timer+= Time.deltaTime*0.5f;
		Timer2 += Time.deltaTime*0.8f;

		hand1eulerz = Mathf.Sin(Timer*3.3333f)+Mathf.Sin(Timer*1.75f);
		hand2eulerz = Mathf.Sin(Timer*4.12345f)+Mathf.Sin(Timer*0.8f);

		Hand1Pivot.transform.eulerAngles = new Vector3(0,0,hand1eulerz*Mathf.Rad2Deg/2 - 45);
		Hand2Pivot.transform.eulerAngles = new Vector3(0,0,hand2eulerz*Mathf.Rad2Deg/2);

		hand1Offset = Mathf.Sin(Timer2*1.4f)+Mathf.Sin(Timer2*2.4f+2);
		hand2Offset = Mathf.Sin(Timer2*1.2f)+Mathf.Sin(Timer2*3f+2);
		JawOffset = Mathf.Sin(Timer2*3f)+Mathf.Sin(Timer2*1.77f)-2;

		Hand1.transform.localPosition = new Vector3(0,hand1Offset,0);
		Hand2.transform.localPosition = new Vector3(0,-1+hand2Offset,0);
		Jaw.transform.localPosition = new Vector3(0,JawOffset*0.33f,0);
		}
		else
		{
			VictoryTimer += Time.deltaTime;

			if(VictoryTimer > 4 && !playeedVctiryMusic)
			{
				playeedVctiryMusic = true;
				Instantiate(VictoryMusic,transform.position,transform.rotation);
			}

			if(VictoryTimer > 5.333333f)
			{
				VictoryScreen.SetActive(true);
			}
		}
	}
}
