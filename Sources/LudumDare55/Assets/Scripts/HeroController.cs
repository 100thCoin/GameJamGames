using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {

	public int LevelID;

	public GetObjectPlayerWants CurrenLevelFilter;

	public float runspeed;

	public GameObject VisHolder; //billboard this bad boy.

	public Animator Anim;
	public Animator Anim2;

	public SpriteRenderer SR1;
	public SpriteRenderer SR2;
	public Sprite Idling;

	public RuntimeAnimatorController RAC_Run;
	public RuntimeAnimatorController RAC_Idle;

	Vector3 Dir;
	public Vector3 PrevFrame;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Global.Dataholder.Level == 0 || Global.Dataholder.PostBoomDialogue) {
			Anim.runtimeAnimatorController = RAC_Idle;
			Anim2.runtimeAnimatorController = RAC_Idle;
			SR1.sprite = Idling;
			SR2.sprite = Idling;
		}

		if (Global.Dataholder.Level == 1) {
			runspeed = 8;
		} else {

			runspeed = 10;
		}

		if (Global.Dataholder.PostBoomDialogue) {
			return;
		}

		Vector3 diff = (Global.Dataholder.Pmov.transform.position - transform.position).normalized;
		VisHolder.transform.eulerAngles = new Vector3 (0, -Mathf.Rad2Deg*Mathf.Atan2 (diff.z, diff.x) -90, 0);
		if (Global.Dataholder.Pmov.AtSpawn) {
			
			return;
		}

		if (CurrenLevelFilter.TheCurrentTarget == null) {

			CurrenLevelFilter.TheCurrentTarget = CurrenLevelFilter.GetTheTargetObject ();

			Anim.runtimeAnimatorController = RAC_Idle;
			Anim2.runtimeAnimatorController = RAC_Idle;

		} else {

			// run towards target.
			Anim.runtimeAnimatorController = RAC_Run;
			Anim2.runtimeAnimatorController = RAC_Run;
			Vector2 Tempdir = new Vector2(CurrenLevelFilter.TheCurrentTarget.transform.position.x - transform.position.x,CurrenLevelFilter.TheCurrentTarget.transform.position.z - transform.position.z).normalized;
			Dir = new Vector3 (Tempdir.x, 0, Tempdir.y);


		}





	}


	void FixedUpdate()
	{
		if (Global.Dataholder.Level == 0 || Global.Dataholder.Level == 3) {
			return;
		}
		if (Global.Dataholder.PostBoomDialogue) {
			return;
		}
		if (Global.Dataholder.Pmov.AtSpawn) {
			Vector3 Dir = new Vector3 (Global.Dataholder.Pmov.transform.position.x, 0, Global.Dataholder.Pmov.transform.position.z + 28);
			if (Dir.magnitude < 0.0001f) {
				transform.position = new Vector3 (0, 0.5f, -28) + new Vector3(-1,0,0) * 4;
			} else {
				transform.position = new Vector3 (0, 0.5f, -28) + Dir.normalized * 4;
			}
			if ((transform.position - PrevFrame).magnitude > 0.05f) {
				Anim.runtimeAnimatorController = RAC_Run;
				Anim2.runtimeAnimatorController = RAC_Run;
			} else {
				Anim.runtimeAnimatorController = RAC_Idle;
				Anim2.runtimeAnimatorController = RAC_Idle;
			}
			PrevFrame = transform.position;
		}
		if (CurrenLevelFilter.TheCurrentTarget != null) {

			transform.position += Dir * runspeed * Time.fixedDeltaTime;

		}

	}


}
