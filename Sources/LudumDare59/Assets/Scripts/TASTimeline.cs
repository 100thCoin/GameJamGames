using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TASSaveState
{

	public bool OpeningEnabled;
	public List<byte> OpeningData;	

	public bool MultiChoiceEnabled;
	public List<byte> MultiChoiceData;	


	public bool BasketballEnabled;
	public List<byte> BasketballData;	
	public bool MGC_Basketball_Fail_Enabled;
	public List<byte> MGC_Basketball_Fail_Data;
	public bool MGC_Basketball_Pass_Enabled;
	public List<byte> MGC_Basketball_Pass_Data;


	public bool TypeGameEnabled;
	public List<byte> TypeGameData;	
	public bool MGC_TypeGame_Fail_Enabled;
	public List<byte> MGC_TypeGame_Fail_Data;
	public bool MGC_TypeGame_Fail2_Enabled;
	public List<byte> MGC_TypeGame_Fail2_Data;
	public bool MGC_TypeGame_Pass_Enabled;
	public List<byte> MGC_TypeGame_Pass_Data;


	public bool Gamblinator1Enabled;
	public List<byte> Gamblinator1Data;	
	public bool MGC_Gamblinator1_Fail_Enabled;
	public List<byte> MGC_Gamblinator1_Fail_Data;
	public bool MGC_Gamblinator1_Pass_Enabled;
	public List<byte> MGC_Gamblinator1_Pass_Data;


	public bool Gamblinator2Enabled;
	public List<byte> Gamblinator2Data;	
	public bool MGC_Gamblinator2_Fail_Enabled;
	public List<byte> MGC_Gamblinator2_Fail_Data;
	public bool MGC_Gamblinator2_Pass_Enabled;
	public List<byte> MGC_Gamblinator2_Pass_Data;

	public bool DuckHuntEnabled;
	public List<byte> DuckHuntData;	

}

[System.Serializable]
public class CellContents
{
	public TASSaveState SaveState;
	public bool Click;
	public Vector2 ClickPos;

	public bool NotYetEmulated;
	public bool EndOfTimeline;
	public bool Null;

	public bool InitialFrame;

	public CellContents()
	{
		NotYetEmulated = true;
	}
}

public class TASTimeline : MonoBehaviour {

	public Camera Cam;

	public SpriteRenderer DeadTimelineFade;
	public float DeadTimelineTimer;

	public bool Play;
	public bool Seek;
	public bool ChetLocked;
	public int SeekUntil;

	public GameObject CellPrefab;
	public GameObject[] Cells;
	public TASTimelineCell[] CellCells;

	public GameObject PhantomCursor;

	public List<CellContents> CellData;
	public CellContents NullCellData;

	public int Top;
	public int CurrentFrame;
	public int CurrentCell;

	public int FrameCount;
	public int LastEmulatedFrame;

	public GameObject SelectedCell;

	public Vector2 RealMousePos;
	public Vector2 MousePosMod;

	public GameObject Scrollbar;
	public LiveReactionButton LiveReactionButton;

	public GameObject OpeningCutscene;

	public OpeningCutsceneManager OpeningMan;

	public MultiChoiceManager MultiChoiceMan;

	public BasketballMain BasketballManager;
	public MidGameCutscenes PostBasketball_Pass;
	public MidGameCutscenes PostBasketball_Fail;

	public TypeGameManager TypeGameManager;
	public MidGameCutscenes PostTypeGame_Pass;
	public MidGameCutscenes PostTypeGame_Fail;
	public MidGameCutscenes PostTypeGame_Fail2;

	public GamblinatorGame Gamblinator1;
	public MidGameCutscenes PostGamblinator1_Pass;
	public MidGameCutscenes PostGamblinator1_Fail;

	public GamblinatorGame Gamblinator2;
	public MidGameCutscenes PostGamblinator2_Pass;
	public MidGameCutscenes PostGamblinator2_Fail;

	public DuckHuntGame DuckHuntManager;

	public SpriteRenderer LockedTimeline;
	public float LockedTimelineTimer;

	public bool CompletedTutorial;
	public bool TutorialClick;
	public bool SkipDialogueThisOneTime;
	public bool SkipDialogueThisOneOtherTime;

	Vector2 DummyVector2 = new Vector2(100,100);

	public bool InitialFrame = true;

