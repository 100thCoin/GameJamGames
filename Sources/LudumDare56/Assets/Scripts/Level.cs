using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public int starting_birds;
	public int starting_frogs;
	public int starting_rabbits;
	public Vector3 PlayerStartPos;
	public Transform RightBarrier;
	public int Index;
	public bool bossfight;
	// Use this for initialization
	void Start () {
		Global.Dataholder.CurrentLevel = this;
		Global.Dataholder.AHUD.Birds = starting_birds;
		Global.Dataholder.AHUD.Frogs = starting_frogs;
		Global.Dataholder.AHUD.Rabbits = starting_rabbits;
		if (!bossfight) {
			Global.Dataholder.CamMov.RoomSize = RightBarrier.transform.position.x -10;

		} else {
			Global.Dataholder.CamMov.RoomSize = 0;
			Global.Dataholder.CamMov.InLargeLevel = false;

		}
		Global.Dataholder.LevelIndex = Index; //for debugging
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}
