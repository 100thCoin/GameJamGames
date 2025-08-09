using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSmackObject : MonoBehaviour {

	public GameObject Target;
	public float SmackTimer;
	public GameObject HeroFollower;
	public bool OneTimeUse;
	public bool Smacked;
	public float SmackedTimer;
	public bool Enemy;

	public float SpinTimer;
	public LookAtPlayer LAP;
	public int Health = 2;
	public float GraceDelay;

	// Use this for initialization
	void Start () {
		if (!Enemy) {
			Health = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Smacked) {
			SmackedTimer += Time.deltaTime;
			Target.transform.position = new Vector3 (Target.transform.position.x, DataHolder.ParabolicLerp(1.36f,7.36f,SmackedTimer,1), Target.transform.position.z);
			if (SmackedTimer > 1) {
				if (Global.Dataholder.ExplosionPrefab) {
					Instantiate (Global.Dataholder.ExplosionPrefab, Target.transform.position, transform.rotation);
				}
				Destroy (Target.gameObject);
			}

		}
		if (Enemy) {
			if (GraceDelay > 0) {
				GraceDelay -= Time.deltaTime;
				if (GraceDelay <= 0) {
					OneTimeUse = false;

				}
			}

			if (SpinTimer > 0) {
				LAP.enabled = false;
				Vector3 diff = (Global.Dataholder.Pmov.transform.position - transform.position).normalized;
				Target.transform.eulerAngles = new Vector3 (0, -Mathf.Rad2Deg * Mathf.Atan2 (diff.z, diff.x) + 90 + (SpinTimer * SpinTimer) * 360 * 5, 0);


				SpinTimer -= Time.deltaTime;




			} else {
				LAP.enabled = true;
			}
		}
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Hero")) {

			if (!OneTimeUse) {
				OneTimeUse = true;
				if (Health <= 0) {
					Do ();
				} else {
					DoEnemy ();
				}
			}
		}

	}


	public void Do()
	{
		OneTimeUse = false;
		Smacked = true;
		bool AllOver = true;
		int i = 0;
		while(i < Global.Dataholder.HeroMov.CurrenLevelFilter.PotentialObjects.Length)
		{
			if(Global.Dataholder.HeroMov.CurrenLevelFilter.PotentialObjects [i]!= null && Global.Dataholder.HeroMov.CurrenLevelFilter.PotentialObjects [i]!= HeroFollower)
			{
				AllOver = false;
				break;
			}
			i++;
		}
		Destroy (HeroFollower);

		if (AllOver && Global.Dataholder.CurrentFailCutscene == null) {
			Global.Dataholder.DoFailCutscene();
		}
	}

	public void DoEnemy()
	{
		Health--;
		if (Health == 0) {
			Do ();
		}
		GraceDelay = 1f;
		SpinTimer = 1.5f;
		Global.Dataholder.HeroMov.CurrenLevelFilter.TheCurrentTarget = Global.Dataholder.HeroMov.CurrenLevelFilter.GetTheTargetObject ();

	}

	public void DoEnemyPlayer()
	{
		SpinTimer = 1.5f;
		Instantiate (Global.Dataholder.SFX_SpinBoost, transform.position, transform.rotation, transform);

	}


	[ContextMenu("set")]
	public void Set()
	{
		Target = transform.parent.parent.gameObject;
		HeroFollower = transform.parent.Find ("PotentialTarget").gameObject;

	}

}
