using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour {

	public bool IgnoreGround2;
	public List<Collider2D> cols; // gets cleaerd in playerController.FixedUpdate()
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Ground" || other.tag == "Ground2" && !IgnoreGround2) {
			if (!other.isTrigger) {
				return;
			}
			bool alreadyExists = false;
			for(int i = 0; i < cols.Count; i++)
			{
				if (cols [i].gameObject == other.gameObject) {
					alreadyExists = true;
					break;
				}
			}

			if (!alreadyExists) {
				cols.Add (other); 
			}
		}
	}
}
