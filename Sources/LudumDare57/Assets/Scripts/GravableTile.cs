using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravableTile : MonoBehaviour {

	public bool Set;
	public List<GravProjectile> GravProjs;
	public bool Static;
	public float Mass = 100;
	public Rigidbody2D RB;
	public Vector3 LocalGravProjCenter;
	public bool Active;
	public Material Connected;
	public Material Unconnected;
	public MeshRenderer MR;
	public bool DontChangeTexture;
	public bool NonStick;
	public float ActiveTimer;
	public Gem AttatchedGemScript;
	public bool NeedsGem;

	public bool Debug;

	public bool GameEnder;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Debug) {
			print ("yo");
		}
		if (NonStick && Active) {
			if (Global.Dataholder.GravTileA != null && Global.Dataholder.GravTileB != null && Global.Dataholder.GravTileA.Active && Global.Dataholder.GravTileB.Active) {
				ActiveTimer += Time.deltaTime;
			} else {
				ActiveTimer = 0;
			}
		} else {
			ActiveTimer = 0;
		}
		if (NeedsGem)
		{
			if(AttatchedGemScript.Collected && AttatchedGemScript.SR.sprite == AttatchedGemScript.GemCollected) {
				gameObject.layer = 10;
				MR.material = Connected;
				Set = true;
			} else {
				gameObject.layer = 0;
				MR.material = Unconnected;
			}
		}

		if (Set) {
			gameObject.layer = 10;
		}
			
	}
		

	public void AddGravProj(GravProjectile GProj)
	{

		if (AttatchedGemScript != null && AttatchedGemScript.Collected) {
			if (!NeedsGem) {
				Instantiate (Global.Dataholder.GravBallPop, GProj.transform.position, transform.rotation);
				Destroy (GProj);
			}
		}
			
		if (Global.Dataholder.GravTileA != null || Global.Dataholder.GravTileB != null) {
			if (Global.Dataholder.GravTileA != null && transform.IsChildOf (Global.Dataholder.GravTileA.transform) && Global.Dataholder.GravTileA != this)
			{
				Global.Dataholder.GravTileA.AddGravProj (GProj);
				return;
			}
			else if(Global.Dataholder.GravTileB != null && transform.IsChildOf (Global.Dataholder.GravTileB.transform) && Global.Dataholder.GravTileB != this) 
			{
				Global.Dataholder.GravTileB.AddGravProj (GProj);
				return;
			}
		}
		if ((Global.Dataholder.GravTileA != null && Global.Dataholder.GravTileA != this) && (Global.Dataholder.GravTileB != null && Global.Dataholder.GravTileB != this) && Global.Dataholder.GravTileB.Active && Global.Dataholder.GravTileA.Active) {
			Global.Dataholder.GravTileA.Clear ();
			Global.Dataholder.GravTileB.Clear ();
			Global.Dataholder.FreezeGravTiles ();
			Global.Dataholder.GravTileA = null;
			Global.Dataholder.GravTileB = null;
		}

		if ((Global.Dataholder.GravTileA == null || Global.Dataholder.GravTileA != this) && (Global.Dataholder.GravTileB == null || Global.Dataholder.GravTileB != this)) {
			if (Global.Dataholder.RemoveTileANext) {
				if (Global.Dataholder.GravTileA != null) {
					Global.Dataholder.GravTileA.Clear ();
					Global.Dataholder.FreezeGravTiles ();
				}
				Global.Dataholder.GravTileA = this;
				MakeParent ();

			} else {
				if (Global.Dataholder.GravTileB != null) {
					Global.Dataholder.GravTileB.Clear ();
					Global.Dataholder.FreezeGravTiles ();
				}
				Global.Dataholder.GravTileB = this;
				MakeParent ();
			}
			Global.Dataholder.RemoveTileANext = !Global.Dataholder.RemoveTileANext;
		}

		GravProjs.Add (GProj);
		if (GravProjs.Count > 4) {
			Instantiate (Global.Dataholder.GravBallPop, GravProjs [0].transform.position, transform.rotation);
			Destroy (GravProjs [0].gameObject);
			GravProjs.RemoveAt (0);
		}
		if (GravProjs.Count >= 1) {
			LocalGravProjCenter = Vector3.zero;
			for (int i = 0; i < GravProjs.Count; i++) {
				LocalGravProjCenter += GravProjs [i].transform.position;
			}
			LocalGravProjCenter /= GravProjs.Count;
			LocalGravProjCenter -= transform.position;
			LocalGravProjCenter = new Vector3 (LocalGravProjCenter.x, LocalGravProjCenter.y, 0);
			Active = true;
		}
	}

	public void Clear()
	{
		for (int i = 0; i < GravProjs.Count; i++) {
			if (GravProjs [i] != null) {
				Instantiate (Global.Dataholder.GravBallPop, GravProjs [i].transform.position, transform.rotation);
				Destroy (GravProjs [i].gameObject);
			}
		}
		LocalGravProjCenter = Vector3.zero;
		Active = false;
		for (int i = 0; i < transform.childCount; i++) {
			GravableTile GT = transform.GetChild (i).GetComponent<GravableTile> ();
			if (GT != null) {
				GT.Clear (); // this isn't *really* recursive.
			}
		}
		GravProjs.Clear ();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (!Static) {
			if (other.tag == "Ground") {
				if (GameEnder) {
					Global.Dataholder.EndingScreenFlash.SetActive (true);
					Global.Dataholder.GameEnded = true;
					Global.Dataholder.VoiceQueue.Clear ();
					if (Global.Dataholder.CurrentVoiceLine != null) {
						Global.Dataholder.CurrentVoiceLine.Abort ();
					}
					return;
				}

				if (Global.Dataholder.GravTileA != null && Global.Dataholder.GravTileB != null) {
					bool checks = false;
					if (Global.Dataholder.GravTileA == this || Global.Dataholder.GravTileB == this) {
						checks = true;
					} else {
						GravableTile P = transform.parent.GetComponent<GravableTile> ();
						if (P != null) {
							if (Global.Dataholder.GravTileA == P || Global.Dataholder.GravTileB == P) {
								if (other.gameObject != P.gameObject) {
									checks = true;
								}
							}
						}
					}

					if (Global.Dataholder.GravTileA.NonStick || Global.Dataholder.GravTileB.NonStick) {
						if (NonStick) {
							if (ActiveTimer > 0.5f) {
								// all clear
							} else {
								return;
							}
						} else {
							return;
						}
					}

					if (checks) {
						if (other.gameObject == Global.Dataholder.GravTileA.gameObject || other.gameObject == Global.Dataholder.GravTileB.gameObject || other.gameObject.transform.parent.gameObject == Global.Dataholder.GravTileA.gameObject || other.gameObject.transform.parent.gameObject == Global.Dataholder.GravTileB.gameObject) 
						{
							if (!Global.Dataholder.GravTileA.Static && !Global.Dataholder.GravTileB.Static && !Global.Dataholder.GravTileA.NonStick && !Global.Dataholder.GravTileB.NonStick) {
								Global.Dataholder.GravTileB.NestUnder (Global.Dataholder.GravTileA);
								Global.Dataholder.GravTileA.MakeParent ();
							}

							Global.Dataholder.TutorialModeNoMoving = false;


							Global.Dataholder.FreezeGravTiles ();
							Global.Dataholder.GravTileA.Clear ();
							Global.Dataholder.GravTileB.Clear ();
							if (!Global.Dataholder.GravTileA.Static) {
								if (!Global.Dataholder.GravTileA.DontChangeTexture) {
									Global.Dataholder.GravTileA.MR.material = Connected;
								}
								if (!Global.Dataholder.GravTileA.NeedsGem) {
									if (!Global.Dataholder.GravTileA.DontChangeTexture) {									
										Global.Dataholder.GravTileA.MR.material = Connected;
									}
									Global.Dataholder.GravTileA.gameObject.layer = 10;
									Global.Dataholder.GravTileA.Set = true;
								} else {
									Global.Dataholder.GravTileA.gameObject.layer = 0;
								}
							}
							if (!Global.Dataholder.GravTileB.Static) {			
								
								if (!Global.Dataholder.GravTileB.NeedsGem) {
									if (!Global.Dataholder.GravTileB.DontChangeTexture) {									
										Global.Dataholder.GravTileB.MR.material = Connected;
									}
									Global.Dataholder.GravTileB.gameObject.layer = 10;
									Global.Dataholder.GravTileB.Set = true;
								} else {
									Global.Dataholder.GravTileB.gameObject.layer = 0;
								}			
							}
							if (!Global.Dataholder.GravTileA.GetComponent<Gem> () && !Global.Dataholder.GravTileB.GetComponent<Gem> ()) {
								Global.Dataholder.ShockwaveObject.transform.position = new Vector3 ((Global.Dataholder.GravTileA.transform.position.x + Global.Dataholder.GravTileB.transform.position.x) / 2, (Global.Dataholder.GravTileA.transform.position.y + Global.Dataholder.GravTileB.transform.position.y) / 2, -15);
								Global.Dataholder.ShockwaveObject.timer = 0;
							}
							Global.Dataholder.GravTileA = null;
							Global.Dataholder.GravTileB = null;
							Global.Dataholder.ComputeReconstructionPercentage ();
						}				
					}
				}
			}
		}
	}

	public void NestUnder(GravableTile Tile)
	{
		if (Tile.transform.IsChildOf (transform)) {
			Tile.transform.parent = transform.parent;
		} 
		transform.parent = Tile.transform;
		// transfer all children
		int c = transform.childCount;
		while(transform.childCount > 0)
		{
			transform.GetChild (0).transform.parent = Tile.transform;
		}
	}

	public void MakeParent()
	{
		if (transform.parent.GetComponent<GravableTile> ()) {
			transform.parent.GetComponent<GravableTile> ().NestUnder (this);
		}
	}


}
