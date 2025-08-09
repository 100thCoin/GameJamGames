using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour {

	public bool Bird;
	public bool Frog;
	public bool Rabbit;
	public int GroupIndex;

	// Use this for initialization
	void Awake () {
		if (Global.Dataholder != null) {
			if (Bird) {
				Global.Dataholder.BirdCage = gameObject;
			}
			if (Frog) {
				Global.Dataholder.FrogCage = gameObject;
			}
			if (Rabbit) {
				Global.Dataholder.RabbitCage = gameObject;
			}
			Global.Dataholder.CageGroup = GroupIndex;
		}
	}
	void Start () {
		if (Bird) {
			Global.Dataholder.BirdCage = gameObject;
		}
		if (Frog) {
			Global.Dataholder.FrogCage = gameObject;
		}
		if (Rabbit) {
			Global.Dataholder.RabbitCage = gameObject;
		}
		Global.Dataholder.CageGroup = GroupIndex;

	}
}
