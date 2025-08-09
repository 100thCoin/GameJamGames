using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceature_Frog : MonoBehaviour {

	public int FrogGroup;

	public bool BeCube;
	public Vector3 CubeLocation;

	public bool IsIdle;
	public float IdleTimer;
	public float Hoptimer;
	public bool IdleHopping;
	public bool IdleHopRight;
	public float CubeTimer;

	public SpriteRenderer SR;
	public Animator Anim;
	public Sprite Hop;
	public RuntimeAnimatorController RAC_Idle;

	public Vector3 Speed;

	public Vector3 CubeRandomOffset;

	public int FrogIndex;

	public bool FrogHasBeenClose;

	public bool Caged;
	public Vector3 CagePos;
	// Use this for initialization
	void Start () {
		CubeTimer = 1;
		float timer = Random.Range (0, 10000f);
		SR.color = new Vector4 (Mathf.Sin(timer)*0.5f+0.65f, Mathf.Sin(timer + Mathf.PI*0.666666f)*0.5f+0.65f, Mathf.Sin(timer + Mathf.PI * 1.333333f)*0.5f+0.65f, 1) + new Vector4(0.5f,0.5f,0.5f,0);

		FrogIndex = Global.Dataholder.FrogIndex[FrogGroup];
		Global.Dataholder.FrogIndex[FrogGroup]++;
		Global.Dataholder.FrogIndex[FrogGroup] &= 0x0F;
		IdleTimer = Random.Range (0, 5f);
		Anim.Play ("Frog", 0, Random.Range (0, 8f));
		SR.flipX = Random.Range (0, 2) == 1;
		Caged = Global.Dataholder.FrogCage != null&& FrogGroup == Global.Dataholder.CageGroup; 

		if (Caged) {
			transform.position = Global.Dataholder.FrogCage.transform.position + new Vector3 (Random.Range(-0.5f,0.5f), -0.75f, 0);
			IsIdle = false;
			return;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Caged) {
			if (Global.Dataholder.FrogCage == null) {
				Caged = false;
			}
			return;
		}


		BeCube = Global.Dataholder.FrogsAreCube[FrogGroup];
		if (Global.Dataholder.FrogHarmonyMode [FrogGroup]) {
			BeCube = true;
			Global.Dataholder.FrogsAreCube [FrogGroup] = true;
		}

		if (!BeCube && IsIdle) {
			CubeTimer = 1;

			if (IdleTimer > 6) {

				// hop time;
				IdleHopping = true;
				IdleTimer = 0;

				if (transform.localPosition.x > 1) {
					IdleHopRight = false;
				} else if (transform.localPosition.x < -1) {
					IdleHopRight = true;
				} else {
					IdleHopRight = Random.Range (0, 4) == 1;
				}

			}

			if (Hoptimer > 0.5f) {
				IdleHopping = false;
				Hoptimer = 0;
				IdleTimer = Random.Range (0, 1.5f);
				transform.localPosition = new Vector3 (transform.localPosition.x, 0.125f, 0);
				Anim.runtimeAnimatorController = RAC_Idle;
				Anim.Play ("Frog", 0, Random.Range (0, 8f));
			}

			if (IdleHopping) {
				Anim.runtimeAnimatorController = null;
				SR.sprite = Hop;
				SR.flipX = IdleHopRight;
				transform.position += new Vector3 (IdleHopRight ? Time.fixedDeltaTime : -Time.fixedDeltaTime, 0, 0);
				transform.localPosition = new Vector3 (transform.localPosition.x, 0.125f + Mathf.Abs (Mathf.Sin (Hoptimer * Mathf.PI * 2) * 0.5f), 0);

				Hoptimer += Time.fixedDeltaTime;

			} else {
				IdleTimer += Time.fixedDeltaTime;
			}

			Speed = Vector3.zero;


		}
		else  
		{
			IsIdle = false;

			Vector2 CubeDimensions =  new Vector2 (2, 0.25f);
			Anim.runtimeAnimatorController = RAC_Idle;

			if (Global.Dataholder.FrogHarmonyMode[FrogGroup] || !BeCube) {
				// cube mode;
				CubeDimensions =  new Vector2 (1, 0.25f);

				CubeTimer += Time.fixedDeltaTime;
				if (BeCube && (CubeTimer > 1 || (CubeRandomOffset - transform.position).magnitude < 0.1f)) {

					CubeTimer = Random.Range (0f, 0.66f);
					CubeLocation = Global.Dataholder.FrogCubePos[FrogGroup];
					CubeRandomOffset = Global.Dataholder.PMov.transform.position + new Vector3 (Random.Range (CubeDimensions.x/2f, -CubeDimensions.x/2f), Random.Range (CubeDimensions.y/2f, -CubeDimensions.y/2f), 0);
					Anim.runtimeAnimatorController = RAC_Idle;

				}

				if (!BeCube) {
					FrogHasBeenClose = false;
					if ((transform.parent.position - transform.position).magnitude < 0.2f) {
						IsIdle = true;
						IdleHopping = false;
						Hoptimer = 0;
						IdleTimer = Random.Range (0, 6f);
						transform.localPosition = new Vector3 (transform.localPosition.x, 0.125f, 0);
						Anim.runtimeAnimatorController = RAC_Idle;
					}

					Speed = (Speed * 1 + (transform.parent.position - transform.position).normalized).normalized;

					transform.position = (transform.position * 2 + (transform.parent.position + new Vector3(0,0.125f,0))) / 3f;			


				} else {
					Speed = (Speed * 1 + (CubeRandomOffset - transform.position).normalized).normalized;
					transform.position = (transform.position * 4 + CubeRandomOffset) / 5f;			

				}
				// move towards cube pos;



			} else {
				CubeLocation = Global.Dataholder.FrogCubePos[FrogGroup];

				CubeTimer += Time.fixedDeltaTime*8;
				if (CubeTimer >= 16) {
					CubeTimer -= 16;
				}
				float CuLerp = CubeTimer + FrogIndex;
				if (CuLerp >= 16) {
					CuLerp -= 16;
				}

				if (CuLerp < 8) {
					CubeRandomOffset = new Vector3 (CubeLocation.x + Mathf.Lerp (-CubeDimensions.x / 2f, CubeDimensions.x / 2f, CuLerp / 8f) * Global.Dataholder.FrogMelodize [FrogGroup], CubeLocation.y + (CubeDimensions.y / 2f), 0);
				} else {
					CubeRandomOffset = new Vector3 (CubeLocation.x + Mathf.Lerp (CubeDimensions.x / 2f, -CubeDimensions.x / 2f, (CuLerp - 8) / 8f) * Global.Dataholder.FrogMelodize [FrogGroup], CubeLocation.y - (CubeDimensions.y / 2f), 0);
				}
				transform.position = (transform.position * 3 + CubeRandomOffset) / 4f;			

				//transform.position = CubeRandomOffset;



			}



			SR.flipX = transform.position.x < CubeRandomOffset.x;





		}
	}
}
