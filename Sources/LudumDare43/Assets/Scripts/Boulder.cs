using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {




	public bool OnGround;

	public GameObject Visual;

	public Vector3 ThreeAxisVel;
	public float Gravity;
	public float Height;

	public GameObject Explosion;

	void Start()
	{

		ThreeAxisVel = new Vector3(Random.Range(-0.7f,0.7f),Random.Range(-0.4f,0.4f),Random.Range(1f,1.5f));

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

			GameObject Boom =Instantiate(Explosion,gameObject.transform.position,gameObject.transform.rotation)as GameObject;
			Boom.name = "SheepHurtbox";

			Destroy(gameObject);

		}

		ThreeAxisVel = new Vector3(ThreeAxisVel.x * 0.94f,ThreeAxisVel.y * 0.94f,ThreeAxisVel.z);

	}




}
