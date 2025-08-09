using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour {

	public bool CompletedTut1;
	public float Tut1Timer;
	public bool CompletedTut2;
	public float Tut2Timer;

	public bool CompletedTut3;
	public bool CompletedTut4;


	public GameObject[] ObjectMenu;

	public GameObject ColorGridPrefab;
	public SpriteRenderer[,] ColorGrid;
	public SpriteRenderer[] ColorGridHardCodedPlayerCase;

	public SpriteRenderer GridPlayerHardcode;

	public bool[,] GridOccupied;
	public bool[][,] GridLevelMask;
	public bool[][,] GridRestrictions;

	public GameObject Selection1;
	public GameObject Selection2;

	public int level;

	bool ColorGridSetUp;

	public int gridWidth;
	public int gridHeight;

	public Color GridColor_Bad;
	public Color GridColor_Good;
	public Color GridColor_Place_Bad;
	public Color GridColor_Place_Good;
	public Color Invisible;

	public Camera Cam;

	public LevelEditorPick CurrentPick;
	public Vector2Int CursorPos;
	public Vector2Int CursorPosWithOffset;

	public Vector2Int PreviousCursorPos;

	public bool ValidPlacement;

	public LevelEditorPick _HC_PickVillain;
	public LevelEditorPick _HC_PickHero;

	public GameObject SFX_FinishTut1;
	public GameObject SFX_FinishTut2;


	// Use this for initialization
	void Start () {
		ColorGrid = new SpriteRenderer[gridWidth, gridHeight];
		GridOccupied = new bool[gridWidth, gridHeight];
		int x = 0;
		int y = 0;
		while (x < gridWidth) {
			while (y < gridHeight) {

				ColorGrid[x,y] = Instantiate(ColorGridPrefab,transform.position + new Vector3(x*2,y*2,0),transform.rotation,transform).GetComponent<SpriteRenderer>();

				y++;
			}
			x++;
			y = 0;
		}

		// create grid restrictions.
		GridLevelMask = new bool[2][,];
		GridLevelMask[0] = new bool[gridWidth,gridHeight];
		GridLevelMask[1] = new bool[gridWidth,gridHeight]; // no restrictions
		x = 0;y = 0;
		while (x < gridWidth) {while (y < gridHeight) {
				GridLevelMask[0][x,y] = y < 3; // All tiles y2 or lower are invalid
		y++;}x++;y = 0;}
		x = 0;y = 0;
		while (x < gridWidth) {while (y < gridHeight) {
				if (y == 2 && x >= 36) {
					GridLevelMask [1] [x, y] = true;
				}
		y++;}x++;y = 0;}


		GridRestrictions = new bool[9][,];
		GridRestrictions[0] = new bool[gridWidth,gridHeight]; //no restrictions
		GridRestrictions[1] = new bool[gridWidth,gridHeight];
		x = 0;y = 0;while (x < gridWidth) {while (y < gridHeight) {
				GridRestrictions[1][x,y] = y > 4; // All tiles greater than y5 are invalid
				y++;}x++;y = 0;}
		
		GridRestrictions[2] = new bool[gridWidth,gridHeight];
		x = 0;y = 0;while (x < gridWidth) {while (y < gridHeight) {
				// Boss tiles. must be at the end.
				if(x < 36 || x > 38 || y > 4){GridRestrictions[2][x,y]=true;}
				y++;}x++;y = 0;}

		GridRestrictions[3] = new bool[gridWidth,gridHeight];
		x = 0;y = 0;while (x < gridWidth) {while (y < gridHeight) {
				GridRestrictions [3] [x, y] = true; // Player placement. Hardcoded tomfoolery, all tiles are invalid.
				y++;}x++;y = 0;}

		GridRestrictions[4] = new bool[gridWidth,gridHeight];
		x = 0;y = 0;while (x < gridWidth) {while (y < gridHeight) {
				GridRestrictions[4][x,y] = y > 6; // All tiles greater than y7 are invalid
				y++;}x++;y = 0;}

		GridRestrictions[5] = new bool[gridWidth,gridHeight];
		x = 0;y = 0;while (x < gridWidth) {while (y < gridHeight) {
				// Boss tiles. must be at the end.
				if(x < 20 || y < 10){GridRestrictions[5][x,y]=true;}
				y++;}x++;y = 0;}

	}
	
	// Update is called once per frame
	void Update () {

		if (!Global.DataHolder.InLevelEditor) {
			CurrentPick = null;
		}

		if (level == 1) {
			Selection1.SetActive (false);
			Selection2.SetActive (true);
		}

		if (CompletedTut1) {
			Tut1Timer += Time.deltaTime;
			_HC_PickHero.NoDont = false;
			Tut1Timer = Mathf.Clamp01 (Tut1Timer);
			_HC_PickHero.SR.color = new Vector4 (Tut1Timer, Tut1Timer, Tut1Timer, 1);
			_HC_PickVillain.SR.color = Color.black;
			_HC_PickVillain.NoDont = true;
		}
		if (CompletedTut2) {
			Tut2Timer += Time.deltaTime;
			_HC_PickHero.NoDont = true;
			_HC_PickVillain.NoDont = true;
			Tut2Timer = Mathf.Clamp01 (Tut2Timer);

			ObjectMenu[0].transform.position = new Vector3(ObjectMenu[0].transform.position.x, DataHolder.TwoCurveLerp(48-15, 80-15,Tut2Timer,1),0);
			ObjectMenu[1].transform.position = new Vector3(ObjectMenu[1].transform.position.x, DataHolder.TwoCurveLerp(80-15, 48-15,Tut2Timer,1),0);

		}

		if (CurrentPick != null) {
			if (CurrentPick.Placed) {
				//unplace it.
				Global.DataHolder.EvilPoints -= CurrentPick.EvilPoints;
				Vector2Int UnplacePos = CurrentPick.PlacePos + CurrentPick.ObjectOffset;
				int x = 0;
				int y = 0;
				while (x < CurrentPick.ObjectDimensions.x) {
					while (y < CurrentPick.ObjectDimensions.y) { // UPDATE TILE COLORS
						if (UnplacePos.x + x >= 0 && UnplacePos.x + x < gridWidth && UnplacePos.y + y >= 0 && UnplacePos.y + y < gridHeight) {							
							GridOccupied [UnplacePos.x + x, UnplacePos.y + y] = false;
						}
						y++;
					}
					x++;
					y = 0;
				}
				CurrentPick.Placed = false;

			}


			if (!ColorGridSetUp) {

				int x = 0;
				int y = 0;
				while (x < gridWidth) {
					while (y < gridHeight) {
						
						ColorGrid [x, y].color = GridLevelMask[level][x,y] ? (level == 0 ? Invisible : GridColor_Bad) : (GridOccupied [x, y] || GridRestrictions[CurrentPick.Rules][x,y]) ? GridColor_Bad : GridColor_Good;

						y++;
					}
					x++;
					y = 0;
				}

				ColorGridSetUp = true;

			}

			Vector3 temp = Cam.ScreenToWorldPoint (Input.mousePosition);
			CursorPos = new Vector2Int (Mathf.FloorToInt ((temp.x - transform.position.x +1)/2), Mathf.FloorToInt ((temp.y - transform.position.y +1)/2));
			CursorPosWithOffset = CursorPos + CurrentPick.ObjectOffset;

			CurrentPick.MoveObject.transform.position = new Vector3(transform.position.x + CursorPosWithOffset.x*2 + CurrentPick.FineObjectOffset.x,transform.position.y + CursorPosWithOffset.y*2 + CurrentPick.FineObjectOffset.y,0);

			 

			if (PreviousCursorPos.x != CursorPosWithOffset.x || PreviousCursorPos.y != CursorPosWithOffset.y) {
				ValidPlacement = true;
				int x = 0;
				int y = 0;
				while (x < CurrentPick.ObjectDimensions.x) {
					while (y < CurrentPick.ObjectDimensions.y) { // RESET TILE COLORS
						if (PreviousCursorPos.x + x >= 0 && PreviousCursorPos.x + x < gridWidth && PreviousCursorPos.y + y >= 0 && PreviousCursorPos.y + y < gridHeight) {
							ColorGrid [PreviousCursorPos.x + x, PreviousCursorPos.y + y].color = GridLevelMask [level] [PreviousCursorPos.x + x, PreviousCursorPos.y + y] ? (level == 0 ? Invisible : GridColor_Bad) : (GridOccupied [PreviousCursorPos.x + x, PreviousCursorPos.y + y] || GridRestrictions [CurrentPick.Rules] [PreviousCursorPos.x + x, PreviousCursorPos.y + y]) ? GridColor_Bad : GridColor_Good;
						}
						y++;
					}
					x++;
					y = 0;
				}
				x = 0;
				y = 0;
				while (x < CurrentPick.ObjectDimensions.x) {
					while (y < CurrentPick.ObjectDimensions.y) { // UPDATE TILE COLORS
						if (CursorPosWithOffset.x + x >= 0 && CursorPosWithOffset.x + x < gridWidth && CursorPosWithOffset.y + y >= 0 && CursorPosWithOffset.y + y < gridHeight) {							
							ColorGrid [CursorPosWithOffset.x + x, CursorPosWithOffset.y + y].color = GridLevelMask [level][CursorPosWithOffset.x + x, CursorPosWithOffset.y + y] ? Invisible : (GridOccupied [CursorPosWithOffset.x + x, CursorPosWithOffset.y + y] || GridRestrictions [CurrentPick.Rules][CursorPosWithOffset.x + x, CursorPosWithOffset.y + y]) ? GridColor_Place_Bad : GridColor_Place_Good;
							if(GridLevelMask [level][CursorPosWithOffset.x + x, CursorPosWithOffset.y + y] || (GridOccupied [CursorPosWithOffset.x + x, CursorPosWithOffset.y + y] || GridRestrictions [CurrentPick.Rules][CursorPosWithOffset.x + x, CursorPosWithOffset.y + y]))
							{
								ValidPlacement = false;
							}
						} else {
							ValidPlacement = false;
						}
						y++;
					}
					x++;
					y = 0;
				}

				if (CurrentPick.Rules == 3) {
					HardCodedCaseForSelectingPlayerSpawn ();
				}


			}

			if (ValidPlacement && Input.GetKeyDown (KeyCode.Mouse0)) {

				CurrentPick.Placed = true;
				CurrentPick.PlacePos = CursorPos;

				if (CurrentPick.Rules == 3) {
					if (!CompletedTut2) {
						Global.DataHolder.VQ.QueuedUp.Enqueue (SFX_FinishTut2);
					}
					CompletedTut2 = true;
				}

				// mark tiles as occupied

				int x = 0;
				int y = 0;
				while (x < CurrentPick.ObjectDimensions.x) {
					while (y < CurrentPick.ObjectDimensions.y) { // UPDATE TILE COLORS
						if (CursorPosWithOffset.x + x >= 0 && CursorPosWithOffset.x + x < gridWidth && CursorPosWithOffset.y + y >= 0 && CursorPosWithOffset.y + y < gridHeight) {							
							GridOccupied [CursorPosWithOffset.x + x, CursorPosWithOffset.y + y] = true;
						}
						y++;
					}
					x++;
					y = 0;
				}

				Global.DataHolder.EvilPoints += CurrentPick.EvilPoints;

				// aaaand we're done here!
				CurrentPick = null;
				x = 0;
				y = 0;
				while (x < gridWidth) {
					while (y < gridHeight) {
						ColorGrid [x, y].color = Invisible;
						y++;
					}
					x++;
					y = 0;
				}
				GridPlayerHardcode.enabled = false;
				ColorGridHardCodedPlayerCase [0].color = Invisible;
				ColorGridHardCodedPlayerCase [1].color = Invisible;
				if (!CompletedTut1) {
					Global.DataHolder.VQ.QueuedUp.Enqueue (SFX_FinishTut1);
				}
				CompletedTut1 = true;
			}


			PreviousCursorPos = CursorPosWithOffset;
		} else {
			ColorGridSetUp = false;
			PreviousCursorPos = new Vector2Int (-1999999, 1999999);
		}


	}


	public void Abort()
	{
		CurrentPick.Placed = false;

		// aaaand we're done here!
		CurrentPick = null;
		int x = 0;
		int y = 0;
		while (x < gridWidth) {
			while (y < gridHeight) {
				ColorGrid [x, y].color = Invisible;
				y++;
			}
			x++;
			y = 0;
		}
		GridPlayerHardcode.enabled = false;
		ColorGridHardCodedPlayerCase [0].color = Invisible;
		ColorGridHardCodedPlayerCase [1].color = Invisible;
		ColorGridSetUp = false;
	}

	public void HardCodedCaseForSelectingPlayerSpawn()
	{
		GridPlayerHardcode.enabled = true;
		ValidPlacement = true;
		ColorGridHardCodedPlayerCase [0].color = GridColor_Good;
		ColorGridHardCodedPlayerCase [1].color = GridColor_Good;
		if (CursorPos.x == -1) {
			if (CursorPos.y == 2 || CursorPos.y == 3) {
				ColorGridHardCodedPlayerCase [0].color = GridColor_Place_Good;
			} else {
				ValidPlacement = false;
			}
			if (CursorPos.y == 3 || CursorPos.y == 4) {
				ColorGridHardCodedPlayerCase [1].color = GridColor_Place_Good;
			} else {
				ValidPlacement = false;
			}

		} else {
			ValidPlacement = false;
		}

	}


}
