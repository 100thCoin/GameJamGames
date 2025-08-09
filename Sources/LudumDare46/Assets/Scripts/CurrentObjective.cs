using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentObjective : MonoBehaviour {

	public TextMesh TM;
	public SpriteRenderer Notes;
	public DataHolder Main;

	public bool DoIt;
	public bool LaterDoIt;

	public string Obj;
	public float Timer;
	public float Timer2;
	public bool Moving;

	public float LastYPos;
	public float LastXPos;
	public float LastScale;

	float TargetYpos = -3;
	float TargetXpos = -3;
	float TargetScale = -3;

	public SpriteRenderer Notebook;
	public Transform NBParent;


	// Use this for initialization
	void Start () {
		NBParent = Notebook.transform.parent;
		TargetYpos = -1.5f;
		TargetXpos = Notebook.transform.position.x;
		TargetScale = Notebook.transform.lossyScale.x;

	}
	
	// Update is called once per frame
	void Update () {

		if(!LaterDoIt)
		{
			DoIt = Main.Progress >= 2.9f;
			Obj = Obj.Replace("#","\n");

		}
		else
		{
			DoIt = Main.Progress == 4.39f;
			Obj = Obj.Replace("#","\n");

		}

		if(DoIt)
		{
			if(!Moving)
			{
			Timer += Time.deltaTime;

			if(Timer > 0.05)
			{
				Timer -= 0.05f;

				if(Obj.Length != 0)
				{

					TM.text += Obj.Substring(0,1);
					Obj = Obj.Substring(1,Obj.Length-1);

				}
			}

				Timer2 += Time.deltaTime;




				if(Timer2 > 4)
				{
					Main.Progress = 3;
					Notebook.color = new Vector4(1,1,1,Timer2-4);
					TM.color = new Vector4(1,1,1,5-Timer2);
					Notebook.enabled = true;

					Notebook.transform.parent = this.transform;
					Notebook.transform.localPosition = new Vector3(0,0,40);
					Notebook.transform.localScale = new Vector3(4,4,4);
							
					LastYPos = Notebook.transform.position.y;
					LastXPos = Notebook.transform.position.x;
					LastScale = 4;

				}

				if(Timer2 > 6)
				{
					Moving = true;

					Timer2 = 0;
					Timer = 0;
				}

			}
			else
			{

				float YDuration = 2;


				if(Timer < YDuration)
				{
					Timer += Time.fixedDeltaTime;

		

					Notebook.transform.parent = Main.transform;

					if(Timer >= YDuration)
					{
						Timer = YDuration;
					}

					float LocalYPos = (((LastYPos - TargetYpos) * Mathf.Pow(Timer,2))/Mathf.Pow(YDuration,2) - ((2*LastYPos - 2*TargetYpos) * Timer)/YDuration + LastYPos);
					float LocalXPos = (((LastXPos - TargetXpos) * Mathf.Pow(Timer,2))/Mathf.Pow(YDuration,2) - ((2*LastXPos - 2*TargetXpos) * Timer)/YDuration + LastXPos);
					float LocalScale = (((LastScale - TargetScale) * Mathf.Pow(Timer,2))/Mathf.Pow(YDuration,2) - ((2*LastScale - 2*TargetScale) * Timer)/YDuration + LastScale);

					Notebook.transform.position = new Vector3(LocalXPos,LocalYPos,Notebook.transform.position.z);
					Notebook.transform.localScale = Vector3.one * LocalScale;

					if(Timer >= YDuration)
					{
						Notebook.transform.parent = NBParent;
						Destroy(gameObject);
					}

				}
			}
		}
	}


}
