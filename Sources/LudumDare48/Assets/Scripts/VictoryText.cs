using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryText : MonoBehaviour {

	public string PlayerName;
	public float FinalTime;

	public float Timer;

	public TextMesh[] Name;
	public TextMesh[] Speedrun;

	public GameObject ParentWin;
	public GameObject ParentName;
	public GameObject ParentSpeed;
	public GameObject ParentQuit;

	public GameObject EntireEndSequence;
	public GameObject Title;

	public SpriteRenderer Menu;

	// Use this for initialization
	void Start () {

		int i = 0;
		string playername = "";
		if(PlayerName != "")
		{
			playername = "Great job, " + PlayerName + "!";
		}

		//convert seconds to minutes and seconds.

		//this is super jank, but it's a game jam and I'm running out of time

		int minutes = 0;
		while(FinalTime > 60)
		{
			FinalTime-=60;
			minutes++;
		}

		FinalTime = Mathf.Round(FinalTime*100f)/100f;

		string FinalTimerString = "" + FinalTime;
		if(FinalTime < 10)
		{
			FinalTimerString = "0" + FinalTimerString;
		}

		string speedrunText = "Speedrun Time: " + minutes + ":" + FinalTimerString;

		while(i < Name.Length)
		{
			Name[i].text = playername;
			Speedrun[i].text = speedrunText;
			i++;
		}

	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;

		if(Timer > 6)
		{
			Menu.enabled = true;
		}

		if(Timer > 8)
		{
			ParentWin.SetActive(true);
		}

		if(Timer > 9)
		{
			ParentName.SetActive(true);
		}

		if(Timer > 12)
		{
			ParentSpeed.SetActive(true);
		}

		if(Timer > 14)
		{
			ParentQuit.SetActive(true);
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				//

				Instantiate(Title,new Vector3(0,0,0),transform.rotation);
				Destroy(EntireEndSequence.gameObject);
			}


		}

	}
}
