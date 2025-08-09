using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Snowman : MonoBehaviour {

	public GameObject Gore;

	public GameObject Snowball;

	public bool Snowballing;
	public float Timer;

	public int Dir;

	public bool HighBall;

	void Update()
	{
		if(Snowballing)
		{
			Timer+= Time.deltaTime;
			if(Timer > 1.5f)
			{
				if(!HighBall)
				{
					GameObject Snowballll = Instantiate(Snowball,gameObject.transform.position + new Vector3 (2*Dir,1,0),transform.rotation,transform.parent);
					Snowballll.GetComponent<Rigidbody>().velocity = new Vector3(Dir*10,5,0);
				}
				else
				{
					GameObject Snowballll = Instantiate(Snowball,gameObject.transform.position + new Vector3 (2*Dir,2,0),transform.rotation,transform.parent);
					Snowballll.GetComponent<Rigidbody>().velocity = new Vector3(Dir*7,15,0);
				}

				HighBall = !HighBall;

				Timer -= 1.5f;
			}


		}
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Killzone" || other.tag == "EnemyDeath")
		{
			Instantiate(Gore,new Vector3(transform.position.x,transform.position.y-1,0.1f),Gore.transform.rotation,transform.parent);

			Destroy(gameObject);
		}
	}
}
