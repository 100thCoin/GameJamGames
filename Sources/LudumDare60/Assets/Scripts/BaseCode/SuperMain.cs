using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super{
	public static SuperMain Dataholder;
}
public class S{
	public static SuperMain Main;
}

public class SuperMain : MonoBehaviour {
	public bool DEBUGGING;

	public bool PreviousVictory_CanSkipCutscenes;

	public GameObject TitleScreen;
	public GameObject TitleCam;
	public GameObject Game;

	public bool LockTitle;

	public GameObject LoadedGame;

	public KeyCode[] ReboundInputs;
	public bool InvertMouse;
	public float Volume_Music;
	public float Volume_SFX;
	public float Volume_Voice;

	public float MusicMultiplier;

	public bool PostTitle;
	public float PostTitleTimer;
	public SpriteRenderer RollingTransition;


	public void StartGame()
	{
		if (LoadedGame != null) {
			Destroy (LoadedGame);
		}
		LoadedGame = Instantiate (Game, Vector3.zero, transform.rotation, transform);
		LoadedGame.SetActive (true);
	}


	public void ReturnToTitle()
	{
		PostTitle = false;
		PostTitleTimer = 0;
		if (LoadedGame != null) {
			Destroy (LoadedGame);
		}
		TitleScreen.SetActive (true);
		// TODO: If you won the game, move the camera to the credits page.
		if (PreviousVictory_CanSkipCutscenes) {
			TitleCam.transform.position = new Vector3 (0, -20, -10);
		}

		MusicMultiplier = 1;
		LockTitle = false;
	}

	// Use this for initialization
	void Start () {
		if (!DEBUGGING) {
			ReturnToTitle ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (PostTitle) {
			PostTitleTimer += Time.deltaTime * 0.3f;
			RollingTransition.color = new Vector4 (0, 0, 0, DataHolder.SinLerp (0f, 1f, Mathf.Clamp01(PostTitleTimer), 1));
		}
	}

	void Awake()
	{
		Super.Dataholder = this;
		S.Main = this;
		Screen.SetResolution (720, 480, false);
		Screen.SetResolution (1440, 960, false);
	}

	[ContextMenu("init")]
	void OnEnable()
	{
		Super.Dataholder = this;
		S.Main = this;

	}






}
