using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main
{
	public static DataHolder Data;
}

public class DataHolder : MonoBehaviour {

	public GameObject Playerr;
	public PlayerMovement PMov;
	public bool InformedOfPumpkinPlan;
	public GameObject PumpkinPlanDialogue;

	public GameObject CurrentVoiceLine;
	public GameObject QueuedVoiceLine;

	public bool KilledChicken;

	public bool EarlySteal;
	public GameObject VoiceKill;
	public GameObject VoiceEarlyKill;

	public int Day;
	public GameObject[] ObjectiveHolders;
	public bool[] DailyObjectives1;
	public bool[] DailyObjectives2;
	public bool[] DailyObjectives3;
	public bool[] DailyObjectives4;
	public bool[] DailyObjectives5;
	public bool[] DailyObjectives6;

	public GameObject[] DaySpecificNPCs;
	public GameObject CornLadyForDay6;

	public bool NeighborIsDead;
	public bool ToddIsDead; //I guess that's the other guy

	public bool Paused;
	public bool HouseOpen;
	public bool DayComplete;
	public BoxCollider HouseCol;
	public SpriteRenderer HouseR;
	public Sprite HouseSOpen;
	public Sprite HouseSClosed;
	public Sprite[] Items;
	public string[] ItemNames;
	public int[] ItemCounts;
	public int[] InvSlotItems;
	public SpriteRenderer[] InvSlotRenderers;
	public SpriteRenderer[] InvRenderers;
	public Sprite SlotSelected;
	public int PrevSlot;
	public Sprite SlotUnselected;
	public int Money;
	public PickupText[] HudItemCounts;

	public int CurrentInvSlot;
	public float ItemNameTimer;
	public TextMesh ItemName;

	public bool InvertMousewheel;

	public float[] invSlotSelTimers;

	public FarmPlot[] TheFarm;
	public FarmPlot SpecialOneForYourPumpkin;

	public PickupText MoneyIndicator;
	public int RemainingCornToSell = 400;

	public GameObject ToDoList;
	public GameObject PauseMenu;


	public float MusicVolume;
	public float VOVolume;
	public bool DisableSubtitles;

	public bool OBj_DigHole;
	public bool Obj_PlantSeed;
	public bool Obj_Water;
	public bool Obj_Eat;
	public bool Obj_EatChicken;

	public bool Obj_CheckCrops;
	public float ObjCheckCropTimer;
	public bool Obj_BuySeeds;
	public bool Obj_SellCorn;
	public bool Obj_BuyTicket;
	public bool Obj_BuySoil;
	public bool Obj_BuyPumpkinSeed;
	public bool Obj_PlantNewCrops;
	public bool obj_Stealcrop;
	public bool Obj_PlantNewSoil;

	public bool Obj_PlantPumpkin;
	public bool Obj_WaterPumpkin;
	public bool Obj_FertilizePumpkin;

	public bool Obj_HarvestPumpkin;

	public int remainingseedsforObj = 5;
	public int remainingseedsforWaterObj = 5;

	public float NeighborIsDeadTimer;

	public bool RevealSleep;

	public bool UpgradedFarm;

	public SpriteRenderer FarmPLot;
	public Sprite FarmMK2;

	public Transform FinalTodd;

	public bool PromptKillNeighbor;
	public bool PromptKillTodd;

	public int TotalCorn;
	public int PeopleKilled;
	public float SpeedrunTimer;
	public bool Win;

	void Awake()
	{
		Main.Data = this;
	}
	void OnEnabled()
	{
		Main.Data = this;
	}
	void OnEnable()
	{
		Main.Data = this;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!Win) {
			SpeedrunTimer += Time.deltaTime;
		}
		if (Day == 3) {
			if (NeighborIsDead) {
				NeighborIsDeadTimer += Time.deltaTime;
			}

		}

		if (Obj_CheckCrops) {
			ObjCheckCropTimer += Time.deltaTime;
		} else {
			ObjCheckCropTimer = 0;
		}

