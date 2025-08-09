using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningShot : MonoBehaviour {

	public float timer;
	public GameObject Main;
	public GameObject Musicc;
	public bool DoneMusic;
	public GameObject Cam;
	public GameObject Damsel;
	public GameObject Map;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(timer > 4.33f)
		{
			Damsel.SetActive(true);
		}
		if(timer > 13)
		{
			if(!DoneMusic)
			{
				DoneMusic = true;
				Instantiate(Musicc,gameObject.transform.position,gameObject.transform.rotation);
				Cam.transform.position = new Vector3(25,0,-10);
				Map.SetActive(false);
			}
				
		}
		if(timer > 17)
		{


			Main.SetActive(true);
			Destroy(gameObject);
		}
	}
}
