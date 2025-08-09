using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public bool AtDoor;
	public DataHolder Main;
	public SpriteRenderer FadeToBack;

	public CameraMove Cam;
	public MoveCharacter P;
	public bool GoesIndoors;

	public Vector3 TransformOffset;
	public float newCamHeight;

	public float DoorTimer;
	public bool Active;
	public bool DoorOnce;

	// Use this for initialization
	void Start () {
		
	}

	void Update()
	{
		if(AtDoor && Input.GetKeyDown(KeyCode.W) && !Main.FreezeUntilFalse)
		{
			Active = true;
			Main.FreezeUntilFalse = true;
		}

		if(Active)
		{
			DoorTimer += Time.deltaTime*2;
			FadeToBack.color = new Vector4(0,0,0,DoorTimer*3);
			if(DoorTimer > 0.5f)
			{
				FadeToBack.color = new Vector4(0,0,0,3-DoorTimer*3);

				if(!DoorOnce)
				{
					DoorOnce = true;
					Cam.StartPos = new Vector3(Cam.StartPos.x,newCamHeight,Cam.StartPos.z);
					P.transform.position += TransformOffset;
					Cam.transform.position += TransformOffset;
					Cam.Inside = GoesIndoors;
					P.Indoors = GoesIndoors;
				}
				if(DoorTimer >= 1)
				{
					Active = false;
					DoorOnce = false;
					Main.FreezeUntilFalse = false;
					DoorTimer = 0;
				}
			}
		}

	}

	// Update is called once per frame
	void FixedUpdate () {

		AtDoor = false;
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag =="Player")
		{
			AtDoor = true;
		}

	}

}