	// Use this for initialization
	void Start () {
		Cells = new GameObject[22];
		CellCells = new TASTimelineCell[22];
		for (int i = 0; i < 22; i++) {
			Cells [i] = Instantiate (CellPrefab, transform.position + new Vector3 (-0.5f, 13.5f - i, 0), transform.rotation, transform);
			CellCells [i] = Cells [i].GetComponent<TASTimelineCell> ();
			CellCells [i].Timeline = this;
			CellCells [i].ID = i;
		}
	}
	public float ScrollbarSize;
	public float ScrollbarPosition;
	public float ScrollbarTemp;
	public TasTimelineScrollbar ScrollbarClass;

	public bool NoFrameAdvance_Monologue;
	public bool NoFrameAdvance_Endgame;
	public bool FrameAdvance;
	public bool FrameRewind;
	public bool BigRewind;
	public bool BigAdvance;

	public bool PendingChetMode;
	public bool LockForTutorial;

	public int Rerecords;

	// Update is called once per frame
	void Update () {

		LockForTutorial = (!CompletedTutorial && (LiveReactionButton.TutorialPage == 5 || LiveReactionButton.TutorialPage == 4));
		bool otherLocksForTut = (!CompletedTutorial && (LiveReactionButton.TutorialPage == 5));

		if (CellData.Count > CurrentFrame && CellData [CurrentFrame].EndOfTimeline) {

			DeadTimelineTimer += Time.deltaTime;
			if (DeadTimelineTimer > 0.75f) {
				DeadTimelineTimer = 0.75f;
			}
			DeadTimelineFade.color = new Vector4 (1, 1, 1, DeadTimelineTimer);
			DeadTimelineFade.gameObject.SetActive (true);

		} else {
			DeadTimelineTimer -= Time.deltaTime*5;
			if (DeadTimelineTimer < 0) {
				DeadTimelineTimer = 0;
			}
			DeadTimelineFade.color = new Vector4 (1, 1, 1, DeadTimelineTimer);
		}
		if (!otherLocksForTut) {
			if (Global.Dataholder.ShowTimeline && !Global.Dataholder.paused) {
				if (!NoFrameAdvance_Monologue && !NoFrameAdvance_Endgame) {
					if (!LockForTutorial) {
						if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)) {
							FrameRewind = true;
						}
						if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)) {
							FrameAdvance = true;
						}
					}
					if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) {
						BigRewind = true;
					}
					if (!LockForTutorial) {
						if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)) {
							BigAdvance = true;
						}
					}
				}
			}
		}
		SelectedCell.transform.position = transform.position + new Vector3(0,20,0);
		for (int i = 0; i < Cells.Length; i++) {
			CellCells [i].UpdateGraphics ();
		}
		CurrentCell = -1;
		if (CurrentFrame >= Top && CurrentFrame < Top + 22) {
			CurrentCell = CurrentFrame - Top;
			SelectedCell.transform.position = Cells[CurrentCell].transform.position;
		}


		if (!LockForTutorial) {
			if (Input.GetAxis ("Mouse ScrollWheel") > 0f) {
				Top--;
				if (Top < 0) {
					Top = 0;
				}
			} else if (Input.GetAxis ("Mouse ScrollWheel") < 0f) {
				Top++;
				if (Top > FrameCount) {
					Top = FrameCount;
				}
			}
		}

		RealMousePos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		MousePosMod = new Vector2((RealMousePos.x / Screen.height)*20-10,(RealMousePos.y / Screen.height)*20-10);
		if (!Global.Dataholder.ShowTimeline) {
			MousePosMod = new Vector2 (MousePosMod.x - 5, MousePosMod.y);
		}

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			if (MousePosMod.x >= -10 && MousePosMod.x <= 10 && MousePosMod.y >= -10 && MousePosMod.y <= 10) {
				if (CurrentCell != -1 && CurrentFrame < FrameCount) {

					if (CompletedTutorial || MultiChoiceMan.AppearTimerFloat == 1) {
						CellData [CurrentFrame].Click = true;
						CellData [CurrentFrame].ClickPos = MousePosMod;
						if (CurrentFrame < LastEmulatedFrame) {
							MarkEverythingAfterThisAsStale (CurrentFrame);
						}
					}

					if (!CompletedTutorial && LiveReactionButton.TutorialPage == 5) {
						TutorialClick = true;
						CellData [CurrentFrame].Click = true;
						CellData [CurrentFrame].ClickPos = MousePosMod;
						if (CurrentFrame < LastEmulatedFrame) {
							MarkEverythingAfterThisAsStale (CurrentFrame);
						}
					}
					if (!CompletedTutorial && MultiChoiceMan.AppearTimerFloat != 1 && MultiChoiceMan.Submit) {
						SkipDialogueThisOneTime = true;
					}
					if (!CompletedTutorial && OpeningCutscene.activeInHierarchy) {
						SkipDialogueThisOneOtherTime = true;
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			if (MousePosMod.x >= -10 && MousePosMod.x <= 10 && MousePosMod.y >= -10 && MousePosMod.y <= 10) {
				if (CurrentCell != -1 && CurrentFrame < FrameCount) {
					CellData [CurrentFrame].Click = false;
					CellData [CurrentFrame].ClickPos = Vector2.zero;
					if (CurrentFrame < LastEmulatedFrame) {
						MarkEverythingAfterThisAsStale (CurrentFrame);
					}

				}
			}
		}

		if (Input.GetKeyDown (KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)) {
			if (CurrentCell != -1 && CurrentFrame < FrameCount) {
				CellData [CurrentFrame].Click = false;
				CellData [CurrentFrame].ClickPos = Vector2.zero;
				if (CurrentFrame < LastEmulatedFrame) {
					MarkEverythingAfterThisAsStale (CurrentFrame);
				}
			}
			
		}
		if (Global.Dataholder.ShowTimeline && !Global.Dataholder.paused && CompletedTutorial) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (!ChetLocked && !Global.Dataholder.paused && !Global.Dataholder.HotkeyPaused) {
					if (Play) {
						Play = false;
					} else if (!Seek) {
						Play = true;
					}
					if (Seek) {
						Seek = false;
					}

				}
			}
		}

		if (CellData.Count > 0 && CellData [CurrentFrame].Click && !Play && !Seek) {
			Vector3 PhantomPos = new Vector3 (((CellData [CurrentFrame].ClickPos.x + 10) / 20)*Screen.height, ((CellData [CurrentFrame].ClickPos.y + 10) / 20)*Screen.height,0);
			PhantomPos = Cam.ScreenToWorldPoint (PhantomPos);
			PhantomCursor.transform.position = new Vector3 (PhantomPos.x, PhantomPos.y, 0);

		} else {
			PhantomCursor.transform.position = transform.position + new Vector3 (0, 20, 0);
		}


		ScrollbarPosition = 0;
		ScrollbarSize = (22.0f/(FrameCount+22))*20;
		if (ScrollbarSize < 0.5f) {
			ScrollbarSize = 0.5f;
		}
		ScrollbarPosition = (Top + 0f) / (FrameCount);
		ScrollbarPosition *=2;
		ScrollbarPosition -= 1;
		Scrollbar.transform.localScale = new Vector3 (1, ScrollbarSize, 1);
		ScrollbarTemp = 20 - Scrollbar.transform.localScale.y;
		if (ScrollbarClass.Dragging && !LockForTutorial) {
			float NewScrollPos = Cam.ScreenToWorldPoint (new Vector3 (((MousePosMod.x + 10) / 20)*Screen.height, ((MousePosMod.y + 10) / 20)*Screen.height,0)).y;

			int Inverse = Mathf.FloorToInt(((((((-NewScrollPos) + 3) / 0.5f) / ScrollbarTemp)+1)/2) * FrameCount);
			if (Inverse < 0) {
				Inverse = 0;
			}
			if (Inverse > FrameCount) {
				Inverse = FrameCount;
			}
			Top = Inverse;
			ScrollbarPosition = (Top + 0f) / (FrameCount);
			ScrollbarPosition *=2;
			ScrollbarPosition -= 1;
			Scrollbar.transform.localPosition = new Vector3 (6.5f, 3 - (ScrollbarTemp * ScrollbarPosition) * 0.5f, 0);

		} else {
			if (float.IsNaN (ScrollbarPosition)) {
				ScrollbarPosition = 0;
			}
			Scrollbar.transform.localPosition = new Vector3 (6.5f, 3 - (ScrollbarTemp * ScrollbarPosition) * 0.5f, 0);
		}


		if (ChetLocked || PendingChetMode) {

			if (MousePosMod.x > 10) {
				LockedTimelineTimer += Time.deltaTime * 5;
				ChetLocked = true; // get pranked
			} else {
				LockedTimelineTimer -= Time.deltaTime*5;
			}
			LockedTimelineTimer = Mathf.Clamp01 (LockedTimelineTimer);
			LockedTimeline.color = new Vector4 (1, 1, 1, LockedTimelineTimer * 0.35f);

		}


	}

	void MarkEverythingAfterThisAsStale(int Frame)
	{
		int i = Frame;
		CellData [i].NotYetEmulated = true;
		i++;
		while (i < CellData.Count) {
			CellData [i].SaveState = null;
			CellData [i].NotYetEmulated = true;
			CellData [i].EndOfTimeline = false;
		
			i++;
		}
		Rerecords++;

		LastEmulatedFrame = Frame;
	}

	void FixedUpdate()
	{

		if (FrameAdvance || FrameRewind || BigRewind || BigAdvance) {
			if (!FrameAdvance && FrameRewind && !BigRewind && !BigAdvance && CurrentFrame >1) {
				LoadFrame(CurrentFrame-1);
				if (CurrentFrame < Top) {
					Top = CurrentFrame;
				}
			}
			if (FrameRewind) {
				FrameAdvance = false;
				BigRewind = false;
				BigAdvance = false;
			}
			if (!FrameAdvance && !FrameRewind && BigRewind && !BigAdvance && CurrentFrame >1) {
				int f = CurrentFrame-1;
				while(f > 1)
				{
					if (CellData [f].Click) {
						break;
					}
					f--;
				}
				LoadFrame(f);
				if (f < Top) {
					if (f < 8) {
						Top = f;
					} else {
						Top = f - 6;
					}

				}
					
			}
			if (!FrameAdvance && !FrameRewind && !BigRewind && BigAdvance) {
				int f = CurrentFrame+1;
				while(f < FrameCount)
				{
					if (CellData [f].Click) {
						break;
					}
					f++;
				}
				if (f < LastEmulatedFrame) {
					LoadFrame (f);
					if (f > Top+18) {
						Top = f-18;
					}
				} else {
					LoadFrame (LastEmulatedFrame);
					Seek = true;
					SeekUntil = f;
				}
			}
			FrameRewind = false;
			BigAdvance = false;
			BigRewind = false;
		}

		bool TimelineEnd = false;
		if (CellData.Count > CurrentFrame && CellData [CurrentFrame].EndOfTimeline) {
			TimelineEnd = true;
			Play = false;
			Seek = false;
			FrameAdvance = false;
		}
		if (!TimelineEnd && !Global.Dataholder.paused) {
			if (Play || Seek || FrameAdvance) {
				if (FrameAdvance) {
					FrameAdvance = false;
				}
				while (CurrentFrame + 2 >= CellData.Count) {
					CellData.Add (new CellContents ());
				}
				if (InitialFrame) {
					InitialFrame = false;
					CellData [0].SaveState = new TASSaveState ();
					CellData [0].InitialFrame = true;
				}
				RunFrame ();
				if (MultiChoiceMan.AppearTimerFloat != 1)
				{
					CurrentFrame++; // stall infinitely without adding new frames.
				}
				if (CurrentFrame >= SeekUntil) {
					Seek = false;
				}
				while (CurrentFrame >= FrameCount) {
					FrameCount++;
				}
				CellData [CurrentFrame - 1].NotYetEmulated = false;
				if (CurrentFrame > LastEmulatedFrame) {
					LastEmulatedFrame = CurrentFrame;
				}

				if (CurrentFrame > Top + 18) {
					Top = CurrentFrame - 18;
				}
			}
		}
	}

	public void RunFrame()
	{
		Vector2 PhantomPos = new Vector3 (((CellData [CurrentFrame].ClickPos.x + 10) / 20)*Screen.height, ((CellData [CurrentFrame].ClickPos.y + 10) / 20)*Screen.height);
		Vector3 CamScreenPos = Cam.ScreenToWorldPoint (new Vector3(PhantomPos.x,PhantomPos.y,0));
		PhantomPos = new Vector2 (CamScreenPos.x, CamScreenPos.y);

		CellData [CurrentFrame + 1].SaveState = new TASSaveState ();

		if (CellData [CurrentFrame].InitialFrame) {
			//OpeningCutscene.SetActive (true);
		}
		if (OpeningMan.gameObject.activeInHierarchy) {
			OpeningMan.TimelineUpdate ((CellData [CurrentFrame].Click || (SkipDialogueThisOneOtherTime && !CompletedTutorial)) ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.OpeningData != null) {
				CellData [CurrentFrame + 1].SaveState.OpeningData.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.OpeningData = OpeningMan.SaveState ();
			CellData [CurrentFrame + 1].SaveState.OpeningEnabled = true;
		}


		if (MultiChoiceMan.gameObject.activeInHierarchy) {
			MultiChoiceMan.TimelineUpdate ((CellData [CurrentFrame].Click || (SkipDialogueThisOneTime && !CompletedTutorial)) ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.MultiChoiceData != null) {
				CellData [CurrentFrame + 1].SaveState.MultiChoiceData.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.MultiChoiceData = MultiChoiceMan.SaveState ();
			CellData [CurrentFrame + 1].SaveState.MultiChoiceEnabled = true;
		}


		if (BasketballManager.gameObject.activeInHierarchy) {
			BasketballManager.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.BasketballData != null) {
				CellData [CurrentFrame + 1].SaveState.BasketballData.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.BasketballData = BasketballManager.SaveState ();
			CellData [CurrentFrame + 1].SaveState.BasketballEnabled = true;
		}
		if (PostBasketball_Fail.gameObject.activeInHierarchy) {
			PostBasketball_Fail.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.MGC_Basketball_Fail_Data != null) {
				CellData [CurrentFrame + 1].SaveState.MGC_Basketball_Fail_Data.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.MGC_Basketball_Fail_Data = PostBasketball_Fail.SaveState ();
			CellData [CurrentFrame + 1].SaveState.MGC_Basketball_Fail_Enabled = true;
		}
		if (PostBasketball_Pass.gameObject.activeInHierarchy) {
			PostBasketball_Pass.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.MGC_Basketball_Pass_Data != null) {
				CellData [CurrentFrame + 1].SaveState.MGC_Basketball_Pass_Data.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.MGC_Basketball_Pass_Data = PostBasketball_Pass.SaveState ();
			CellData [CurrentFrame + 1].SaveState.MGC_Basketball_Pass_Enabled = true;
		}

		if (TypeGameManager.gameObject.activeInHierarchy) {
			TypeGameManager.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.TypeGameData != null) {
				CellData [CurrentFrame + 1].SaveState.TypeGameData.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.TypeGameData = TypeGameManager.SaveState ();
			CellData [CurrentFrame + 1].SaveState.TypeGameEnabled = true;
		}
		if (PostTypeGame_Fail.gameObject.activeInHierarchy) {
			PostTypeGame_Fail.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Fail_Data != null) {
				CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Fail_Data.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Fail_Data = PostTypeGame_Fail.SaveState ();
			CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Fail_Enabled = true;
		}
		if (PostTypeGame_Fail2.gameObject.activeInHierarchy) {
			PostTypeGame_Fail2.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Fail2_Data != null) {
				CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Fail2_Data.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Fail2_Data = PostTypeGame_Fail2.SaveState ();
			CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Fail2_Enabled = true;
		}
		if (PostTypeGame_Pass.gameObject.activeInHierarchy) {
			PostTypeGame_Pass.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Pass_Data != null) {
				CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Pass_Data.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Pass_Data = PostTypeGame_Pass.SaveState ();
			CellData [CurrentFrame + 1].SaveState.MGC_TypeGame_Pass_Enabled = true;
		}

		if (Gamblinator1.gameObject.activeInHierarchy) {
			Gamblinator1.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.Gamblinator1Data != null) {
				CellData [CurrentFrame + 1].SaveState.Gamblinator1Data.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.Gamblinator1Data = Gamblinator1.SaveState ();
			CellData [CurrentFrame + 1].SaveState.Gamblinator1Enabled = true;
		}
		if (PostGamblinator1_Fail.gameObject.activeInHierarchy) {
			PostGamblinator1_Fail.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator1_Fail_Data != null) {
				CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator1_Fail_Data.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator1_Fail_Data = PostGamblinator1_Fail.SaveState ();
			CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator1_Fail_Enabled = true;
		}
		if (PostGamblinator1_Pass.gameObject.activeInHierarchy) {
			PostGamblinator1_Pass.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator1_Pass_Data != null) {
				CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator1_Pass_Data.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator1_Pass_Data = PostGamblinator1_Pass.SaveState ();
			CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator1_Pass_Enabled = true;
		}

		if (Gamblinator2.gameObject.activeInHierarchy) {
			Gamblinator2.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.Gamblinator2Data != null) {
				CellData [CurrentFrame + 1].SaveState.Gamblinator2Data.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.Gamblinator2Data = Gamblinator2.SaveState ();
			CellData [CurrentFrame + 1].SaveState.Gamblinator2Enabled = true;
		}
		if (PostGamblinator2_Fail.gameObject.activeInHierarchy) {
			PostGamblinator2_Fail.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator2_Fail_Data != null) {
				CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator2_Fail_Data.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator2_Fail_Data = PostGamblinator2_Fail.SaveState ();
			CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator2_Fail_Enabled = true;
		}
		if (PostGamblinator2_Pass.gameObject.activeInHierarchy) {
			PostGamblinator2_Pass.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator2_Pass_Data != null) {
				CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator2_Pass_Data.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator2_Pass_Data = PostGamblinator2_Pass.SaveState ();
			CellData [CurrentFrame + 1].SaveState.MGC_Gamblinator2_Pass_Enabled = true;
		}

		if (DuckHuntManager.gameObject.activeInHierarchy) {
			DuckHuntManager.TimelineUpdate (CellData [CurrentFrame].Click ? PhantomPos : DummyVector2);
			if (CellData [CurrentFrame + 1].SaveState.DuckHuntData != null) {
				CellData [CurrentFrame + 1].SaveState.DuckHuntData.Clear ();
			}
			CellData [CurrentFrame + 1].SaveState.DuckHuntData = DuckHuntManager.SaveState ();
			CellData [CurrentFrame + 1].SaveState.DuckHuntEnabled = true;
		}

	}

	public void LoadFrame(int Frame)
	{

		CurrentFrame = Frame;

		OpeningMan.gameObject.SetActive(CellData[CurrentFrame].SaveState.OpeningEnabled);
		if (CellData[CurrentFrame].SaveState.OpeningEnabled) {
			OpeningMan.LoadState (CellData [CurrentFrame].SaveState.OpeningData);
		}

		MultiChoiceMan.gameObject.SetActive(CellData[CurrentFrame].SaveState.MultiChoiceEnabled);
		if (CellData[CurrentFrame].SaveState.MultiChoiceEnabled) {
			MultiChoiceMan.LoadState (CellData [CurrentFrame].SaveState.MultiChoiceData);
		}


		BasketballManager.gameObject.SetActive(CellData[CurrentFrame].SaveState.BasketballEnabled);
		if (CellData[CurrentFrame].SaveState.BasketballEnabled) {
			BasketballManager.LoadState (CellData [CurrentFrame].SaveState.BasketballData);
		}
		PostBasketball_Fail.gameObject.SetActive(CellData[CurrentFrame].SaveState.MGC_Basketball_Fail_Enabled);
		if (CellData[CurrentFrame].SaveState.MGC_Basketball_Fail_Enabled) {
			PostBasketball_Fail.LoadState (CellData [CurrentFrame].SaveState.MGC_Basketball_Fail_Data);
		}
		PostBasketball_Pass.gameObject.SetActive(CellData[CurrentFrame].SaveState.MGC_Basketball_Pass_Enabled);
		if (CellData[CurrentFrame].SaveState.MGC_Basketball_Pass_Enabled) {
			PostBasketball_Pass.LoadState (CellData [CurrentFrame].SaveState.MGC_Basketball_Pass_Data);
		}

		TypeGameManager.gameObject.SetActive(CellData[CurrentFrame].SaveState.TypeGameEnabled);
		if (CellData[CurrentFrame].SaveState.TypeGameEnabled) {
			TypeGameManager.LoadState (CellData [CurrentFrame].SaveState.TypeGameData);
		}
		PostTypeGame_Fail.gameObject.SetActive(CellData[CurrentFrame].SaveState.MGC_TypeGame_Fail_Enabled);
		if (CellData[CurrentFrame].SaveState.MGC_TypeGame_Fail_Enabled) {
			PostTypeGame_Fail.LoadState (CellData [CurrentFrame].SaveState.MGC_TypeGame_Fail_Data);
		}
		PostTypeGame_Fail2.gameObject.SetActive(CellData[CurrentFrame].SaveState.MGC_TypeGame_Fail2_Enabled);
		if (CellData[CurrentFrame].SaveState.MGC_TypeGame_Fail2_Enabled) {
			PostTypeGame_Fail2.LoadState (CellData [CurrentFrame].SaveState.MGC_TypeGame_Fail2_Data);
		}
		PostTypeGame_Pass.gameObject.SetActive(CellData[CurrentFrame].SaveState.MGC_TypeGame_Pass_Enabled);
		if (CellData[CurrentFrame].SaveState.MGC_TypeGame_Pass_Enabled) {
			PostTypeGame_Pass.LoadState (CellData [CurrentFrame].SaveState.MGC_TypeGame_Pass_Data);
		}

		Gamblinator1.gameObject.SetActive(CellData[CurrentFrame].SaveState.Gamblinator1Enabled);
		if (CellData[CurrentFrame].SaveState.Gamblinator1Enabled) {
			Gamblinator1.LoadState (CellData [CurrentFrame].SaveState.Gamblinator1Data);
		}
		PostGamblinator1_Fail.gameObject.SetActive(CellData[CurrentFrame].SaveState.MGC_Gamblinator1_Fail_Enabled);
		if (CellData[CurrentFrame].SaveState.MGC_Gamblinator1_Fail_Enabled) {
			PostGamblinator1_Fail.LoadState (CellData [CurrentFrame].SaveState.MGC_Gamblinator1_Fail_Data);
		}
		PostGamblinator1_Pass.gameObject.SetActive(CellData[CurrentFrame].SaveState.MGC_Gamblinator1_Pass_Enabled);
		if (CellData[CurrentFrame].SaveState.MGC_Gamblinator1_Pass_Enabled) {
			PostGamblinator1_Pass.LoadState (CellData [CurrentFrame].SaveState.MGC_Gamblinator1_Pass_Data);
		}

		Gamblinator2.gameObject.SetActive(CellData[CurrentFrame].SaveState.Gamblinator2Enabled);
		if (CellData[CurrentFrame].SaveState.Gamblinator2Enabled) {
			Gamblinator2.LoadState (CellData [CurrentFrame].SaveState.Gamblinator2Data);
		}
		PostGamblinator2_Fail.gameObject.SetActive(CellData[CurrentFrame].SaveState.MGC_Gamblinator2_Fail_Enabled);
		if (CellData[CurrentFrame].SaveState.MGC_Gamblinator2_Fail_Enabled) {
			PostGamblinator2_Fail.LoadState (CellData [CurrentFrame].SaveState.MGC_Gamblinator2_Fail_Data);
		}
		PostGamblinator2_Pass.gameObject.SetActive(CellData[CurrentFrame].SaveState.MGC_Gamblinator2_Pass_Enabled);
		if (CellData[CurrentFrame].SaveState.MGC_Gamblinator2_Pass_Enabled) {
			PostGamblinator2_Pass.LoadState (CellData [CurrentFrame].SaveState.MGC_Gamblinator2_Pass_Data);
		}

		DuckHuntManager.gameObject.SetActive(CellData[CurrentFrame].SaveState.DuckHuntEnabled);
		if (CellData[CurrentFrame].SaveState.DuckHuntEnabled) {
			DuckHuntManager.LoadState (CellData [CurrentFrame].SaveState.DuckHuntData);
		}
	}

	public void ClickTimelineCell(int ID)
	{
		if (LockForTutorial) {
			return;
		}
		if (ChetLocked) {
			return;
		}
		int Target = ID + Top;
		if (Target == 0) {
			Target++;
		}
		if (Target < LastEmulatedFrame) {
			Seek = false;
			SeekUntil = -1;
			LoadFrame (ID + Top);
		} else {
			if (CurrentFrame < LastEmulatedFrame) {
				LoadFrame (LastEmulatedFrame);
			}
			SeekUntil = Target;
			Seek = true;
		}
	}



	public CellContents GetCellContentsFromCellID(int ID)
	{
		if (ID + Top >= CellData.Count) {
			return NullCellData;
		}
		return CellData[ID + Top];
	}

	public void RemoveAllClicksAfterThis(int Frame)
	{
		int i = Frame;
		while (i < CellData.Count) {
			CellData [i].SaveState = null;
			CellData [i].NotYetEmulated = true;
			CellData [i].EndOfTimeline = false;
			CellData [i].Click = false;
			i++;
		}
		Rerecords++;
		LastEmulatedFrame = Frame;
	}



	public TASSaveState GameInit;



	public TASTimeline Copy;
	[ContextMenu("Copy")]
	public void CopyResetStates()
	{
		GameInit.OpeningData = Copy.GameInit.OpeningData;

		GameInit.MultiChoiceData = Copy.GameInit.MultiChoiceData;

		GameInit.BasketballData = Copy.GameInit.BasketballData;

		GameInit.MGC_Basketball_Fail_Data = Copy.GameInit.MGC_Basketball_Fail_Data;

		GameInit.MGC_Basketball_Pass_Data = Copy.GameInit.MGC_Basketball_Pass_Data;

		GameInit.TypeGameData = Copy.GameInit.TypeGameData;

		GameInit.MGC_TypeGame_Fail_Data = Copy.GameInit.MGC_TypeGame_Fail_Data;

		GameInit.MGC_TypeGame_Fail2_Data = Copy.GameInit.MGC_TypeGame_Fail2_Data;

		GameInit.MGC_TypeGame_Pass_Data = Copy.GameInit.MGC_TypeGame_Pass_Data;

		GameInit.Gamblinator1Data = Copy.GameInit.Gamblinator1Data;

		GameInit.MGC_Gamblinator1_Fail_Data = Copy.GameInit.MGC_Gamblinator1_Fail_Data;

		GameInit.MGC_Gamblinator1_Pass_Data = Copy.GameInit.MGC_Gamblinator1_Pass_Data;

		GameInit.Gamblinator2Data = Copy.GameInit.Gamblinator2Data;

		GameInit.MGC_Gamblinator2_Fail_Data = Copy.GameInit.MGC_Gamblinator2_Fail_Data;

		GameInit.MGC_Gamblinator2_Pass_Data = Copy.GameInit.MGC_Gamblinator2_Pass_Data;

		GameInit.DuckHuntData = Copy.GameInit.DuckHuntData;
	}

	[ContextMenu("Init")]
	public void CreateResetStates()
	{
		OpeningMan.TimelineUpdate (DummyVector2);
		GameInit.OpeningData = OpeningMan.SaveState ();

		MultiChoiceMan.TimelineUpdate (DummyVector2);
		GameInit.MultiChoiceData = MultiChoiceMan.SaveState ();

		BasketballManager.TimelineUpdate (DummyVector2);
		GameInit.BasketballData = BasketballManager.SaveState ();

		PostBasketball_Fail.TimelineUpdate (DummyVector2);
		GameInit.MGC_Basketball_Fail_Data = PostBasketball_Fail.SaveState ();

		PostBasketball_Pass.TimelineUpdate (DummyVector2);
		GameInit.MGC_Basketball_Pass_Data = PostBasketball_Pass.SaveState ();

		TypeGameManager.TimelineUpdate (DummyVector2);
		GameInit.TypeGameData = TypeGameManager.SaveState ();

		PostTypeGame_Fail.TimelineUpdate (DummyVector2);
		GameInit.MGC_TypeGame_Fail_Data = PostTypeGame_Fail.SaveState ();

		PostTypeGame_Fail2.TimelineUpdate (DummyVector2);
		GameInit.MGC_TypeGame_Fail2_Data = PostTypeGame_Fail2.SaveState ();

		PostTypeGame_Pass.TimelineUpdate (DummyVector2);
		GameInit.MGC_TypeGame_Pass_Data = PostTypeGame_Pass.SaveState ();

		Gamblinator1.TimelineUpdate (DummyVector2);
		GameInit.Gamblinator1Data = Gamblinator1.SaveState ();

		PostGamblinator1_Fail.TimelineUpdate (DummyVector2);
		GameInit.MGC_Gamblinator1_Fail_Data = PostGamblinator1_Fail.SaveState ();

		PostGamblinator1_Pass.TimelineUpdate (DummyVector2);
		GameInit.MGC_Gamblinator1_Pass_Data = PostGamblinator1_Pass.SaveState ();

		Gamblinator2.TimelineUpdate (DummyVector2);
		GameInit.Gamblinator2Data = Gamblinator2.SaveState ();

		PostGamblinator2_Fail.TimelineUpdate (DummyVector2);
		GameInit.MGC_Gamblinator2_Fail_Data = PostGamblinator2_Fail.SaveState ();

		PostGamblinator2_Pass.TimelineUpdate (DummyVector2);
		GameInit.MGC_Gamblinator2_Pass_Data = PostGamblinator2_Pass.SaveState ();

		DuckHuntManager.TimelineUpdate (DummyVector2);
		GameInit.DuckHuntData = DuckHuntManager.SaveState ();


	}


}
