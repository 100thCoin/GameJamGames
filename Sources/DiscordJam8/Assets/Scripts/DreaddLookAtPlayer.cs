using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreaddLookAtPlayer : MonoBehaviour {

	public SpriteRenderer SR;

	void Start()
	{
		Global.DataHolder.AxeObject.GetComponent<BridgeCollapseSequence> ().Dreadd = SR;
		Global.DataHolder.AxeObject.GetComponent<BridgeCollapseSequence> ().DreaddAnim = GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update () {
		SR.flipX = Global.DataHolder.PMov.transform.position.x > transform.position.x;
	}
}
