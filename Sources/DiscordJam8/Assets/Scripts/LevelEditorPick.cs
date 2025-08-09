using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorPick : MonoBehaviour {

	public int Index;


	public bool MouseOver;
	public bool Selected;
	public bool NoDont;
	public int Grace;
	public SpriteRenderer Highlight;
	public SpriteRenderer SR;
	public SpriteRenderer Hoverover;

	public Vector2Int ObjectDimensions;
	public Vector2Int ObjectOffset;
	public Vector2 FineObjectOffset;

	public GameObject MoveObject;
	public GameObject PrefabObject;

	public int Rules; // restrictions on where this goes.

	public bool Placed;
	public Vector2Int PlacePos;

	public int EvilPoints;
	public string Name;


	// Use this for initialization
	void Start () {
		Global.DataHolder.EditorPickList.Add (this);
	}

	void OnMouseOver () {
		if (!NoDont) {
			MouseOver = true;
			Grace = 3;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Hoverover != null) {
			Hoverover.transform.position = new Vector3 (0, 0, -500);
		}
		if (Global.DataHolder.LevEditMain.CurrentPick == this) {
			Highlight.sortingOrder = -2;
			Highlight.color = new Vector4 (1f, 0, 0, 1);
			Global.DataHolder.LevEditTooltip.text = "" + Name + ":\n" + "Evil Points: " + EvilPoints;
			Global.DataHolder.TooltipGrace = 3;
		} else {
			if (Grace > 0) {
				Highlight.sortingOrder = 2;
				Highlight.color = new Vector4 (1f, 0.25f, 0.25f, 0.5f);
				if (Placed && Hoverover != null) {

					// highlight existing object
					Hoverover.transform.position = Global.DataHolder.LevEditMain.transform.position + new Vector3(PlacePos.x,PlacePos.y,0)*2;
					Hoverover.color = new Vector4 (0.25f,0.25f,1,0.5f);
					Hoverover.sortingOrder = 8;
				}
			} else {
				if (Placed) {
					Highlight.color = new Vector4 (0.25f,0.25f,1,0.5f);
					Highlight.sortingOrder = 2;

				} else {
					Highlight.color = new Vector4 (0, 0, 0, 0);
				}
			}
		}

		if (Grace > 0) {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				if (Global.DataHolder.LevEditMain.CurrentPick != this) {
					if (Global.DataHolder.LevEditMain.CurrentPick != null) {
						// uh-oh, we have another object currently selected.
						Global.DataHolder.LevEditMain.CurrentPick.MoveObject.transform.position = new Vector3 (0, 0, -600);
						Global.DataHolder.LevEditMain.Abort ();
					}

					Global.DataHolder.LevEditMain.CurrentPick = this;

				} else {
					MoveObject.transform.position = new Vector3 (0, 0, -600);
					Global.DataHolder.LevEditMain.Abort ();
				}
			}
			Global.DataHolder.LevEditTooltip.text = "" + Name + ":\n" + "Evil Points: " + EvilPoints;
			Global.DataHolder.TooltipGrace = 3;
		}

		if(Grace< 0)
		{
			MouseOver = false;
		}
		Grace--;
	}
}
