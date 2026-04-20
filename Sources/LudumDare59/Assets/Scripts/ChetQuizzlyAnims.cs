using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizzlyAnim
{
	public string Name; // for editor convenience
	public Sprite Eye;
	public Sprite LeftHand;
	public Sprite RightHand;
	public Sprite LeftFoot;
	public Sprite RightFoot;

	public Vector2 EyePos;
	public Vector2 TieHairPos;
	public Vector2 LeftHandPos;
	public Vector2 RightHandPos;
	public Vector2 LeftFootPos;
	public Vector2 RightFootPos;

	public float TieHairEuler;

	public bool JustSpinTieHair;
	public float EyeballBobForce;
	public float EyeballBobSpeed;
	public bool EyeballBobLaugh;
	public bool Eyeball_Wiggle;

}


public class ChetQuizzlyAnims : MonoBehaviour {

	float EyeballFloatTimer;
	public byte EyeBallByteTimer;

	public SpriteRenderer SR_Eye;
	public SpriteRenderer SR_TieHair;
	public SpriteRenderer SR_LeftHand;
	public SpriteRenderer SR_RightHand;
	public SpriteRenderer SR_LeftFoot;
	public SpriteRenderer SR_RightFoot;


	public QuizzlyAnim[] QuizzlyAnims;
	public byte CurrentAnim;
	int prevAnim;

	public float Swooshiness;
	public float AnimTexDelay;

	public byte AnimTimer;
	public byte TieSpinAnimTimer;
	float TieSpinAnimTimerFloat;
	public float PrevTieEuler;
	float EyeWiggleMult = 0.035f;

	public float TieSpinSpeed;
	// Use this for initialization
	void Start () {
		
	}

	[ContextMenu("Reset to Idle")]
	public void ResetToIdle()
	{
		SR_Eye.sprite = QuizzlyAnims [0].Eye;
		SR_LeftHand.sprite = QuizzlyAnims [0].LeftHand;
		SR_RightHand.sprite = QuizzlyAnims [0].RightHand;
		SR_LeftFoot.sprite = QuizzlyAnims [0].LeftFoot;
		SR_RightFoot.sprite = QuizzlyAnims [0].RightFoot;
		SR_Eye.transform.localPosition = new Vector3(QuizzlyAnims [0].EyePos.x,QuizzlyAnims [0].EyePos.y,0);
		SR_TieHair.transform.localPosition = new Vector3(QuizzlyAnims [0].TieHairPos.x,QuizzlyAnims [0].TieHairPos.y,0);
		SR_LeftHand.transform.localPosition = new Vector3(QuizzlyAnims [0].LeftHandPos.x,QuizzlyAnims [0].LeftHandPos.y,0);
		SR_RightHand.transform.localPosition = new Vector3(QuizzlyAnims [0].RightHandPos.x,QuizzlyAnims [0].RightHandPos.y,0);
		SR_LeftFoot.transform.localPosition = new Vector3(QuizzlyAnims [0].LeftFootPos.x,QuizzlyAnims [0].LeftFootPos.y,0);
		SR_RightFoot.transform.localPosition = new Vector3(QuizzlyAnims [0].RightFootPos.x,QuizzlyAnims [0].RightFootPos.y,0);
		SR_TieHair.transform.eulerAngles = Vector3.zero;

	}

