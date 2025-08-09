using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostACE : MonoBehaviour {

	public float Timer_ScreenFlash;
	public float Timer_SnapshotFade;
	public float Timer_Zoom;
	public float Timer_Zoom_Speed;
	public float Timer_Zoom_SpeedSpeed;

	public float Timer_Delay;

	public bool Happening;
	public  bool Happening2;

	public int DoFlash;
	public int DoFade;
	public int DoZoom;
	public bool Delaying;

	public DataHolder Main;

	public Vector2 ZoomPosS;
	public Vector2 ZoomPosD;
	public float ZoomSizeS;
	public float ZoomSizeD;
	public float ZoomDur;

	public bool Clubbin;

	public PostProcessingProfile profile;

	public HexViewer Hex;

	public void Abort()
	{
		Happening2 = false;
		Happening = false;
		DoFlash = 0;
		DoFade = 0;
		DoZoom = 0;
		Delaying = false;
		Timer_ScreenFlash = 0;
		Timer_SnapshotFade = 0;
		Timer_Zoom = ZoomDur;
		Timer_Delay = 0;
		Timer_Zoom_Speed =0;
		Timer_Zoom_SpeedSpeed = 0;

		ChromaticAberrationModel.Settings temp = profile.chromaticAberration.settings;
		temp.intensity = 0;
		profile.chromaticAberration.settings = temp;

		BloomModel.Settings temp1 = profile.bloom.settings;
		temp1.bloom.threshold = 3;
		profile.bloom.settings = temp1;

		VignetteModel.Settings temp2 = profile.vignette.settings;
		temp2.intensity = 0;
		profile.vignette.settings = temp2;

		MotionBlurModel.Settings temp3 = profile.motionBlur.settings;
		temp3.shutterAngle = 0;
		profile.motionBlur.settings = temp3;

		Main.MainCamera.orthographicSize = ZoomSizeS;
		Main.MainCamera.transform.position = new Vector3(ZoomPosS.x,ZoomPosS.y,-10);

	}

	void Start()
	{
		Main.SnapShotMat.SetFloat("_Alpha",1);
	}

	// Update is called once per frame
	void Update () {

		if(Happening)
		{
			Happening2 = true;
			Happening = false;
			DoFlash = 1;
			DoFade = 0;
			DoZoom = 0;
			Timer_ScreenFlash = 0;
			Timer_SnapshotFade = 0;
			Timer_Zoom = ZoomDur;
			Timer_Delay = 0;
			Timer_Zoom_Speed =0;
			Timer_Zoom_SpeedSpeed = 0;
		}

		if(DoFlash == 1)
		{
			Timer_ScreenFlash+=Time.deltaTime*5;
			Main.ScreenFlash.color = new Vector4(1,1,1,1-Timer_ScreenFlash);
			if(Timer_ScreenFlash >= 1)
			{
				DoFlash = 2;
				Delaying = true;
				Timer_Delay = 0;
				Main.ScreenFlash.color = new Vector4(1,1,1,0);

			}
		}
		if(DoFade == 1)
		{
			Timer_SnapshotFade+=Time.deltaTime*1.5f;
			Main.SnapShotMat.SetFloat("_Alpha",1-Timer_SnapshotFade);
			if(Timer_SnapshotFade >= 1)
			{
				DoFade = 2;
				Delaying = true;
				Timer_Delay = 0;
				Main.SnapShotMat.SetFloat("_Alpha",0);
			}
		}
		if(DoZoom == 1)
		{
			Timer_Zoom-=Time.deltaTime*Timer_Zoom_Speed;
			Timer_Zoom_Speed+=Time.deltaTime * Timer_Zoom_SpeedSpeed;
			Timer_Zoom_SpeedSpeed+=Time.deltaTime*4;

			float CamPosX = Mathf.Sin((Mathf.PI*(Timer_Zoom))/(2*(ZoomDur)))*(ZoomPosS.x-ZoomPosD.x)+ZoomPosD.x;
			float CamPosY = Mathf.Sin((Mathf.PI*(Timer_Zoom))/(2*(ZoomDur)))*(ZoomPosS.y-ZoomPosD.y)+ZoomPosD.y;
			float CamSize = Mathf.Sin((Mathf.PI*(Timer_Zoom))/(2*(ZoomDur)))*(ZoomSizeS - ZoomSizeD)+ZoomSizeD;
			Main.MainCamera.transform.position = new Vector3(CamPosX,CamPosY,-10);
			Main.MainCamera.orthographicSize = CamSize;

			if(Main.LevelNum == Main.FinalLevelNum)
			{
				Main.FinalLevelUI.transform.localPosition = new Vector3(7.3f,Mathf.Sin((Mathf.PI*(Timer_Zoom))/(2*(ZoomDur)))*(5-12.8f)+12.8f,0);

			}


			ChromaticAberrationModel.Settings temp = profile.chromaticAberration.settings;
			temp.intensity = -Timer_Zoom*5+ZoomDur;
			profile.chromaticAberration.settings = temp;

			BloomModel.Settings temp1 = profile.bloom.settings;
			temp1.bloom.threshold = 2 -(ZoomDur-Timer_Zoom)*1;
			if(temp1.bloom.threshold < 1.2f){temp1.bloom.threshold = 1.2f;}
			profile.bloom.settings = temp1;

			VignetteModel.Settings temp2 = profile.vignette.settings;
			temp2.intensity = -Timer_Zoom*2+ZoomDur;
			if(temp2.intensity < 0){temp2.intensity = 0;}
			if(temp2.intensity > 0.3f){temp2.intensity = 0.3f;}
			profile.vignette.settings = temp2;

			MotionBlurModel.Settings temp3 = profile.motionBlur.settings;
			temp3.shutterAngle = Timer_Zoom_Speed/2f;
			profile.motionBlur.settings = temp3;

			if(Timer_Zoom <= 0)
			{
				Main.MainCamera.transform.position = new Vector3(ZoomPosD.x,ZoomPosD.y,-10);
				Main.MainCamera.orthographicSize = ZoomSizeD;
				Delaying = true;
				DoZoom = 2;
				Timer_Delay = 0;

			}

		}

		if(Delaying)
		{
			if(DoFlash ==2)
			{
				Timer_Delay+= Time.deltaTime;
				if(Timer_Delay > 1.2f)
				{
					DoFade = 1;
					DoFlash = 3;
					Timer_Delay = 0;
					Delaying = false;
				}
			}
			if(DoFade ==2)
			{
				Timer_Delay+= Time.deltaTime;
				if(Timer_Delay > 0.2f)
				{
					DoZoom = 1;
					DoFade = 3;
					Timer_Delay = 0;
					Delaying = false;
				}
			}
			if(DoZoom ==2)
			{
				Timer_Delay+= Time.deltaTime;

				ChromaticAberrationModel.Settings temp = profile.chromaticAberration.settings;
				temp.intensity -= Time.deltaTime*10;
				profile.chromaticAberration.settings = temp;

				BloomModel.Settings temp1 = profile.bloom.settings;
				temp1.bloom.threshold += Time.deltaTime*8;
				profile.bloom.settings = temp1;

				VignetteModel.Settings temp2 = profile.vignette.settings;
				temp2.intensity -= Time.deltaTime*8;
				if(temp2.intensity < 0){temp2.intensity = 0;}
				profile.vignette.settings = temp2;

				MotionBlurModel.Settings temp3 = profile.motionBlur.settings;
				temp3.shutterAngle = 1-Timer_Delay;
				profile.motionBlur.settings = temp3;

				if(Timer_Delay > 1f)
				{
					//DoFade = 1;
					DoZoom = 3;
					Timer_Delay = 0;
					Delaying = false;
					if(!Clubbin)
					{
						Hex.DoNextProg = true;
					}
					else
					{
						Main.Finale.gameObject.SetActive(true);
						Main.Finale.UpdateTM();
					}
				}
			}
		}

	}
}
