using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coreball : MonoBehaviour {

	public float Timer;

	public GameObject Vis;
	public GameObject VisHolder;

	public GameObject LookAway;

	public SpriteRenderer White;
	public SpriteRenderer Bubb;

	public GameObject Parts;

	public bool DoIt;

	public DataHolder Main;

	public Vector3 CamStart;
	public Vector3 CamDestt;


	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
	}
	
	// Update is called once per frame
	void Update () {

		if(DoIt)
		{
			Timer += Time.deltaTime;

			VisHolder.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(transform.position.y - LookAway.transform.position.y,transform.position.x - LookAway.transform.position.x) * Mathf.Rad2Deg);

			White.color = new Vector4(1,1,1,1-Timer);
			Bubb.color = new Vector4(1,1,1,Timer);

			float CamPos = 0;
			float CamDest = 2;
			float Duration = 1;

			float BubbMove = (((CamPos - CamDest) * Mathf.Pow(Timer,2))/Mathf.Pow(Duration,2) - ((2*CamPos - 2*CamDest) * Timer)/Duration + CamPos);

			Vis.transform.localPosition = new Vector3(BubbMove,0,0);
			Vis.transform.localScale = new Vector3(1/(1+BubbMove/2),1+BubbMove/2,1);

			Main.Cam.transform.position = new Vector3((CamStart.x * (1 - BubbMove/2f)) + (CamDestt.x * (BubbMove/2f)),(CamStart.y * (1 - BubbMove/2f)) + (CamDestt.y * (BubbMove/2f)),Main.Cam.transform.position.z);
			Main.Cam.Cam.orthographicSize = (8 * (1 - BubbMove/2f)) + (4 * (BubbMove/2f));

			if(Timer > 1)
			{
				Instantiate(Parts,transform.position,transform.rotation,transform.parent);
				Main.FadeWhiter.SetActive(true);
				Destroy(gameObject);
			}
		}
	}


	void OnTriggerStay(Collider other)
	{
		if(other.name == "Key")
		{
			Main.PseudoPause = true;
			CamDestt = transform.position;
			CamStart = Main.Cam.transform.position;
			LookAway = other.gameObject;
			DoIt = true;
		}


	}
}
