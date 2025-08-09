using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMain : MonoBehaviour {

	public GameObject Title;
	public GameObject Gameplay;

	public GameObject TitlePrefab;
	public GameObject GameplayPrefab;

	public void LoadTitle()
	{

		Destroy (Gameplay);
		Title = Instantiate (TitlePrefab, new Vector3 (0, 0, 0), transform.rotation);

	}

	public void LoadGameplay()
	{

		Destroy (Title);
		Gameplay = Instantiate (GameplayPrefab, new Vector3 (0, 0, 0), transform.rotation);

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
