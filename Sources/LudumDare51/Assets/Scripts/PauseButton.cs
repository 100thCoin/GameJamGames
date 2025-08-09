using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {


	public bool VolumeVoice;
	public bool VolumeMusic;
	public bool CB;
	public bool Quit;
	public bool TestVoice;
	public bool Subtitles;

	public TextMesh CBText;
	public SpriteRenderer CBExample;
	public Sprite CBOn;
	public Sprite CBOff;

	public Transform T1;
	public Transform T2;

	public bool Hold;
	public Camera Cam;
	public GameObject Knob;

	public GameObject[] VoiceTests;

	public bool TitlePlay;
	public bool TitleQuit;
	public bool TitleCreds;
	public bool TitleBackTo;
	public GameObject FullGamePrefab;


	void Start()
	{
		if(VolumeMusic)
		{			
			float Mousepos = G.Main.MusicVolume;
			Knob.transform.position = new Vector3(Mathf.Lerp(T1.transform.position.x,T2.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
			CBText.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100) + "%";
		}
		if(VolumeVoice)
		{
			
			float Mousepos = G.Main.MusicVolume;
			Knob.transform.position = new Vector3(Mathf.Lerp(T1.transform.position.x,T2.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
			CBText.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100) + "%";
		}
	}

	// Update is called once per frame
	void OnMouseOver () {
	
		if(!G.Main.LeavingTitle)
		{
			if(TitlePlay)
			{
				if(Input.GetKeyDown(KeyCode.Mouse0))
				{

					G.Main.BeginGame();
				}
			}

			if(TitleQuit)
			{
				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					Application.Quit();
				}
			}

			if(TitleCreds)
			{
				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					Cam.transform.position = new Vector3(0,-19,-50);
				}
			}
			if(TitleBackTo)
			{
				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					Cam.transform.position = new Vector3(0,0,-50);
				}
			}
		}

		if(CB)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				G.Main.ColorBlindMode = !G.Main.ColorBlindMode;
				CBText.text = G.Main.ColorBlindMode ? "[ True ]" : "[ False ]";
				CBExample.sprite = G.Main.ColorBlindMode ? CBOn : CBOff;
			}
		}

		if(VolumeMusic)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				Hold = true;
			}

		}
		if(VolumeVoice)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				Hold = true;
			}
		}

		if(Subtitles)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				G.Main.SubtitlesActive = !G.Main.SubtitlesActive;
				CBText.text = G.Main.SubtitlesActive ? "[ Subtitles On ]" : "[ Subtitles Off ]";

			}
		}

		if(Quit)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				GameObject Temp = G.Main.gameObject;
				Instantiate(FullGamePrefab,new Vector3(0,0,0), FullGamePrefab.transform.rotation);
				Destroy(Temp);
			}

		}

		if(TestVoice)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
			Instantiate(VoiceTests[Mathf.RoundToInt(Random.Range(0,2))]);
			}
		}


	}

	void Update()
	{
		if(VolumeMusic)
		{
			if(Input.GetKeyUp(KeyCode.Mouse0))
			{
				Hold = false;
			}
			if(Hold)
			{
				float Mousepos = Input.mousePosition.x;
				float T1x = Cam.WorldToScreenPoint(T1.transform.position).x;
				float T2x = Cam.WorldToScreenPoint(T2.transform.position).x;
				float diff = T2x - T1x;
				Mousepos = (Mousepos-T1x)/diff;
				G.Main.MusicVolume = Mathf.Clamp01(Mousepos);
				Knob.transform.position = new Vector3(Mathf.Lerp(T1.transform.position.x,T2.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
				CBText.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100) + "%";

				if(!G.Main.playingTheGame)
				{
					G.Main.VoiceVolume = Mathf.Clamp01(Mousepos);

				}

			}
		}
		if(VolumeVoice)
		{
			if(Input.GetKeyUp(KeyCode.Mouse0))
			{
				Hold = false;
			}
			if(Hold)
			{
				float Mousepos = Input.mousePosition.x;
				float T1x = Cam.WorldToScreenPoint(T1.transform.position).x;
				float T2x = Cam.WorldToScreenPoint(T2.transform.position).x;
				float diff = T2x - T1x;
				Mousepos = (Mousepos-T1x)/diff;
				G.Main.VoiceVolume = Mathf.Clamp01(Mousepos);
				Knob.transform.position = new Vector3(Mathf.Lerp(T1.transform.position.x,T2.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
				CBText.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100) + "%";
			}
		}
	}

}
