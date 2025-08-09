using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepDetector : MonoBehaviour {

	public bool DetectingDesiredSheep;
	public MaryController Mary;

	public GameObject OohPickMe;
	public bool GraceFrame;

	void FixedUpdate()
	{

		if(!GraceFrame)
		{
		DetectingDesiredSheep = false;
		OohPickMe = null;
		}

		if(GraceFrame)
		{
			GraceFrame = false;
			if(OohPickMe == null)
			{
				DetectingDesiredSheep = false;
				OohPickMe = null;
			}

		}
	}


	void OnTriggerStay(Collider other)
	{

		if(other.gameObject.name == "SheepType_" + Mary.DesiredSheepType)
		{
			OohPickMe = other.gameObject;

			if(OohPickMe.GetComponent<AI_Sheep>().DeathTimer != 0)
			{
				return;
			}

			DetectingDesiredSheep = true;
			GraceFrame = true;
		}




	}



}
