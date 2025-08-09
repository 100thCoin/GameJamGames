using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundMatManagerForTransition : MonoBehaviour {

	public Material BoundsNextLevel;
	public Material BoundsThisLevel;

	public void AssignNextLevelMat()
	{
		int i = 0;
		while (i < transform.childCount) {
			transform.GetChild (i).gameObject.GetComponent<Renderer> ().material = BoundsNextLevel;
			i++;
		}
	}
	public void AssignThisLevelMat()
	{
		int i = 0;
		while (i < transform.childCount) {
			transform.GetChild (i).gameObject.GetComponent<Renderer> ().material = BoundsThisLevel;
			i++;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
