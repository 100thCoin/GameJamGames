using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLine : MonoBehaviour {

	public RuntimeAnimatorController Talking;
	public string[] Lines;
	public float[] ComeIn;
	public float Timer;
	public AudioClip Clip;
	public AudioSource AS;
	public bool Done;

	public bool SpawnSkeletonBreakerAFterMessage3;
	public bool SpawnShootHereAfterMessage7;
	// Use this for initialization
	void Start () {
		AS.clip = Clip;
		AS.enabled = false;
		AS.enabled = true;
		Global.Dataholder.HUDPortrait.Anim.runtimeAnimatorController = Talking;
		Global.Dataholder.HUDPortrait.Active = true;

	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;
		for (int i = ComeIn.Length-1; i >= 0; i--) {
			if (Timer > ComeIn[i]) {
				Global.Dataholder.HudText.text = Lines [i].Replace("#","\n");
				if (SpawnSkeletonBreakerAFterMessage3 && i == 3) {
					Global.Dataholder.SpawnSkeletonBreaker = true;
				}
				if (SpawnShootHereAfterMessage7 && i == 5) {
					Global.Dataholder.ShootHereIndicator.SetActive (true);
				}

				break;
			}

		}
		if(Timer > Clip.length && !AS.loop)
		{
			Global.Dataholder.HudText.text = "";
			Global.Dataholder.HUDPortrait.Active = false;
			Done = true;
		}

	}

	public void Abort()
	{
		Global.Dataholder.HudText.text = "";
		Global.Dataholder.HUDPortrait.Active = false;
		Done = true;

		Destroy (gameObject);
	}
}
