using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour {

	public int LevelNum;
	public HexViewer HexView;
	public Transform AceSnapHolder;
	public Transform LevelHolder;
	public GameObject CurrentLevel;
	public GameObject CurrentLevelUI;
	public Camera SnapshotCam;
	public Camera MainCamera;
	public Camera LevelCam;

	public Renderer ScreenQuad;
	public Renderer Snapshot;
	public Material NormalScreenMat;
	public Material BWScreenMat;
	public Material SnapShotMat;

	public SpriteRenderer ScreenFlash;

	public PostACE PACE;

	public GameObject AceMusicObject;
	public GameObject RetryTextObject;

	public GameObject[] LevelList;
	public GameObject[] LevelUIList;
	public bool[] HexadecimalLevels;
	public int[] LevelSizes;
	public string[] LevelNames;

	public TextMesh LevelName;

	public bool InGame;
	public bool LevelClear;
	public float screenflashTimer;

	public bool NextLevel;
	public AudioSource LevelMusic;

	public float TimerSeconds;
	public int Resets;

	public FinaleStats Finale;

	public bool PressEscToQuit;

	public GameObject FinalLevelUI;
	public int FinalLevelNum;

	public float Volume;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(PressEscToQuit)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}

		}

		if(InGame)
		{
			TimerSeconds+= Time.deltaTime;
		}

		if(LevelMusic.volume < 1*Volume)
		{
			LevelMusic.volume+= Time.deltaTime*3*Volume;
		}

		if(screenflashTimer > 0)
		{
			screenflashTimer-=Time.deltaTime * 5;
			ScreenFlash.color = new Vector4(1,1,1,screenflashTimer);
			if(screenflashTimer <= 0)
			{
				ScreenFlash.color = new Vector4(1,1,1,0);
			}		
		}
		if(InGame)
		{
			if(Input.GetKeyDown(KeyCode.R) && !LevelClear)
			{
				Destroy(CurrentLevel.gameObject);
				CurrentLevel = Instantiate(LevelList[LevelNum],Vector3.zero,transform.rotation,LevelHolder);
				HexView.ReloadBytes = true;
				HexView.Abort();
				screenflashTimer = 1;
				ScreenFlash.color = new Vector4(1,1,1,1);
				PACE.Abort();
				ScreenQuad.material = NormalScreenMat;
				LevelCam.enabled = true;
				if(AceMusicObject != null)
				{
					AceMusicObject.GetComponent<AceVolumeFade>().Done = true;
				}
				if(RetryTextObject!= null)
				{
					Destroy(RetryTextObject.gameObject);
				}
				SnapShotMat.SetFloat("_Alpha",1);
				if(LevelMusic.pitch == 0)
				{
					LevelMusic.volume = 0;
				}
				LevelMusic.pitch = 1;

				Snapshot.enabled = false;
				PACE.Happening2 = false;
				Resets++;
			}
		}

		if(NextLevel)
		{

			NextLevel = false;
			LevelClear = false;
			Destroy(CurrentLevel.gameObject);
			LevelNum++;
			CurrentLevel = Instantiate(LevelList[LevelNum],Vector3.zero,transform.rotation,LevelHolder);
			HexView.Abort();
			if(CurrentLevelUI!= null){Destroy(CurrentLevelUI.gameObject);}
			CurrentLevelUI = Instantiate(LevelUIList[LevelNum],HexView.transform.position,transform.rotation,HexView.transform);
			screenflashTimer = 1;
			ScreenFlash.color = new Vector4(1,1,1,1);
			PACE.Abort();
			ScreenQuad.material = NormalScreenMat;
			LevelCam.enabled = true;
			LevelCam.GetComponent<CamControl>().Hexadecimal = HexadecimalLevels[LevelNum];
			LevelCam.GetComponent<CamControl>().UpdateScreen = true;
			HexView.Size = LevelSizes[LevelNum];
			HexView.CreateBar = true;
			LevelName.text = LevelNames[LevelNum];
			if(LevelMusic.pitch == 0)
			{
				LevelMusic.volume = 0;
			}
			LevelMusic.pitch = 1;

			if(LevelNum == FinalLevelNum)
			{
				Destroy(HexView.gameObject);
				FinalLevelUI.SetActive(true);
			}
			HexView.ReloadBytes = true;
			PACE.Happening2 = false;
		}

	}
}
