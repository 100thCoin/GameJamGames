using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterScanGun : MonoBehaviour {

	public Vector3 Offset;
	public Camera Cam;
	public MoveChar Char;
	public SpriteRenderer SR;
	public Sprite Regular;
	public Sprite Beep;
	public float beepTimer;

	public GameObject BeepSFX;

	public float Delay;

	public TextMesh MessageAboutCloset;

	// Update is called once per frame
	void Update () {
		Delay -=Time.deltaTime;
		SR.sprite = beepTimer > 0 ? Beep : Regular;
		beepTimer -= Time.deltaTime;

		if(Char.AtCheckout)
		{
			Vector3 mousepos = Cam.ScreenToWorldPoint(Input.mousePosition);
			transform.position = mousepos + Offset;
			

		
		
		}
	}

	public void Scan()
	{
		if(Delay <0)
		{
			beepTimer = 0.25f;
			Instantiate(BeepSFX,transform.position,transform.rotation);
			Char.Customer.HappyLeave = true;
			Char.Customer.HappyLeaveTimer = 0;
			Delay = 1;
			Char.Customer.WaitingForYou = false;
			if(!Char.ClearedTutorial)
			{
				//MessageAboutCloset.gameObject.SetActive(true);
			}

			Char.ClearedTutorial = true;
			Char.DelayBeforeQuake = 2;
			Char.AssignTextT("");

		}
	}


}
