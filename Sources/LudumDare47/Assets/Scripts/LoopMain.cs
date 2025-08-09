using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMain : MonoBehaviour {

	public QuadStrip[] TheLoop;
	public GameObject[] TheLoopConnector;
	public GameObject Quad;
	public QuadStrip[] OriginalLoop;

	public GameObject Level2_InsideSeam;

	public GameObject[] OriginalConnector;
	QuadStrip First;


	public Char_Movement Char;

	public float Radius = 81.48733086f/5.859375f;
	public float Length = 512/5.859375f;

	public Vector3 LookAt;

	public bool CreateLoop;
	public int DestinedLoopQuadCount;

	public GameObject Empty;

	public bool SliceItLeft;
	public bool SliceItRight;

	public int SliceAt;

	public string tempName;
	public string tempName2;

	public bool Snaking;

	public float StripLength;

	public GameObject SnakePivot;

	public float VerticalSpeed;
	public float Velocity;

	public bool CountDown;

	public int flip = 1;

	public Vector3 TempPos;
	public float TempRot;

	public bool CreateLevel2;
	public Camera_Movement CamMov;
	public Camera Cam1;
	public Camera Cam2;

	public GameObject QuadLvl2;

	public GameObject Level2Pivot;

	public bool Redirect;
	public Transform Repos;
	public Transform ReFocus;

	public bool TestRedirect;

	public GameObject CleanLoop2;

	public bool MegaDebugDumbWhat;

	public DataHolder Main;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if(Snaking)
		{

			if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Char.JamUntilSnakeRight)
			{

				if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
				{
					VerticalSpeed -= Time.fixedDeltaTime * Velocity;
					Main.HaltTip3 = true;

				}
				else if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
				{
					VerticalSpeed += Time.fixedDeltaTime * Velocity;
					Main.HaltTip3 = true;

				}
				else
				{
					VerticalSpeed *= 0.8f;
				}

				VerticalSpeed = Mathf.Abs(VerticalSpeed) > 1 ? 1 * new Vector2(VerticalSpeed,0).normalized.x : VerticalSpeed;

				float XDist = new Vector2(1,VerticalSpeed*0.075f).normalized.x;
				float YDist = new Vector2(1,VerticalSpeed*0.075f).normalized.y;

				if(Redirect)
				{
					XDist = new Vector2(Repos.position.x-ReFocus.position.x,Repos.position.z-ReFocus.position.z).normalized.x;
					YDist = new Vector2(Repos.position.x-ReFocus.position.x,Repos.position.z-ReFocus.position.z).normalized.y;
				}

				float ModX = XDist * Mathf.Cos(TheLoopConnector[0].transform.eulerAngles.y * Mathf.Deg2Rad) + YDist * Mathf.Sin(-TheLoopConnector[0].transform.eulerAngles.y * Mathf.Deg2Rad);
				float ModY = YDist * Mathf.Cos(TheLoopConnector[0].transform.eulerAngles.y * Mathf.Deg2Rad) + XDist * Mathf.Sin(TheLoopConnector[0].transform.eulerAngles.y * Mathf.Deg2Rad);

				if(Redirect)
				{
					Redirect = false;
					ModX  = XDist;
					ModY = YDist;
				}

				transform.position -= new Vector3(StripLength*ModX,0,-StripLength*ModY);

				float angle = Mathf.Atan2(YDist,XDist)*Mathf.Rad2Deg;



				TheLoopConnector[0].transform.localEulerAngles -= new Vector3(0,angle,0);
				//transform.localEulerAngles -= new Vector3(0,angle,0);

				int i = 1;
				float Temp = angle; //holding right
				float Temp2 = 0;
				while(i < TheLoopConnector.Length)
				{
					Temp2 = Temp;
					Temp = TheLoopConnector[i].transform.localEulerAngles.y;
					TheLoopConnector[i].transform.localEulerAngles = new Vector3(0,Temp2,0);
					i++;
				}
			}
			else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Char.JamUntilSnakeLeft)
			{

				if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
				{
					VerticalSpeed += Time.fixedDeltaTime * Velocity;
					Main.HaltTip3 = true;

				}
				else if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
				{
					VerticalSpeed -= Time.fixedDeltaTime * Velocity;
					Main.HaltTip3 = true;

				}
				else
				{
					VerticalSpeed *= 0.8f;
				}

				VerticalSpeed = Mathf.Abs(VerticalSpeed) > 1 ? 1 * new Vector2(VerticalSpeed,0).normalized.x : VerticalSpeed;

				float XDist = new Vector2(1,VerticalSpeed*0.075f).normalized.x;
				float YDist = new Vector2(1,VerticalSpeed*0.075f).normalized.y;

				if(Redirect)
				{
					XDist = new Vector2(Repos.position.x-ReFocus.position.x,Repos.position.z-ReFocus.position.z).normalized.x;
					YDist = new Vector2(Repos.position.x-ReFocus.position.x,Repos.position.z-ReFocus.position.z).normalized.y;
				}

				float ModX = XDist * Mathf.Cos(TheLoopConnector[0].transform.eulerAngles.y * Mathf.Deg2Rad) + YDist * Mathf.Sin(-TheLoopConnector[0].transform.eulerAngles.y * Mathf.Deg2Rad);
				float ModY = YDist * Mathf.Cos(TheLoopConnector[0].transform.eulerAngles.y * Mathf.Deg2Rad) + XDist * Mathf.Sin(TheLoopConnector[0].transform.eulerAngles.y * Mathf.Deg2Rad);

				if(Redirect)
				{
					Redirect = false;
					ModX  = XDist;
					ModY = YDist;
				}

				transform.position += new Vector3(StripLength*ModX,0,-StripLength*ModY);

				float angle = Mathf.Atan2(YDist,XDist)*Mathf.Rad2Deg;

				TheLoopConnector[0].transform.localEulerAngles -= new Vector3(0,angle,0);
				//transform.localEulerAngles -= new Vector3(0,angle,0);

				int i = 1;
				float Temp = angle; //holding right
				float Temp2 = 0;
				while(i < TheLoopConnector.Length)
				{
					Temp2 = Temp;
					Temp = TheLoopConnector[i].transform.localEulerAngles.y;
					TheLoopConnector[i].transform.localEulerAngles = new Vector3(0,Temp2,0);
					i++;
				}
			}
			else
			{
				VerticalSpeed *= 0.8f;

			}
		}


		if(SliceItLeft)
		{
			SliceItLeft = false;
			if(SliceAt >DestinedLoopQuadCount-8)
			{
				SliceAt = DestinedLoopQuadCount-8;
			}
			else if(SliceAt <8)
			{
				SliceAt = 8;
			}
			TempPos = TheLoop[SliceAt].transform.parent.position;
			TempRot = TheLoop[SliceAt].transform.parent.eulerAngles.y;
			FixLoop();

		

			SliceLeft(SliceAt);
			Char.SlcieDist = SliceAt;
			CountDown = false;
			tempName = TheLoop[0].name;
			tempName2 = TheLoop[1].name;
		}
		if(SliceItRight)
		{
			if(SliceAt >DestinedLoopQuadCount-8)
			{
				SliceAt = DestinedLoopQuadCount-8;
			}
			else if(SliceAt <8)
			{
				SliceAt = 8;
			}
			SliceItRight = false;
			TempPos = TheLoop[SliceAt-1].transform.parent.position;
			TempRot = TheLoop[SliceAt-1].transform.parent.eulerAngles.y;
			FixLoop();
			CountDown = true;
			SliceRight(SliceAt);
			Char.SlcieDist = SliceAt;

			tempName = TheLoop[0].name;
			tempName2 = TheLoop[1].name;

		}


		if(CreateLevel2)
		{
			CamMov.SnakingFocus.transform.parent = transform;
			CamMov.GradualPivot.transform.parent = transform;
			CamMov.GradualPivot2.transform.parent = transform;
			int i = 0;
			while (i < TheLoop.Length)
			{
				Destroy(OriginalLoop[i].gameObject);
				Destroy(OriginalConnector[i].gameObject);
				i++;
			}
			if(Char.SnakingLeft)
			{
				Char.transform.localPosition = new Vector3(17f,-16.5625f,-1);
			}
			else 
			{
				Char.transform.localPosition = new Vector3(-17f,-16.5625f,-1);
			}

			Char.StripLength = 64;
			Radius*=2;
			Length*=2;

			Char.Sliced = false;
			Char.Snaking = false;
			Snaking = false;
			CamMov.PerfectRing = true;

			DestinedLoopQuadCount*=2;

			TheLoop = new QuadStrip[0];
			OriginalLoop = new QuadStrip[0];
			TheLoopConnector = new GameObject[0];
			OriginalConnector = new GameObject[0];

			Quad = QuadLvl2;

			CamMov.Snaking = false;
			CamMov.Dist*= 1.5f;
			CamMov.Yoff *= 1.5f;
			transform.position = Level2Pivot.transform.position;
			CamMov.Pivot = transform.position;
			CreateLoop = true;
		}


		if(CreateLoop)
		{
			if(DestinedLoopQuadCount < TheLoop.Length)
			{
				DestroyCurrentLoop(); //probably just debug use
			}

			CreateLoop = false;
			int i = 0;
			int j=0;

			TheLoop = new QuadStrip[DestinedLoopQuadCount];
			TheLoopConnector = new GameObject[DestinedLoopQuadCount];
			OriginalLoop = new QuadStrip[DestinedLoopQuadCount];
			OriginalConnector = new GameObject[DestinedLoopQuadCount];

			float frac = 1f/TheLoop.Length;
			StripLength = Length/TheLoop.Length;

			if(TheLoop[i] == null)
			{
				TheLoop[i] = Instantiate(Quad,transform.position,transform.rotation,transform).GetComponent<QuadStrip>();
			}
			TheLoop[i].transform.localScale = new Vector3(Length/TheLoop.Length,10.24f,Length/TheLoop.Length);
			TheLoop[i].transform.localPosition = new Vector3(0,0,-1)*Radius;
			TheLoop[i].transform.localEulerAngles = new Vector3(0,Mathf.Atan2(TheLoop[i].transform.localPosition.z + LookAt.z,TheLoop[i].transform.position.x + LookAt.x)*Mathf.Rad2Deg+90,0);
			TheLoop[i].Scale = frac;
			TheLoop[i].Offset = 0;
			OriginalLoop[i] = TheLoop[i];

			TheLoopConnector[i] = Instantiate(Empty,transform.position,transform.rotation,TheLoop[i].transform);
			TheLoopConnector[i].transform.parent = TheLoop[i].transform;
			TheLoopConnector[i].transform.localPosition = new Vector3(-0.5f,0,0);
			TheLoopConnector[i].transform.parent = transform;
			OriginalConnector[i] = TheLoopConnector[i];
			TheLoop[i].transform.parent= TheLoopConnector[i].transform;
			First = TheLoop[i];

			TheLoop[i].name = "Strip " + i;
			TheLoopConnector[i].name = "" + i;

			i++;
			while(i < TheLoop.Length)
			{
				if(TheLoopConnector[i] == null)
				{
					TheLoopConnector[i] = Instantiate(Empty,TheLoop[i-1].transform.position,transform.rotation,TheLoop[i-1].transform);
					TheLoopConnector[i].transform.localPosition = new Vector3(0.5f,0,0);
				}
				if(TheLoop[i] == null)
				{
					TheLoop[i] = Instantiate(Quad,TheLoop[i-1].transform.position,transform.rotation,TheLoopConnector[i].transform).GetComponent<QuadStrip>();
				}
				TheLoop[i].transform.parent = TheLoopConnector[i].transform;
				TheLoop[i].transform.localPosition = new Vector3(0.5f,0,0);
				TheLoopConnector[i].transform.localEulerAngles = new Vector3(0,-360f/TheLoop.Length,0);
				TheLoop[i].transform.localScale = new Vector3(1,1,1);
				TheLoop[i].Scale = frac;
				TheLoop[i].Offset = i*frac;
				TheLoop[i].name = "Strip " + i;
				TheLoopConnector[i].name = "" + i;

				OriginalLoop[i] = TheLoop[i];
				OriginalConnector[i] = TheLoopConnector[i];
				i++;
			}

			if(CreateLevel2)
			{
				TheLoopConnector[0].transform.localPosition = new Vector3(-27.8f,0,-4.2f);

			}

		}

		if(CreateLevel2)
		{
			CreateLevel2 = false;
			CamMov.Lvl2 = 1;

			CamMov.BonusFOV = -15;
			CamMov.BonusY = -1;
			CamMov.BonusX = 0;
			CamMov.BonusYRot = 0;

			//TheLoopConnector[0].transform.localPosition = new Vector3(-25.6f,0,-10.8f);
		}

	}

	public void DestroyCurrentLoop()
	{
		int i = 0;
		while(i < TheLoop.Length)
		{
			Destroy(TheLoop[i].gameObject);
			Destroy(TheLoopConnector[i].gameObject);
			i++;
		}
	}

	public void SliceRight(int Slice) //Right of the player, order array from right to left.
	{
		Slice--;
		if(Slice == -1)
		{
			Slice = TheLoop.Length-1;
		}

		if(!MegaDebugDumbWhat)
		{

		CleanLoop2.SetActive(false);
		TheLoop[Slice].transform.parent = transform;
		QuadStrip[] TEMP = new QuadStrip[TheLoop.Length]; 
		GameObject[] TempCon = new GameObject[TheLoopConnector.Length]; 


		int i = Slice;
		int c = 0;

		TEMP[c] = TheLoop[i];
		TempCon[c] = TheLoopConnector[i];
		i--;
		c++;
		if(i == -1)
		{
			i = TheLoop.Length-1;
		}
		while (i != Slice)
		{
			TEMP[c] = TheLoop[i];
			TempCon[c] = TheLoopConnector[i];


			i--;
			c++;
			if(i == -1)
			{
				i = TheLoop.Length-1;
			}
		}

		i=0;
		TempCon[i].transform.parent = transform;

		TEMP[i].transform.parent = TempCon[i].transform;
		TheLoop[i] = TEMP[i];
		TheLoopConnector[i] = TempCon[i];
		Vector3 P5 = new Vector3(-0.5f,0,0);

		i++;
		while(i < TEMP.Length)
		{
			TempCon[i].transform.parent = TEMP[i-1].transform;
			TempCon[i].transform.GetChild(0).parent = transform;
			TempCon[i].transform.localPosition = P5;
			//TEMP[i].transform.localPosition = new Vector3(-0.5f,0,0);
			//TEMP[i].transform.localScale = Vector3.one;

			TEMP[i].transform.parent = TempCon[i].transform;
			TheLoop[i] = TEMP[i];
			TheLoopConnector[i] = TempCon[i];

			i++;
		}

		//TheLoop[0].transform.parent.position = TempPos;
		//TheLoop[0].transform.parent.eulerAngles = new Vector3(0,TempRot,0);
		}
	}

	public void SliceLeft(int Slice) //Left of the player, order array from Left to Right.
	{
		CleanLoop2.SetActive(false);

		TheLoop[Slice].transform.parent.parent = transform;

		if(!MegaDebugDumbWhat)
		{
		TheLoop[Slice].transform.parent = transform;

		QuadStrip[] TEMP = new QuadStrip[TheLoop.Length]; 
		GameObject[] TempCon = new GameObject[TheLoopConnector.Length]; 

		int i = Slice;
		int c = 0;

		//print("Temp: ("+TempPos.x +", "+ TempPos.y +", " + TempPos.z +") : " + TempRot)

		print(TheLoop[i].name);
		TEMP[c] = TheLoop[i];
		TempCon[c] = TheLoopConnector[i];
		i++;
		c++;
		if(i == TheLoop.Length)
		{
			i = 0;
		}

		while (i != Slice)
		{
			TEMP[c] = TheLoop[i];
			TempCon[c] = TheLoopConnector[i];


			i++;
			c++;
			if(i == TheLoop.Length)
			{
				i = 0;
			}
		}

		i=0;
		TempCon[i].transform.parent = transform;
		TEMP[i].transform.parent = TempCon[i].transform;
		Vector3 P5 = new Vector3(0.5f,0,0);

		TEMP[i].transform.localPosition = P5;

		TheLoop[i] = TEMP[i];
		TheLoopConnector[i] = TempCon[i];
		i++;
		while(i < TEMP.Length)
		{
			TempCon[i].transform.parent = TEMP[i-1].transform;
			TempCon[i].transform.localPosition = P5;

			TEMP[i].transform.parent = TempCon[i].transform;
			TEMP[i].transform.localPosition = new Vector3(0.5f,0,0);

			TheLoop[i] = TEMP[i];
			TheLoopConnector[i] = TempCon[i];

			i++;
		}
		TheLoop[0].transform.parent.position = TempPos;
		TheLoop[0].transform.parent.eulerAngles = new Vector3(0,TempRot,0);
		}
	}

	public void FixLoop()
	{
		First.transform.parent = transform;


		int i=0;
		OriginalConnector[i].transform.parent = transform;
		Vector3 P5 = new Vector3(0.5f,0,0);

		OriginalLoop[i].transform.parent = OriginalConnector[i].transform;
		OriginalLoop[i].transform.localEulerAngles = Vector3.zero;
		OriginalLoop[i].transform.localPosition = P5;

		TheLoop[i] = OriginalLoop[i];
		TheLoopConnector[i] = OriginalConnector[i];

		i++;
		while(i < OriginalLoop.Length)
		{
			OriginalConnector[i].transform.parent = OriginalLoop[i-1].transform;
			OriginalConnector[i].transform.GetChild(0).parent = transform;
			OriginalConnector[i].transform.localPosition = P5;

			OriginalLoop[i].transform.parent = OriginalConnector[i].transform;
			OriginalLoop[i].transform.localEulerAngles = Vector3.zero;

			TheLoop[i] = OriginalLoop[i];
			TheLoopConnector[i] = OriginalConnector[i];

			i++;
		}
	}

}
