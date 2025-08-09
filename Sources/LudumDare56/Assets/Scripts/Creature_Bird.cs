using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_Bird : MonoBehaviour {

	public int BirdGroup;

	public bool BeCube;
	public Vector3 CubeLocation;

	public bool IsIdle;
	public float IdleTimer;
	public float Hoptimer;
	public bool IdleHopping;
	public bool IdleHopRight;
	public float CubeTimer;

	public Vector3 Speed;

	public SpriteRenderer SR;
	public Animator Anim;
	public Sprite Hop;
	public RuntimeAnimatorController RAC_Idle;
	public RuntimeAnimatorController RAC_Fly;

	public Vector3 CubeRandomOffset;

	public bool PerimeterBird;
	public int BirdIndex;

	public bool BirdHasBeenClose;

	public float HarmonySwish;

	public bool Caged;
	public Vector3 CagePos;

	// Use this for initialization
	void Start () {
		CubeTimer = 1;
		float timer = Random.Range (0, 10000f);
		SR.color = new Vector4 (Mathf.Sin(timer)*0.5f+0.65f, Mathf.Sin(timer + Mathf.PI*0.666666f)*0.5f+0.65f, Mathf.Sin(timer + Mathf.PI * 1.333333f)*0.5f+0.65f, 1) + new Vector4(0.5f,0.5f,0.5f,0);

		if (PerimeterBird) {
			BirdIndex = Global.Dataholder.birdIndex[BirdGroup];
			Global.Dataholder.birdIndex[BirdGroup]++;
			Global.Dataholder.birdIndex[BirdGroup] &= 0x1F;
		}
		IdleTimer = Random.Range (0, 1f);
		SR.flipX = Random.Range (0, 2) == 1;
		Anim.Play ("Bird_Idle", 0, Random.Range (0, 8f));
		Caged = Global.Dataholder.BirdCage != null && BirdGroup == Global.Dataholder.CageGroup; 

		if (Caged) {
			transform.position = Global.Dataholder.BirdCage.transform.position + new Vector3 (Random.Range(-0.5f,0.5f), -0.75f, 0);
			IsIdle = false;
			return;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Caged) {
			if (Global.Dataholder.BirdCage == null) {
				Caged = false;
			}
			return;
		}


		BeCube = Global.Dataholder.BirdsAreCube[BirdGroup];
		if (!(PerimeterBird || !Global.Dataholder.BirdHarmonyMode[BirdGroup])) {
			BeCube = false;
			BirdHasBeenClose = false;
			CubeTimer = 1;
		}
		if (!BeCube && IsIdle) {
			BirdHasBeenClose = false;
			CubeTimer = 1;

			if (IdleTimer > 2) {

				// hop time;
				IdleHopping = true;
				IdleTimer = 0;

				if (transform.localPosition.x > 1) {
					IdleHopRight = false;
				} else if (transform.localPosition.x < -1) {
					IdleHopRight = true;
				} else {
					IdleHopRight = Random.Range (0, 2) == 1;
				}

			}

			if (Hoptimer > 0.5f) {
				IdleHopping = false;
				Hoptimer = 0;
				IdleTimer = Random.Range (0, 1.5f);
				transform.localPosition = new Vector3 (transform.localPosition.x, 0.125f, 0);
				Anim.runtimeAnimatorController = RAC_Idle;
				Anim.Play ("Bird_Idle", 0, Random.Range (0, 8f));
			}

			if (IdleHopping) {
				Anim.runtimeAnimatorController = null;
				SR.sprite = Hop;
				SR.flipX = IdleHopRight;
				transform.position += new Vector3 (IdleHopRight ? Time.fixedDeltaTime : -Time.fixedDeltaTime, 0, 0);
				transform.localPosition = new Vector3 (transform.localPosition.x, 0.125f + Mathf.Abs (Mathf.Sin (Hoptimer * Mathf.PI * 4) * 0.25f), 0);

				Hoptimer += Time.fixedDeltaTime;

			} else {
				IdleTimer += Time.fixedDeltaTime;
			}

			Speed = Vector3.zero;

			
		} else {
			IsIdle = false;
			Anim.runtimeAnimatorController = RAC_Fly;

			Vector2 CubeDimensions = Global.Dataholder.BirdHarmonyMode[BirdGroup] ? new Vector2 (2, 2) : new Vector2 (4, 2);

			if (!PerimeterBird || !BeCube) {
				// cube mode;
				CubeTimer += Time.fixedDeltaTime;
				if (CubeTimer > 1 || (CubeRandomOffset - transform.position).magnitude < 0.1f) {

					CubeTimer = Random.Range (0f, 0.66f);
					CubeLocation = Global.Dataholder.BirdCubePos[BirdGroup];
					CubeRandomOffset = CubeLocation + new Vector3 (Random.Range (CubeDimensions.x/2f, -CubeDimensions.x/2f), Random.Range (CubeDimensions.y/2f, -CubeDimensions.y/2f), 0);
					Anim.runtimeAnimatorController = RAC_Idle;

					Anim.Play ("Bird_Idle", 0, Random.Range (0, 8f));

				}

				if (!BeCube) {
					BirdHasBeenClose = false;

					if ((transform.parent.position - transform.position).magnitude < 0.1f) {
						IsIdle = true;
						IdleHopping = false;
						Hoptimer = 0;
						IdleTimer = Random.Range (0, 1.5f);
						transform.localPosition = new Vector3 (transform.localPosition.x, 0.125f, 0);
						Anim.runtimeAnimatorController = RAC_Idle;
						Anim.Play ("Bird_Idle", 0, Random.Range (0, 8f));
					}

					Speed = (Speed * 1 + (transform.parent.position - transform.position).normalized).normalized;

					transform.position = (transform.position * 15 + transform.parent.position) / 16f;			


				} else {
					Speed = (Speed * 1 + (CubeRandomOffset - transform.position).normalized).normalized;

					if (!BirdHasBeenClose && BeCube) {
						transform.position = (transform.position*2 + CubeRandomOffset)/3f;			
						if ((CubeRandomOffset - transform.position).magnitude < 0.5f) {
							BirdHasBeenClose = true;		
						}
					} else {

						if ((CubeRandomOffset - transform.position).magnitude > 8f && BeCube) {
							transform.position += Speed * 1.5f;			
						} else if ((CubeRandomOffset - transform.position).magnitude > 6f) {
							transform.position += Speed * 0.5f;			
						} else if ((CubeRandomOffset - transform.position).magnitude > 4f) {
							transform.position += Speed * 0.2f;			
						} else {
							transform.position += Speed * 0.05f;
						}
					}
				}
				// move towards cube pos;



			} else {
				//PerimeterBird
				CubeLocation = Global.Dataholder.BirdCubePos[BirdGroup];

				CubeTimer += Time.fixedDeltaTime*8;
				if (CubeTimer >= 32) {
					CubeTimer -= 32;
				}
				float CuLerp = CubeTimer + BirdIndex;
				if (CuLerp >= 32) {
					CuLerp -= 32;
				}


				if (CuLerp < 8) {
					CubeRandomOffset = new Vector3 (CubeLocation.x + Mathf.Lerp(-CubeDimensions.x/2f,CubeDimensions.x/2f,CuLerp/8f)*Global.Dataholder.BirdHarmonize[BirdGroup], CubeLocation.y + (CubeDimensions.y / 2f)*Global.Dataholder.BirdHarmonize[BirdGroup], 0);
				}
				else if (CuLerp < 16) {
					CubeRandomOffset = new Vector3 (CubeLocation.x + (CubeDimensions.x / 2f)*Global.Dataholder.BirdHarmonize[BirdGroup], CubeLocation.y + Mathf.Lerp(CubeDimensions.y/2f,-CubeDimensions.y/2f,(CuLerp-8)/8f)*Global.Dataholder.BirdHarmonize[BirdGroup], 0);
				}
				else if (CuLerp < 24) {
					CubeRandomOffset = new Vector3 (CubeLocation.x + Mathf.Lerp(CubeDimensions.x/2f,-CubeDimensions.x/2f,(CuLerp-16)/8f)*Global.Dataholder.BirdHarmonize[BirdGroup], CubeLocation.y - (CubeDimensions.y / 2f)*Global.Dataholder.BirdHarmonize[BirdGroup], 0);
				}
				else {
					CubeRandomOffset = new Vector3 (CubeLocation.x - (CubeDimensions.x / 2f)*Global.Dataholder.BirdHarmonize[BirdGroup], CubeLocation.y + Mathf.Lerp(-CubeDimensions.y/2f,CubeDimensions.y/2f,(CuLerp-24)/8f)*Global.Dataholder.BirdHarmonize[BirdGroup], 0);
				}
				transform.position = (transform.position * 3 + CubeRandomOffset) / 4f;			

				//transform.position = CubeRandomOffset;



			}



			SR.flipX = transform.position.x < CubeRandomOffset.x;



		}




	}
}
