using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeenyTheKey : MonoBehaviour {

	// make the key go speeen

	public Transform Vis;
	public float speed;

	public float PlayerPrevPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float playerXPos = Global.Dataholder.PlayerMov2D.transform.position.x;

		if (PlayerPrevPos > transform.position.x != playerXPos > transform.position.x) {

			float PlayerSpeed = Global.Dataholder.PlayerMov2D.CurrentSpeed;
			if (Global.Dataholder.PlayerMov2D.transform.position.y > transform.position.y) {
				PlayerSpeed *= -1;
			}
			speed = PlayerSpeed;
		}
		PlayerPrevPos = playerXPos;
		speed *= 0.985f;

		Vis.transform.eulerAngles += new Vector3 (0, 0, speed * Time.deltaTime * 40);

	}
}
