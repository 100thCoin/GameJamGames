using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DuckGameCutscene
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


public class DuckHuntGame : MonoBehaviour {

	public DuckGameCutscene[] FailDuckGame;
	public DuckGameCutscene[] ShootHostFail;
	public DuckGameCutscene[] ShootHostVictory;
	public bool DoingCutscene;
	public byte WhichCutscene;
	public byte WhichSlide;

	DuckGameCutscene[][] Cutscenamajig;

	public Transform Duck;

	public ChetQuizzlyAnims Chet;

	public byte Phase;

	public byte Step;

	public bool Fired;

	public DuckHuntButton Button;

	public GameObject Aim;
	public TASTimeline Timeline;

	public byte Timer_SubSecond;
	public byte Timer_Seconds;

	public SubtitleOutline Subtitles;

	public bool Progress_2_IShotHimButFailed;

	public GameObject VoiceLinePrefab;
	public GameObject CurrentVoiceLine;
	public AudioSource TempAS;

	// Use this for initialization
	void Start () {
		Cutscenamajig = new DuckGameCutscene[3][];
		Cutscenamajig [0] = FailDuckGame;
		Cutscenamajig [1] = ShootHostFail;
		Cutscenamajig [2] = ShootHostVictory;

	}
	
	// Update is called once per frame

	void Update()
	{
		if (!Fired) {

			Vector3 PhantomPos = new Vector3 (((Timeline.MousePosMod.x + 10) / 20) * Screen.height, ((Timeline.MousePosMod.y + 10) / 20) * Screen.height, 0);
			PhantomPos = Timeline.Cam.ScreenToWorldPoint (PhantomPos);
			Aim.transform.position = new Vector3 (PhantomPos.x, PhantomPos.y, 0);
		} else {
			Aim.transform.position = new Vector3 (0, -500, 0);
		}

	}

	public void TimelineUpdate (Vector2 ClickPos) {

		Global.Dataholder.Timeline.LiveReactionButton.CurrentLiveReaction = FailDuckGame[0].InternalMonologue;

		if (Progress_2_IShotHimButFailed) {
			Global.Dataholder.Timeline.LiveReactionButton.CurrentLiveReaction = ShootHostFail[0].InternalMonologue;
		}



		Button.CheckEmulatedMouseClick (ClickPos);

		if(Phase >0 || (Phase == 0 && Step > 2))
		{
			if (ClickPos.x != 100 && !DoingCutscene) {
				// we missed.
				DoingCutscene = true;
				Fired = true;
			}
		}

		Step++;
		if (Phase == 0) {
			if (Step > 140) {
				Step = 0;
				Phase++;
			}
			Duck.transform.localPosition = new Vector3 (0, -500, 0);
		}
		if (Phase == 1) {
			Duck.transform.localPosition = new Vector3 (Mathf.Sin (Step * Step * 32) * 5 - 5, Mathf.Sin (Step * 40) * 4 + 13);
			if (Step > 15) {
				Step = 0;
				Phase++;
			}
		}
		if (Phase == 2) {
			if (Step > 15) {
				Step = 15;
				if (!DoingCutscene) {
					DoingCutscene = true;
				}
			}
			Duck.transform.localPosition = new Vector3 (0, -500, 0);
		}


		if (DoingCutscene) {

			if (Timer_SubSecond > 2 || Timer_Seconds > 0) {
				if (ClickPos.x != 100 && Super.Dataholder.PreviousVictory_CanSkipCutscenes) {
					Timer_Seconds = 99;
				}
			}
			Timer_SubSecond++;
			if (Timer_SubSecond >= 60) {
				Timer_SubSecond = 0;
				Timer_Seconds++;
			}

			if (CurrentVoiceLine == null) {
				CurrentVoiceLine = Instantiate (VoiceLinePrefab, transform.position, transform.rotation, transform);
				TempAS = CurrentVoiceLine.GetComponent<AudioSource> ();
				TempAS.clip = Cutscenamajig [WhichCutscene] [WhichSlide].AudioClip;
				TempAS.Play ();
			}
			if (TempAS.clip != Cutscenamajig [WhichCutscene] [WhichSlide].AudioClip) {
				Destroy (CurrentVoiceLine);
				CurrentVoiceLine = Instantiate (VoiceLinePrefab, transform.position, transform.rotation, transform);
				TempAS = CurrentVoiceLine.GetComponent<AudioSource> ();
				TempAS.clip = Cutscenamajig [WhichCutscene] [WhichSlide].AudioClip;
				TempAS.Play ();
			}

			if (Timer_Seconds + 1 > Cutscenamajig [WhichCutscene] [WhichSlide].Duration) {
				float subsec = (Timer_SubSecond + 0f) / 60f;
				if (Timer_Seconds + subsec > Cutscenamajig [WhichCutscene] [WhichSlide].Duration) {
					Timer_Seconds = 0;
					Timer_SubSecond = 0;
					if (Cutscenamajig [WhichCutscene] [WhichSlide].EndTimeline) {
						Timeline.CellData [Timeline.CurrentFrame + 1].EndOfTimeline = true;

						Timeline.LiveReactionButton.CurrentLiveReaction = Cutscenamajig [WhichCutscene] [WhichSlide].InternalMonologue;
						Timeline.LiveReactionButton.Active = true;
					} else {
						if (Cutscenamajig [WhichCutscene] [WhichSlide].NextMinigame != null) {

							Cutscenamajig [WhichCutscene] [WhichSlide].NextMinigame.SetActive (true);
							gameObject.SetActive (false);

						} else {
							WhichSlide++;
						}
					}
				}
			}
			if (!Timeline.CellData [Timeline.CurrentFrame + 1].EndOfTimeline) {
				Subtitles.text = Cutscenamajig [WhichCutscene] [WhichSlide].Subtitles;
			} else {
				Subtitles.text = "";
			}
			Chet.CurrentAnim = Cutscenamajig [WhichCutscene] [WhichSlide].ChetAnimation;
		} else {
			if (CurrentVoiceLine != null) {
				Destroy (CurrentVoiceLine);
			}
		}
		Chet.TimelineUpdate ();

	}


