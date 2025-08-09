using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCombat : MonoBehaviour {

	public GameObject World;
	public GameObject Combat;

	public TextMesh Text;

	public GameObject EC;

	public bool Mercy;

	public AttackButton AB;

	public DataHolder Main;

	public bool First;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {


		if(!Mercy)
		{
			Text.color = Vector4.one;
		}
		Mercy = false;




	}


	void OnMouseOver()
	{
		Mercy = true;
		Text.color = Color.yellow;

		if(Input.GetKeyDown(KeyCode.Mouse0))
		{

			World.SetActive(true);
			Combat.SetActive(false);

			EC.SetActive(false);

			int i = 0;
			while (i < AB.FlowerList.Length)
			{
				if(AB.FlowerList[i] != null)
				{
					Destroy(AB.FlowerList[i].gameObject);
				}
				i++;
			}




			if(Main.Progress == 3.1f)
			{

				Main.Progress = 3.14f;

				Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsSaysToStopOnBy);
				Main.UnlockedYellow = true;

			}



		}
	}
}
