using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownFolk : MonoBehaviour {


	public GameObject StoreSD1;
	public GameObject StoreSD2;

	public GameObject Dialogue;


	public bool SeeMary;
	public bool GraceFrame;


	public int NPCType;

	public GameObject SusMeat;
	public GameObject LambMeat;
	public GameObject BigLambMeat;


	void FixedUpdate()
	{

		if(!GraceFrame)
		{
			SeeMary = false;
			Dialogue.SetActive(false);
		}
		else
		{
			GraceFrame = false;
		}



	}


	void Update()
	{







	}


	void OnTriggerStay(Collider other)
	{

		if(other.gameObject.name == "Mary")
		{

			//Mary = other.gameObject.GetComponent<MaryController>();
			SeeMary = true;
			GraceFrame = true;
			Dialogue.SetActive(true);



		}

		if(other.gameObject.name == "ThrowBox")
		{
			if(NPCType == 0)
			{
			Instantiate(SusMeat,gameObject.transform.position,gameObject.transform.rotation);
			}

			if(NPCType == 1)
			{
				Instantiate(SusMeat,gameObject.transform.position,gameObject.transform.rotation);

				Instantiate(LambMeat,gameObject.transform.position,gameObject.transform.rotation);
				Instantiate(LambMeat,gameObject.transform.position,gameObject.transform.rotation);
				Instantiate(LambMeat,gameObject.transform.position,gameObject.transform.rotation);
				Instantiate(LambMeat,gameObject.transform.position,gameObject.transform.rotation);
				Instantiate(LambMeat,gameObject.transform.position,gameObject.transform.rotation);
				Instantiate(LambMeat,gameObject.transform.position,gameObject.transform.rotation);
				Instantiate(LambMeat,gameObject.transform.position,gameObject.transform.rotation);


				Instantiate(BigLambMeat,gameObject.transform.position,gameObject.transform.rotation);
				Instantiate(BigLambMeat,gameObject.transform.position,gameObject.transform.rotation);

			}

			if(NPCType == 2)
			{
				Instantiate(SusMeat,gameObject.transform.position,gameObject.transform.rotation);

				Instantiate(LambMeat,gameObject.transform.position,gameObject.transform.rotation);
				Instantiate(LambMeat,gameObject.transform.position,gameObject.transform.rotation);

				Instantiate(BigLambMeat,gameObject.transform.position,gameObject.transform.rotation);


			}


			StoreSD1.SetActive(true);
			StoreSD2.SetActive(true);



			Destroy(gameObject.transform.parent.gameObject);

		}


	}
}
