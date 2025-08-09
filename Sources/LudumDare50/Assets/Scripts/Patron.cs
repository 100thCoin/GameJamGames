using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Patron : MonoBehaviour {

	public GameObject CheckoutAlt;
	public SpriteRenderer SR;
	public SpriteRenderer SR_Alt;
	public SpriteRenderer SR_Rev;
	public SpriteRenderer ReviewBG;

	public MoveChar Char;

	public Sprite[] skins;
	public Sprite[] VHSSkins;

	public Text TM;
	public Text[] TMOutline;


	bool swapDir;
	float SwapDirTimer;

	public float speed;

	public bool FistCustomerOfTheDay;

	public bool PreEnterBuilding;
	public bool EnterBuilding;
	public bool WalkToMovies;
	public bool SearchForMovie; // timer
	public bool CanBeReady;
	public bool ReadyWhenYouAre;
	public bool WaitingForYou; // timer
	public bool AtCounter; // timer
	public bool HappyLeave;
	public bool SadgeLeave;

	public bool WinLevel;
	public bool LoseLevel;

	public float RandomArriveTimer;
	public float SearchTimer;
	public float WaitTimer;
	public float PlaceBoxTimer;
	public float HappyLeaveTimer;
	public float leaveAccel;


	public float WinTimer;

	public GameObject ScanBox;
	public SpriteRenderer BoxSR;

	public TextMesh PatronStatus;

	public GameObject BellChime;



	// Use this for initialization
	void Start () {
		AssignText("");



	}
	
	// Update is called once per frame
	void Update () {


		if(Char.Paused)
		{
			return;
		}




		if(transform.localPosition.x >= -4)
		{
			CheckoutAlt.transform.localScale =  Vector3.one * (-transform.localPosition.x*0.2f+3.2f);
			CheckoutAlt.transform.localPosition = Vector3.zero;
		}
		else
		{
			CheckoutAlt.transform.localScale =  Vector3.one * 4;
			CheckoutAlt.transform.localPosition = new Vector3(transform.localPosition.x*2 +8,0,0);
		}

		if(PreEnterBuilding)
		{
			AssignText("");

			RandomArriveTimer -= Time.deltaTime;

			if(RandomArriveTimer <0)
			{
				//arrive;

				if(!FistCustomerOfTheDay)
				{
					Instantiate(BellChime);
				}
				PreEnterBuilding = false;
				EnterBuilding = true;
				transform.position = new Vector3(-41,298,0);

				int r = Random.Range(0,skins.Length);

				SR.sprite = skins[r];
				SR_Alt.sprite = skins[r];
				SR_Rev.sprite = skins[r];
				ReviewBG.color = new Vector4(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),1);

				r = Random.Range(0,VHSSkins.Length);
				BoxSR.sprite = VHSSkins[r];

			}
		}

		if(EnterBuilding)
		{
			transform.position += new Vector3(Time.deltaTime*speed,0,0);
			SR.flipX = true;
			AssignText("Customer: Arriving");

			if(transform.localPosition.x >= 12)
			{
				EnterBuilding = false;
				transform.localPosition = new Vector3(12,-2,0);
				SearchForMovie = true;
				if(FistCustomerOfTheDay)
				{
					SearchTimer = 2;

					FistCustomerOfTheDay = false;
				}
				else
				{
					SearchTimer = 30;
				}
				CanBeReady = !Char.MainRoom;

				
			}

		}

		if(SearchForMovie)
		{
			SwapDirTimer -= Time.deltaTime;
			SearchTimer -= Time.deltaTime;

			AssignText("Customer: Searching (" + (Mathf.Round(SearchTimer*100f)/100f) +")");



			if(SwapDirTimer < 0)
			{
				swapDir = !swapDir;
				SwapDirTimer += Random.Range(0.25f,3);

			}
			SR.flipX = swapDir;

			if(SearchTimer < 0)
			{
				AtCounter = true;
				SearchForMovie = false;

			}

			if(CanBeReady)
			{
				ReadyWhenYouAre = Char.MainRoom;
			}


			if(ReadyWhenYouAre)
			{
				AssignText("Customer: Ready when you are");

				if(Char.AtCheckout)
				{

					AtCounter = true;
					SearchForMovie = false;
				}
			}


		}

		if(AtCounter)
		{
			AssignText("Customer: Walking to checkout");

			SR.flipX = false;
			transform.position += new Vector3(-Time.deltaTime*speed,0,0);
			if(transform.localPosition.x <= -4)
			{
				AtCounter = false;
				WaitingForYou = true;
				WaitTimer = 30;
				transform.localPosition = new Vector3(-4,-2,0);

			}
		}
		if(WaitingForYou)
		{
			WaitTimer -= Time.deltaTime;
			AssignText("Customer: Waiting (" + (Mathf.Round(WaitTimer*100f)/100f) +")");

			if(WaitTimer < 29.7f)
			{
				ScanBox.SetActive(true);
				// play SFX
			}

			if(WaitTimer < 0)
			{
				SadgeLeave = true;

			}

		}



		if(HappyLeave)
		{
			AssignText("Customer: Satisfied");

			HappyLeaveTimer+=Time.deltaTime;
			if(HappyLeaveTimer > 0.5f)
			{
				ScanBox.SetActive(false);
			}
			if(HappyLeaveTimer > 1)
			{
				leaveAccel += Time.deltaTime*3;
				transform.position -= new Vector3(leaveAccel*Time.deltaTime*5,0,0);
			}
			else
			{
				leaveAccel= 0;
			}

			if(HappyLeaveTimer > 3)
			{
				HappyLeave = false;

				if(Char.BoneSupports >= Char.SupportGoal)
				{

					WinLevel = true;
					WinTimer = 0;
				}
				else
				{
					if(Char.ClearedTutorial3)
					{
						if(Char.Level == 0)
						{

							RandomArriveTimer = Random.Range(30f + Char.FailedDays*10,60f+ Char.FailedDays*10);
						}
						else if(Char.Level == 1)
						{

							RandomArriveTimer = Random.Range(20f+ Char.FailedDays*10,50f+ Char.FailedDays*10);
						}
						else
						{

							RandomArriveTimer = Random.Range(10f+ Char.FailedDays*10,30f+ Char.FailedDays*10);
						}
					}
					else
					{
						RandomArriveTimer = 1000000;
					}
					PreEnterBuilding = true;

				}
			}
		}

		if(SadgeLeave)
		{
			AssignText("Customer: Displeased");

			HappyLeaveTimer+=Time.deltaTime;

			if(HappyLeaveTimer > 0.5f)
			{
				//ScanBox.SetActive(false);
			}
			if(HappyLeaveTimer > 1)
			{
				leaveAccel += Time.deltaTime*3;
				transform.position -= new Vector3(leaveAccel*Time.deltaTime*5,0,0);
			}
			else
			{
				leaveAccel= 0;
			}
			transform.position -= new Vector3(leaveAccel*Time.deltaTime*5,0,0);


			if(HappyLeaveTimer > 3)
			{
				if(!LoseLevel)
				{
					Char.GenerateBadReview();
				}

				LoseLevel = true;

			}

		}

		if(WinLevel)
		{
			AssignText("");

			WinTimer+= Time.deltaTime;

			if(WinTimer > 0.25f)
			{
				Char.DayComplete = true;

			}

		}

	}

	void AssignText(string s)
	{
		TM.text = s;
		int i = 0;
		while(i < TMOutline.Length)
		{
			TMOutline[i].text = s;
			i++;
		}
	}

}
