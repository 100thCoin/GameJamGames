using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {

	public Vector3 Target;
	public Vector3 Begin;
	public float Timer;

	public GameObject Player;
	public BoxCollider Box;

	public Collider DebugHit;

	public PlatformerMovement PMov;
	public SpriteRenderer SR;

	// Use this for initialization
	void Start () {

		Begin = transform.position;
		RaycastHit Hit;
		if (Physics.Raycast (transform.position + new Vector3(0,0.5f,0), -(transform.position-Player.transform.position).normalized,out Hit, 48, 1)) {
			DebugHit = Hit.collider;
			Target = Hit.point;
		}
		Timer = Random.Range (-0.15f, 0f);
	}
	
	// Update is called once per frame
	void Update () {

		if (PMov.JustShotHandTimer > 0) {

			SR.color = new Vector4 (1, 1, 1, 1-PMov.JustShotHandTimer*3);


			return;

		}

		transform.eulerAngles = new Vector3 (0, 0, -Mathf.Atan2 ((Target.x - Begin.x), (Target.y - Begin.y)) * Mathf.Rad2Deg + 180);
		Timer += Time.deltaTime;
		if (Timer < 1f) {
			transform.localScale = Vector3.one * DataHolder.ParabolicLerp (0, 1, Mathf.Clamp01(Timer*5), 1);
			transform.position = Begin - (Target - Begin).normalized * DataHolder.SinLerp(0,3,Timer,0.5f);
			Box.enabled = false;

		} else {
			if (Timer < 1.2f) {
				Box.enabled = true;

				transform.position = new Vector3 (DataHolder.ParabolicLerp(Target.x,Begin.x, (1.2f-Timer)*5, 1),DataHolder.ParabolicLerp(Target.y,Begin.y, (1.2f-Timer)*5, 1),0);
			} else {
				Box.enabled = false;
				transform.position = Target;
			}
		}


	}
}
