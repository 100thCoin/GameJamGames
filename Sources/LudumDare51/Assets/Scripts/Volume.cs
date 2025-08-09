using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour {

	public AudioSource AS;
	public bool VA;
	public bool Acapella;
	public bool Title;
	public bool woop;

	public GameObject NextClip;
	public string Transcript;
	public float DestroyDelay;

	public bool DoOnce;

	public bool EnableAcapella;

	// Use this for initialization
	void Start () {
		AS.volume = VA ? G.Main.VoiceVolume : G.Main.MusicVolume;
		Transcript = Transcript.Replace("#","\n");
	}
	
	// Update is called once per frame
	void Update () {




		if(VA)
		{
			if(!G.Main.playingTheGame)
			{
				Destroy(gameObject);
			}


			G.Main.WatchIsStillTalking = true;
			AS.volume = G.Main.VoiceVolume;

			G.Main.Subs.Text = Transcript;

			if(!AS.isPlaying)
			{
				G.Main.Subs.Text = "";
				G.Main.WatchGrace = false;

				DestroyDelay-= Time.deltaTime;
				if(DestroyDelay <=0)
				{
					if(!DoOnce)
					{

						DoOnce = true;
						if(NextClip != null)
						{
							if(G.Main.CurrentLevel == 0 && G.Main.LevelClear)
							{
								// don't
							}
							else
							{
								Instantiate(NextClip);
							}
						}
					}
					if(EnableAcapella)
					{
						G.Main.LocalMusicVolumeAcapella = 1;
						G.Main.AcapellaOST = true;
						G.Main.AcapellaMusic.SetActive(true);
						G.Main.PlayingTheAcapellaMusic = true;
					}

					Destroy(gameObject);
				}
				else
				{
					if(G.Main.CurrentLevel == 0 && !G.Main.UnlockedWatch)
					{
						G.Main.Level0Watch.runtimeAnimatorController = null;
					}
					G.Main.UIWatch.runtimeAnimatorController = G.Main.UIWatchIdle;

				}
			}
			else
			{
				G.Main.WatchGrace = true;
			}


		}
		else
		{
			if(!Acapella)
			{
				if(!woop)
				{
					float VaDamp = 1;
					if(G.Main.WatchIsStillTalking)
					{
						VaDamp = 0.5f;
					}
					AS.volume = G.Main.MusicVolume * G.Main.LocalMusicVolume * VaDamp;
				}
				else
				{
					AS.volume = G.Main.MusicVolume;

				}
				if(!Title)
				{
					AS.pitch = G.Main.LocalMusicVolume;
				}
			}
			else
			{
				AS.volume = G.Main.MusicVolume * G.Main.LocalMusicVolumeAcapella;
			}
		}
	}
}
