using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject UnloadChild;
	public GameObject SolidChild;
	public GameObject LevelBounds;
	public EntranceSlot Entrance;

	public GameObject TV;
	public int TVId;
	public Vector2 SpawnPos;
	public bool DEBUG;

	// Use this for initialization
	void Start () {
		if (DEBUG) {
			print ("DEBUG SET UP ROOM");
			Happen ();
		}

	}

	public void Happen()
	{
		UnloadChild.transform.parent = Global.DataHolder.UnloadThisDuringShiftMode.transform;
		SolidChild.transform.parent = Global.DataHolder.DontUnloadThisDuringShiftMode.transform;
		Global.DataHolder.CurrentLoadedToBeUnloaded = UnloadChild;
		Global.DataHolder.CurrentLoadedLevel = SolidChild;
		Global.DataHolder.SpawnPos = SpawnPos;
		Global.DataHolder.ScreenBounds = LevelBounds;
		if (DEBUG) {
			Entrance.SpawnPlayer = true;
			Entrance.Level1Delay = true;

			Global.DataHolder.TVHold.ReplaceTV (1, TV);

			Destroy (gameObject);
		}
	}

}
