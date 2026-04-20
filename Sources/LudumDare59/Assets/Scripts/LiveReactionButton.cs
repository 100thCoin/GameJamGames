using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LiveReactions
{
	public string Name; // for editor convenience
	public Sprite[] Face;
	[TextArea(5, 10)]
	public string[] Subtitles;
	public AudioClip[] Clip;

	public int Seen;
}

public class LiveReactionButton : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite NoHover;
	public Sprite Hover;
	public Sprite ChetMode;

	public bool Active;
	public float ActiveTimer;

	public GameObject Cool;
	public SpriteRenderer CoolFace;
	public GameObject Banner1;
	public GameObject Banner2;

	public LiveReactions Tutorial;
	public bool DoingTutorial;
	public int TutorialPage;

	public LiveReactions CurrentLiveReaction;

	public SubtitleOutline Subtitles;

	public TASTimeline Timeline;

	public bool IncOnce;

	public GameObject VoiceLinePrefab;
	public GameObject CurrentVoiceLine;
	public AudioSource TempAS;

	public int hoverGrace;
	void OnMouseOver()
	{
		hoverGrace = 2;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Timeline.ChetLocked) {
			SR.sprite = ChetMode;
			return;
		}

		if (!Active) {
			if (CurrentVoiceLine != null) {
				Destroy (CurrentVoiceLine);
			}
			if (hoverGrace > 0) {
				SR.sprite = Hover;

				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					Timeline.Play = false;
					Timeline.Seek = false;
					Active = true;
				}

			} else {
				SR.sprite = NoHover;

			}
			hoverGrace--;
			ActiveTimer -= Time.deltaTime*2;
			if (ActiveTimer < 0) {
				ActiveTimer = 0;
			}
			Subtitles.text = "";
			if (IncOnce) {
				IncOnce = false;
				CurrentLiveReaction.Seen++;
				if (CurrentLiveReaction.Seen >= CurrentLiveReaction.Subtitles.Length) {
					CurrentLiveReaction.Seen = CurrentLiveReaction.Subtitles.Length - 1;
				}
			}
		} else {
			if (!DoingTutorial) {
				//active
				SR.sprite = Hover;

				if (CurrentVoiceLine == null) {
					CurrentVoiceLine = Instantiate (VoiceLinePrefab, transform.position, transform.rotation, transform);
					TempAS = CurrentVoiceLine.GetComponent<AudioSource> ();
					TempAS.clip = CurrentLiveReaction.Clip [CurrentLiveReaction.Seen];
					TempAS.Play ();
				}

				ActiveTimer += Time.deltaTime * 2;
				if (ActiveTimer > 1) {
					ActiveTimer = 1;
				}

				if (Input.GetKeyDown (KeyCode.Mouse0)) {

					Active = false;
				}
				Subtitles.text = CurrentLiveReaction.Subtitles [CurrentLiveReaction.Seen];
				CoolFace.sprite = CurrentLiveReaction.Face [CurrentLiveReaction.Seen];
				IncOnce = true;
			} else {
				// TUTORIAL
				if (TutorialPage != 5) {
					ActiveTimer += Time.deltaTime * 2;
					if (ActiveTimer > 1) {
						ActiveTimer = 1;
					}
				} else {
					ActiveTimer -= Time.deltaTime * 2;
					if (ActiveTimer < 0) {
						ActiveTimer = 0;
					}
				}

				if (CurrentVoiceLine == null) {
					CurrentVoiceLine = Instantiate (VoiceLinePrefab, transform.position, transform.rotation, transform);
					TempAS = CurrentVoiceLine.GetComponent<AudioSource> ();
					TempAS.clip = Tutorial.Clip [TutorialPage];
					TempAS.Play ();
				}

				if (TempAS.clip != Tutorial.Clip [TutorialPage]) {
					Destroy (CurrentVoiceLine);
					CurrentVoiceLine = Instantiate (VoiceLinePrefab, transform.position, transform.rotation, transform);
					TempAS = CurrentVoiceLine.GetComponent<AudioSource> ();
					TempAS.clip = Tutorial.Clip [TutorialPage];
					TempAS.Play ();
				}

				Subtitles.text = Tutorial.Subtitles [TutorialPage];
				CoolFace.sprite = Tutorial.Face [TutorialPage];

				if (TutorialPage == 0) {

					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						TutorialPage++;
					}
				}
				else if (TutorialPage == 1) {

					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						TutorialPage++;
					}
				}
				else if (TutorialPage == 2) {

					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						TutorialPage++;
					}
				}
				else if (TutorialPage == 3) {

					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						TutorialPage++;
						Global.Dataholder.ShowTimeline = true;
					}
				}
				else if (TutorialPage == 4) {
					if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
						TutorialPage++;
					}
				}
				else if (TutorialPage == 5) {
					if (Timeline.TutorialClick) {
						TutorialPage++;
					}
				}
				else if (TutorialPage == 6) {
					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						TutorialPage++;
					}
				}
				else if (TutorialPage == 7) {
					if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
						DoingTutorial = false;
						Timeline.CompletedTutorial = true;
						Active = false;

						if (Timeline.MultiChoiceMan.PressedGreat) {
							CurrentLiveReaction = Timeline.MultiChoiceMan.Good;
						} else {
							CurrentLiveReaction = Timeline.MultiChoiceMan.Great;
						}

					}
				}
			}
		}

		Cool.transform.localPosition = new Vector3 (DataHolder.ParabolicLerp(20,-24,ActiveTimer,1), -7.7f, 0);
		Banner1.transform.localPosition = new Vector3 (-96,DataHolder.ParabolicLerp(19,13,ActiveTimer,1), 0);
		Banner2.transform.localPosition = new Vector3 (-96,DataHolder.ParabolicLerp(-19,-13,ActiveTimer,1), 0);
	}
}
