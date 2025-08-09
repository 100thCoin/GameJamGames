using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Button : MonoBehaviour {

	public Vector2 Bounds;
	public SpriteRenderer SR;
	public Sprite Off;
	public Sprite Hover;
	public Sprite On;
	public Sprite InUse;

	public int GroupIndex;
	public int AnimalIndex;

	public bool Small;
	public HUD_Button SisterButton;
	public bool Small_Harmony;

	public HUD_Button ChildHarmonyButton;
	public HUD_Button ParentButton;

	public bool LevelClearHud;

	//CurrentHUDButtonsInUse

	public bool Selected;
	public bool AlreadyPlaced;

	public MakingChangesWillDropTheOrb MSG;


	public bool SpecialCaseForLevel1;
	public GameObject SpecialCaseHolder;
	// Use this for initialization
	void Start () {
		
	}
	public bool lTEST;
	
	// Update is called once per frame
	void Update () {

		if (SpecialCaseForLevel1) {
			if (Global.Dataholder.LevelIndex == 0) {
				SpecialCaseHolder.SetActive (false);
			} else {
				SpecialCaseHolder.SetActive (true);
			}
		}

		lTEST = false;
		//calculate mouse position

		Vector3 MousePos =  (Global.Dataholder.ScreenCam.ScreenToWorldPoint(Input.mousePosition) - Global.Dataholder.ScreenCam.transform.position)*2 + Global.Dataholder.RenderTexCam.transform.position;
		MousePos = new Vector3 (MousePos.x, MousePos.y, 10);

		AlreadyPlaced = false;



		if (!Small && !LevelClearHud) {
			Selected = this == Global.Dataholder.CurrentHUDButtonSelection;
			if (AnimalIndex == 0) {
				AlreadyPlaced = Global.Dataholder.BirdsAreCube [GroupIndex];
			} else if (AnimalIndex == 1) {
				AlreadyPlaced = Global.Dataholder.FrogsAreCube [GroupIndex];
			} else if (AnimalIndex == 2) {
				AlreadyPlaced = Global.Dataholder.RabbitsAreCube [GroupIndex];
			}
		} else if (ParentButton == Global.Dataholder.CurrentHUDButtonSelection && !LevelClearHud) {
			Selected = Small_Harmony == Global.Dataholder.CurrentHudSelectionHarmony;
		} else {

			if (Small) {
				if (Small_Harmony) {
					if (ParentButton.AnimalIndex == 1) {
						Selected = Global.Dataholder.FrogHarmonyMode [GroupIndex];
					}
				} else {
					if (ParentButton.AnimalIndex == 1) {
						Selected = !Global.Dataholder.FrogHarmonyMode [GroupIndex];
					}
				}
			}

		}

		bool okay = false;
		if (LevelClearHud) {
			okay = Global.Dataholder.PMov.InsideClearStage && Global.Dataholder.PlayerIsCarryingGoal;

		} else {
			okay = Global.Dataholder.PMov.InsideStage;
		}

		if (okay && (MousePos.x > transform.position.x - Bounds.x / 2f && MousePos.x < transform.position.x + Bounds.x / 2f && MousePos.y > transform.position.y - Bounds.y / 2f && MousePos.y < transform.position.y + Bounds.y / 2f)) {
			SR.sprite = Hover;
			Global.Dataholder.MouseOnHUDGrace = 4;
			if (LevelClearHud) {
				MSG.Hoverride = 6;
			} else {
				if (Small) {
					int hoverride = ParentButton.AnimalIndex*2;
					if (Small_Harmony) {
						hoverride++;
					}
					MSG.Hoverride = hoverride;
				} else {

					int hoverride = AnimalIndex*2;
					if (ChildHarmonyButton.Selected) {
						hoverride++;
					}
					MSG.Hoverride = hoverride;
				}


			}



			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				if (!LevelClearHud) {

					if (Global.Dataholder.PlayerIsCarryingGoal) {

						Global.Dataholder.PlayerIsCarryingGoal = false;
						Global.Dataholder.RespawnOrb ();
					}

					if (!Small) {
						Global.Dataholder.CurrentHUDButtonSelection = this;
						Global.Dataholder.CurrentHudSelectionHarmony = ChildHarmonyButton.Selected;
						Global.Dataholder.CurrentHudSelectionGroupIndex = GroupIndex;
						Global.Dataholder.CurrentHudSelectionAnimalType = AnimalIndex;
						if (AnimalIndex == 0) {
							Global.Dataholder.BirdsAreCube [GroupIndex] = false;
							int i = 0;
							while (i < 3) {
								if (Global.Dataholder.RabbitAttachedToBirds [i]) {
									Global.Dataholder.RabbitAttachedToBirds [i] = false;
									Global.Dataholder.RabbitsAreCube [i] = false;
								}
								i++;
							}
						} else if (AnimalIndex == 1) {
							Global.Dataholder.FrogsAreCube [GroupIndex] = false;
						} else if (AnimalIndex == 2) {
							Global.Dataholder.RabbitsAreCube [GroupIndex] = false;
						}
					} else {
						SisterButton.Selected = false;
						Selected = true;
						if (Global.Dataholder.CurrentHUDButtonSelection == ParentButton) {
							Global.Dataholder.CurrentHudSelectionHarmony = Small_Harmony;
						}
						if (ParentButton.AnimalIndex == 0) {
							Global.Dataholder.BirdHarmonyMode [ParentButton.GroupIndex] = Small_Harmony;
							int i = 0;
							while (i < 3) {
								if (Global.Dataholder.RabbitAttachedToBirds [i]) {
									Global.Dataholder.RabbitAttachedToBirds [i] = false;
									Global.Dataholder.RabbitsAreCube [i] = false;
								}
								i++;
							}
						} else if (ParentButton.AnimalIndex == 1) {
							if (Global.Dataholder.FrogHarmonyMode [ParentButton.GroupIndex] && !Small_Harmony) {
								if (Global.Dataholder.FrogsAreCube [ParentButton.GroupIndex]) {
									// frogs are already out, in harmony mode.
									Global.Dataholder.FrogsAreCube [ParentButton.GroupIndex] = false;
									Global.Dataholder.CurrentHUDButtonSelection = ParentButton;
									Global.Dataholder.CurrentHudSelectionHarmony = false;
									Global.Dataholder.CurrentHudSelectionGroupIndex = ParentButton.GroupIndex;
									Global.Dataholder.CurrentHudSelectionAnimalType = 1;

								}
							}
							Global.Dataholder.FrogHarmonyMode [ParentButton.GroupIndex] = Small_Harmony;

						} else if (ParentButton.AnimalIndex == 2) {
							Global.Dataholder.RabbitHarmonyMode [ParentButton.GroupIndex] = Small_Harmony;
							Global.Dataholder.RabbitsAreCube [ParentButton.GroupIndex] = false;
							Global.Dataholder.CurrentHUDButtonSelection = ParentButton;
							Global.Dataholder.CurrentHudSelectionAnimalType = 2;
							Global.Dataholder.CurrentHudSelectionGroupIndex = ParentButton.GroupIndex;
							Global.Dataholder.CurrentHudSelectionHarmony = Small_Harmony;


						}

					}

				} else {

					// Level clear!!!

					print ("LEVEL CLEAR!");
					Instantiate (Global.Dataholder.CircleBig, Global.Dataholder.PMov.transform.position + new Vector3 (0, 1, 0), transform.rotation);
					Instantiate (Global.Dataholder.LevelClearArrow, Global.Dataholder.PMov.transform.position + new Vector3 (0, -70, 0), transform.rotation,Global.Dataholder.CurrentLevel.transform);
					Global.Dataholder.PlayerIsCarryingGoal = false;
					Global.Dataholder.PMov.InsideClearStage = false;
					Global.Dataholder.PMov.BetweenLevels = true;
				}
			}


			if (Selected) {
				SR.sprite = On;
			}
		} else {
			if (Selected) {
				SR.sprite = On;
			} else {
				SR.sprite = Off;
			}
			if (AlreadyPlaced) {
				SR.sprite = InUse;
			}
		}



	}
}
