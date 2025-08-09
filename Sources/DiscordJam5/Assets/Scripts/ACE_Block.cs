using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACE_Block : MonoBehaviour {

	public GameObject OtherO;
	public DataHolder Main;
	public GameObject AceMusic;
	public bool Clubbin;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		//literally anything
		OtherO = other.gameObject;
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
		transform.parent = Main.AceSnapHolder;
		other.transform.parent = Main.AceSnapHolder;
		Main.LevelCam.Render();
		Main.LevelCam.enabled = false;
		Main.CurrentLevel.SetActive(false);
		Destroy(Main.CurrentLevel);
		Main.SnapshotCam.Render();
		Main.ScreenFlash.color = new Vector4(1,1,1,1);
		Main.SnapShotMat.SetFloat("_Alpha",1);

		Main.ScreenQuad.material = Main.BWScreenMat;
		Main.PACE.Happening = true;
		Main.PACE.Clubbin = Clubbin;
		Main.Snapshot.enabled = true;
		GameObject Ace = Instantiate(AceMusic,transform.position,transform.rotation,Main.LevelHolder) as GameObject;
		Ace.GetComponent<AceVolumeFade>().Main = Main;
		Ace.GetComponent<AceVolumeFade>().AS.volume = Main.Volume;
		Main.AceMusicObject = Ace;
		Main.LevelMusic.pitch = 0;
		Destroy(other.gameObject);
		Destroy(gameObject);
	}


}
