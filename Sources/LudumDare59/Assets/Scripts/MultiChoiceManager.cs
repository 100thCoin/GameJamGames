using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiChoiceCutscene
{
	public byte ChetAnimation;
	[TextArea(5, 10)]
	public string Subtitles;
	public bool EndTimeline;
	public float Duration;
	public AudioClip AudioClip;
	public LiveReactions InternalMonologue;
	public GameObject NextMinigame;
}

public class MultiChoiceManager : MonoBehaviour {

	public float Timer;
	public bool Submit;

	public MultiChoiceButton[] AllTheButtons;

	public MultiChoiceCutscene Fail_Great;
	public MultiChoiceCutscene Fail_Good;
	public MultiChoiceCutscene Win;

	MultiChoiceCutscene[] Cutscenamajig;
	public bool DoingCutscene;
	public byte WhichCutscene;

	public byte PressedButton;

	public byte AppearTimer;
	public bool GameOnScreen;
	public float AppearTimerFloat;

	public Transform Holder;

	public ChetQuizzlyAnims Chet;

	public bool FirstPass = true;
	public bool PressedGreat; // In which case, the correct answer is "Good"
	public byte Timer_SubSecond;
	public byte Timer_Seconds;

	public SubtitleOutline Subtitles;

	public TASTimeline Timeline;

	public LiveReactions Good;
	public LiveReactions Great;

	public GameObject VoiceLinePrefab;
	public GameObject CurrentVoiceLine;
	public AudioSource TempAS;
	// Use this for initialization
	void Start () {
		Cutscenamajig = new MultiChoiceCutscene[3];
		Cutscenamajig [0] = Fail_Great;
		Cutscenamajig [1] = Fail_Good;
		Cutscenamajig [2] = Win;
	}

	public bool HackyKeyPress;

	public AudioSource OpeningLine;

	// Update is called once per frame
	public void TimelineUpdate (Vector2 ClickPos) {

		if (AppearTimer == 0 && !Submit) {
			OpeningLine.Play ();
		}
		if (!Global.Dataholder.Timeline.Play) {
			OpeningLine.Stop ();
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
			if ((PressedButton == 1 && !PressedGreat) || (PressedButton == 0 && PressedGreat)) {
				DoingCutscene = true;
				WhichCutscene = 2;
			} else {
				DoingCutscene = true;
				if (PressedGreat) {
					WhichCutscene = 1;
				} else {
					WhichCutscene = 0;
				}
			}
		}

		AppearTimerFloat = ((AppearTimer + 0f) / 60f);
		Holder.transform.localPosition = new Vector3(0,DataHolder.ParabolicLerp(-13,0,AppearTimerFloat,1),0);


		for (int i = 0; i < AllTheButtons.Length; i++) {
			AllTheButtons [i].CheckEmulatedMouseClick (ClickPos);
			if (HackyKeyPress) {
				HackyKeyPress = false;
				PressedButton = (byte)i;
				if (FirstPass) {
					FirstPass = false;
					PressedGreat = (i == 1);
				}
				Submit = true;
			}
		}

		if (DoingCutscene) {
			Timer_SubSecond++;
			if (Timer_SubSecond >= 60) {
				Timer_SubSecond = 0;
				Timer_Seconds++;
			}

			if (CurrentVoiceLine == null) {
				CurrentVoiceLine = Instantiate (VoiceLinePrefab, transform.position, transform.rotation, transform);
				TempAS = CurrentVoiceLine.GetComponent<AudioSource> ();
				TempAS.clip = Cutscenamajig [WhichCutscene].AudioClip;
				TempAS.Play ();
			}

			if (ClickPos.x != 100 && Super.Dataholder.PreviousVictory_CanSkipCutscenes) {
				Timer_Seconds = 99;
			}

			if (Timer_Seconds + 1 > Cutscenamajig [WhichCutscene].Duration) {
				float subsec = (Timer_SubSecond + 0f) / 60f;
				if (Timer_Seconds + subsec > Cutscenamajig [WhichCutscene].Duration) {
					Timer_Seconds = 0;
					Timer_SubSecond = 0;
					if (Cutscenamajig [WhichCutscene].EndTimeline) {
						Timeline.CellData [Timeline.CurrentFrame + 1].EndOfTimeline = true;
						if (!Timeline.LiveReactionButton.DoingTutorial) {
							Timeline.LiveReactionButton.CurrentLiveReaction = Cutscenamajig [WhichCutscene].InternalMonologue;
							Timeline.LiveReactionButton.Active = true;
						} else {
							Timeline.LiveReactionButton.CurrentLiveReaction = Timeline.LiveReactionButton.Tutorial;
							Timeline.LiveReactionButton.Active = true;
						}
					} else {
						if (CurrentVoiceLine != null) {
							Destroy (CurrentVoiceLine);
						}
						Global.Dataholder.Timeline.BasketballManager.LoadState (Global.Dataholder.Timeline.GameInit.BasketballData);
						Cutscenamajig [WhichCutscene].NextMinigame.SetActive (true);
						gameObject.SetActive (false);
					}
				}
			}
			if (!Timeline.CellData [Timeline.CurrentFrame + 1].EndOfTimeline) {
				Subtitles.text = Cutscenamajig [WhichCutscene].Subtitles;
			} else {
				Subtitles.text = "";
			}
			Chet.CurrentAnim = Cutscenamajig [WhichCutscene].ChetAnimation;
		} else {
			if (CurrentVoiceLine != null) {
				Destroy (CurrentVoiceLine);
			}
		}
		Chet.TimelineUpdate ();

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
		State.Add ((byte)(Submit ? 1 : 0));
		State.Add ((byte)(DoingCutscene ? 1 : 0));
		State.Add (WhichCutscene);
		State.Add (Timer_Seconds);
		State.Add (Timer_SubSecond);

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
		Submit = State [6] == 1;
		DoingCutscene = State [7] == 1;
		WhichCutscene = State [8];
		Timer_Seconds = State [9];
		Timer_SubSecond = State [10];

		if (!DoingCutscene) {
			Subtitles.text = "";
		} else {
			if (!Timeline.CellData [Timeline.CurrentFrame + 1].EndOfTimeline) {
				Subtitles.text = Cutscenamajig [WhichCutscene] .Subtitles;
			} else {
				Subtitles.text = "";
			}
		}

	}
}
