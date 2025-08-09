using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingChangesWillDropTheOrb : MonoBehaviour {

	public SpriteRenderer SR;
	public HUD_Dropdown HUD;

	public Sprite OrbWarning;

	public Sprite MSG_BirdMel;
	public Sprite MSG_BirdHar;
	public Sprite MSG_FrogMel;
	public Sprite MSG_FrogHar;
	public Sprite MSG_RabbitMel;
	public Sprite MSG_RabbitHar;

	public Sprite MSG_ORB;

	public int Hoverride;

	public bool GoalText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GoalText || (Global.Dataholder.AHUD.prevtotal) == 0) {
			SR.sprite = null;
		}

		if (!GoalText && Global.Dataholder.PlayerIsCarryingGoal) {
			SR.sprite = OrbWarning;
		} else {

			if (Hoverride == -1 && !GoalText) {
				if (Global.Dataholder.CurrentHudSelectionAnimalType == 0) {
					if (Global.Dataholder.CurrentHudSelectionHarmony) {
						SR.sprite = MSG_BirdHar;
					} else {
						SR.sprite = MSG_BirdMel;
					}
				} else if (Global.Dataholder.CurrentHudSelectionAnimalType == 1) {
					if (Global.Dataholder.CurrentHudSelectionHarmony) {
						SR.sprite = MSG_FrogHar;
					} else {
						SR.sprite = MSG_FrogMel;
					}
				} else if (Global.Dataholder.CurrentHudSelectionAnimalType == 2) {
					if (Global.Dataholder.CurrentHudSelectionHarmony) {
						SR.sprite = MSG_RabbitHar;
					} else {
						SR.sprite = MSG_RabbitMel;
					}
				} else {
					SR.sprite = null;
				}
			}
			else {
				switch (Hoverride) {
				case 0:
					SR.sprite = MSG_BirdMel;
					break;
				case 1:
					SR.sprite = MSG_BirdHar;
					break;
				case 2:
					SR.sprite = MSG_FrogMel;
					break;
				case 3:
					SR.sprite = MSG_FrogHar;
					break;
				case 4:
					SR.sprite = MSG_RabbitMel;
					break;
				case 5:
					SR.sprite = MSG_RabbitHar;
					break;
				case 6:
					SR.sprite = MSG_ORB;
					break;

				}


			}

		}
		SR.color = new Vector4 (1, 1, 1, HUD.Timer);
		Hoverride = -1;
	}
}
