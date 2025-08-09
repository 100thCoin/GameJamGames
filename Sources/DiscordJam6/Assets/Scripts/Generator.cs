using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {


	public GameObject[] Generatables;

	public GameObject[] GenerateOnce;

	public int WieViele;

	public Transform Map;

	// Use this for initialization
	void Start () {

		int i = 0;
		Vector2 Pos = new Vector2(0,0);
	
		while(i < GenerateOnce.Length)
		{
			Pos = RandomV2();
			Instantiate(GenerateOnce[i],Pos,transform.rotation,transform);
			i++;
		}

		i = 0;
		int RNG = 0;
		while(i < WieViele)
		{
			Pos = RandomV2();
			RNG = Random.Range(0,Generatables.Length);
			Instantiate(Generatables[RNG],Pos,transform.rotation,transform);
			i++;
		}




	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Vector2 RandomV2()
	{
		Vector2 Temp = new Vector2(0,0);

		int X = Random.Range(-100,100);
		int Y = Random.Range(-100,100);

		Temp = new Vector2(X,Y);

		while(Temp.magnitude < 35 || Temp.magnitude > 95)
		{
			X = Random.Range(-100,100);
			Y = Random.Range(-100,100);

			Temp = new Vector2(X,Y);
		}


		return Temp;
	}


}