		int d1 = Day + 1;
		if (d1 == 1) {
			ObjectiveHolders [0].SetActive(true);
			if (DailyObjectives1 [0] && DailyObjectives1 [1] && DailyObjectives1 [2] && DailyObjectives1 [3]) {
				RevealSleep = true;
				HouseOpen = true;
				DayComplete = true;
				DailyObjectives1 [0] = false; // to prevent this happening every frame
			}
		}
		else if (d1 == 2) {

			//0bj 6 is sleep
			ObjectiveHolders [0].SetActive(false);
			ObjectiveHolders [1].SetActive(true);
			DaySpecificNPCs [1].SetActive(true);
			DaySpecificNPCs [0].SetActive(false);

			if (DailyObjectives2[0] && DailyObjectives2[1]  && DailyObjectives2[2] && DailyObjectives2[3] && DailyObjectives2[4] && DailyObjectives2[5] && DailyObjectives2[7] && DailyObjectives2[8] && DailyObjectives2[9] && DailyObjectives2[10]) {
				HouseOpen = true;
				DayComplete = true;
				DailyObjectives2 [0] = false; // to prevent this happening every frame
			}
		}
		else if (d1 == 3) {

			//0bj 3 is sleep
			ObjectiveHolders [0].SetActive(false);
			ObjectiveHolders [1].SetActive(false);
			ObjectiveHolders [2].SetActive(true);
			DaySpecificNPCs [2].SetActive(true);
			DaySpecificNPCs [1].SetActive(false);
			if (DailyObjectives3[0] && DailyObjectives3[1] && DailyObjectives3[2] && (!PromptKillNeighbor || (PromptKillNeighbor && DailyObjectives3[4]))) {
				HouseOpen = true;
				DayComplete = true;
				DailyObjectives3 [0] = false; // to prevent this happening every frame
			}
		}
		else if (d1 == 4) {

			ObjectiveHolders [0].SetActive(false);
			ObjectiveHolders [1].SetActive(false);
			ObjectiveHolders [2].SetActive(false);
			ObjectiveHolders [3].SetActive(true);
			DaySpecificNPCs [3].SetActive(true);
			DaySpecificNPCs [2].SetActive(false);
			DaySpecificNPCs [1].SetActive(false);
			DaySpecificNPCs [0].SetActive(false);
			if (DailyObjectives4[0] && DailyObjectives4[1] && DailyObjectives4[2] && DailyObjectives4[3]) {
				HouseOpen = true;
				DayComplete = true;
				DailyObjectives4 [0] = false; // to prevent this happening every frame
			}
		}
		else if (d1 == 5) {

			ObjectiveHolders [0].SetActive(false);
			ObjectiveHolders [1].SetActive(false);
			ObjectiveHolders [2].SetActive(false);
			ObjectiveHolders [3].SetActive(false);
			ObjectiveHolders [4].SetActive(true);
			DaySpecificNPCs [4].SetActive(true);
			DaySpecificNPCs [3].SetActive(false);
			DaySpecificNPCs [2].SetActive(false);
			DaySpecificNPCs [1].SetActive(false);
			DaySpecificNPCs [0].SetActive(false);

			if (DailyObjectives5[0] && DailyObjectives5[1] && DailyObjectives5[2] && DailyObjectives5[5]) {
				HouseOpen = true;
				DayComplete = true;
				DailyObjectives5 [0] = false; // to prevent this happening every frame
			}
		}
		else if (d1 == 6) {

			ObjectiveHolders [0].SetActive(false);
			ObjectiveHolders [1].SetActive(false);
			ObjectiveHolders [2].SetActive(false);
			ObjectiveHolders [3].SetActive(false);
			ObjectiveHolders [4].SetActive(true);
			ObjectiveHolders [5].SetActive(true);
			DaySpecificNPCs [5].SetActive(true);

			DaySpecificNPCs [4].SetActive(false);
			DaySpecificNPCs [3].SetActive(false);
			DaySpecificNPCs [2].SetActive(false);
			DaySpecificNPCs [1].SetActive(false);
			DaySpecificNPCs [0].SetActive(false);

		}

		MoneyIndicator.Text = "$" + Money;
		MoneyIndicator.Start ();

		if (ItemNameTimer > 0) {	
			ItemNameTimer -= Time.deltaTime*2;
			ItemName.color = new Vector4 (1, 1, 1, ItemNameTimer);
		}
		if (HouseOpen) {
			HouseR.sprite = HouseSOpen;
			HouseCol.enabled = false;
		} else {
			HouseR.sprite = HouseSClosed;
			HouseCol.enabled = true;
		}

