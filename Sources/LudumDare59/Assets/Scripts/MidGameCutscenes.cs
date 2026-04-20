using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MidGameScene
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

public class MidGameCutscenes : MonoBehaviour {

	public byte Scene;
	public byte Timer_SubSecond;
	public byte Timer_Seconds;
	public SubtitleOutline Subtitles;

	public ChetQuizzlyAnims Chet;


	public MidGameScene[] Scenes;

	public TASTimeline Timeline;

	public bool GoToTypeGame;
	public bool GoToGamble1;
	public bool GoToGamble2;
	public bool GoToDuck;

	public GameObject VoiceLinePrefab;
	public GameObject CurrentVoiceLine;
	public AudioSource TempAS;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void TimelineUpdate (Vector2 ClickPos) {
	
		Timer_SubSecond++;
		if (Timer_SubSecond >= 60) {
			Timer_SubSecond = 0;
			Timer_Seconds++;
		}

		if (ClickPos.x != 100 && Super.Dataholder.PreviousVictory_CanSkipCutscenes) {
			Timer_Seconds = 99;
		}

		if (CurrentVoiceLine == null) {
			CurrentVoiceLine = Instantiate (VoiceLinePrefab, transform.position, transform.rotation, transform);
			TempAS = CurrentVoiceLine.GetComponent<AudioSource> ();
			TempAS.clip = Scenes [Scene].AudioClip;
			TempAS.Play ();
		}
		if (TempAS.clip != Scenes [Scene].AudioClip) {
			Destroy (CurrentVoiceLine);
			CurrentVoiceLine = Instantiate (VoiceLinePrefab, transform.position, transform.rotation, transform);
			TempAS = CurrentVoiceLine.GetComponent<AudioSource> ();
			TempAS.clip = Scenes [Scene].AudioClip;
			TempAS.Play ();
		}

		if (Timer_Seconds + 1 > Scenes [Scene].Duration) {
			float subsec = (Timer_SubSecond + 0f) / 60f;
			if (Timer_Seconds + subsec > Scenes [Scene].Duration) {
				Timer_Seconds = 0;
				Timer_SubSecond = 0;
				if (Scenes [Scene].EndTimeline) {
					Timeline.CellData [Timeline.CurrentFrame + 1].EndOfTimeline = true;

					Timeline.LiveReactionButton.CurrentLiveReaction = Scenes [Scene].InternalMonologue;
					Timeline.LiveReactionButton.Active = true;
				} else {
					if (Scenes [Scene].NextMinigame != null) {

						if (GoToTypeGame) {
							Global.Dataholder.Timeline.TypeGameManager.LoadState (Global.Dataholder.Timeline.GameInit.TypeGameData);
						}
						if (GoToGamble1) {
							Global.Dataholder.Timeline.Gamblinator1.LoadState (Global.Dataholder.Timeline.GameInit.Gamblinator1Data);
						}
						if (GoToGamble2) {
							Global.Dataholder.Timeline.Gamblinator2.LoadState (Global.Dataholder.Timeline.GameInit.Gamblinator2Data);
						}
						if (GoToDuck) {
							Global.Dataholder.Timeline.DuckHuntManager.LoadState (Global.Dataholder.Timeline.GameInit.DuckHuntData);
						}
						Scenes [Scene].NextMinigame.SetActive (true);
						gameObject.SetActive (false);

					} else {
						Scene++;
					}
				}
			}
		}
		if (Timeline.CellData == null || Timeline.CellData.Count == 0) {
			return;
		}
		if (!Timeline.CellData [Timeline.CurrentFrame + 1].EndOfTimeline) {
			Subtitles.text = Scenes [Scene].Subtitles;
		} else {
			Subtitles.text = "";
		}
		Chet.CurrentAnim = Scenes [Scene].ChetAnimation;
		Chet.TimelineUpdate ();
	}

	public List<byte> SaveState()
	{
		List<byte> State = new List<byte> ();
		State.Add (Scene);
		State.Add (Timer_SubSecond);
		State.Add (Timer_Seconds);
		State.Add (Chet.AnimTimer);
		State.Add (Chet.TieSpinAnimTimer);
		State.Add (Chet.CurrentAnim);	
		return State;
	}

	public void LoadState(List<byte> State)
	{
		Scene = State [0];
		Timer_SubSecond = State [1];
		Timer_Seconds = State [2];
		Chet.AnimTimer = State [3];
		Chet.TieSpinAnimTimer = State [4];
		Chet.CurrentAnim = State [5];
		Chet.ForceAnim (Chet.CurrentAnim);



		Subtitles.text = Scenes [Scene].Subtitles;
		if (!Timeline.CellData [Timeline.CurrentFrame + 1].EndOfTimeline && !Timeline.CellData[Timeline.CurrentFrame].EndOfTimeline) {
			Subtitles.text = Scenes [Scene].Subtitles;
		} else {
			Subtitles.text = "";
		}
	}

}
