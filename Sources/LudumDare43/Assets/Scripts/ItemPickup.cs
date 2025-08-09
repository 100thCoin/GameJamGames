using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	public GameObject PickupText;

	public int HowMuch;
	public bool Gold;
	public bool Lamb;
	public bool Upgrade;


	public bool OnGround;

	public GameObject Visual;

	public Vector3 ThreeAxisVel;
	public float Gravity;
	public float Height;

	public MaryController Mary;

	// Use this for initialization
	void Start () {
		gameObject.transform.parent = GameObject.Find("Game").gameObject.transform;


		Mary = GameObject.Find("Mary").GetComponent<MaryController>();
	
		ThreeAxisVel = new Vector3(Random.Range(-0.3f,0.3f),Random.Range(-0.3f,0.3f),Random.Range(0.2f,0.3f));

	}

	void FixedUpdate()
	{

			gameObject.transform.position = new Vector3(gameObject.transform.position.x + ThreeAxisVel.x, gameObject.transform.position.y + ThreeAxisVel.y,-2);

			ThreeAxisVel = new Vector3(ThreeAxisVel.x,ThreeAxisVel.y,ThreeAxisVel.z - Gravity);

			Height = Height + ThreeAxisVel.z;

		Visual.gameObject.transform.localPosition = new Vector3(0,Height,0);

			if(Height <= 0)
			{
			Visual.gameObject.transform.localPosition = new Vector3(0,0,0);

			}

			ThreeAxisVel = new Vector3(ThreeAxisVel.x * 0.94f,ThreeAxisVel.y * 0.94f,ThreeAxisVel.z);

	}


	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.name == "Mary")
		{
			if(Gold)
			{
				other.GetComponent<MaryController>().Gold += HowMuch;
			}
			else if(Lamb)
			{
				other.GetComponent<MaryController>().LambMeat += HowMuch;
			}
			else
			{
				// i donno yet

				if(HowMuch == 0)
				{
					other.GetComponent<MaryController>().HasKey = true;
				}
				else if(HowMuch == 1)
				{
					other.GetComponent<MaryController>().HP++;
				}
			}


			Instantiate(PickupText,gameObject.transform.position,gameObject.transform.rotation);

			Destroy(gameObject);


		}




	}



	void Update()
	{

		if(Mary.Callsheep)
		{
			Destroy(gameObject);

		}

	}

	void LateUpdate()
	{
		if(Mary.Callsheep)
		{
			Destroy(gameObject);

		}
	}

}
