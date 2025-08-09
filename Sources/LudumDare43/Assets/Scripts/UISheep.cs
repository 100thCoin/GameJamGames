using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISheep : MonoBehaviour {

	public MaryController Mary;

	public int keynum;

	public Sprite Grey;
	public Sprite Green;

	public GameObject Count;
	public GameObject Sheep;

	public Sprite[] Sheeps;





	// Update is called once per frame
	void Update () {
	
		if(Mary.SheepType.Length >= keynum+1)
		{
			if(!Count.activeSelf)
			{
				gameObject.GetComponent<SpriteRenderer>().enabled = true;
				Count.SetActive(true);
				Sheep.SetActive(true);

				Sheep.GetComponent<SpriteRenderer>().sprite = Sheeps[Mary.SheepType[keynum]];


			}

			Count.GetComponent<TextMesh>().text = "" + Mary.SheepCount[keynum];

			if(Mary.DesiredSheepType ==  Mary.SheepType[keynum])
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = Green;

			}
			else
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = Grey;
			}

			if(Mary.SheepCount[keynum] == 0)
			{
				Count.GetComponent<TextMesh>().color = new Vector4(1,0,0,1);
			}
			else
			{
				Count.GetComponent<TextMesh>().color = new Vector4(1,1,1,1);

			}


		}





	}
}
