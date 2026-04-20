using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCutsceneManager : MonoBehaviour {

	public SpriteRenderer Chet;
	public Transform BG;

	public byte Timer_Seconds;
	public byte Timer_SubSecond;

	public SpriteRenderer Spotlight1;
	public SpriteRenderer Spotlight2;

	public Sprite ChetWave;
	public GameObject Subtitle_Ladies;
	public GameObject Subtitle_Chet;
	public GameObject Subtitles_Quizzler;
	public GameObject BG2;

	public GameObject Minigame;

	public AudioSource Opening;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void TimelineUpdate (Vector2 ClickPos) {

		if (Timer_Seconds == 0 && Timer_SubSecond == 0) {
			Opening.Play ();
		}
		if (!Global.Dataholder.Timeline.Play) {
			Opening.Stop ();
		}

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
		float Timer = 0f + Timer_Seconds + (Timer_SubSecond / 60f);



		if (Timer > 4.8f) {
			Chet.sprite = ChetWave;
		}

		if (Timer > 5 && Timer < 6) {
			BG.transform.localPosition = new Vector3 (DataHolder.SinLerp (-24, 0, Timer - 5, 1), DataHolder.SinLerp (5, 0, Timer - 5, 1), 0);
			BG.localScale = new Vector3 (1, 1, 1) * DataHolder.SinLerp (1.5f, 0.8f, Timer - 5, 1);
			Spotlight1.color = new Vector4 (1, 1, 1, (11 - Timer * 2) * (162f / 256f));
			Spotlight2.color = new Vector4 (1, 1, 1, (11 - Timer * 2) * (162f / 256f));
		} else if (Timer >= 6){
			BG.transform.localPosition = new Vector3 (0, 0, 0);
			BG.localScale = new Vector3 (1, 1, 1) * 0.8f;
			Spotlight1.color = new Vector4 (1, 1, 1, 0);
			Spotlight2.color = new Vector4 (1, 1, 1, 0);

		}

		if (Timer < 2.5f) {
			Subtitle_Ladies.SetActive (true);
		} else {
			Subtitle_Ladies.SetActive (false);
		}

		if (Timer > 2.6f && Timer < 4.7f) {
			Subtitle_Chet.SetActive (true);
		} else {
			Subtitle_Chet.SetActive (false);
		}

		if (Timer > 5f && Timer < 8.5f) {
			Subtitles_Quizzler.SetActive (true);
		} else {
			Subtitles_Quizzler.SetActive (false);
		}

		if (Timer > 8.7f && Timer < 12f) {
			BG2.SetActive (true);
			BG.gameObject.SetActive (false);
		} else {
			BG2.SetActive (false);
			BG.gameObject.SetActive (true);
		}

		if (Timer >= 12) {
			Global.Dataholder.Timeline.OpeningMan.LoadState (Global.Dataholder.Timeline.GameInit.OpeningData);
			Minigame.SetActive (true);
			gameObject.SetActive (false);
		}

	}



	public List<byte> SaveState()
	{
		List<byte> State = new List<byte> ();
		State.Add (Timer_Seconds);
		State.Add (Timer_SubSecond);

		return State;
	}

	public void LoadState(List<byte> State)
	{
		Timer_Seconds = State [0];
		Timer_SubSecond = State [0];
		float Timer = 0f + Timer_Seconds + (Timer_SubSecond / 60f);

		if (Timer > 4.8f) {
			Chet.sprite = ChetWave;
		}

		if (Timer > 5 && Timer < 6) {
			BG.transform.localPosition = new Vector3 (DataHolder.SinLerp (-24, 0, Timer - 5, 1), DataHolder.SinLerp (5, 0, Timer - 5, 1), 0);
			BG.localScale = new Vector3 (1, 1, 1) * DataHolder.SinLerp (1.5f, 0.8f, Timer - 5, 1);
			Spotlight1.color = new Vector4 (1, 1, 1, (11 - Timer * 2) * (162f / 256f));
			Spotlight2.color = new Vector4 (1, 1, 1, (11 - Timer * 2) * (162f / 256f));
		} else if (Timer >= 6){
			BG.transform.localPosition = new Vector3 (0, 0, 0);
			BG.localScale = new Vector3 (1, 1, 1) * 0.8f;
			Spotlight1.color = new Vector4 (1, 1, 1, 0);
			Spotlight2.color = new Vector4 (1, 1, 1, 0);

		}

		if (Timer < 2.5f) {
			Subtitle_Ladies.SetActive (true);
		} else {
			Subtitle_Ladies.SetActive (false);
		}

		if (Timer > 2.6f && Timer < 4.7f) {
			Subtitle_Chet.SetActive (true);
		} else {
			Subtitle_Chet.SetActive (false);
		}

		if (Timer > 5f && Timer < 8.5f) {
			Subtitles_Quizzler.SetActive (true);
		} else {
			Subtitles_Quizzler.SetActive (false);
		}

		if (Timer > 8.7f && Timer < 12f) {
			BG2.SetActive (true);
			BG.gameObject.SetActive (false);
		} else {
			BG2.SetActive (false);
			BG.gameObject.SetActive (true);
		}

		if (Timer >= 12) {
			Minigame.SetActive (true);
			gameObject.SetActive (false);
		}

	}

}
