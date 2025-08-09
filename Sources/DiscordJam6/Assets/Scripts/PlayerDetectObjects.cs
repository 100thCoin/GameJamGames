using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PickUp{

	public string Name;
	public GameObject Splat;
	public GameObject PlacedVer;
	public float PlacementDisplacement;
	public Sprite PtoolSprite;
	public Material PtoolMat;
	public float SDRYOffset;
	public bool InsertAtCap;
}

public class PlayerDetectObjects : MonoBehaviour {

	public CamControl Cam;

	public NameObject CurrentDetection;

	public bool PickupMode;

	public SphereCollider Col;

	public List<PickUp> Pickups;

	Vector3 LPos;

	public Transform PlacementTool;
	public SpriteDepthLayer placementSDR;
	public SpriteRenderer PlacementRenderer;

	public Transform Map;

	public Dataholder Main;

	public bool DetectingPillar;
	public GameObject PillarText;
	public GameObject PillarTextNotReady;

	public SpriteRenderer Pillar;
	public GameObject BeginChallenge;

	public int TUTStep;

	public GameObject[] TUTTexts;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		TUTTexts[0].SetActive(false);
		TUTTexts[1].SetActive(false);
		TUTTexts[2].SetActive(false);
		if(TUTStep <3)
		{
			TUTTexts[TUTStep].SetActive(true);
		}


		Pillar.color = Color.white;
		DetectingPillar = false;
		PillarText.SetActive(false);
		PillarTextNotReady.SetActive(false);
		PickupMode = Cam.Distance > 31;
		if(Pickups.Count > 0 && !PickupMode)
		{
			PlacementRenderer.transform.localPosition = new Vector3(0,Pickups[0].PlacementDisplacement,0);
			placementSDR.YOffset = Pickups[0].SDRYOffset;
			PlacementRenderer.material = Pickups[0].PtoolMat;
			PlacementRenderer.sprite = Pickups[0].PtoolSprite;
		}
		else
		{
			PlacementRenderer.transform.localPosition = Vector3.zero;
			placementSDR.YOffset = 0;
			PlacementRenderer.material = null;
			PlacementRenderer.sprite = null;
		}

		Vector3 offset = new Vector3(0,0,0);
		if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			offset += new Vector3(-1,0,0);
		}
		else if(!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
		{
			offset += new Vector3(1,0,0);
		}

		if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
		{
			offset += new Vector3(0,-1,0);
		}
		else if(!Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
		{
			offset += new Vector3(0,1,0);
		}

		offset = offset.normalized * 2;
		if(offset.x != 0 || offset.y != 0)
		{
			LPos = offset;
		}
		else
		{
			offset = LPos;
		}

		if(PickupMode)
		{
			if(CurrentDetection != null)
			{
				CurrentDetection.SR.color = Color.white;
				CurrentDetection = null;
			}

			Col.center = offset;
		}
		else
		{
			PlacementTool.transform.localPosition = -offset*1.5f;

		}

	}

	void Update()
	{
		if(Main.Paused)
		{
			return;
		}
		
		if(!PickupMode)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				if(DetectingPillar && !Main.CantEtnerDragonRoomNow)
				{

					//start dragon fight

					Main.Char.WaitForShotOnDragon = true;
					Main.StartDragonFight = true;
					Instantiate(BeginChallenge,transform.position,transform.rotation,Map.transform);
					Main.MainMusic.pitch = 0;
					Main.MainMusic.volume = 0;

					Main.CantEtnerDragonRoomNow = true;
					Main.LoadTheDragon.Load = true;
					Main.LoadTheDragon.LoadTimer = 0;
				}
				else
				{
					if(TUTStep == 1)
					{
						TUTStep++;
					}

					Cam.LowersignDelay = 5;
					Cam.Sign.SetActive(true);
					if(Pickups.Count > 0)
					{
						//spawn the new object

						Instantiate(Pickups[0].PlacedVer,PlacementRenderer.transform.position,PlacementRenderer.transform.rotation,Map);
						Instantiate(Pickups[0].Splat,PlacementRenderer.transform.position,PlacementRenderer.transform.rotation,Map);
						if(Pickups[0].InsertAtCap)
						{
							Main._TownLore.CapNames.Add(Pickups[0].Name);
						}
						else
						{
							Main._TownLore.Names.Add(Pickups[0].Name);
						}
						Main._TownName.UpdateText();
						Pickups.RemoveAt(0);
					}
				}
			}
		}
		else if(CurrentDetection != null)
		{
			if(PickupMode)
			{
				if(Input.GetKeyDown(KeyCode.Space))
				{
					PickUp P = new PickUp();
					P.Name = CurrentDetection.ChosenName;
					P.Splat = CurrentDetection.SplatObj;
					P.PlacedVer = CurrentDetection.PlacedVer;
					P.PlacementDisplacement = -CurrentDetection.GetComponent<SpriteDepthLayer>().YOffset - 1;
					P.SDRYOffset = -P.PlacementDisplacement+1;
					P.PtoolMat = CurrentDetection.SR.material;
					P.PtoolSprite = CurrentDetection.SR.sprite;
					P.InsertAtCap = CurrentDetection.Cap;
					Pickups.Add(P);

					Instantiate(P.Splat,CurrentDetection.transform.position,CurrentDetection.transform.rotation,CurrentDetection.transform.parent);

					Destroy(CurrentDetection.gameObject);

					if(TUTStep == 0)
					{
						TUTStep++;
					}
					if(TUTStep == 2)
					{
						TUTStep++;
					}
				}
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(PickupMode)
		{
			if(other.tag == "Pick")
			{
				CurrentDetection = other.gameObject.GetComponent<NameObject>();
				CurrentDetection.SR.color = Color.red;
			}
		}

		if(!PickupMode)
		{
			if(other.tag == "Pillar")
			{

				bool ready = Main._TownName.FullTownName.Length > 90;

				PillarText.SetActive(ready);
				PillarTextNotReady.SetActive(!ready);
				DetectingPillar = ready;

				Pillar.color = Color.red;
			}
		}
	}


}
