using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TASTimelineCell : MonoBehaviour {

	public int ID;
	public TASTimeline Timeline;

	public TextMesh TM_FrameCount;
	public TextMesh TM_ClickText;
	public SpriteRenderer SR;
	public Sprite Cell_Good;
	public Sprite Cell_NothingYet;
	public Sprite Cell_EndOfTimeline;

	CellContents Contents;

	public int hoverGrace;
	void OnMouseOver()
	{
		hoverGrace = 2;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hoverGrace > 0) {

			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				Timeline.ClickTimelineCell (ID);
			}

		}
		hoverGrace--;
	}

	public void UpdateGraphics()
	{
		Contents = Timeline.GetCellContentsFromCellID (ID);
		if (Contents.Null) {
			SR.sprite = Cell_NothingYet;
		} else {
			if (Contents.EndOfTimeline) {
				SR.sprite = Cell_EndOfTimeline;
			}
			else if (Contents.NotYetEmulated) {
				SR.sprite = Cell_NothingYet;
			}else {
				SR.sprite = Cell_Good;
			}
		}
		TM_FrameCount.text = (ID + Timeline.Top).ToString();
		if (Contents.Click) {
			TM_ClickText.text = "Clicked (" + Mathf.Round (Contents.ClickPos.x * 10) / 10f + ", " + Mathf.Round (Contents.ClickPos.y * 10) / 10f + ")";
		} else {
			TM_ClickText.text = "";
		}
	}
}