	public void ForceAnim(int Anim)
	{
		SR_Eye.sprite = QuizzlyAnims [Anim].Eye;
		SR_LeftHand.sprite = QuizzlyAnims [Anim].LeftHand;
		SR_RightHand.sprite = QuizzlyAnims [Anim].RightHand;
		SR_LeftFoot.sprite = QuizzlyAnims [Anim].LeftFoot;
		SR_RightFoot.sprite = QuizzlyAnims [Anim].RightFoot;
		SR_Eye.transform.localPosition = new Vector3(QuizzlyAnims [Anim].EyePos.x,QuizzlyAnims [Anim].EyePos.y,0);
		SR_TieHair.transform.localPosition = new Vector3(QuizzlyAnims [Anim].TieHairPos.x,QuizzlyAnims [Anim].TieHairPos.y,0);
		SR_LeftHand.transform.localPosition = new Vector3(QuizzlyAnims [Anim].LeftHandPos.x,QuizzlyAnims [Anim].LeftHandPos.y,0);
		SR_RightHand.transform.localPosition = new Vector3(QuizzlyAnims [Anim].RightHandPos.x,QuizzlyAnims [Anim].RightHandPos.y,0);
		SR_LeftFoot.transform.localPosition = new Vector3(QuizzlyAnims [Anim].LeftFootPos.x,QuizzlyAnims [Anim].LeftFootPos.y,0);
		SR_RightFoot.transform.localPosition = new Vector3(QuizzlyAnims [Anim].RightFootPos.x,QuizzlyAnims [Anim].RightFootPos.y,0);
		SR_TieHair.transform.eulerAngles = new Vector3(0,0,QuizzlyAnims [Anim].TieHairEuler);

	}


	
	// Update is called once per frame
	public void TimelineUpdate () {

		EyeBallByteTimer ++;
		if (EyeBallByteTimer >= 60) {
			EyeBallByteTimer = 0;
		}
		TieSpinAnimTimerFloat = ((TieSpinAnimTimer + 0f) / 60f);
		EyeballFloatTimer += Time.fixedDeltaTime;
		float Eyebonus = Mathf.Sin (EyeballFloatTimer * Mathf.PI*2 * QuizzlyAnims [CurrentAnim].EyeballBobSpeed) * QuizzlyAnims [CurrentAnim].EyeballBobForce;
		float HandBonus = Mathf.Sin (EyeballFloatTimer* Mathf.PI*2 * QuizzlyAnims [CurrentAnim].EyeballBobSpeed -0.8f) * QuizzlyAnims [CurrentAnim].EyeballBobForce;
		float TieBonus = Mathf.Sin (EyeballFloatTimer* Mathf.PI*2 * QuizzlyAnims [CurrentAnim].EyeballBobSpeed -0.4f) * QuizzlyAnims [CurrentAnim].EyeballBobForce;
		if (QuizzlyAnims [CurrentAnim].EyeballBobLaugh) {
			Eyebonus = Mathf.Abs (Eyebonus);
			HandBonus = Mathf.Abs (HandBonus);
			TieBonus = Mathf.Abs (TieBonus);
		}


		SR_Eye.transform.localPosition = new Vector3((SR_Eye.transform.localPosition.x * Swooshiness + QuizzlyAnims [CurrentAnim].EyePos.x) / (Swooshiness + 1),(SR_Eye.transform.localPosition.y * Swooshiness + (QuizzlyAnims [CurrentAnim].EyePos.y+Eyebonus)) / (Swooshiness + 1),0);
		SR_TieHair.transform.localPosition = new Vector3((SR_TieHair.transform.localPosition.x * Swooshiness + QuizzlyAnims [CurrentAnim].TieHairPos.x) / (Swooshiness + 1),(SR_TieHair.transform.localPosition.y * Swooshiness + QuizzlyAnims [CurrentAnim].TieHairPos.y+TieBonus) / (Swooshiness + 1),0);
		SR_LeftHand.transform.localPosition = new Vector3((SR_LeftHand.transform.localPosition.x * Swooshiness + QuizzlyAnims [CurrentAnim].LeftHandPos.x) / (Swooshiness + 1),(SR_LeftHand.transform.localPosition.y * Swooshiness + QuizzlyAnims [CurrentAnim].LeftHandPos.y+HandBonus) / (Swooshiness + 1),0);
		SR_RightHand.transform.localPosition = new Vector3((SR_RightHand.transform.localPosition.x * Swooshiness + QuizzlyAnims [CurrentAnim].RightHandPos.x) / (Swooshiness + 1),(SR_RightHand.transform.localPosition.y * Swooshiness + QuizzlyAnims [CurrentAnim].RightHandPos.y+HandBonus) / (Swooshiness + 1),0);
		SR_LeftFoot.transform.localPosition = new Vector3((SR_LeftFoot.transform.localPosition.x * Swooshiness + QuizzlyAnims [CurrentAnim].LeftFootPos.x) / (Swooshiness + 1),(SR_LeftFoot.transform.localPosition.y * Swooshiness + QuizzlyAnims [CurrentAnim].LeftFootPos.y) / (Swooshiness + 1),0);
		SR_RightFoot.transform.localPosition = new Vector3((SR_RightFoot.transform.localPosition.x * Swooshiness + QuizzlyAnims [CurrentAnim].RightFootPos.x) / (Swooshiness + 1),(SR_RightFoot.transform.localPosition.y * Swooshiness + QuizzlyAnims [CurrentAnim].RightFootPos.y) / (Swooshiness + 1),0);

		if (QuizzlyAnims [CurrentAnim].Eyeball_Wiggle) {
			SR_Eye.transform.localPosition = new Vector3(QuizzlyAnims [CurrentAnim].EyePos.x + Random.Range(-EyeWiggleMult,EyeWiggleMult)*2,QuizzlyAnims [CurrentAnim].EyePos.y + Random.Range(-EyeWiggleMult,EyeWiggleMult)*2,0);
			SR_LeftHand.transform.localPosition = new Vector3(QuizzlyAnims [CurrentAnim].LeftHandPos.x + Random.Range(-EyeWiggleMult,EyeWiggleMult),QuizzlyAnims [CurrentAnim].LeftHandPos.y + Random.Range(-EyeWiggleMult,EyeWiggleMult),0);
			SR_RightHand.transform.localPosition = new Vector3(QuizzlyAnims [CurrentAnim].RightHandPos.x + Random.Range(-EyeWiggleMult,EyeWiggleMult),QuizzlyAnims [CurrentAnim].RightHandPos.y + Random.Range(-EyeWiggleMult,EyeWiggleMult),0);
			SR_TieHair.transform.localPosition = new Vector3(QuizzlyAnims [CurrentAnim].TieHairPos.x + Random.Range(-EyeWiggleMult,EyeWiggleMult),QuizzlyAnims [CurrentAnim].TieHairPos.y + Random.Range(-EyeWiggleMult,EyeWiggleMult),0);
			SR_LeftFoot.transform.localPosition = new Vector3(QuizzlyAnims [CurrentAnim].LeftFootPos.x + Random.Range(-EyeWiggleMult,EyeWiggleMult),QuizzlyAnims [CurrentAnim].LeftFootPos.y + Random.Range(-EyeWiggleMult,EyeWiggleMult),0);
			SR_RightFoot.transform.localPosition = new Vector3(QuizzlyAnims [CurrentAnim].RightFootPos.x + Random.Range(-EyeWiggleMult,EyeWiggleMult),QuizzlyAnims [CurrentAnim].RightFootPos.y + Random.Range(-EyeWiggleMult,EyeWiggleMult),0);

		}

		if (prevAnim != CurrentAnim) {
			prevAnim = CurrentAnim;
			AnimTimer = 0;
		}

		AnimTimer++;

		if (AnimTimer > AnimTexDelay) {
			SR_Eye.sprite = QuizzlyAnims [CurrentAnim].Eye;
			SR_LeftHand.sprite = QuizzlyAnims [CurrentAnim].LeftHand;
			SR_RightHand.sprite = QuizzlyAnims [CurrentAnim].RightHand;
			SR_LeftFoot.sprite = QuizzlyAnims [CurrentAnim].LeftFoot;
			SR_RightFoot.sprite = QuizzlyAnims [CurrentAnim].RightFoot;
		}

		if (!QuizzlyAnims [CurrentAnim].JustSpinTieHair) {
			if (QuizzlyAnims [CurrentAnim].TieHairEuler == 180) {

				if (SR_TieHair.transform.localEulerAngles.z != 180) {
					if (PrevTieEuler != QuizzlyAnims [CurrentAnim].TieHairEuler) {
						TieSpinAnimTimer = 0;
					}
					TieSpinAnimTimerFloat = ((TieSpinAnimTimer + 0f) / 60f);
					SR_TieHair.transform.localEulerAngles = new Vector3 (0, 0, DataHolder.SinLerp (0, 180, Mathf.Clamp01 (TieSpinAnimTimerFloat), 1));
				}
				TieSpinAnimTimer += 4;
				if (TieSpinAnimTimer > 60) {
					TieSpinAnimTimer = 60;
				}
				PrevTieEuler = QuizzlyAnims [CurrentAnim].TieHairEuler;

			} else {
				// current anim is 0 rotation
				if (SR_TieHair.transform.localEulerAngles.z != 0) {
				
					if (PrevTieEuler != QuizzlyAnims [CurrentAnim].TieHairEuler) {
						TieSpinAnimTimer = 0;
					}
					TieSpinAnimTimerFloat = ((TieSpinAnimTimer + 0f) / 60f);
					SR_TieHair.transform.localEulerAngles = new Vector3 (0, 0, DataHolder.SinLerp (-180, 0, Mathf.Clamp01 (TieSpinAnimTimerFloat), 1));
				}
				TieSpinAnimTimer += 4;
				if (TieSpinAnimTimer > 60) {
					TieSpinAnimTimer = 60;
				}
				PrevTieEuler = QuizzlyAnims [CurrentAnim].TieHairEuler;

			}
		} else {

			SR_TieHair.transform.localEulerAngles += new Vector3 (0, 0, TieSpinSpeed);

		}

	}



}
