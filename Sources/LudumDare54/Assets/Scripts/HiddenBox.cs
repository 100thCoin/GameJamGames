using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBox : MonoBehaviour {

	public bool Collected;
	public GameObject Reward;
	public float timer;
	public bool Hit;
	public CubicBezierCurve Curve;
	public GameObject Vis;
	public GameObject Col;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Hit) {

			timer += Time.deltaTime;

			if (timer > 1) {
				timer = 1;
			}
			if (Reward != null) {
				Reward.SetActive (true);
				Reward.transform.position = Curve.GetPoint (timer);
			}
			Vis.SetActive (false);
			Col.SetActive (false);
		}



	}

	void OnTriggerStay(Collider other)
	{

		if(other.CompareTag("Player"))
		{
			Hit = true;
			Global.Dataholder.PlayerMov3D.RB.velocity = new Vector3 (Global.Dataholder.PlayerMov3D.RB.velocity.x, 0, Global.Dataholder.PlayerMov3D.RB.velocity.z);

		}

	}
}
