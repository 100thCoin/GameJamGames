using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour {

	public bool Quit;
	public bool Play;
	public bool Credits;
	public bool RTFromCreds;

	public GameObject TitleScreen;
	public GameObject Opening;


	public GameObject Camera;

	void OnMouseOver()
	{
		print(gameObject.name);

		if(Input.GetKeyDown(KeyCode.Mouse0))
		{

			if(Quit)
			{
				Application.Quit();
			}

			if(Credits)
			{
				Camera.transform.position = new Vector3(-32,0,-10);
			}

			if(RTFromCreds)
			{
				Camera.transform.position = new Vector3(0,0,-10);
			}


			if(Play)
			{
				Opening.SetActive(true);
				Destroy(TitleScreen);
			}





		}
		

	}
}
