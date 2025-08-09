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

	public GameObject TitleScreen;
	public GameObject Game;

	public bool LockTitle;

	public GameObject LoadedGame;

	public KeyCode[] ReboundInputs;
	public bool InvertMouse;
	public float Volume_Music;
	public float Volume_SFX;
	public float Volume_Voice;

	public float MusicMultiplier;


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
		if (LoadedGame != null) {
			Destroy (LoadedGame);
		}
		TitleScreen.SetActive (true);
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

	}

	void Awake()
	{
		Super.Dataholder = this;
		S.Main = this;
		Screen.SetResolution (768, 512, false);
		Screen.SetResolution (1536, 1024, false);

	}

	void OnEnable()
	{
		Super.Dataholder = this;
		S.Main = this;

	}






}
