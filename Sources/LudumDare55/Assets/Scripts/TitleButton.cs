using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite On;
	public Sprite Off;
	public GameObject Camera;
	public int over;

	public bool Play;
	public bool Credits;
	public bool Return;
	public bool Quit;

	public bool MouseOverPlay;
	public SpriteRenderer Playwell;
	public float playwellTimer;

	public TitleManager TitleMan;
	public float PlayDelay = 1;
	public SpriteRenderer TitleCover;
	public float PressedPlayTimer = -1;
	public GameObject TitleObject;
	public GameObject KeyREbinder;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
		if (Play) {
			
			if (MouseOverPlay) {
				Playwell.sprite = On;
				playwellTimer += Time.deltaTime * 4;
				playwellTimer = Mathf.Clamp01 (playwellTimer);
				Playwell.transform.position = new Vector3 (Playwell.transform.position.x, DataHolder.ParabolicLerp (-41, -39, playwellTimer, 1), 0);
			} else {
				Playwell.sprite = Off;
				playwellTimer -= Time.deltaTime * 4;
				playwellTimer = Mathf.Clamp01 (playwellTimer);
				Playwell.transform.position = new Vector3 (Playwell.transform.position.x, DataHolder.ParabolicLerp (-39, -41, 1 - playwellTimer, 1), 0);

			}

			if (TitleMan.TitleLockedWePlaying) {
				PlayDelay -= Time.deltaTime;
				PlayDelay = Mathf.Clamp01 (PlayDelay);
				Super.Dataholder.MusicMultiplier = PlayDelay;

				Playwell.transform.position = new Vector3 (Playwell.transform.position.x, DataHolder.ParabolicLerp (0, -39, PlayDelay, 1), 0);
				PressedPlayTimer += Time.deltaTime;
				TitleCover.color = new Vector4 (0, 0, 0, PressedPlayTimer);
				if (PressedPlayTimer > 1) {
					Super.Dataholder.LoadKeyRebinder ();
					//KeyREbinder.SetActive (true);
					Destroy (TitleObject);
				}
			}


		}
		MouseOverPlay = false;
		if (!Play) {
			if (TitleMan.TitleLockedWePlaying) {
				return;
			}

			SR.sprite = Off;
			over--;
		}
	}


	// Update is called once per frame
	void LateUpdate () {
		if (TitleMan.TitleLockedWePlaying) {
			return;
		}
		if (!Play) {
			
			if (over > 0) {
				SR.sprite = On;
			}
		}
	}

	void OnMouseOver()
	{
		if (TitleMan.TitleLockedWePlaying) {
			return;
		}
		if (Play) {
			MouseOverPlay = true;
		}

		over = 2;
		if (Input.GetKeyDown (KeyCode.Mouse0)) {

			if (Play) {
				TitleMan.TitleLockedWePlaying = true;


			}
			if (Credits) {
				Camera.transform.position = new Vector3 (0, 38, -10);
			}
			if (Return) {
				Camera.transform.position = new Vector3 (0, 0, -10);
			}
			if (Quit) {
				Application.Quit ();
			}


		}

	}

}
