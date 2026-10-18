using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[ContextMenu("Set Up Main")]
	void SetUpMain()
	{
		GameObject SM = new GameObject ();
		SM.name = "SuperMain";
		SM.transform.position = new Vector3 (0, 0, 0);
		SM.transform.eulerAngles = new Vector3 (0, 0, 0);
		SM.transform.localScale = new Vector3 (1, 1, 1);
		SM.AddComponent<SuperMain> ();

		GameObject M = new GameObject ();
		M.name = "Main";
		M.transform.position = new Vector3 (0, 0, 0);
		M.transform.eulerAngles = new Vector3 (0, 0, 0);
		M.transform.localScale = new Vector3 (1, 1, 1);
		M.AddComponent<DataHolder> ();
		M.transform.parent = SM.transform;

		GameObject T = new GameObject ();
		T.name = "TitleScreen";
		T.transform.position = new Vector3 (0, 0, 0);
		T.transform.eulerAngles = new Vector3 (0, 0, 0);
		T.transform.localScale = new Vector3 (1, 1, 1);
		T.transform.parent = SM.transform;
		TitleManager TitleMan = T.AddComponent<TitleManager> ();

		GameObject T1 = new GameObject ();
		T1.name = "Title";
		T1.transform.position = new Vector3 (0, 0, 0);
		T1.transform.eulerAngles = new Vector3 (0, 0, 0);
		T1.transform.localScale = new Vector3 (1, 1, 1);
		T1.transform.parent = T.transform;
		SpriteRenderer T1SR = T1.AddComponent<SpriteRenderer> ();
		Texture2D T1TempTex = new Texture2D (384, 256);
		for (int x = 0; x < T1TempTex.width; x++) {
			for(int y = 0; y < T1TempTex.height; y++)
			{
				T1TempTex.SetPixel(x,y,Color.grey);
			}
		}
		T1SR.sprite = Sprite.Create(T1TempTex, new Rect(0, 0, T1TempTex.width, T1TempTex.height), new Vector2(0.5f, 0.5f), 16);

		GameObject T1C = new GameObject ();
		T1C.name = "Credits";
		T1C.transform.position = new Vector3 (0, -20, 0);
		T1C.transform.eulerAngles = new Vector3 (0, 0, 0);
		T1C.transform.localScale = new Vector3 (1, 1, 1);
		T1C.transform.parent = T.transform;
		SpriteRenderer T1CSR = T1C.AddComponent<SpriteRenderer> ();
		T1CSR.sprite = Sprite.Create(T1TempTex, new Rect(0, 0, T1TempTex.width, T1TempTex.height), new Vector2(0.5f, 0.5f), 16);

		GameObject TCam = new GameObject ();
		TCam.name = "Title Camera";
		TCam.transform.position = new Vector3 (0, 0, -10);
		TCam.transform.eulerAngles = new Vector3 (0, 0, 0);
		TCam.transform.localScale = new Vector3 (1, 1, 1);
		TCam.transform.parent = T.transform;
		Camera T1Camera = TCam.AddComponent<Camera> ();
		T1Camera.orthographic = true;
		T1Camera.orthographicSize = 8;

		GameObject TB_P = GameObject.CreatePrimitive (PrimitiveType.Cube);
		TB_P.name = "TitleButton_Play";
		TB_P.transform.position = new Vector3 (-8, 0, 0);
		TB_P.transform.eulerAngles = new Vector3 (0, 0, 0);
		TB_P.transform.localScale = new Vector3 (6, 2, 1);
		TB_P.transform.parent = T.transform;
		TitleButtons ButtonPlay = TB_P.AddComponent<TitleButtons> ();
		ButtonPlay.Play = true;
		ButtonPlay.TMan = TitleMan;
		ButtonPlay.Camera = T1Camera.gameObject;
		ButtonPlay.SR = T1SR;
		ButtonPlay.NoHover = T1SR.sprite;

		GameObject TB_C = GameObject.CreatePrimitive (PrimitiveType.Cube);
		TB_C.name = "TitleButton_Creds";
		TB_C.transform.position = new Vector3 (-8, -3, 0);
		TB_C.transform.eulerAngles = new Vector3 (0, 0, 0);
		TB_C.transform.localScale = new Vector3 (6, 2, 1);
		TB_C.transform.parent = T.transform;
		TitleButtons ButtonCreds = TB_C.AddComponent<TitleButtons> ();
		ButtonCreds.Credits = true;
		ButtonCreds.TMan = TitleMan;
		ButtonCreds.Camera = T1Camera.gameObject;
		ButtonCreds.SR = T1SR;
		ButtonCreds.NoHover = T1SR.sprite;
		ButtonCreds.CamMove = new Vector3 (0, -20, -10);

		GameObject TB_Q = GameObject.CreatePrimitive (PrimitiveType.Cube);
		TB_Q.name = "TitleButton_Quit";
		TB_Q.transform.position = new Vector3 (-8, -6, 0);
		TB_Q.transform.eulerAngles = new Vector3 (0, 0, 0);
		TB_Q.transform.localScale = new Vector3 (6, 2, 1);
		TB_Q.transform.parent = T.transform;
		TitleButtons ButtonQuit = TB_Q.AddComponent<TitleButtons> ();	
		ButtonQuit.Quit = true;
		ButtonQuit.TMan = TitleMan;
		ButtonQuit.Camera = T1Camera.gameObject;
		ButtonQuit.SR = T1SR;
		ButtonQuit.NoHover = T1SR.sprite;

		GameObject TB_B = GameObject.CreatePrimitive (PrimitiveType.Cube);
		TB_B.name = "TitleButton_Return";
		TB_B.transform.position = new Vector3 (0, -26, 0);
		TB_B.transform.eulerAngles = new Vector3 (0, 0, 0);
		TB_B.transform.localScale = new Vector3 (6, 2, 1);
		TB_B.transform.parent = T.transform;
		TitleButtons ButtonBack = TB_B.AddComponent<TitleButtons> ();	
		ButtonBack.Back = true;
		ButtonBack.TMan = TitleMan;
		ButtonBack.Camera = T1Camera.gameObject;
		ButtonBack.SR = T1CSR;
		ButtonBack.NoHover = T1SR.sprite;
		ButtonBack.CamMove = new Vector3 (0, 0, -10);

		GameObject TVolS = GameObject.CreatePrimitive (PrimitiveType.Cube);
		TVolS.name = "TitleVolumeSlider";
		TVolS.transform.position = new Vector3 (-4, -3, 0);
		TVolS.transform.eulerAngles = new Vector3 (0, 0, 0);
		TVolS.transform.localScale = new Vector3 (1, 8, 1);
		TVolS.transform.parent = T.transform;
		TitleVolumeSlider VolumeSlider = TVolS.AddComponent<TitleVolumeSlider> ();

		GameObject TVol_Top = new GameObject ();
		TVol_Top.name = "top";
		TVol_Top.transform.position = new Vector3 (-4, 1, 0);
		TVol_Top.transform.parent = VolumeSlider.transform;
		GameObject TVol_Bottom = new GameObject ();
		TVol_Bottom.name = "bottom";
		TVol_Bottom.transform.position = new Vector3 (-4, -7, 0);
		TVol_Bottom.transform.parent = VolumeSlider.transform;
		GameObject TVol_Slider = new GameObject ();
		TVol_Slider.name = "slider";
		TVol_Slider.transform.position = new Vector3 (-4, -3, -1);
		TVol_Slider.transform.parent = VolumeSlider.transform;
		Texture2D TVol_SliderTempTex = new Texture2D (24, 12);
		for (int x = 0; x < TVol_SliderTempTex.width; x++) {
			for(int y = 0; y < TVol_SliderTempTex.height; y++)
			{
				TVol_SliderTempTex.SetPixel(x,y,Color.white);
			}
		}
		SpriteRenderer TVol_SliderSR = TVol_Slider.AddComponent<SpriteRenderer> ();
		TVol_SliderSR.sprite = Sprite.Create(TVol_SliderTempTex, new Rect(0, 0, TVol_SliderTempTex.width, TVol_SliderTempTex.height), new Vector2(0.5f, 0.5f), 16);
		GameObject TVol_TM = new GameObject ();
		TVol_TM.name = "TextMesh";
		TVol_TM.transform.position = new Vector3 (-2.5f, -3, -1);
		TVol_TM.transform.eulerAngles = new Vector3 (0, 0, 0);
		TVol_TM.transform.localScale = new Vector3 (1, 1, 1);
		TVol_TM.transform.parent = VolumeSlider.transform;
		TextMesh VolumeTM = TVol_TM.AddComponent <TextMesh>();
		VolumeTM.text = "50%";
		VolumeTM.color = new Vector4 (1, 1, 1, 1);

		VolumeSlider.Slider = TVol_Slider.transform;
		VolumeSlider.Top = TVol_Top.transform;
		VolumeSlider.Bottom = TVol_Bottom.transform;
		VolumeSlider.Cam = T1Camera;
		VolumeSlider.TM = VolumeTM;
	}
}
