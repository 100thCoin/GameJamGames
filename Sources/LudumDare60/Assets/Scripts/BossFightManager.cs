using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour {

	public GameObject CurrentPlayedCard;
	public Transform CardHolder;
	public GameObject CardPrefab;

	public bool HIT;
	public bool NEXT;
	public int Round;
	public string[] CardTexts;
	public string[] HintTexts;

	public TextMesh Hint;
	public float HintTimer;

	public float PhaseTimer;

	public Dialogue[] Dialogues;
	public Dialogue CurrentDialogue;
	public Dialogue MISSDialogue;

	public float HitTimer;
	public bool HitAnim;
	public int[] CorrectCards;

	public bool MISS;


	public float InCombatTimer;
	public SpriteRenderer Featherworthy;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.HandInBattle) {
			InCombatTimer += Time.deltaTime * 2;
			InCombatTimer = Mathf.Clamp01 (InCombatTimer);
			Featherworthy.transform.localPosition = new Vector3 (0, DataHolder.ParabolicLerp(10,4.81f,InCombatTimer,1), 0);


		}


		if (MISS) {
			MISS = false;
			MISSDialogue.StartDialogue ();
		}

		if (HIT) {
			HitAnim = true;
			HitTimer = 0;	

			HIT = false;
		}
		if (CurrentDialogue != null) {
			if (!CurrentDialogue.Active) {
				NEXT = true;
				CurrentDialogue = null;
			}
		}

		if (HitAnim) {
			HitTimer += Time.deltaTime;
			if (CurrentPlayedCard == null) {
				HitTimer = 0;
				HitAnim = false;
			}
			if (HitTimer > 0.2f) {
				CurrentPlayedCard.GetComponent<BossCard> ().Destruct = true;
				HitAnim = false;
				if (Round < 4) {
					Dialogues [Round].StartDialogue ();
					CurrentDialogue = Dialogues [Round];
				}
				if (Round == 3) {

					WinFight ();

				}

			}
		}


		if (NEXT) {
			PhaseTimer = 0;
			NEXT = false;
			if (Round < 3) {
				if (CurrentPlayedCard != null) {
					Destroy (CurrentPlayedCard);
				}
				CurrentPlayedCard = Instantiate (CardPrefab, CardHolder.transform.position, CardHolder.transform.rotation, CardHolder.transform);
				BossCard BC = CurrentPlayedCard.GetComponent<BossCard> ();
				BC.TM.text = CardTexts [Round].Replace ("#", "\n");
				Hint.text = HintTexts [Round];
				Round++;
			}
		}

	}

	void LateUpdate()
	{
		PhaseTimer += Time.deltaTime;

		Hint.color = new Vector4 (Hint.color.r, Hint.color.g, Hint.color.b, PhaseTimer - 1);

	}

	void WinFight()
	{



	}

}
