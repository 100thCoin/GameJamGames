using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeControl : MonoBehaviour {

	public DataHolder Aud;

	public AudioSource aud;

	void Update () {
		aud.volume = Aud.Volume;
	}
}
