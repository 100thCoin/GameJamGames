using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreatureController : MonoBehaviour {

	public SpriteRenderer CursorObject;
	public SpriteRenderer[] CursorOrbiters;
	public float VisiblityFade;

	public SpriteRenderer BirdBoundingBox;
	public Sprite BirdBoundingBox_Melody;
	public Sprite BirdBoundingBox_Harmony;

	public Sprite FrogBoundingBox_Melody;

	public Sprite RabbitBoundingBox_Melody;
	public Sprite RabbitBoundingBox_Harmony;

	public Sprite MelodyIcon;
	public Sprite HarmonyIcon;

	public HudPlacementNoNos[] NoNoDetectors;

	public float NoNoVibrateTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		NoNoVibrateTimer -= Time.deltaTime*6;
		NoNoVibrateTimer = Mathf.Clamp01 (NoNoVibrateTimer);

		Vector3 VibePos = new Vector3 (Random.Range (-NoNoVibrateTimer, NoNoVibrateTimer), Random.Range (-NoNoVibrateTimer, NoNoVibrateTimer), 0);


		CursorObject.sprite = Global.Dataholder.CurrentHudSelectionHarmony ? HarmonyIcon : MelodyIcon;
		bool Nono = false;
		bool Yesyes = false;

		if (Global.Dataholder.PMov.InsideStage) {

			if (Global.Dataholder.CurrentHUDButtonSelection != null) {


				if (Input.GetKeyDown (KeyCode.Mouse1)) {

					// switch to harmony mode
					Global.Dataholder.CurrentHudSelectionHarmony = !Global.Dataholder.CurrentHudSelectionHarmony;
					if (Global.Dataholder.CurrentHudSelectionAnimalType == 0) {
						Global.Dataholder.BirdHarmonyMode[Global.Dataholder.CurrentHudSelectionGroupIndex] = !Global.Dataholder.BirdHarmonyMode[Global.Dataholder.CurrentHudSelectionGroupIndex];
					}
					else if (Global.Dataholder.CurrentHudSelectionAnimalType == 1) {
						Global.Dataholder.FrogHarmonyMode[Global.Dataholder.CurrentHudSelectionGroupIndex] = !Global.Dataholder.FrogHarmonyMode[Global.Dataholder.CurrentHudSelectionGroupIndex];
					}
					else if (Global.Dataholder.CurrentHudSelectionAnimalType == 2) {
						Global.Dataholder.RabbitHarmonyMode[Global.Dataholder.CurrentHudSelectionGroupIndex] = !Global.Dataholder.RabbitHarmonyMode[Global.Dataholder.CurrentHudSelectionGroupIndex];
					}

				}



				int j = 0;
				while (j < NoNoDetectors.Length) {

					if (NoNoDetectors [j].Nono > 0) {
						if (NoNoDetectors [j].RabbitHarmony) {
							Yesyes = true;
						} else {
							Nono = true;
						}
						break;
					}

					j++;
				}


				if (Global.Dataholder.CurrentHudSelectionAnimalType == 0) {
					// BIRD //////////////////////////////////////////////////////////////////////
					BirdBoundingBox.transform.localScale = new Vector3(1,1,1);

					VisiblityFade += Time.deltaTime * 4;

					// bird cursor
					Vector3 MousePos = (Global.Dataholder.ScreenCam.ScreenToWorldPoint (Input.mousePosition) - Global.Dataholder.ScreenCam.transform.position) * 2 + Global.Dataholder.RenderTexCam.transform.position;
					MousePos = new Vector3 (MousePos.x, MousePos.y, 0);
					if (MousePos.y > 9.333f) {
						MousePos = new Vector3 (MousePos.x, 9.333f, MousePos.z);
					}
					CursorObject.transform.position = MousePos + VibePos;
					BirdBoundingBox.transform.position = CursorObject.transform.position;

					// change temp vis box dimensions
					if (!Global.Dataholder.CurrentHudSelectionHarmony) {
						BirdBoundingBox.sprite = BirdBoundingBox_Melody;
					} else {
						BirdBoundingBox.sprite = BirdBoundingBox_Harmony;
					}

					if (Global.Dataholder.MouseOnHUDGrace < 0) {
						if (Input.GetKeyDown (KeyCode.Mouse0)) {
							if (!Nono) {
								// place it!
								Global.Dataholder.BirdCubePos [Global.Dataholder.CurrentHudSelectionGroupIndex] = MousePos;
								Global.Dataholder.BirdsAreCube [Global.Dataholder.CurrentHudSelectionGroupIndex] = true;
								Global.Dataholder.CurrentHUDButtonSelection = null;
								Global.Dataholder.CurrentHudSelectionAnimalType = -1;
							} else {
								NoNoVibrateTimer = 1f;
							}
						}
					}
				} else if (Global.Dataholder.CurrentHudSelectionAnimalType == 1) {
					// FROG //////////////////////////////////////////////////////////////////////
					VisiblityFade += Time.deltaTime * 4;
					BirdBoundingBox.transform.localScale = new Vector3(1,1,1);

					Vector3 MousePos = (Global.Dataholder.ScreenCam.ScreenToWorldPoint (Input.mousePosition) - Global.Dataholder.ScreenCam.transform.position) * 2 + Global.Dataholder.RenderTexCam.transform.position;
					MousePos = new Vector3 (MousePos.x, MousePos.y, 0);
					if (MousePos.y > 8.333f) {
						MousePos = new Vector3 (MousePos.x, 8.333f, MousePos.z);
					}
					CursorObject.transform.position = MousePos + VibePos;
					RaycastHit[] Hits = Physics.RaycastAll(MousePos,new Vector3 (0, -1, 0),999,0x100);
					Vector3 RayHitPoint1 = Global.Dataholder.Minus500;
					if (Hits.Length != 0) {
						Vector3 RayHitPoint = Hits[0].point;
						CursorObject.transform.position = RayHitPoint + VibePos;
						RayHitPoint1 = RayHitPoint;
					} else {
						CursorObject.transform.position = new Vector3(0,0,-500);
						Nono = true;
					}

					// change temp vis box dimensions
					if (!Global.Dataholder.CurrentHudSelectionHarmony) {
						BirdBoundingBox.sprite = FrogBoundingBox_Melody;
					} else {
						BirdBoundingBox.sprite = null;
						CursorObject.transform.position = Global.Dataholder.PMov.transform.position;
					}
					BirdBoundingBox.transform.position = CursorObject.transform.position;

					if (Global.Dataholder.MouseOnHUDGrace < 0) {
						if (Input.GetKeyDown (KeyCode.Mouse0)) {
							if (!Nono) {
								// place it!
								if (!Global.Dataholder.CurrentHudSelectionHarmony) {									
									Global.Dataholder.FrogCubePos [Global.Dataholder.CurrentHudSelectionGroupIndex] = RayHitPoint1;
								}
								Global.Dataholder.FrogsAreCube [Global.Dataholder.CurrentHudSelectionGroupIndex] = true;
								Global.Dataholder.CurrentHUDButtonSelection = null;
								Global.Dataholder.CurrentHudSelectionAnimalType = -1;

							} else {
								NoNoVibrateTimer = 1f;
							}
						}
					}


				}
				else {
					// RABBIT //////////////////////////////////////////////////////////////////////
					VisiblityFade += Time.deltaTime * 4;
					int closestRightindex = 0;
					float closestRight = 99999;
					int closestLeftindex = 0;
					float closestLeft = 99999;
					if (!Global.Dataholder.CurrentHudSelectionHarmony) 
					{
						BirdBoundingBox.sprite = RabbitBoundingBox_Melody;
						Vector3 MousePos = (Global.Dataholder.ScreenCam.ScreenToWorldPoint (Input.mousePosition) - Global.Dataholder.ScreenCam.transform.position) * 2 + Global.Dataholder.RenderTexCam.transform.position;
						if (MousePos.y > 8.333f) {
							MousePos = new Vector3 (MousePos.x, 8.333f, MousePos.z);
						}
						MousePos = new Vector3 (MousePos.x, MousePos.y, 0);
						CursorObject.transform.position = MousePos + VibePos;
						RaycastHit[] HitsLeft = Physics.RaycastAll(MousePos,new Vector3 (-1, 0, 0),999,0x400);
						RaycastHit[] HitsRight = Physics.RaycastAll(MousePos,new Vector3 (1, 0, 0),999,0x400);

						RaycastHit[] FailLeft = Physics.RaycastAll(MousePos,new Vector3 (-1, 0, 0),999,0x4000);
						RaycastHit[] FailRight = Physics.RaycastAll(MousePos,new Vector3 (1, 0, 0),999,0x4000);

						bool OnBirds = false;

						Vector3 RayHitPoint1 = Global.Dataholder.Minus500;
						if (HitsLeft.Length != 0 && HitsRight.Length!= 0) {


							int k = 0;
							while (k < HitsLeft.Length) {
								float mfdnmskjbhfksd = Mathf.Abs (HitsLeft [k].point.x - MousePos.x);
								if (mfdnmskjbhfksd< closestLeft) {
									closestLeft = mfdnmskjbhfksd;
									closestLeftindex = k;
								}

								k++;
							}


							k = 0;
							while (k < HitsRight.Length) {
								float mfdnmskjbhfksd = Mathf.Abs (HitsRight [k].point.x - MousePos.x);
								if (mfdnmskjbhfksd< closestRight) {
									closestRight = mfdnmskjbhfksd;
									closestRightindex = k;
								}
								k++;
							}

							if (HitsLeft [closestLeftindex].transform.gameObject.name == "WallBirdCube" || HitsRight [closestRightindex].transform.gameObject.name == "WallBirdCube") {
								OnBirds = true;
							}

							Vector3 RayHitPoint = (HitsLeft[closestLeftindex].point + HitsRight[closestRightindex].point)/2f;
							CursorObject.transform.position = MousePos;
							RayHitPoint1 = RayHitPoint;
							BirdBoundingBox.transform.localScale = new Vector3(Mathf.Abs(HitsRight[closestRightindex].point.x - HitsLeft[closestLeftindex].point.x),1,1);
							BirdBoundingBox.transform.position = RayHitPoint + VibePos;
							if (FailLeft.Length != 0 || FailRight.Length != 0) {
								Nono = true;
							}

						} else {
							CursorObject.transform.position = new Vector3(0,0,-500);
							Nono = true;
						}

						// change temp vis box dimensions


						if (Global.Dataholder.MouseOnHUDGrace < 0) {
							if (Input.GetKeyDown (KeyCode.Mouse0)) {
								if (!Nono) {
									// place it!
									if (!Global.Dataholder.CurrentHudSelectionHarmony) {									
										Global.Dataholder.RabbitCubePos [Global.Dataholder.CurrentHudSelectionGroupIndex] = RayHitPoint1;
										Global.Dataholder.RabbitCubeDimensions [Global.Dataholder.CurrentHudSelectionGroupIndex] = new Vector3 (Mathf.Abs(HitsRight[closestRightindex].point.x - HitsLeft[closestLeftindex].point.x), 0, 0);
										Global.Dataholder.RabbitAttachedToBirds [Global.Dataholder.CurrentHudSelectionGroupIndex] = OnBirds;
									}
									Global.Dataholder.RabbitsAreCube [Global.Dataholder.CurrentHudSelectionGroupIndex] = true;
									Global.Dataholder.CurrentHUDButtonSelection = null;
									Global.Dataholder.CurrentHudSelectionAnimalType = -1;

								} else {
									NoNoVibrateTimer = 1f;
								}
							}
						}


					} else {
						// RABBIT HARMONY //////////////////////////////////////////////

						BirdBoundingBox.sprite = RabbitBoundingBox_Harmony;
						Vector3 MousePos = (Global.Dataholder.ScreenCam.ScreenToWorldPoint (Input.mousePosition) - Global.Dataholder.ScreenCam.transform.position) * 2 + Global.Dataholder.RenderTexCam.transform.position;
						MousePos = new Vector3 (MousePos.x, MousePos.y, 0);
						CursorObject.transform.position = MousePos + VibePos;
						RaycastHit[] HitsUp = Physics.RaycastAll(MousePos,new Vector3 (0, 1, 0),999,0x300);
						RaycastHit[] HitsDown = Physics.RaycastAll(MousePos,new Vector3 (0, -1, 0),999,0x300);

						if (Yesyes) {



							Vector3 RayHitPoint1 = Global.Dataholder.Minus500;
							if (HitsUp.Length != 0 && HitsDown.Length != 0) {
							
								int closestUpindex = 0;
								float closestUp = 99999;
								int closestDownindex = 0;
								float closestDown = 99999;

								int k = 0;
								while (k < HitsUp.Length) {
									float mfdnmskjbhfksd = Mathf.Abs (HitsUp [k].point.x - MousePos.x);
									if (mfdnmskjbhfksd< closestUp) {
										closestUp = mfdnmskjbhfksd;
										closestUpindex = k;
									}

									k++;
								}


								k = 0;
								while (k < HitsDown.Length) {
									float mfdnmskjbhfksd = Mathf.Abs (HitsDown [k].point.x - MousePos.x);
									if (mfdnmskjbhfksd< closestDown) {
										closestDown = mfdnmskjbhfksd;
										closestDownindex = k;
									}
									k++;
								}





								Vector3 RayHitPoint = (HitsUp [closestUpindex].point + HitsDown [closestDownindex].point) / 2f;

								if ( HitsUp [closestUpindex].collider.gameObject.layer == 8 && HitsDown [closestDownindex].collider.gameObject.layer == 9) {
									// valid
									CursorObject.transform.position = MousePos + VibePos;
									BirdBoundingBox.transform.localScale = new Vector3 (2, Mathf.Abs (HitsUp [closestUpindex].point.y - HitsDown [closestDownindex].point.y)+1, 1);
									BirdBoundingBox.transform.position = RayHitPoint + VibePos;
									RayHitPoint1 = RayHitPoint;

								} else {
									//CursorObject.transform.position = new Vector3(0,0,-500);
									Nono = true;
								}



							} else {
								//CursorObject.transform.position = new Vector3(0,0,-500);
								Nono = true;
							}



							// change temp vis box dimensions


							if (Global.Dataholder.MouseOnHUDGrace < 0) {
								if (Input.GetKeyDown (KeyCode.Mouse0)) {
									if (!Nono) {
										// place it!
										if (Global.Dataholder.CurrentHudSelectionHarmony) {									
											Global.Dataholder.RabbitCubePos [Global.Dataholder.CurrentHudSelectionGroupIndex] = BirdBoundingBox.transform.position;
											Global.Dataholder.RabbitCubeDimensions [Global.Dataholder.CurrentHudSelectionGroupIndex] = new Vector3 (1,Mathf.Abs(HitsUp[0].point.y - HitsDown[0].point.y), 0);

										}
										Global.Dataholder.RabbitsAreCube [Global.Dataholder.CurrentHudSelectionGroupIndex] = true;
										Global.Dataholder.CurrentHUDButtonSelection = null;
										Global.Dataholder.CurrentHudSelectionAnimalType = -1;

									} else {
										NoNoVibrateTimer = 1f;
									}
								}
							}

						} else {
							Nono = true;

						}
						if (Nono) {
							BirdBoundingBox.transform.localScale = new Vector3 (2,2, 1);
							BirdBoundingBox.transform.position = CursorObject.transform.position;
						}

					}

				}


			} else {
				VisiblityFade -= Time.deltaTime*4;
			}
		} else {
			VisiblityFade -= Time.deltaTime*4;
		}

		VisiblityFade = Mathf.Clamp01 (VisiblityFade);

		if (!Nono) {
			CursorObject.color = new Vector4 (1, 1, 1, VisiblityFade);
			BirdBoundingBox.color = new Vector4 (1, 1, 1, VisiblityFade * 0.5f);

			int i = 0;
			while (i < CursorOrbiters.Length) {
				CursorOrbiters[i].color = new Vector4 (1, 1, 1, VisiblityFade);

				i++;
			}
		} else {
			CursorObject.color = new Vector4 (1, 0, 0, VisiblityFade);
			BirdBoundingBox.color = new Vector4 (1, 0, 0, VisiblityFade * 0.5f);

			int i = 0;
			while (i < CursorOrbiters.Length) {
				CursorOrbiters[i].color = new Vector4 (1, 0, 0, VisiblityFade);

				i++;
			}
		}


	}
}
