using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGame : MonoBehaviour {

	public GameObject Reward;
	public SpriteRenderer RewardSR;
	public Sprite RewardBlank;
	public Sprite RewardFull;

	public GameObject Bar;
	public GameObject BarHolder;

	public float Percent;

	public Vector3 SmoothPos;
	public Collectible Rew;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		int i = 0;
		int j = 0;
		while (i < Global.Dataholder.RibbonMan.Colliders.Length) {

			if (Global.Dataholder.RibbonMan.Colliders [i].InTheChaseGame) {
				j++;
			}

			i++;
		}

		Percent = (j+0f) / i;

		if (Percent >= 0.999f) {

			Vector3 RelativePlayerPos = Global.Dataholder.PlayerMov3D.HiddenPlayer.transform.position - transform.position;
			Vector3 RelativeRibbonEnd = Global.Dataholder.RibbonMan.Colliders [Global.Dataholder.RibbonMan.Colliders.Length - 1].transform.position - transform.position;


			Vector3 FunPos = new Vector3 (((transform.position.x - RelativePlayerPos.x) * 2 + (RelativeRibbonEnd.x + transform.position.x)) / 3f, transform.position.y + 3, ((transform.position.z - RelativePlayerPos.z) * 2 + (RelativeRibbonEnd.z + transform.position.z)) / 3f);



			SmoothPos = new Vector3 ((SmoothPos.x * 5 + FunPos.x) / 6f, (SmoothPos.y * 5 + FunPos.y) / 6f, (SmoothPos.z * 5 + FunPos.z) / 6f);
			if (Reward != null) {
				RewardSR.sprite = RewardFull;

				Reward.transform.position = SmoothPos;
				Rew.Inactive = false;
			}
		} else {
			Vector3 FunPos = transform.position + new Vector3 (0, 4, 0);
			SmoothPos = new Vector3((SmoothPos.x*5 + FunPos.x)/6f,(SmoothPos.y*5 + FunPos.y)/6f,(SmoothPos.z*5 + FunPos.z)/6f);
			if (Reward != null) {
				
				RewardSR.sprite = RewardBlank;
				Rew.Inactive = true;
				Reward.transform.position = SmoothPos;
			}
		}


		if (Bar != null) {

			Bar.transform.localScale = new Vector3 (Percent, 1, 1);
			Bar.transform.localPosition = new Vector3 (-0.5f + Percent * 0.5f, 0, -0.5f);
		} 

		if(Reward == null)
		{
			BarHolder.gameObject.SetActive (false);

		}

	}
}
