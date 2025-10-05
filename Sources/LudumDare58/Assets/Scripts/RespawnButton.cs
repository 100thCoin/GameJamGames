using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnButton : MonoBehaviour {


	public int hoverGrace;
	public SpriteRenderer SR;
	public Sprite Hover;
	public Sprite NoHover;

	public GameObject Voice_DejaVu;

	public Room TargetRoom;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		if (hoverGrace > 0) {
			SR.sprite = Hover;

			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				Global.Dataholder.MainCamera.transform.position = new Vector3 (TargetRoom.transform.position.x, TargetRoom.transform.position.y, Global.Dataholder.MainCamera.transform.position.z);
				Global.Dataholder.ActiveRoom = TargetRoom;
				Instantiate (Voice_DejaVu, transform.position, transform.rotation, transform.parent);
				Global.Dataholder.ScreenFlashTimer = 0.75f;
				Global.Dataholder.ArbitraryChecks [10] = true;
				Global.Dataholder.PlayerDead = false;
			}

		} else {
			SR.sprite = NoHover;
		}

		hoverGrace--;
	}
	void OnMouseOver()
	{
		hoverGrace = 2;
	}
}
