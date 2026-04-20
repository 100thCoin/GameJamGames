using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour {

	public Transform Gun;
	public SpriteRenderer GunSR;
	public Camera Cam;

	public SpriteRenderer Gunshot;
	public Transform ShotHit;

	public Transform GunshotAnchor;
	public Transform GunshotAnchor_Flipy;

	public GameObject GunshotParticles;

	public float GunshotTimer;

	public Collider TargetHand;
	public GameObject DeadHandParticles;

	public GameObject BG_EyeMask;
	public float JustShotHandTimer;

	public SpriteRenderer EyeSheild1;
	public SpriteRenderer EyeSheild2;

	public float ShotEyeTimer;
	public Collider EyeballCollider;

	public SpriteRenderer FadeToWhite;

	public Rigidbody RB;

	public GameObject VictoryCutscene;
	public GameObject ParentGameObject;

	public TASTimeline Timeline;

	public GameObject DeadSplat;

	public bool Dead;
	public float DeadTimer;

	public int InitialTopValue;
	public int DeadTop;

	public SpriteRenderer FadeToWhite2;
	public float RespawnTime = 1;

	public List<GameObject> BossSpikes;
	public GameObject BossSpikePrefab;

	public float SpikeTimer;

	// Use this for initialization
	void Start () {
		InitialTopValue = Timeline.CurrentFrame-3;
		if (InitialTopValue < 10) {
			InitialTopValue = 10;
		}
	}


	void TimelineUpdate()
	{

	}

	void FixedUpdate()
	{
		Physics.Simulate (Time.fixedDeltaTime);
		RB.AddForce(new Vector3(0,-20,0)); //gravity.

	}

	// Update is called once per frame
	void Update () {

		if (Dead) {
			Timeline.Play = false;
			RB.isKinematic = true;
			DeadTimer += Time.deltaTime;
			if (DeadTimer < 1) {

				float TopLerp = DataHolder.SinLerp (DeadTop, InitialTopValue + 5, DeadTimer, 1);
				Timeline.Top = Mathf.RoundToInt (TopLerp);
			}
			if (DeadTimer >=1 && DeadTimer < 2) {
				Timeline.Top = InitialTopValue;

				float HandLerp = DataHolder.SinLerp (0, 1, DeadTimer-1, 1);
				TargetHand.gameObject.transform.localPosition = new Vector3 (Mathf.Lerp (11, 15, HandLerp), Mathf.Lerp (11, 10, HandLerp), 0);
			}
			if (DeadTimer > 2) {
				DeadTimer = 0;
				RespawnTime = 0;
				Dead = false;
				DeadSplat.SetActive (false);
				TargetHand.gameObject.transform.localPosition = new Vector3 (11, 11, 0);
				transform.localPosition = new Vector3 (-8, -12, 0);
				SpikeTimer = 0;
				Timeline.Play = true;
				Timeline.CurrentFrame = InitialTopValue + 3;

				Timeline.RemoveAllClicksAfterThis (Timeline.CurrentFrame);

				for (int i = 0; i < BossSpikes.Count; i++) {
					Destroy (BossSpikes [i]);
				}
				BossSpikes.Clear ();

			}

		} else {
			RB.isKinematic = false;
			DeadTimer = 0;

			if (JustShotHandTimer == 0) {
				SpikeTimer += Time.deltaTime;
				if (SpikeTimer > 0.5f) {
					SpikeTimer -= 2;
					int spikeCount = Random.Range (2, 5);
					for (int i = 0; i < spikeCount; i++) {
						GameObject Sp = Instantiate (BossSpikePrefab, new Vector3 (Random.Range (-21, 5), Random.Range (-1, 7), 0), transform.rotation, transform.parent);
						BossSpikes.Add (Sp);
						BossAttack BA = Sp.GetComponent<BossAttack> ();
						BA.Player = gameObject;
						BA.PMov = this;
					}

				}
			}
		}


		if (RespawnTime < 1) {
			RespawnTime += Time.deltaTime*5;
			FadeToWhite2.gameObject.SetActive (true);
			FadeToWhite2.color = new Vector4(1,1,1,1-RespawnTime);
		}


		Vector3 GunPoint = Cam.ScreenToWorldPoint (Input.mousePosition);
		Gun.transform.localPosition = (new Vector3(GunPoint.x,GunPoint.y,0)-(new Vector3 (transform.position.x,transform.position.y+ 0.5f, 0))).normalized*2;
		Gun.transform.localEulerAngles = new Vector3 (0, 0, Mathf.Atan2 (-Gun.transform.localPosition.x, Gun.transform.localPosition.y) * Mathf.Rad2Deg + 90);
		GunSR.flipY = (Gun.transform.localPosition.x < 0);

		RaycastHit Hit;
		if (Physics.Raycast (transform.position + new Vector3(0,0.5f,0), Gun.transform.localPosition.normalized,out Hit, 48)) {

			ShotHit.transform.position = Hit.point;


		}

		if (JustShotHandTimer > 0) {
			Dead = false;
			DeadSplat.SetActive (false);
			JustShotHandTimer += Time.deltaTime;

			BG_EyeMask.transform.localPosition = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), 0) * JustShotHandTimer * JustShotHandTimer;
			FadeToWhite.color = new Vector4 (1, 1, 1, JustShotHandTimer * 0.5f);
			FadeToWhite.gameObject.SetActive (true);
			if (JustShotHandTimer > 2) {
				JustShotHandTimer = 2;
				VictoryCutscene.SetActive (true);
				Timeline.gameObject.SetActive (false);
				ParentGameObject.SetActive (false);
			}
		}


		if (GunshotTimer > 0) {
			GunshotTimer -= Time.deltaTime;
			if (GunshotTimer < 0) {
				GunshotTimer = 0;
			}
			Gunshot.color = new Vector4 (1, 1, 1, GunshotTimer * 10);
		}

		if (ShotEyeTimer > 0) {
			ShotEyeTimer -= Time.deltaTime*3;
			if (ShotEyeTimer < 0) {
				ShotEyeTimer = 0;
			}
			EyeSheild1.color = new Vector4 (1, 1, 1, ShotEyeTimer);
			EyeSheild2.color = new Vector4 (1, 1, 1, ShotEyeTimer);
		}

		if (Input.GetKeyDown (KeyCode.Mouse0) && !Dead) {
			if (GunSR.flipY) {
				Instantiate (GunshotParticles, GunshotAnchor_Flipy.transform.position, transform.rotation, transform.parent);
			}
			else
			{
				Instantiate (GunshotParticles, GunshotAnchor.transform.position, transform.rotation, transform.parent);
			}
			GunshotTimer = 0.1f;
			Gunshot.transform.position = ((transform.position + new Vector3 (0, 0.5f, 0)+ Gun.transform.localPosition.normalized*2.5f) + (ShotHit.transform.position)) / 2.0f;
			Gunshot.transform.localScale = new Vector3 (Mathf.Abs (((transform.position + new Vector3 (0, 0.5f, 0) + Gun.transform.localPosition.normalized*2.5f) - (ShotHit.transform.position)).magnitude), 0.1f, 1);
			Gunshot.transform.localEulerAngles = Gun.transform.localEulerAngles;
			Instantiate (GunshotParticles, ShotHit.transform.position, transform.rotation, transform.parent);
			RB.AddForce (new Vector3 (Hit.point.x - transform.position.x, Hit.point.y - transform.position.y, 0).normalized * -1000);
			if (Hit.collider == TargetHand) {
				TargetHand.gameObject.SetActive (false);
				Instantiate (DeadHandParticles, TargetHand.transform.position, TargetHand.transform.rotation, transform.parent);
				JustShotHandTimer = 0.1f;
			}
			if (Hit.collider == EyeballCollider) {
				ShotEyeTimer = 1;

			}
		}

		if (transform.position.magnitude > 40) {
			// OOB prevention
			transform.position = new Vector3(-18,-12,0);
		}


	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("BossSpike")) {
			DeadSplat.SetActive (true);
			Dead = true;
			DeadTop = Timeline.Top;
		}


	}

}
