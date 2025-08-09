using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridButton : MonoBehaviour {

	public bool Mercy;
	public SpriteRenderer SR;
	public Sprite Glow;
	public Sprite NoGlow;

	public Farm F;

	public bool Combat;

	public GameObject Farm2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SR.enabled = (!F.FirstSeed && F.OpenSlots == 0);

		if(!Mercy)
		{
			SR.sprite = NoGlow;
		}
		Mercy = false;
	}


	void OnMouseOver()
	{
		Mercy = true;
		SR.sprite = Glow;

		if(Input.GetKeyDown(KeyCode.Mouse0) && SR.enabled)
		{

			int totalSeeds = F.SeedHolder.transform.childCount;


			int Output = 1; //Orange bulb





			// if chain, 2, Yellow bulb
			if(totalSeeds > 2)
			{
				if(F.TypeCount[1] > F.TypeCount[4] || F.TypeCount[4] > F.TypeCount[1])
				{
					Output = 2;
				}
			}

			if(F.TypeCount[0] == 0 && F.TypeCount[1] > 0 && F.TypeCount[2] > 0 && F.TypeCount[3] == 0 && F.TypeCount[4] > F.TypeCount[1] && F.TypeCount[5] == 0 && F.TypeCount[6] > 0)
			{
				Output = 4;
			}




			// if loop, 3, Green bulb
			if(totalSeeds > 7)
			{
				int j = 0;
				bool FoundLoop = false;

				int comma = 0;
				int X = 0;
				int Y = 0;

				print("checkloop:");

				while(j < totalSeeds)
				{
					comma = F.SeedHolder.transform.GetChild(j).name.IndexOf(",");
					X = int.Parse(F.SeedHolder.transform.GetChild(j).name.Substring(0,comma));
					Y = int.Parse(F.SeedHolder.transform.GetChild(j).name.Substring(comma+1,F.SeedHolder.transform.GetChild(j).name.Length-comma-1));

					//Check for loop.


					if(
						F.SeedHolder.transform.Find(""+(X+1)+","+(Y)) != null &&
						F.SeedHolder.transform.Find(""+(X+2)+","+(Y)) != null &&
						F.SeedHolder.transform.Find(""+(X+2)+","+(Y-1)) != null &&
						F.SeedHolder.transform.Find(""+(X+2)+","+(Y-2)) != null &&
						F.SeedHolder.transform.Find(""+(X+1)+","+(Y-2)) != null &&
						F.SeedHolder.transform.Find(""+(X)+","+(Y-2)) != null &&
						F.SeedHolder.transform.Find(""+(X)+","+(Y-1)) != null &&
						F.SeedHolder.transform.Find(""+(X+1)+","+(Y-1)) == null)
					{
						FoundLoop = true;
						break;
					}



					j++;

				}

				if(FoundLoop)
				{
					Output = 3;
				}


			}



			// if you made the specific rose recepe, 0, red






			// Post-Tilde, the ash is replaced with Passion
			if(F.TypeCount[0] > 0 && F.TypeCount[5] > 3 && F.TypeCount[1] == 0 && F.TypeCount[2] == 0 && F.TypeCount[3] == 0 && F.TypeCount[4] == 0 && F.TypeCount[6] == 0)
			{
				Output = 5;

				//then you win the game by planting this outside
			}


			// red
			if(F.TypeCount[0] > 0 && F.TypeCount[1] == 0 && F.TypeCount[2] == 0 && F.TypeCount[3] == 0 && F.TypeCount[4] == 0 && F.TypeCount[5] == 0 && F.TypeCount[6] > 0)
			{
				Output = 0;
			}
			// orange
			if(F.TypeCount[0] == 0 && F.TypeCount[1] > 0 && F.TypeCount[2] == 0 && F.TypeCount[3] == 0 && F.TypeCount[4] == 0 && F.TypeCount[5] == 0 && F.TypeCount[6] > 0)
			{
				Output = 1;
			}
			// yellow
			if(F.TypeCount[0] == 0 && F.TypeCount[1] == 0 && F.TypeCount[2] > 0 && F.TypeCount[3] == 0 && F.TypeCount[4] == 0 && F.TypeCount[5] == 0 && F.TypeCount[6] > 0)
			{
				Output = 2;
			}
			//green
			if(F.TypeCount[0] == 0 && F.TypeCount[1] == 0 && F.TypeCount[2] == 0 && F.TypeCount[3] > 0 && F.TypeCount[4] == 0 && F.TypeCount[5] == 0 && F.TypeCount[6] > 0)
			{
				Output = 3;
			}
			// blue
			if(F.TypeCount[0] == 0 && F.TypeCount[1] == 0 && F.TypeCount[2] == 0 && F.TypeCount[3] == 0 && F.TypeCount[4] > 0 && F.TypeCount[5] == 0 && F.TypeCount[6] > 0)
			{
				Output = 4;
			}
			if(F.TypeCount[0] == 0 && F.TypeCount[1] == 0 && F.TypeCount[2] == 0 && F.TypeCount[3] == 0 && F.TypeCount[4] == 0 && F.TypeCount[5] == 0 && F.TypeCount[6] > 0)
			{
				Output = 6;
			}

			if(F.TypeCount[5] > 0)
			{

				Output +=10;
			} 



			if(F.TypeCount[0] == 0 && F.TypeCount[1] == 1 && F.TypeCount[2] == 2 && F.TypeCount[3] == 2 && F.TypeCount[4] == 2 && F.TypeCount[5] == 5 && F.TypeCount[6] == 0)
			{
				Output = 0;
			}

			if(F.TypeCount[0] == 4 && F.TypeCount[1] == 0 && F.TypeCount[2] == 0 && F.TypeCount[3] == 0 && F.TypeCount[4] == 0 && F.TypeCount[5] == 8 && F.TypeCount[6] == 0)
			{
				Output = 15;
				GameObject.Find("Main").GetComponent<DataHolder>().Progress = 4.59f;
			}

			int i = 0;
			while(i<F.TypeCount.Length)
			{
				F.TypeCount[i] = 0;
				i++;
			}


			// white, one of each color except purple / red.
			if(Output == 16)
			{
				Output = 6;

			}


			if(Output == 0 && (GameObject.Find("Main").GetComponent<DataHolder>().Progress == 3.9f || GameObject.Find("Main").GetComponent<DataHolder>().Progress == 3.91f))
			{
				Output = 9;

			}


			print("HybridOutput: " + Output);



			i = 0;
			while(i < totalSeeds)
			{
				Destroy(F.SeedHolder.transform.GetChild(i).gameObject);
				i++;
			}
			F.FirstSeed = true;
			i = 0;
			while(i<F.TypeCount.Length)
			{
				F.TypeCount[i] = 0;
				i++;
			}
			if(!Combat)
			{
				F.Main.ExitFarm = true;
			}
			F.PlacingBulb = true;
			F.ChangeSeedVisual(-1);

			F.BulbID = Output;

			//GameObject.Find("Main").GetComponent<DataHolder>().SeedStock -= totalSeeds;

			Instantiate(F.BulbVis[Output],F.PSeedHolder.transform.position,transform.rotation,F.PSeedHolder.transform);

			if(Combat)
			{
				Farm2.SetActive(false);
				F.AttackB.PrizeSeeds += totalSeeds-1;
			}

		}
	}

}
