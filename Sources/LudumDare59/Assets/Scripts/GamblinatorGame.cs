using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GamblinatorCard
{
	public bool Settled;
	public byte Pos;
	public byte RNGSeq;
	public SpriteRenderer C1;
	public SpriteRenderer C2;
	public bool AlwaysCherries;

	public byte DebugCardType;
}

public class GamblinatorGame : MonoBehaviour {

	byte[] CardTypeByRNG = {
		0,
		0,
		1,
		2,
		3,
		4,
		0,
		2,
		4,
		1,
		1,
		4,
		3,
		1,
		0,
		0,
		2,
		0,
		4,
		1,
		2,
		0,
		3,
		1,
		4,
		2,
		3,
		0,
		1,
		3,
		2,
		4,
		0,
		0,
		0
	};


	public GamblinatorCard[] Cards;

	public Sprite[] CardTypes;

	public byte CurrentCard;

	public byte ExitTimer;

	public GamblinatorButton Button;

	public GameObject PassCutscene;
	public GameObject FailCutscene;

	public LiveReactions GenericGambling;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void TimelineUpdate (Vector2 ClickPos) {
		if (Global.Dataholder != null) {
			
			Global.Dataholder.Timeline.LiveReactionButton.CurrentLiveReaction = GenericGambling;
		}

		if (CurrentCard >= Cards.Length) {
			ExitTimer ++;
			if (ExitTimer > 30) {
				// check if you won.
				int seed = Cards[0].DebugCardType;
				bool AllMatch = true;
				for (int i = 1; i < Cards.Length; i++) {
					if (Cards [i].DebugCardType != seed) {
						AllMatch = false;
						break;
					}
				}

				if (AllMatch) {
					Global.Dataholder.Timeline.PostGamblinator1_Pass.LoadState (Global.Dataholder.Timeline.GameInit.MGC_Gamblinator1_Pass_Data);

					PassCutscene.SetActive (true);
					gameObject.SetActive (false);
				} else {
					Global.Dataholder.Timeline.PostGamblinator1_Fail.LoadState (Global.Dataholder.Timeline.GameInit.MGC_Gamblinator1_Fail_Data);

					FailCutscene.SetActive (true);
					gameObject.SetActive (false);
				}
			}

		}

		Button.CheckEmulatedMouseClick (ClickPos);



		for (int i = 0; i < Cards.Length; i++) {
			if (Cards [i].Settled) {				
				Cards [i].Pos = 6;
			} else {
				Cards [i].Pos++;
				if (Cards [i].Pos == 10) {
					Cards [i].Pos = 0;
					Cards [i].RNGSeq++;
				}
				if (Cards [i].RNGSeq > 32) {
					Cards [i].RNGSeq = 0;
				}
				if (Cards [i].AlwaysCherries) {
					Cards [i].RNGSeq = 0;
				}
			}

			Cards[i].DebugCardType = CardTypeByRNG [Cards [i].RNGSeq+1];

			Cards[i].C1.sprite = CardTypes [CardTypeByRNG [Cards[i].RNGSeq]];
			Cards[i].C2.sprite = CardTypes [CardTypeByRNG [Cards[i].RNGSeq + 1]];

			Cards[i].C1.transform.localPosition = new Vector3 (0, -3.33f - Cards[i].Pos, 0);
			Cards[i].C2.transform.localPosition = new Vector3 (0, -3.33f - Cards[i].Pos + 10, 0);
		}


	}

	public void Ping()
	{
		if (CurrentCard < Cards.Length) {
			Cards [CurrentCard].Settled = true;
			CurrentCard++;
		}
	}

	public List<byte> SaveState()
	{
		List<byte> State = new List<byte> ();
		State.Add (CurrentCard);
		State.Add (ExitTimer);
		for (int i = 0; i < Cards.Length; i++) {
			State.Add ((byte)(Cards [i].Settled ? 1 : 0));
			State.Add (Cards [i].Pos);
			State.Add (Cards [i].RNGSeq);
		}
		return State;
	}

	public void LoadState(List<byte> State)
	{
		CurrentCard = State [0];
		ExitTimer = State [1];

		int p = 2;

		for (int i = 0; i < Cards.Length; i++) {
			Cards [i].Settled = State [p] == 1;
			p++;
			Cards [i].Pos = State [p];
			p++;
			Cards [i].RNGSeq = State [p];
			p++;
		}

		for (int i = 0; i < Cards.Length; i++) {
			if (Cards [i].Settled) {				
				Cards [i].Pos = 6;
			} else {
				if (Cards [i].Pos == 10) {
					Cards [i].Pos = 0;
					Cards [i].RNGSeq++;
				}
				if (Cards [i].RNGSeq > 32) {
					Cards [i].RNGSeq = 0;
				}
				if (Cards [i].AlwaysCherries) {
					Cards [i].RNGSeq = 0;
				}
			}

			Cards[i].DebugCardType = CardTypeByRNG [Cards [i].RNGSeq];

			Cards[i].C1.sprite = CardTypes [CardTypeByRNG [Cards[i].RNGSeq]];
			Cards[i].C2.sprite = CardTypes [CardTypeByRNG [Cards[i].RNGSeq + 1]];

			Cards[i].C1.transform.localPosition = new Vector3 (0, -3.33f - Cards[i].Pos, 0);
			Cards[i].C2.transform.localPosition = new Vector3 (0, -3.33f - Cards[i].Pos + 10, 0);
		}

	}

}
