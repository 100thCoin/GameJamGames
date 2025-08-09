using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	public MoveCharacter P;

	public int CurrentTile;
	public GameObject[] Tiles;
	public int[] RemainingTiles;
	public Vector2[] TileDimensions;

	public Sprite[] TileCHR;

	public GhostCheckMain[] CheckTile;

	public bool DelayFrame;
	public SpriteRenderer Ghost;
	public Camera Cam;

	public Vector2 CurrentTileDimensions;

	public Transform ConstructionMap;

	public GameObject Nail;

	public bool Clicked;

	public GameObject CurrentSelection;
	public bool Grace;

	public DataHolder Main;

	public GameObject HammerItem;
	public GameObject Item;

	public int PlacementCount;

	public float HammerCountdown;
	public float ItemCountdown;

	public int Hammercount;

	// Use this for initialization
	void Start () {
		
	}

	void fixedUpdate()
	{
		CheckTile[0].gameObject.SetActive(false);
		CheckTile[1].gameObject.SetActive(false);
		CheckTile[2].gameObject.SetActive(false);
		CheckTile[3].gameObject.SetActive(false);
		CheckTile[4].gameObject.SetActive(false);
		CheckTile[5].gameObject.SetActive(false);

		CheckTile[CurrentTile].gameObject.SetActive(true);

	}
	
	// Update is called once per frame
	void LateUpdate () {

		if(HammerCountdown > 0)
		{
			HammerCountdown -= Time.deltaTime;
			if(HammerCountdown <=0)
			{
				Vector3 SpawnPoint = new Vector3(Random.Range(-5,8),-5+Hammercount,0);
				Instantiate(HammerItem,SpawnPoint,transform.rotation,ConstructionMap);
			}

		}

		if(ItemCountdown > 0)
		{
			ItemCountdown -= Time.deltaTime;
			if(ItemCountdown <=0)
			{
				Vector3 SpawnPoint = new Vector3(Random.Range(-5,8),-4+Hammercount,0);
				Instantiate(Item,SpawnPoint,transform.rotation,ConstructionMap);

			}

		}


		if(Input.GetKeyDown(KeyCode.Alpha1)){CurrentTile = 0; Grace=true;}
		else if(Input.GetKeyDown(KeyCode.Alpha2)){CurrentTile = 1;Grace=true;}
		else if(Input.GetKeyDown(KeyCode.Alpha3)){CurrentTile = 2;Grace=true;}
		else if(Input.GetKeyDown(KeyCode.Alpha4)){CurrentTile = 3;Grace=true;}
		else if(Input.GetKeyDown(KeyCode.Alpha5)){CurrentTile = 4;Grace=true;}
		else if(Input.GetKeyDown(KeyCode.Alpha6)){CurrentTile = 5;Grace=true;}




		CurrentSelection.transform.localPosition = new Vector3(CurrentTile,0,10);

		CheckTile[0].gameObject.SetActive(false);
		CheckTile[1].gameObject.SetActive(false);
		CheckTile[2].gameObject.SetActive(false);
		CheckTile[3].gameObject.SetActive(false);
		CheckTile[4].gameObject.SetActive(false);
		CheckTile[5].gameObject.SetActive(false);

		CheckTile[CurrentTile].gameObject.SetActive(true);

		if(!DelayFrame)
		{
			if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // forward
			{
				Grace=true;
				CurrentTile++;
				DelayFrame = true;
				if(CurrentTile >= Tiles.Length)
				{
					CurrentTile = 0;
				}
			}
			else if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // backwards
			{
				Grace=true;
				CurrentTile--;
				DelayFrame = true;
				if(CurrentTile <= -1)
				{
					CurrentTile = Tiles.Length-1;
				}
			}
		}
		else
		{
			DelayFrame = false;
		}

		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			Clicked = true;
		}

	}

	public void BuildConfirm()
	{
		if(Grace)
		{
			Grace = false;
			return;
		}
		if(!P.Indoors)
		{
			Ghost.sprite = TileCHR[CurrentTile];
			CurrentTileDimensions = TileDimensions[CurrentTile];
			Ghost.size = CurrentTileDimensions*0.5f;


			bool GoodToPlace = CheckTile[CurrentTile].MasterPass;
			Ghost.color = GoodToPlace ? new Vector4(0,1,0,0.5f) : new Vector4(1,0,0,0.5f);

			if(Ghost.transform.position.x > 12 || RemainingTiles[CurrentTile] <= 0)
			{
				GoodToPlace = false;
				Ghost.color = new Vector4(0,0,0,0);
			}

			float dist = (Ghost.transform.position - transform.position).magnitude;

			if(dist > 4)
			{
				GoodToPlace = false;
				Ghost.color = new Vector4(1,1,1,0.2f);
			}

			if(GoodToPlace)
			{
				if(Clicked)
				{
					
					if(RemainingTiles[CurrentTile] > 0)
					{
						Main.PlayerHasPlacedSomething = true;
						Instantiate(Tiles[CurrentTile],Ghost.transform.position,Ghost.transform.rotation,ConstructionMap);
						if(CheckTile[CurrentTile].RequiresScaffoldID)
						{
							Instantiate(Nail,CheckTile[CurrentTile].CurrentScaffold.transform.position + new Vector3(0,1.25f,0),transform.rotation,ConstructionMap);
						}
						RemainingTiles[CurrentTile]--;

						PlacementCount++;

						if(Ghost.transform.position.y > 0)
						{
							Main.AllowThreeFires = true;
						}

						if(PlacementCount % 5 == 1)
						{

							// spawn hammer
							HammerCountdown = 1.6f;
						}

						if(PlacementCount % 3 == 2)
						{

							// spawn item
							ItemCountdown = 2.6f;

						}



					}
				}

			}
			Clicked = false;

			Ghost.transform.position = Cam.ScreenToWorldPoint(Input.mousePosition);

			Vector2 OffsetOffset = new Vector2(CurrentTileDimensions.x % 2 == 0 ? 0 : 0.25f, CurrentTileDimensions.y % 2 == 0 ? 0 :0.25f);

			Ghost.transform.position = new Vector3(Ghost.transform.position.x+OffsetOffset.x,Ghost.transform.position.y+OffsetOffset.y,0);

			Ghost.transform.position = new Vector3(Mathf.Round((Ghost.transform.position.x)*2f)/2f - OffsetOffset.x,Mathf.Round((Ghost.transform.position.y)*2f)/2f - OffsetOffset.y,0);
			if(Ghost.transform.position.x > 12)
			{
				Ghost.color = new Vector4(0,0,0,0);
			}
		}
		else // P indoors
		{
			Ghost.transform.position = new Vector3(0,0,0);
		}
	}
}
