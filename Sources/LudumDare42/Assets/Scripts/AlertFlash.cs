using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertFlash : MonoBehaviour {

	public bool Appear;
	public float Scale;
	public float AppearTimer;
	public Vector3 Pos;
	public float ColorTimer;
	public GameObject Asteroid;
	public bool DoneIt;
	public bool Decor;
	// Use this for initialization
	void Start () {
		Pos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Appear)
		{
			AppearTimer+=Time.deltaTime * 3;
			Scale = 0 - Mathf.Pow(2 * AppearTimer -1,2) + 1 + AppearTimer * 0.25f;
			if(AppearTimer >= 0.71f)
			{
				Scale =1;
				AppearTimer = 1;
				Appear = false;

			}
		}
		gameObject.transform.localScale = new Vector3(1,Scale,1);
		gameObject.transform.position = new Vector3(Pos.x,Pos.y + Scale,Pos.z);
		ColorTimer += Time.deltaTime * 4;

		gameObject.GetComponent<SpriteRenderer>().color =
			new Vector4(
				1,
				Mathf.Cos(ColorTimer),
				Mathf.Cos(ColorTimer),
				1);

		if(ColorTimer > 12 && !DoneIt)
		{
			DoneIt = true;
			Instantiate(Asteroid,new Vector3 (gameObject.transform.position.x,gameObject.transform.position.y - 10,gameObject.transform.position.z),gameObject.transform.rotation);
		}
		if(ColorTimer > 12.5f)
		{
			Destroy(gameObject);
		}


	}
}
