using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeGameManager : MonoBehaviour {

	public TextMesh TM;
	public float Timer;
	public string TypedWord;
	public bool Submit;

	public TypeGameButton[] AllTheButtons;

	public byte PressedButton;

	public byte AppearTimer;
	public bool GameOnScreen;
	public float AppearTimerFloat;

	public Transform Holder;

	public ChetQuizzlyAnims Chet;

	public string[] Answer;
	public GameObject PassCutscene;
	public GameObject FailCutscene;
	public GameObject FailCutscene2;

	public MidGameCutscenes FailCut;

	public byte Timer_SubSecond;
	public byte Timer_Seconds;


	// Use this for initialization
	void Start () {
		
	}

	public bool HackyKeyPress;


	// Update is called once per frame
	public void TimelineUpdate (Vector2 ClickPos) {
		if (Global.Dataholder != null) {
			
			Global.Dataholder.Timeline.LiveReactionButton.CurrentLiveReaction = FailCut.Scenes [1].InternalMonologue;
		}

		Chet.TimelineUpdate ();

		Timer_SubSecond++;
		if (Timer_SubSecond >= 60) {
			Timer_SubSecond = 0;
			Timer_Seconds++;
		}

		if (Timer_Seconds > 5 && !Submit) {
			Global.Dataholder.Timeline.PostTypeGame_Fail2.LoadState (Global.Dataholder.Timeline.GameInit.MGC_TypeGame_Fail2_Data);

			FailCutscene2.SetActive (true);
			gameObject.SetActive (false);
		}


		if (Submit) {
			GameOnScreen = false;
		}

		if (GameOnScreen) {
			AppearTimer++;
			if (AppearTimer > 60) {
				AppearTimer = 60;
			}
		}
		else
		{
			AppearTimer--;
			if (AppearTimer > 250) {
				AppearTimer = 0;
			}
		}

		if (AppearTimer == 0 && Submit) {

			bool Win = false;
			for (int i = 0; i < Answer.Length; i++) {
				if (TypedWord == Answer [i]) {
					Win = true;
				}
			}

			if (Win) {
				Global.Dataholder.Timeline.PostTypeGame_Pass.LoadState (Global.Dataholder.Timeline.GameInit.MGC_TypeGame_Pass_Data);
				PassCutscene.SetActive (true);
				gameObject.SetActive (false);
			} else {
				Global.Dataholder.Timeline.PostTypeGame_Fail.LoadState (Global.Dataholder.Timeline.GameInit.MGC_TypeGame_Fail_Data);
				FailCutscene.SetActive (true);
				gameObject.SetActive (false);
			}
		}

		AppearTimerFloat = ((AppearTimer + 0f) / 60f);
		Holder.transform.localPosition = new Vector3(0,DataHolder.ParabolicLerp(-13,0,AppearTimerFloat,1),0);


		for (int i = 0; i < AllTheButtons.Length; i++) {
			AllTheButtons [i].CheckEmulatedMouseClick (ClickPos);
			if (HackyKeyPress) {
				HackyKeyPress = false;
				PressedButton = (byte)i;
			}
		}
		TM.text = TypedWord;

	}

	public void AddChar(string Char)
	{
		for (int i = 0; i < AllTheButtons.Length; i++) {
			AllTheButtons [i].SR.sprite = AllTheButtons [i].Unpressed;
			AllTheButtons [i].TM.transform.localPosition = new Vector2 (0, 0.08f);
		}


		if (Char == "Submit") {
			Submit = true;
		} else if (Char == "Space") {
			TypedWord += " ";
		} else if (Char == "Erase") {
			TypedWord = TypedWord.Substring (0, TypedWord.Length - 1);
		} else {
			TypedWord += Char;
		}
	}


	public List<byte> SaveState()
	{
		List<byte> State = new List<byte> ();
		State.Add ((byte)PressedButton);
		State.Add (AppearTimer);
		State.Add (Chet.AnimTimer);
		State.Add (Chet.TieSpinAnimTimer);
		State.Add (Chet.CurrentAnim);	
		State.Add ((byte)(GameOnScreen ? 1 : 0));
		State.Add ((byte)TypedWord.Length);
		State.Add ((byte)(Submit ? 1 : 0));
		State.Add (Timer_Seconds);
		State.Add (Timer_SubSecond);

		char[] CharArray = TypedWord.ToCharArray ();
		for (int i = 0; i < CharArray.Length; i++) {
			State.Add ((byte)CharArray[i]);
		}
		return State;
	}

	public void LoadState(List<byte> State)
	{
		PressedButton = State [0];
		for (int i = 0; i < AllTheButtons.Length; i++) {
			AllTheButtons [i].SR.sprite = AllTheButtons [i].Unpressed;
			AllTheButtons [i].TM.transform.localPosition = new Vector2 (0, 0.08f);
		}
		if (PressedButton != 255) {
			AllTheButtons [PressedButton].SR.sprite = AllTheButtons [PressedButton].Pressed;
			AllTheButtons [PressedButton].TM.transform.localPosition = new Vector3 (0, -0.047f, 0);
		}
		AppearTimer = State [1];
		AppearTimerFloat = ((AppearTimer + 0f) / 60f);
		Holder.transform.localPosition = new Vector3(0,DataHolder.ParabolicLerp(-13,0,AppearTimerFloat,1),0);

		Chet.AnimTimer = State [2];
		Chet.TieSpinAnimTimer = State [3];
		Chet.CurrentAnim = State [4];

		Chet.ForceAnim (Chet.CurrentAnim);


		GameOnScreen = State [5] == 1;
		int CharCount = State [6];
		Submit = State [7] == 1;
		Timer_Seconds = State [8];
		Timer_SubSecond = State [9];

		TypedWord = "";
		for (int i = 0; i < CharCount; i++) {
			TypedWord += (char)State [10 + i];
		}
		TM.text = TypedWord;

	}

}