		int i = 0;
		while (i < 7) {
			InvSlotRenderers [i].sprite = Items [InvSlotItems [i]];
			invSlotSelTimers[i] += Time.deltaTime * (CurrentInvSlot == i ? 1 : -1)*8;
			invSlotSelTimers [i] = Mathf.Clamp01 (invSlotSelTimers [i]);
			InvRenderers [i].transform.localPosition = new Vector3 (InvRenderers [i].transform.localPosition.x, -10.75f + SinLerp (0, 0.25f,invSlotSelTimers[i],1),10);
			if (ItemCounts [InvSlotItems [i]] > 1) {
				HudItemCounts [i].Text = "x" + ItemCounts [InvSlotItems [i]];
				HudItemCounts[i].Start ();
			} else {
				HudItemCounts [i].Text = "";
				HudItemCounts[i].Start ();

			}
			i++;
		}

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			CurrentInvSlot = 0;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			CurrentInvSlot = 1;
		}
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			CurrentInvSlot = 2;
		}
		if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			CurrentInvSlot = 3;
		}
		if(Input.GetKeyDown(KeyCode.Alpha5))
		{
			CurrentInvSlot = 4;
		}
		if(Input.GetKeyDown(KeyCode.Alpha6))
		{
			CurrentInvSlot = 5;
		}
		if(Input.GetKeyDown(KeyCode.Alpha7))
		{
			CurrentInvSlot = 6;
		}

		if (InvertMousewheel) {
			if (Input.GetAxis ("Mouse ScrollWheel") > 0f) { // forward
				CurrentInvSlot++;
				if (CurrentInvSlot >= 7) {
					CurrentInvSlot -= 7;
				}
			} else if (Input.GetAxis ("Mouse ScrollWheel") < 0f) { // backwards
				CurrentInvSlot--;
				if (CurrentInvSlot <= -1) {
					CurrentInvSlot += 7;
				}
			}
		} else {
			if (Input.GetAxis ("Mouse ScrollWheel") > 0f) { // forward
				CurrentInvSlot--;
				if (CurrentInvSlot <= -1) {
					CurrentInvSlot += 7;
				}
			} else if (Input.GetAxis ("Mouse ScrollWheel") < 0f) { // backwards

				CurrentInvSlot++;
				if (CurrentInvSlot >= 7) {
					CurrentInvSlot -= 7;
				}
			}
		}
		if (PrevSlot != CurrentInvSlot) {
			PrevSlot = CurrentInvSlot;
			ItemNameTimer = 3;
			int j = 0;
			while (j < 7) {
				InvRenderers [j].sprite = CurrentInvSlot == j ? SlotSelected : SlotUnselected;

				j++;
			}
			ItemName.text = ItemNames [InvSlotItems [CurrentInvSlot]];
		}

	}


	public void AddItem(int id)
	{

		int i = 0;
		while (i < 7) {
			if (InvSlotItems [i] == 0) {
				InvSlotItems [i] = id;
				if (CurrentInvSlot == i) {
					ItemName.text = ItemNames [InvSlotItems [CurrentInvSlot]];
					ItemNameTimer = 3;
				}
				break;
			}
			i++;
		}
	}

	public void RemoveItem(int id)
	{
		int i = 0;
		while (i < 7) {
			if (InvSlotItems [i] == id) {
				InvSlotItems [i] = 0;
				break;
			}
			i++;
		}
	}









	// Various smooth lerp functions
	public static float ParabolicLerp(float sPos, float dPos, float t, float dur)
	{
		return (((sPos-dPos)*Mathf.Pow(t,2))/Mathf.Pow(dur,2))-(2*(sPos-dPos)*(t))/(dur)+sPos;
	}
	public static float SinLerp(float sPos, float dPos, float t, float dur)
	{
		return Mathf.Sin((Mathf.PI*(t))/(2*dur))*(dPos-sPos) + sPos;
	}
	public static float TwoCurveLerp(float sPos, float dPos, float t, float dur)
	{
		return -Mathf.Cos(Mathf.PI*t*(1/dur))*0.5f*(dPos-sPos)+0.5f*(sPos+dPos);
	}

	// Converts a float in seconds to a string in MN:SC.DC format
	// example: 68.1234 becomes "1:08.12"
	public static string StringifyTime(float time)
	{
		string s = "";
		int min = 0;
		while(time >= 60){time-=60;min++;}
		time = Mathf.Round(time*100f)/100f;
		s = "" + time;
		if(!s.Contains(".")){s+=".00";}
		else{if(s.Length == s.IndexOf(".")+2){s+="0";}}
		if(s.IndexOf(".") == 1){s = "0" + s;}
		s = min + ":" + s;
		return s;
	}


}
