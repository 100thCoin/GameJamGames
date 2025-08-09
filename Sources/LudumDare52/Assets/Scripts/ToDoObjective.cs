using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToDoObjective : MonoBehaviour {

	public bool Complete;
	public bool Failed;
	public float FadeAwayTimer;

	public Text TM;

	public Image Checkbox;
	public Sprite Checked;
	public Sprite FailChecked;

	public int ObjectiveID;
	public bool Hidden;
	public float UnHideTimer;

	public int Day;

	public bool Red;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int d1 = Day+1; //make sense

		if (!Hidden && !(Complete || Failed)) {
			UnHideTimer += Time.deltaTime * 2;
			if (Red) {
				TM.color = new Vector4 (0.66f, 0, 0, UnHideTimer);
			} else {
				TM.color = new Vector4 (0, 0, 0, UnHideTimer);
			}
			Checkbox.color = new Vector4 (0, 0, 0, UnHideTimer);
		} else if (Hidden) {
			TM.color = new Vector4 (0, 0, 0, 0);
			Checkbox.color = new Vector4 (0, 0, 0, 0);
		}

		if(Complete || Failed)
		{
			if (d1 == 1) {Main.Data.DailyObjectives1 [ObjectiveID] = true;}
			if (d1 == 2) {Main.Data.DailyObjectives2 [ObjectiveID] = true;}
			if (d1 == 3) {Main.Data.DailyObjectives3 [ObjectiveID] = true;}
			if (d1 == 4) {Main.Data.DailyObjectives4 [ObjectiveID] = true;}
			if (d1 == 5) {Main.Data.DailyObjectives5 [ObjectiveID] = true;}
			if (d1 == 6) {Main.Data.DailyObjectives6 [ObjectiveID] = true;}

			Checkbox.sprite = Complete ? Checked : FailChecked;
			FadeAwayTimer += Time.deltaTime;

			if (FadeAwayTimer > 2) {
				TM.color = new Vector4 (0, 0, 0, 3 - FadeAwayTimer);
				Checkbox.color = new Vector4 (0, 0, 0,3 - FadeAwayTimer);

			}
			if (FadeAwayTimer > 3) {

				Destroy (gameObject);

			}
			return;
		}




		if (d1 == 1) {
			if (ObjectiveID == 0) {
				if (Main.Data.OBj_DigHole) {
					Main.Data.OBj_DigHole = false;
					Complete = true;
				}
			}
			else if (ObjectiveID == 1) {
				if (Main.Data.remainingseedsforObj <=0) {
					Complete = true;
				}
			}
			else if (ObjectiveID == 2) {
				if (Main.Data.remainingseedsforWaterObj <=0) {
					Complete = true;
				}
			}
			else if (ObjectiveID == 3) {
				if (Main.Data.Obj_Eat) {
					Complete = true;
				}
			}
			else if (ObjectiveID == 4) {
				if (Main.Data.RevealSleep) {
					Main.Data.RevealSleep = false;
					Hidden = false;
				}
			}
		}
		else if (d1 == 2) {
			if (ObjectiveID == 0 && UnHideTimer > 1) {
				if (Main.Data.Obj_CheckCrops) {
					Complete = true;
				}
			}
			if (ObjectiveID == 3) {
				if (Main.Data.Obj_BuySeeds) {
					Complete = true;
				}
			}
			if (ObjectiveID == 4) {
				if (Main.Data.Obj_PlantNewCrops) {
					Complete = true;
				}
			}
			if (ObjectiveID == 5) {
				if (Main.Data.Obj_Eat) {
					Complete = true;
				}
			}
			// 6 = sleep
			if (ObjectiveID == 7) {
				if (Main.Data.obj_Stealcrop) {
					Main.Data.obj_Stealcrop = false;
					Complete = true;
				}
			}
			if (ObjectiveID == 8) {
				if (Main.Data.Obj_SellCorn) {
					Main.Data.Obj_SellCorn = false;
					Complete = true;
				}
			}
			if (ObjectiveID == 9) {
				if (Main.Data.Obj_BuySoil) {
					Main.Data.Obj_BuySoil = false;
					Complete = true;
				}
			}
			if (ObjectiveID == 10) {
				if (Main.Data.Obj_PlantNewSoil) {
					Main.Data.Obj_PlantNewSoil = false;
					Complete = true;
				}
			}

			float FailDelay = 4;
			float ArriveDelay = 6;
			if (ObjectiveID == 1) {
				if (Main.Data.ObjCheckCropTimer > FailDelay) {
					Failed = true;
				}
			}
			if (ObjectiveID == 2) {
				if (Main.Data.ObjCheckCropTimer > FailDelay) {
					Failed = true;
				}
			}

			if (ObjectiveID == 7 || ObjectiveID == 8 || ObjectiveID == 9 || ObjectiveID == 10) {
				if (Main.Data.ObjCheckCropTimer > ArriveDelay) {
					Hidden = false;
				}
			}

		}

		if (d1 == 3) {

			if (ObjectiveID == 0 && UnHideTimer > 1) {
				if (Main.Data.Obj_CheckCrops) {
					Complete = true;
				}
			}
			float ArriveDelay = 3;
			if (ObjectiveID == 1 || ObjectiveID == 2 || ObjectiveID == 3) {
				if (Main.Data.ObjCheckCropTimer > ArriveDelay) {
					Hidden = false;
				}
			}

			if (ObjectiveID == 1) {
				if (Main.Data.Obj_BuyTicket) {
					Main.Data.Obj_BuyTicket = false;
					Complete = true;
				}
			}
			if (ObjectiveID == 2) {
				if (Main.Data.Obj_Eat) {
					Complete = true;
				}
			}

			if (ObjectiveID == 4) {
				if (Main.Data.PromptKillNeighbor) {
					Hidden = false;
				}
				if(Main.Data.NeighborIsDead)
				{
					Complete = true;
				}
			}

		}


		if (d1 == 4) {

			if (ObjectiveID == 0) {
				if (Main.Data.Obj_EatChicken) {
					Complete = true;
				}
			}
			float ArriveDelay = 3;
			if (ObjectiveID == 1) {
				if (Main.Data.PromptKillNeighbor) {
					Hidden = false;
				}
				if (Main.Data.NeighborIsDead) {
					Complete = true;
				}
			}
		
			if (ObjectiveID == 2 || ObjectiveID == 3 || ObjectiveID == 4) {
				if (Main.Data.NeighborIsDeadTimer > 4) {
					Hidden = false;
				}
			}

			if (ObjectiveID == 2) {
				if (Main.Data.Obj_PlantPumpkin) {
					Complete = true;
				}
			}
			if (ObjectiveID == 3) {
				if (Main.Data.Obj_WaterPumpkin) {
					Complete = true;
				}
			}

		}

		if (d1 == 5) {

			if (ObjectiveID == 0) {
				if (Main.Data.Obj_WaterPumpkin) {
					Complete = true;
				}
			}
				

			if (ObjectiveID == 1) {
				if (Main.Data.Obj_FertilizePumpkin) {
					Complete = true;
				}
			}
			if (ObjectiveID == 2 || ObjectiveID == 3) {
				if (Main.Data.Obj_Eat) {
					Complete = true;
				}
			}


			if (ObjectiveID == 5) {
				if (Main.Data.PromptKillTodd) {
					Hidden = false;
				}
				if (Main.Data.ToddIsDead) {
					Complete = true;
				}
			}
		}

		if (d1 == 6) {

			if (ObjectiveID == 0) {
				if (Main.Data.Obj_HarvestPumpkin) {
					Complete = true;
				}
			}


			if (ObjectiveID == 1) {
				if (Main.Data.Playerr.transform.position.y < -25) {
					Complete = true;
				}
			}
		
		}

	
	}


}
