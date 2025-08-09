using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShifter : MonoBehaviour {

	public bool OneTimeUse;
	public bool DoneOnce;
	public SpriteRenderer SR;
	public SpriteRenderer SRAlt;
	public Sprite Deactive;
	public Sprite DeactiveAlt;

	void OnTriggerEnter(Collider other)
	{
		if (DoneOnce) {
			return;
		}

		if (other.tag == "Bomb") {

			Global.DataHolder.ScreenShiftCursor.SetActive (true);
			Global.DataHolder.ScreenShiftCursor.transform.position = new Vector3 (transform.position.x, transform.position.y, -15);
			Global.DataHolder.ActivateScreenShiftMode ();

			Instantiate (Global.DataHolder.Player.ExplosionPrefab, other.transform.position, transform.rotation, transform.parent.parent);
			//SFX isnt here yet, so...
			if (Global.DataHolder.Player.BombExplosion_SFX != null) {
				Instantiate (Global.DataHolder.Player.BombExplosion_SFX, transform.position, transform.rotation);
			}

			Destroy (other.gameObject);

			if (OneTimeUse) {
				SR.sprite = Deactive;
				SRAlt.sprite = DeactiveAlt;
				DoneOnce = true;
			}

		}
	}

}
