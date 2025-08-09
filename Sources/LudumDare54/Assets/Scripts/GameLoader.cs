using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {

	public GameObject Copycopy;
	public GameObject CurrentGame;

	// Use this for initialization
	void Start () {

		CurrentGame = Instantiate (Copycopy, transform.position, transform.rotation);
		CurrentGame.GetComponent<DataHolder> ().GameLoad = this;
		CurrentGame.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void REGENERATE()
	{
		float musicvol = Global.Dataholder.MusicVolume;
		float SFXVol =  Global.Dataholder.SFXVolume;
		Destroy (CurrentGame);
		CurrentGame = Instantiate (Copycopy, transform.position, transform.rotation);
		CurrentGame.GetComponent<DataHolder> ().GameLoad = this;
		CurrentGame.SetActive (true);
		CurrentGame.GetComponent<DataHolder> ().MusicVolume = musicvol;
		CurrentGame.GetComponent<DataHolder> ().SFXVolume = SFXVol;

	}

}
