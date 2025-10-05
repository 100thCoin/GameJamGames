using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club4_Scene : MonoBehaviour {

	public float timer;

	public RuntimeAnimatorController GunPull;
	public RuntimeAnimatorController GunHold;
	public RuntimeAnimatorController GunShoot;

	public RuntimeAnimatorController TriviaDead;

	public GameObject ShotDead_SFX;
	public bool DoOnce;

	// Use this for initialization
	void Start () {
	
		Global.Dataholder.Inventory [6] = true;
	}
	
	// Update is called once per frame
	void Update () {



		timer += Time.deltaTime;
		if (timer > 2.84f) {

			Global.Dataholder.MainCamera.transform.position = new Vector3 (40, 20, -10);

		}

		if (timer > 11.9f) {

			Global.Dataholder.Club4_Collector.runtimeAnimatorController = GunPull;

		}

		if (timer > 13.8f) {
			Global.Dataholder.Club4_BlackBar.transform.localScale = new Vector3 (64, DataHolder.SinLerp (16, 1, (timer - 13.8f),0.5f), 1);
			Global.Dataholder.Club4_BlackBar.color = new Vector4 (0, 0, 0, timer - 13.8f);
		}

		if (timer > 14.1f) {

			Global.Dataholder.Club4_GunFlash.SetActive (true);
			if (!DoOnce) {
				Instantiate (ShotDead_SFX, transform.position, transform.rotation, transform.parent);
				DoOnce = true;
			}
		}
		if (timer > 14.3f) {

			Global.Dataholder.Club4_Collector.runtimeAnimatorController = GunShoot;
			Global.Dataholder.Club4_BlackBar.color = new Vector4 (0, 0, 0, 0);

			Global.Dataholder.Club4_Trivia.runtimeAnimatorController = TriviaDead;
		
		}

		if (timer > 15 && timer < 16) {

			Global.Dataholder.ScreenSwoop.color = new Vector4(0,0,0,DataHolder.SinLerp(189f/256f,149f/256f,timer-15,1));

		}
			

		if (timer > 17 && timer < 17.5f) {

			Global.Dataholder.ScreenSwoop.color = new Vector4(0,0,0,DataHolder.SinLerp(149f/256f,0.48f,timer-17,0.5f));

		}

		if (timer > 18) {
			Global.Dataholder.MainCamera.transform.position = new Vector3 (40, 30, -10);
			Global.Dataholder.ScreenSwoop.color = new Vector4(0,0,0,DataHolder.SinLerp(0.48f,1,timer-18,1f));

		}
		if (timer > 19) {
			Destroy (gameObject);
		}
	}

	void LateUpdate()
	{
		Global.Dataholder.GameWatchView = false;
	}
}
