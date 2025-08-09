using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmPlot : MonoBehaviour {

	public SpriteRenderer[] Tiles;

	public Sprite Seed;
	public Sprite SeedWatered;

	public bool PlayerLand;
	public int CropState;
	public bool MouseInside;

	public SpriteRenderer MouseOver;

	public GameObject Gibs;

	public bool PumpkinPlant;
	public Camera Cam;

	public float PumpkinPlantBad;

	public Sprite PumpkinStage1;
	public Sprite PumpkinStage1_waterred;
	public Sprite PumpkinStage2;

	public int PumpkinStage;
	public bool WateredPumpkin;
	public Collider Pump1;
	public Collider Pump2;
	public GameObject ItemDrop;
	public bool dropPumpkinOnce;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		MouseOver.enabled = false;

		if (PumpkinPlant && Main.Data.Day >= 3) {

			if (PumpkinStage == 2) {

				Tiles [1].enabled = true;
				Tiles [1].sprite = PumpkinStage2;
				Pump1.enabled = true;
				Pump2.enabled = true;
			}

			if (CropState == -1) {
				Vector3 Mousepos = Input.mousePosition;
				Vector3 Pos = Cam.ScreenToWorldPoint (Mousepos);
				transform.position = new Vector3 (Pos.x, Pos.y, 0);
				if (PumpkinPlantBad >= 0 && Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 1) {
					MouseOver.enabled = true;
					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						CropState = 0;
						Main.Data.OBj_DigHole = true;
						ChangeState ();
					}
				} else {
					MouseOver.enabled = false;
				}
			} else if (CropState == 0 && MouseInside) {

				if (Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 9) {
					MouseOver.enabled = true;
					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						CropState = 1;
						Main.Data.Obj_PlantPumpkin = true;
						Main.Data.RemoveItem (9);
						ChangeState ();
					}
				}

			}
			else if (CropState == 1 && MouseInside) {

				if (Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 3) {
					MouseOver.enabled = true;
					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						CropState = 2;
						Main.Data.Obj_WaterPumpkin = true;
						ChangeState ();
					}
				}
			
			}

			if (Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 11 && MouseInside) {
				MouseOver.enabled = true;
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					Main.Data.Obj_FertilizePumpkin = true;
					Main.Data.RemoveItem (11);
				}

			}

			MouseInside = false;

			return;
		}

		if (MouseInside) {
			if (CropState == -1) {
				if (Main.Data.InvSlotItems[Main.Data.CurrentInvSlot] == 1) {
					MouseOver.enabled = true;
					if (Input.GetKeyDown(KeyCode.Mouse0)) {
						CropState = 0;
						Main.Data.OBj_DigHole = true;
						ChangeState ();
					}

				}
			}
			if (CropState == 0) {
				if (Main.Data.InvSlotItems[Main.Data.CurrentInvSlot] == 2 && Main.Data.ItemCounts[2] >0) {
					MouseOver.enabled = true;
					if (Input.GetKeyDown(KeyCode.Mouse0)) {
						Main.Data.ItemCounts[2]--;
						Main.Data.remainingseedsforObj--;
						CropState = 1;
						ChangeState ();
						if (Main.Data.ItemCounts[2] <= 0) {
							Main.Data.RemoveItem (2);
						}
					}

				}
				if (Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 9) {
					MouseOver.enabled = true;
					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						if (!Main.Data.InformedOfPumpkinPlan) {
							Main.Data.InformedOfPumpkinPlan = true;

							if (Main.Data.CurrentVoiceLine == null) {
								Main.Data.CurrentVoiceLine = Instantiate (Main.Data.PumpkinPlanDialogue);
							} else {
								Main.Data.QueuedVoiceLine = Main.Data.PumpkinPlanDialogue;
							}
						}

					}
				}
			}
			if (CropState == 1) {
				if (Main.Data.InvSlotItems[Main.Data.CurrentInvSlot] == 3) {
					MouseOver.enabled = true;
					if (Input.GetKeyDown(KeyCode.Mouse0)) {
						CropState = 2;
						Main.Data.remainingseedsforWaterObj--;
						if (Main.Data.Day == 1) {
							Main.Data.Obj_PlantNewCrops = true;
						}
						ChangeState ();
						if (Main.Data.ItemCounts[2] <= 0) {
							Main.Data.RemoveItem (2);
						}
					}

				}
			}

		}
		MouseInside = false;

	}

	public void ChangeState()
	{

		Tiles[0].enabled = CropState == 0;
		Tiles[1].enabled = (CropState == 1 ||CropState ==  2);
		Tiles[2].enabled = CropState == 3; // dead


		if (CropState == 1) {
			Tiles [1].sprite = Seed;
		}
		if (CropState == 2) {
			Tiles [1].sprite = SeedWatered;
		}

		if (PumpkinPlant) {
			if (PumpkinStage == 1) {
				if (CropState == 1) {
					Tiles [1].sprite = PumpkinStage1;
				}
				if (CropState == 2) {
					Tiles [1].sprite = PumpkinStage1_waterred;
				}
			}

		}

	}

	void OnMouseOver () {

		MouseInside = true;
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Shovel" && CropState == 3) {
			Instantiate (Gibs, transform.position,transform.parent.rotation);
			CropState = 0;
			ChangeState ();
		}

		if(PumpkinPlant)
		{
			if (other.tag == "Shovel" && PumpkinStage == 2 &&!dropPumpkinOnce) 
				{
				dropPumpkinOnce = true;
				ItemDrop ID = Instantiate (ItemDrop, transform.position,transform.parent.rotation).GetComponent<ItemDrop>();
				ID.ItemId = 10;
				Main.Data.Obj_HarvestPumpkin = true;
				ID.SR.GetComponent<SpriteRenderer> ().sprite = Main.Data.Items [10];
				Destroy (gameObject);
				}


			}

	}

	void OnTriggerStay(Collider other)
	{
		if (!PumpkinPlant) {
			return;
		}
		if (other.tag == "GoodForPumpkin") {
			PumpkinPlantBad = 0.1f;
		}

	}

	void FixedUpdate()
	{
		if (PumpkinPlant) {
			PumpkinPlantBad -= Time.fixedDeltaTime;
		}

	}
}
