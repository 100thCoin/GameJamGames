using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Subtitle
{
	public string Text;
	public float TimeIndex;
}

[System.Serializable]
public class SubtitleSet
{
	public Subtitle[] Titles;
	public float FinalMessageDuration;
}



public class Subtitles : MonoBehaviour {

	public int SubIndex;
	public SubtitleSet SubtitleSet;
	public bool Active;
	public float Timer;
	public TextMesh[] TMs;
	string ActiveMessage; // used entirely for the !Active message clear so it happens just once.

	public void Initialize(SubtitleSet NewTitles)
	{
		SubIndex = 0;
		Timer = 0;
		Active = true;
		SubtitleSet = NewTitles;
		WriteAllTextMeshes ("");
		ActiveMessage = "";
	}

	public void Clear()
	{
		SubIndex = 0;
		Timer = 0;
		Active = false;
		SubtitleSet = null;
		WriteAllTextMeshes ("");
		ActiveMessage = "";
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Active && !SuperMain.M.DisableSubtitles) {
			Timer += Time.deltaTime;
			if (SubIndex == -1) {
				//post final message.
				if (Timer > SubtitleSet.FinalMessageDuration) {
					Active = false;
					Global.DataHolder.RemoveCurrentVoiceLine ();
				}
			}
			else if (Timer > SubtitleSet.Titles [SubIndex].TimeIndex) {
				WriteAllTextMeshes (SubtitleSet.Titles [SubIndex].Text);
				ActiveMessage = SubtitleSet.Titles [SubIndex].Text;
				SubIndex++;
				if (SubIndex >= SubtitleSet.Titles.Length) {
					SubIndex = -1;
					Timer = 0;
				}
			}



		} else {
			if (ActiveMessage != "") {
				WriteAllTextMeshes ("");
				ActiveMessage = "";
			}
		}




	}

	void WriteAllTextMeshes(string Message)
	{
		foreach (var M in TMs) {M.text = Message;}
	}


}