	public void Ping()
	{
		// oh dang, bro just shot Chet Quizzly!

		Fired = true;
		// Did we win or lose?
		if (Chet.CurrentAnim == 2) {
			// win
			WhichCutscene = 2;
			WhichSlide = 0;
			DoingCutscene = true;
			Timeline.PendingChetMode = true;
			Timer_Seconds = 0;
			Timer_SubSecond = 0;
		} else {
			//bruh
			WhichCutscene = 1;
			WhichSlide = 0;
			DoingCutscene = true;
			Timer_Seconds = 0;
			Timer_SubSecond = 0;
		}


	}

	public void PingNoAmmo()
	{
		// TODO: drop a voice line saying "Dang, out of ammo"

	}

	public List<byte> SaveState()
	{
		List<byte> State = new List<byte> ();
		State.Add ((byte)(Fired ? 1 : 0));
		State.Add ((byte)(DoingCutscene ? 1 : 0));
		State.Add (WhichCutscene);
		State.Add (WhichSlide);
		State.Add (Phase);
		State.Add (Step);
		State.Add (Chet.AnimTimer);
		State.Add (Chet.TieSpinAnimTimer);
		State.Add (Chet.CurrentAnim);	
		State.Add (Timer_SubSecond);
		State.Add (Timer_Seconds);
		return State;
	}

	public void LoadState(List<byte> State)
	{
		Fired = State [0] == 1;
		DoingCutscene = State [1] == 1;
		WhichCutscene = State [2];
		WhichSlide = State [3];
		Phase= State [4];
		Step= State [5];
		Chet.AnimTimer = State [6];
		Chet.TieSpinAnimTimer = State [7];
		Chet.CurrentAnim = State [8];
		Timer_SubSecond = State [9];
		Timer_Seconds = State [10];

		Chet.ForceAnim (Chet.CurrentAnim);
	}

}
