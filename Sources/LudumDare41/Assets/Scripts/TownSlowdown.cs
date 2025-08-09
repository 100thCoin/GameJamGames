using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownSlowdown : MonoBehaviour {
	public bool DoneOnce;

	public GameObject Forest;
	public GameObject Dragon;
	public AudioClip DragonMusic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(GameObject.Find("Main Camera").gameObject.transform.position.x > gameObject.transform.position.x - 30 && !DoneOnce)
		{
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ParallaxMult -= Time.deltaTime*0.25f;


			if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ParallaxMult < 0.5f)
			{
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ParallaxMult = 0.5f;
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town += 1;
				if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().FastMode)
					{
						GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town += 1;
						
					}

				if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town < 10)
				{
				Instantiate(Forest,new Vector2(gameObject.transform.position.x+152,gameObject.transform.position.y),gameObject.transform.rotation,gameObject.transform.parent.transform);
				}
				else
				{
					Instantiate(Dragon,new Vector2(gameObject.transform.position.x+152,gameObject.transform.position.y),gameObject.transform.rotation,gameObject.transform.parent.transform);
					GameObject.Find("Music").gameObject.GetComponent<AudioSource>().clip = DragonMusic;
					GameObject.Find("Music").gameObject.GetComponent<AudioSource>().Stop();
					GameObject.Find("Music").gameObject.GetComponent<AudioSource>().Play();
				}
			DoneOnce = true;
			}
		}

		if(GameObject.Find("Main Camera").gameObject.transform.position.x > gameObject.transform.position.x + 30)
		{
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ParallaxMult += Time.deltaTime * 0.25f;


			if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ParallaxMult > 1)
			{
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ParallaxMult = 1;
				Destroy(this);
			}
		}


	}
}