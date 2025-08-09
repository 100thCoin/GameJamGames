using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

	public bool MouseOver;
	public int Grace;

	public bool ReturnToTitle;
	public bool CopyLevelString;

	public bool Title_StoryMode;
	public bool Title_Strings;
	public bool Title_Credits;
	public bool Title_RTS;
	public bool Title_Quit;


	public GameObject TitleCamera;

	public float CopyTimer;
	public TextMesh CopiedTM;


	public SpriteRenderer Highlight;

	// Use this for initialization
	void Start () {
		Highlight.enabled = false;

	}

	void OnMouseOver () {
		if (!Title_Strings || (Title_Strings && Super.Dataholder.UnlockedUsingLevelStrings)) {
			MouseOver = true;
			Grace = 3;
		}
	}

	// Update is called once per frame
	void Update () {

		if (Super.Dataholder.EnteringMenu > 0) {
			return;
		}

		if (CopyTimer > 0) {
			CopyTimer -= Time.deltaTime;
			if (CopyTimer <= 0) {
				CopiedTM.text = "";
			}
		}

		Highlight.enabled = Grace > 0;

		if (Grace > 0 && Input.GetKeyDown (KeyCode.Mouse0)) {

			if (CopyLevelString) {

				string result = "";

				// modify `result` to be some value based on the positions of everything in the level the player built.

				int i = 0;
				while (i < Global.DataHolder.EditorPickList.Count) {

					int j = 0;
					while (j < Global.DataHolder.EditorPickList.Count) {
						if (Global.DataHolder.EditorPickList [j].Index == i) {
							if (Global.DataHolder.EditorPickList [j].Placed) {
								result += LUT [Global.DataHolder.EditorPickList [j].PlacePos.x] + LUT [Global.DataHolder.EditorPickList [j].PlacePos.y];
							} else {
								result += "!!";
							}

							break;
						}
						j++;
					}

					i++;
				}

				GUIUtility.systemCopyBuffer = result;
				CopyTimer = 2;
				CopiedTM.text = "Copied!";

			}
			if (ReturnToTitle) {
				Super.Dataholder.ReturnToTitle ();
			}
			if (Title_Credits) {
				TitleCamera.transform.position = new Vector3 (0, -40, -10);
			}
			if (Title_RTS) {
				TitleCamera.transform.position = new Vector3 (0, 0, -10);
			}
			if (Title_Quit) {
				Application.Quit ();
			}
			if (Title_Strings) {

				// Parse the string
				bool reject = false;
				Vector2 tempPos;

				//failsafe 1
				string IN = GUIUtility.systemCopyBuffer;
				if (IN.Length != 30 * 2) {
					reject = true;
				}
				if (!reject) {

					int p = 0;
					while(p < IN.Length)
					{
				
						if (IN.ToCharArray() [p] != '!' && IN.ToCharArray() [p + 1] != '!') {
							if (ReverseLut (IN.ToCharArray() [p + 1]) < 16) {
							} else {
								reject = true;
							}
						} else {
							if (p == 0) {
								reject = true;
							}
						}
						p += 2;
						break;
					}


				}
				if (!reject) {

					// Load the StringMode Main

					Super.Dataholder.EnteringMenu = 1;
					Super.Dataholder.StartLevelStringMode = true;
					Super.Dataholder.LevelString = IN;
				}
				else
				{
					TitleCamera.transform.position = new Vector3 (0, -80, -10);

				}










			}
			if (Title_StoryMode) {

				Super.Dataholder.EnteringMenu = 1;
				Super.Dataholder.StartLevelStringMode = false;


			}
		}



		Grace--;
		if (Grace <= 0) {
			MouseOver = false;
		}
	}

	string[] LUT = {
		"a",
		"b",
		"c",
		"d",
		"e",
		"f",
		"g",
		"h",
		"i",
		"j",
		"k",
		"l",
		"m",
		"n",
		"o",
		"p",
		"q",
		"r",
		"s",
		"t",
		"u",
		"v",
		"w",
		"x",
		"y",
		"z",
		"A",
		"B",
		"C",
		"D",
		"E",
		"F",
		"G",
		"H",
		"I",
		"J",
		"K",
		"L",
		"M",
		"N"
	};

	int ReverseLut(char IN)
	{
		string n = "" + IN;
		switch (n) {
		case "a": return 0;
		case "b": return 1;
		case "c": return 2;
		case "d": return 3;
		case "e": return 4;
		case "f": return 5;
		case "g": return 6;
		case "h": return 7;
		case "i": return 8;
		case "j": return 9;
		case "k": return 10;
		case "l": return 11;
		case "m": return 12;
		case "n": return 13;
		case "o": return 14;
		case "p": return 15;
		case "q": return 16;
		case "r": return 17;
		case "s": return 18;
		case "t": return 19;
		case "u": return 20;
		case "v": return 21;
		case "w": return 22;
		case "x": return 23;
		case "y": return 24;
		case "z": return 25;
		case "A": return 26;
		case "B": return 27;
		case "C": return 28;
		case "D": return 29;
		case "E": return 30;
		case "F": return 31;
		case "G": return 32;
		case "H": return 33;
		case "I": return 34;
		case "J": return 35;
		case "K": return 36;
		case "L": return 37;
		case "M": return 38;
		case "N": return 39;
		default:
			print ("HUUUUGE ERROR");
			return 0;
		}
	}


}
