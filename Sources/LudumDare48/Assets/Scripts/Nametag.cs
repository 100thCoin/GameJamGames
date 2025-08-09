using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nametag : MonoBehaviour {

	public bool Initial;
	public bool Write;

	public float InitializeTimer;

	public TextMesh NameText;
	public float BackspaceDelay;

	public bool leave;

	public DataHolder Main;

	public AudioSource MainMusic;
	public AudioSource GoofyMusic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Initial)
		{
			InitializeTimer += Time.deltaTime;

			float Duration = 0.75f;
			float CamPos = 15;
			float CamDest = 0;

			float InitializeHeight = (((CamPos - CamDest) * Mathf.Pow(InitializeTimer,2))/Mathf.Pow(Duration,2) - ((2*CamPos - 2*CamDest) * InitializeTimer)/Duration + CamPos);

			GoofyMusic.volume = 1-(InitializeHeight/15);
			GoofyMusic.pitch = 1-(InitializeHeight/15);
			MainMusic.volume = InitializeHeight/15;
			MainMusic.pitch = InitializeHeight/15;

			transform.localPosition = new Vector3(0,InitializeHeight,0.32f);

			if(InitializeTimer > Duration)
			{
				InitializeHeight = 0;
				GoofyMusic.volume = 1-(InitializeHeight/15);
				GoofyMusic.pitch = 1-(InitializeHeight/15);
				MainMusic.volume = InitializeHeight/15;
				MainMusic.pitch = InitializeHeight/15;
				transform.localPosition = new Vector3(0,InitializeHeight,0.32f);

				Initial = false;
				Write = true;
			}

			//transform.localPosition


		}

		if(!Initial && !leave && Write)
		{
			if(Input.GetKeyDown(KeyCode.Return))
			{
				leave = true;
				Write = false;
				Main.PlayerName = NameText.text;
				Main.PseudoPause = false;
				return;
			}


			if(NameText.text.Length < 8 && !Input.GetKey(KeyCode.Backspace))
			{
				NameText.text += Input.inputString;
			}

			if(NameText.text.Length > 0)
			{
				bool DoBack = false;
				if(Input.GetKey(KeyCode.Backspace))
				{
					BackspaceDelay += Time.deltaTime;
				}
				else
				{
					BackspaceDelay = 0;
				}
				if(Input.GetKeyDown(KeyCode.Backspace))
				{
					DoBack = true;
				}
				if(BackspaceDelay > 0.5f)
				{
					DoBack = true;
					BackspaceDelay -= 0.03f;
				}

				if(DoBack)
				{
					NameText.text = NameText.text.Substring(0,NameText.text.Length-1);

				}
			}
		}

		if(leave)
		{


			InitializeTimer -= Time.deltaTime;

			float Duration = 0.75f;
			float CamPos = -15;
			float CamDest = 0;

			float InitializeHeight = (((CamPos - CamDest) * Mathf.Pow(InitializeTimer,2))/Mathf.Pow(Duration,2) - ((2*CamPos - 2*CamDest) * InitializeTimer)/Duration + CamPos);

			transform.localPosition = new Vector3(0,InitializeHeight,0.32f);

			GoofyMusic.volume = 1-(InitializeHeight/-15);
			GoofyMusic.pitch = 1-(InitializeHeight/-15);
			MainMusic.volume = (InitializeHeight/-15);
			MainMusic.pitch = (InitializeHeight/-15);

			if(InitializeTimer < 0)
			{
				InitializeHeight= -15;
				GoofyMusic.volume = 1-(InitializeHeight/-15);
				GoofyMusic.pitch = 1-(InitializeHeight/-15);
				MainMusic.volume = (InitializeHeight/-15);
				MainMusic.pitch = (InitializeHeight/-15);
				leave = false;
				NameText.text = "";
			}

		}

	}
}
