using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginGame : MonoBehaviour {

	public bool DoOnce;

	void OnMouseOver () {
		if(Input.GetKeyDown(KeyCode.Mouse0) && !DoOnce)
		{
			DoOnce = true;
			GameObject Cam =GameObject.Find("Main Camera").gameObject;

			if(	Cam.gameObject.GetComponent<CameraMover>().EventNum == 7)
			{
				Cam.gameObject.GetComponent<CameraMover>().EventNum=-1;

			}
			GameObject.Find("InGame").gameObject.transform.FindChild("Music").gameObject.SetActive(true);
			Cam.GetComponent<CameraMover>().EventNum++;


			gameObject.transform.parent.parent.gameObject.GetComponent<Whoop>().Grow = true;
			gameObject.transform.parent.parent.gameObject.transform.position = new Vector3(0,0,-5);

			Destroy(gameObject.transform.parent.gameObject);
		}


	}
}
