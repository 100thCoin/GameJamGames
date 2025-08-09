using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexViewer : MonoBehaviour {

	public SpriteRenderer LineProgress;
	public float Progress;
	public int Size;

	public float Timer;
	public float SProg;
	public float DProg;
	public bool UpdateProgress;
	public float UpdateDuration;
	public bool DoNextProg;
	public GameObject LeftEnd;
	public GameObject RightEnd;
	public GameObject BlackBar;
	public bool CreateBar;
	public float ShortStall;
	public bool Stall;

	public bool Failed;
	public bool LevelCleared;

	public HexByte[] Bytes;

	public GameObject LevelClearText;
	public GameObject RetryText;
	public int CheckByte;

	public GameObject[] NotchList;

	public DataHolder Main;

	public bool ReloadBytes;

	public GameObject[] Chimes;


	// Use this for initialization
	void Start () {
		
	}

	void LateUpdate()
	{
		if(ReloadBytes)
		{
			ReloadBytes = false;
			int i = 0;
			while(i < Bytes.Length)
			{
				Bytes[i].OnEnable();
				i++;
			}
		}
	}

	public void Abort()
	{
		DoNextProg = false;
		UpdateProgress = false;
		Progress = 0;
		CheckByte = 0;
		SProg = 0;
		DProg = 0;
		LineProgress.color = Color.white;
	}

	// Update is called once per frame
	void Update () {

		if(CreateBar)
		{
			CreateBar = false;
			LeftEnd.transform.localPosition = new Vector3(-Size-1,-2,0);
			RightEnd.transform.localPosition = new Vector3(Size+1,-2,0);
			BlackBar.transform.localScale = new Vector3(Size*2+3,2,1);
			CheckByte = 0;
			NotchList[0].SetActive(false);
			NotchList[1].SetActive(false);
			NotchList[2].SetActive(false);
			NotchList[3].SetActive(false);
			NotchList[4].SetActive(false);

			if(Size >0 && Size <=5)
			{
				NotchList[Size-1].SetActive(true);
			}
		}





		if(DoNextProg)
		{
			DoNextProg = false;
			SProg = DProg;
			DProg+=1;
			Timer = 0;
			if(DProg == 1)
			{
				DProg = 1.25f;
			}
			else if(DProg == Size+1.25f)
			{
				DProg+=0.5f;
			}
			UpdateProgress = true;
		}


		if(UpdateProgress)
		{
			Timer+=Time.deltaTime;
			Progress = -Mathf.Cos(Mathf.PI*Timer*(1/UpdateDuration))*0.5f*(DProg-SProg)+0.5f*(DProg+SProg);
			if(Timer > UpdateDuration)
			{
				Progress = DProg;
				UpdateProgress = false;
				if(CheckByte >= Bytes.Length)
				{
					LevelCleared = true;
					Main.LevelClear = true;
					VolumeThis V = Instantiate(LevelClearText,new Vector3(0,0,0),transform.rotation,transform).GetComponent<VolumeThis>();
					Main.AceMusicObject.GetComponent<AceVolumeFade>().Done = true;
					V.Main = Main;
					V.AS.volume = Main.Volume;
				}
				else
				{

					Stall = true;
					ShortStall = 0;
				}
			}
		}

		if(Stall)
		{
			ShortStall+= Time.deltaTime;
			if(ShortStall > 0.5f)
			{
				ShortStall = 0;
				Stall = false;
				Failed = !Bytes[CheckByte].ConditionMet;
				if(Failed)
				{
					LineProgress.color = Color.red;
					Main.RetryTextObject = Instantiate(RetryText,new Vector3(0,0,0),transform.rotation,transform);
					Main.AceMusicObject.GetComponent<AceVolumeFade>().Done = true;
					Main.RetryTextObject.GetComponent<VolumeThis>().Main = Main;
					Main.RetryTextObject.GetComponent<VolumeThis>().AS.volume = Main.Volume;
				}
				else
				{
					if(Size == 1)
					{
						VolumeThis V = Instantiate(Chimes[0],transform.position,transform.rotation,transform).GetComponent<VolumeThis>();
						V.AS.volume = Main.Volume;
						V.Main = Main;
					}
					else if (Size == 2)
					{
						VolumeThis V = Instantiate(Chimes[CheckByte],transform.position,transform.rotation,transform).GetComponent<VolumeThis>();
						V.AS.volume = Main.Volume;
						V.Main = Main;
					}
					else if (Size == 3)
					{
						if(CheckByte < 2)
						{
							VolumeThis V = Instantiate(Chimes[CheckByte],transform.position,transform.rotation,transform).GetComponent<VolumeThis>();
							V.AS.volume = Main.Volume;
							V.Main = Main;
						}
						else
						{
							VolumeThis V = Instantiate(Chimes[4],transform.position,transform.rotation,transform).GetComponent<VolumeThis>();
							V.AS.volume = Main.Volume;
							V.Main = Main;					
						}
					}
					else if (Size == 4)
					{
						if(CheckByte < 3)
						{
							VolumeThis V = Instantiate(Chimes[CheckByte],transform.position,transform.rotation,transform).GetComponent<VolumeThis>();
							V.AS.volume = Main.Volume;
							V.Main = Main;
						}
						else
						{
							VolumeThis V = Instantiate(Chimes[4],transform.position,transform.rotation,transform).GetComponent<VolumeThis>();
							V.AS.volume = Main.Volume;
							V.Main = Main;	
						}
					}
					else
					{
						VolumeThis V = Instantiate(Chimes[CheckByte],transform.position,transform.rotation,transform).GetComponent<VolumeThis>();
						V.AS.volume = Main.Volume;
						V.Main = Main;
					}
					CheckByte++;
					DoNextProg = true;
				}
			}
		}


		LineProgress.transform.localScale = new Vector3((Progress)*2,1.9f,1);
		LineProgress.transform.localPosition = new Vector3(-(Size+1.5f) + (Progress),-2,1);
	}
}
