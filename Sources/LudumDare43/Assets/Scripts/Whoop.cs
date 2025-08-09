using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whoop : MonoBehaviour {

	public float Size;

	public GameObject TopLeft;
	public GameObject BottomLeft;
	public GameObject BottomRight;
	public GameObject TopRight;

	public bool Grow;

	public bool Auto;
	public float AutoTimer;
	public GameObject SpecialDelivery;
	public GameObject Destroyyy;

	// Use this for initialization
	void Start () {

		Size = 4;

	}

	// Update is called once per frame
	void Update () {

		if(Auto)
		{
			AutoTimer += Time.deltaTime;

			if(GameObject.Find("Menu") != null)
			{

				GameObject.Find("Menu").GetComponent<AudioSource>().volume = 1- AutoTimer;

			}


			if(AutoTimer >= 1)
			{

				Auto = false;
				Grow = true;
				GameObject Main = Instantiate(SpecialDelivery,new Vector3(0,0,0),gameObject.transform.rotation) as GameObject;
				Main.name = "Game";
				gameObject.transform.position = new Vector3(11,1,-98);
				gameObject.transform.parent = GameObject.Find("Mary").gameObject.transform;
				Destroy(Destroyyy);
			}




		}








			if (Grow) {

			Size = Size + 10.7f * Time.deltaTime;

			} else {
				if (Size > 0) 
				{
				Size = Size - 10.7f * Time.deltaTime;
				}
				if (Size < 0)
				{
					Size = 0;

				}
			}



		gameObject.transform.localScale = new Vector3 (Size, Size, 1);

		TopLeft.gameObject.transform.position = new Vector3 (gameObject.transform.position.x - 16 + 4 * Size,gameObject.transform.position.y + 16 + 4 * Size,-98);
		TopRight.gameObject.transform.position = new Vector3 (gameObject.transform.position.x + 16 + 4 * Size,gameObject.transform.position.y + 16 - 4 * Size,-98);
		BottomRight.gameObject.transform.position = new Vector3 (gameObject.transform.position.x + 16 - 4 * Size,gameObject.transform.position.y - 16 - 4 * Size,-98);
		BottomLeft.gameObject.transform.position = new Vector3 (gameObject.transform.position.x - 16 - 4 * Size,gameObject.transform.position.y - 16 + 4 * Size,-98);



	}

}
