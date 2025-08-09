using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite RegularRoom;
	public Sprite Alt;


	public bool ClosetBroom;
	public bool ClosetWardrobe;
	public bool ClosetDoor;
	public bool RoomDoor;
	public bool Register;

	public bool Grace;

	public MoveChar Char;
	public TextMesh Error;
	public float ErrorTimer;

	void Update()
	{

		if(Char.DayFailCollapse || Char.FailReview)
		{
			return;
		}

		ErrorTimer -=Time.deltaTime;
		if(Error != null)
		{
			Error.gameObject.SetActive(ErrorTimer >0);
		}

		if(SR.sprite == Alt)
		{
			// press W to interact

			if(Input.GetKeyDown(KeyCode.W))
			{

				if(ClosetBroom)
				{

					// if witch
					if(Char.Witch)
					{
						Char.MountBroom();
					}
					else
					{
						ErrorTimer = 4;
					}
				}
				else if(ClosetWardrobe)
				{
					if(!Char.InWardrobe)
					{
						Char.InWardrobe = true;
						Char.Cam.transform.position = new Vector3(11,244,-50);
						Char.WardMan.GenerateRandomWardrobe();
					}
				}
				else if(ClosetDoor)
				{
					if(!Char.Witch)
					{
						Char.DesignatedAltitude = 298;
						Char.MainRoom = true;
					}
					else
					{
						ErrorTimer = 4;
					}
				}
				else if(RoomDoor)
				{
					if(Char.ClearedTutorial)
					{
						Char.DesignatedAltitude = 212;
						Char.MainRoom = false;
						Char.Cam.transform.position = new Vector3(11,214,-50);
						if(!Char.BuildingIsCollapsing)
						{
							Char.BuildingIsCollapsing = true;
							Char.Screenshake = 0.75f;
						}
						if(!Char.ClearedTutorial2)
						{
							Char.AssignTextT("Hop on the magic broom.");
						}
					}
				}
				else if(Register)
				{
					if(!Char.AtCheckout)
					{
						Char.AtCheckout = true;
						Char.Cam.transform.position = new Vector3(-14,350,-50);
					}
				}
			}
		}
	}


	void FixedUpdate()
	{


		if(!Grace)
		{
			SR.sprite = RegularRoom;
		}
		Grace = false;
	}


	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			if(RoomDoor)
			{
				if(Char.ClearedTutorial)
				{
					SR.sprite = Alt;
					Grace = true;
				}

			}
			else
			if(other.transform.parent.GetComponent<MoveChar>().OnFeet)
			{
				SR.sprite = Alt;
				Grace = true;
			}
		}
	}



}
