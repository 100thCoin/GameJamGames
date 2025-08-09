using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDragonRoom : MonoBehaviour {

	public bool Unload;
	public float UnloadTimer;
	public bool Load;
	public float LoadTimer;

	public SpriteRenderer FadeBlack;

	bool ClearFade = false;
	public float clearfadeTimer;

	public Dataholder Main;

	public GameObject UIGun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Load)
		{
			LoadTimer += Time.deltaTime;
			FadeBlack.color = new Vector4(0,0,0,LoadTimer*0.5f);

			if(LoadTimer > 3)
			{
				Main.MainMap.SetActive(false);
				Main.DragonMap = Instantiate(Main.DragonMapPrefab,new Vector3(0,0,0),transform.rotation,transform.parent.parent);
				ClearFade = true;
				clearfadeTimer = 1;
				Load = false;
				LoadTimer = 0;

				Main.Cam.DragonArenaMode = true;
				Main.Char.CanShoot = true;
				UIGun.SetActive(true);
				Main.Char.FinalTownName = Main._TownName.FullTownName.Replace("\n","");
				Main.Char.Health = 3;
				Main.Char.UpdateGunName();
				Main.Dead = false;
			}
		}

		if(Unload)
		{
			UnloadTimer += Time.deltaTime;
			FadeBlack.color = new Vector4(0,0,0,UnloadTimer*0.5f);

			if(UnloadTimer > 2)
			{
				Destroy(Main.DragonMap);
				Main.MainMap.SetActive(true);

				ClearFade = true;
				clearfadeTimer = 1;
				Unload = false;
				UnloadTimer = 0;

				Main.Cam.DragonArenaMode = false;
				Main.Char.CanShoot = false;
				UIGun.SetActive(false);
				Main.Char.FinalTownName = Main._TownName.FullTownName.Replace("\n","");
				Main.Char.Health = 3;
				Main.Char.UpdateGunName();
				Main.MainMusic.pitch = 1;
				Main.CantEtnerDragonRoomNow = false;
				Main.Char.transform.position = new Vector3(0,0,0);
				Main.Char.SR.enabled = true;
				Main.Paused = false;
				Main.Char.WaitForShotOnDragon = false;
			}

		}

		if(ClearFade)
		{
			clearfadeTimer -= Time.deltaTime;
			FadeBlack.color = new Vector4(0,0,0,clearfadeTimer);
			if(clearfadeTimer < 0)
			{
				ClearFade = false;
			}
		}


	}
}
