using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbPlot : MonoBehaviour {

	public bool Filled;
	public bool FilledOnce;
	public Farm F;
	public GameObject SeedPlanter;

	public bool Mercy;
	public Camera Cam;

	public SpriteRenderer SR;

	public bool Combat;

	public int CombatID;

	public bool Dark;
	public bool TempHide;

	public DataHolder Main;

	public bool MiggsTable;
	public bool TildeGarden;


	// Use this for initialization
	void OnEnable () {

		SeedPlanter = F.PSeedHolder;

		if(Combat)
		{
			Filled = false;
			FilledOnce = false;

		}

	}

	// Update is called once per frame
	void Update () {

		if(!Combat)
		{
			if(F.PlacingBulb && !(Dark && Main.Progress <= 1.4f) && !(TempHide && Main.Progress < 1.4f))
			{
				if(!Filled)
				{
					Vector3 MousePos = Cam.ScreenToWorldPoint (Input.mousePosition) + new Vector3(0,0,2);
					float Dist = new Vector2(MousePos.x-transform.position.x,MousePos.y-transform.position.y).magnitude;

					SR.color = new Vector4(1,1,1,3-Dist);

				}
			}
			else
			{
				SR.color = new Vector4(1,1,1,0);
			}
		}
	}

	void OnMouseOver()
	{
		if(!Filled)
		{
			Mercy = true;
		}
	}


	void LateUpdate()
	{

		if(Mercy)
		{

			SeedPlanter.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z+0.1f);

			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				if(F.BulbID != -1 && F.BulbID != 9 && F.BulbID != 15 && !MiggsTable)
				{
					
					if(!FilledOnce && SR.color.a > 0.01f)
					{
						if(Dark && GameObject.Find("Main").GetComponent<DataHolder>().Progress < 3.6f)
						{

							GameObject.Find("Main").GetComponent<DataHolder>().WL.DEBUGBADIDEA = true;

						}
						else
						{


							if(Dark)
							{
								FilledOnce = true;
								Filled = true;
								if(!Combat)
								{
									if(F.BulbID >=10)
									{

									}
									else
									{
										F.PlantBulb(8,transform);
										if(Main.Progress == 3.6f || Main.Progress == 3.61f)
										{
											GameObject.Find("Main").GetComponent<DataHolder>().Progress = 3.7f;

										}
									}
								}
							}
							else
							{

								FilledOnce = true;
								Filled = true;

								print(F.BulbID);
								if(!Combat)
								{
									print(Main.Progress);
								
								// for combat just use different prefabls

									if(Main.Progress == 3 && F.BulbID == 2)
									{

										Main.Progress = 3.05f;

									}
								}
								if(GameObject.Find("Main").GetComponent<DataHolder>().Progress == 1)
								{
									GameObject.Find("Main").GetComponent<DataHolder>().Progress = 1.1f;
									Main.WL.DEBUGPLANTONE = true;
								}


								if(!Combat)
								{

								if((Main.Progress == 3.32 || Main.Progress == 3.33 || Main.Progress == 3.34) && F.BulbID == 3)
								{
									GameObject.Find("Main").GetComponent<DataHolder>().Progress = 3.3541f;
									
								}
								}
								if(!Combat)
								{
									F.PlantBulb(F.BulbID,transform);
								}
								else

								{
									F.PlantCombatBulb(F.BulbID,transform,CombatID);
									F.AttackB.PlayerPlants[CombatID] = true;

								}

							}
						}
					}
				}
				else if(F.BulbID == 9)
				{
					if(!MiggsTable)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MonoBringToMiggs);
					}
					else
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsMacaroni);
						F.PlacingBulb = false;


						F.BulbID = -1;
						F.SelectedSeed = false;
						F.ChangeSeedVisual(-1);
					}
				}
				else if(F.BulbID == 15)
				{
					if(!TildeGarden)
					{
						if(!Combat)
						{
							Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MonoSpecialPlace);

						}
						else
						{
							F.PlantCombatBulb(14,transform,CombatID); //rip lol
							F.AttackB.PlayerPlants[CombatID] = true;
						}
					}
					else
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeHowIsTheFlower);
						F.PlacingBulb = false;


						F.BulbID = -1;
						F.SelectedSeed = false;
						F.ChangeSeedVisual(-1);
					}
				}
			}




		}

		Mercy = false;
	}


}
