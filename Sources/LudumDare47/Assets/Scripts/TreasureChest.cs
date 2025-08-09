using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour {

	//MAGIC NUBMERS
	float m = 0.892f;
	float a = 1.076f;
	float d = 2.5f;
	float b = 4;
	float c = 0.5f;
	//Don't question my math!

	// Update is called once per frame

	public bool Opening;
	public float ItemTimer;

	public GameObject Contents;

	public bool Open;

	public GameObject ThreeDCol;

	public GameObject SecondContents;
	public GameObject SecondThreeDCol;

	public bool InRange;

	public Animator Anim;
	public RuntimeAnimatorController OpeningAnim;

	public bool threeDoOnce;

	public TreeExplode Tree;

	void Update()
	{

		if(Opening)
		{
			ItemTimer += Time.deltaTime;

			if(ItemTimer >=0 && !threeDoOnce)
			{

				float Timer2 = -Mathf.Cos(Mathf.PI*ItemTimer) * 0.5f+0.5f;

				Contents.transform.localScale = Vector3.one * (((c*Mathf.Sin(d*Mathf.PI*m*Timer2))/(Mathf.PI + Mathf.Pow(m*Timer2,3))-Mathf.Pow(m*Timer2,2)+a*m*Timer2)*4 + (-Mathf.Pow(-1+2*ItemTimer,2)+1)*0.5f)/1.223f;
				if(ItemTimer>0.82f)
				{
					Contents.transform.localScale = Vector3.one;
					ItemTimer = -1;
					Opening = false;
					Open = true;
					ThreeDCol.SetActive(true);
					threeDoOnce = true;
					if(Tree != null)
					{
						Tree.DoIt = true;
					}

				}
			}
		}
			
	}
}
