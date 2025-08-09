using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonManager : MonoBehaviour {

	public Mesh RibbonMesh;
	public int SliceCount;
	public float DistanceBetweenSlices;
	public float RibbonHeight;
	public List<Vector3> PointsTop;
	public List<Vector3> PointsBottom;
	public bool MovingLeftEnd;
	public bool RibbonIsOrientedLeft; // we need to flip the order of all the points when changing direction. the movement code is unchanged.

	public MeshFilter RibbonMeshFilter;

	public Transform Player;
	public Vector3 PlayerLastPosition;

	public bool TEST;

	public bool PlayerIsInsideRibbon;

	public RibbonColl[] Colliders;

	public string[] TagsByRibbonID;

	public bool JustUpdoodled;

	public void Updoodle(float length)
	{
		// I pray I can get this to work swiftly.

		//length of 8 = 512 strips
		//so that's 64 strips per length

		SliceCount = Mathf.RoundToInt (64 * length);

		List<Vector3> NewPointsTop = new List<Vector3> (SliceCount);
		List<Vector3> NewPointsBottom = new List<Vector3> (SliceCount);

		int i = 0;
		while (i < SliceCount) {
			if (i < PointsTop.Count) {
				NewPointsTop.Add (PointsTop [i]);
				NewPointsBottom.Add (PointsBottom [i]);
			} else {
				NewPointsTop.Add (PointsTop [PointsTop.Count-1]);
				NewPointsBottom.Add (PointsBottom [PointsTop.Count-1]);
			}

			i++;	
		}

		PointsTop = NewPointsTop;
		PointsBottom = NewPointsBottom;
		JustUpdoodled = true;
	}


	// Use this for initialization
	void Start () {

		int i = 0;
		while (i < SliceCount) {
			PointsTop.Add(new Vector3(-i*DistanceBetweenSlices,RibbonHeight,0));
			PointsBottom.Add(new Vector3(-i*DistanceBetweenSlices,0,0));
			i++;	
		}

		PlayerLastPosition = Player.position;


		Mesh M = new Mesh ();

		List<Vector3> CombinedList = new List<Vector3>();
		Vector2[] uvs = new Vector2[SliceCount*4];

		i = 0;
		while (i < SliceCount) {

			CombinedList.Add (PointsTop [i]);
			CombinedList.Add (PointsBottom [i]);
			if (MovingLeftEnd) {
				uvs [i * 2] = new Vector2 (1-(i + 0f) / SliceCount, 1);
				uvs [(i * 2) + 1] = new Vector2 (1-(i + 0f) / SliceCount, 0);
			} else {
				uvs [i * 2] = new Vector2 ((i + 0f) / SliceCount, 1);
				uvs [(i * 2) + 1] = new Vector2 ((i + 0f) / SliceCount, 0);
			}
			i++;
		}

		i = 0;
		// duplicate all the vertices for nice backface lighting
		while (i < SliceCount) {

			CombinedList.Add (CombinedList [i*2]);
			CombinedList.Add (CombinedList [(i*2)+1]);
			uvs [SliceCount*2+(i * 2)] = uvs [i * 2];
			uvs [SliceCount*2+(i * 2)+1] = uvs [(i * 2)+1];
			i++;
		}


		i = 0;

		List<int> tris = new List<int> ();

		while (i < SliceCount-1) {

			tris.Add (i * 2);
			tris.Add ((i * 2)+1);
			tris.Add ((i * 2)+3);

			tris.Add (i * 2);
			tris.Add ((i * 2)+3);
			tris.Add ((i * 2)+2);

			tris.Add ((SliceCount*2+i * 2)+3);
			tris.Add ((SliceCount*2+i * 2)+1);
			tris.Add (SliceCount*2+i * 2);


			tris.Add ((SliceCount*2+i * 2)+2);
			tris.Add ((SliceCount*2+i * 2)+3);
			tris.Add (SliceCount*2+i * 2);


			i++;
		}


		M.Clear ();
		M.vertices = CombinedList.ToArray();
		M.triangles = tris.ToArray();



		M.uv = uvs;


		M.RecalculateBounds ();
		M.RecalculateNormals ();
		M.RecalculateTangents();
		RibbonMeshFilter.mesh = M;



	}
	
	// Update is called once per frame
	void Update () {

		if (TEST) {
			if (MovingLeftEnd != RibbonIsOrientedLeft) {
				RibbonIsOrientedLeft = MovingLeftEnd;

				PointsTop.Reverse ();
				PointsBottom.Reverse ();

				//recalculate diff?
			}
			// calculate how much the player moved.

			float diff = (Player.position - PlayerLastPosition).magnitude;

			if (diff > 0.01f || JustUpdoodled) {
				JustUpdoodled = false;

				// the player moved, so now we have to tug the entire ribbon along one node at a time.
				List<Vector3> NewPointsTop = new List<Vector3>();
				List<Vector3> NewPointsBottom = new List<Vector3>();




				int n = 0;
				while (n < SliceCount) {
					//diff is the distance we need to move the slice.
					float PointOffset = n * DistanceBetweenSlices;
					if (PointOffset - diff <= 0) {
						// just lerp between point 0 and the player
						float distFromN0 = (diff - PointOffset);
						float t = distFromN0 / diff;

						Vector3 NewTop = new Vector3 (Mathf.Lerp (PointsTop [0].x, Player.position.x, t), Mathf.Lerp (PointsTop [0].y, Player.position.y + RibbonHeight * 0.5f, t), Mathf.Lerp (PointsTop [0].z, Player.position.z, t));
						Vector3 NewBottom = new Vector3 (Mathf.Lerp (PointsBottom [0].x, Player.position.x, t), Mathf.Lerp (PointsBottom [0].y, Player.position.y - RibbonHeight * 0.5f, t), Mathf.Lerp (PointsBottom [0].z, Player.position.z, t));

						if (float.IsNaN (NewTop.x) || float.IsNaN (NewTop.y) || float.IsNaN (NewTop.z) || float.IsNaN (NewBottom.x) || float.IsNaN (NewBottom.y) || float.IsNaN (NewBottom.z)) {
							return;

						}

						NewPointsTop.Add (NewTop);
						NewPointsBottom.Add (NewBottom);
					} else {
						// find the index to lerp with
						int ind = Mathf.CeilToInt ((PointOffset - diff) / DistanceBetweenSlices);
						// find the amount to lerp
						float t = (diff % DistanceBetweenSlices) / DistanceBetweenSlices; // t needs to be between 0 and 1, not 0 and DistanceBetweenSlices
						// get new position
						Vector3 NewTop = new Vector3 (Mathf.Lerp (PointsTop [ind].x, PointsTop [ind - 1].x, t), Mathf.Lerp (PointsTop [ind].y, PointsTop [ind - 1].y, t), Mathf.Lerp (PointsTop [ind].z, PointsTop [ind - 1].z, t));
						Vector3 NewBottom = new Vector3 (Mathf.Lerp (PointsBottom [ind].x, PointsBottom [ind - 1].x, t), Mathf.Lerp (PointsBottom [ind].y, PointsBottom [ind - 1].y, t), Mathf.Lerp (PointsBottom [ind].z, PointsBottom [ind - 1].z, t));
						if (float.IsNaN (NewTop.x) || float.IsNaN (NewTop.y) || float.IsNaN (NewTop.z) || float.IsNaN (NewBottom.x) || float.IsNaN (NewBottom.y) || float.IsNaN (NewBottom.z)) {

							return;

						}

						NewPointsTop.Add (NewTop);
						NewPointsBottom.Add (NewBottom);
					}
					n++;
				}

				PointsTop = NewPointsTop;
				PointsBottom = NewPointsBottom;

				Mesh M = new Mesh ();

				List<Vector3> CombinedList = new List<Vector3>();
				Vector2[] uvs = new Vector2[SliceCount*4];

				int i = 0;
				while (i < SliceCount) {

					CombinedList.Add (PointsTop [i]);
					CombinedList.Add (PointsBottom [i]);
					if (MovingLeftEnd) {
						uvs [i * 2] = new Vector2 (1-(i + 0f) / SliceCount, 1);
						uvs [(i * 2) + 1] = new Vector2 (1-(i + 0f) / SliceCount, 0);
					} else {
						uvs [i * 2] = new Vector2 ((i + 0f) / SliceCount, 1);
						uvs [(i * 2) + 1] = new Vector2 ((i + 0f) / SliceCount, 0);
					}
					i++;
				}

				i = 0;
				// duplicate all the vertices for nice backface lighting
				while (i < SliceCount) {

					CombinedList.Add (CombinedList [i*2]);
					CombinedList.Add (CombinedList [(i*2)+1]);
					uvs [SliceCount*2+(i * 2)] = uvs [i * 2];
					uvs [SliceCount*2+(i * 2)+1] = uvs [(i * 2)+1];
					i++;
				}


				i = 0;

				List<int> tris = new List<int> ();

				while (i < SliceCount-1) {

					tris.Add (i * 2);
					tris.Add ((i * 2)+1);
					tris.Add ((i * 2)+3);

					tris.Add (i * 2);
					tris.Add ((i * 2)+3);
					tris.Add ((i * 2)+2);

					tris.Add ((SliceCount*2+i * 2)+3);
					tris.Add ((SliceCount*2+i * 2)+1);
					tris.Add (SliceCount*2+i * 2);


					tris.Add ((SliceCount*2+i * 2)+2);
					tris.Add ((SliceCount*2+i * 2)+3);
					tris.Add (SliceCount*2+i * 2);


					i++;
				}


				M.Clear ();
				M.vertices = CombinedList.ToArray();
				M.triangles = tris.ToArray();



				M.uv = uvs;


				M.RecalculateBounds ();
				M.RecalculateNormals ();
				M.RecalculateTangents();
				RibbonMeshFilter.mesh = M;

				//the mesh is good

				// update the colldiers;

				i = 0;
				int div = SliceCount / Colliders.Length;

				while (i < Colliders.Length) {

					// current slice position

					Vector3 position = PointsTop [i * div] - new Vector3 (0, RibbonHeight/2f, 0);
					Colliders [i].transform.position = position;

					if (Global.Dataholder.TriStrip) {

						float t = 0;
						if (RibbonIsOrientedLeft) {
							t = (i * div + 0f) / SliceCount;

						} else {
							t = 1 - (i * div + 0f) / SliceCount;

						}
						if (t > Global.Dataholder.TriStripProperties [0].StartT && t < Global.Dataholder.TriStripProperties [0].StopT) {
							Colliders [i].tag = TagsByRibbonID [Global.Dataholder.TriStripProperties [0].ID];
						} else if (t > Global.Dataholder.TriStripProperties [1].StartT && t < Global.Dataholder.TriStripProperties [1].StopT) {
							Colliders [i].tag = TagsByRibbonID [Global.Dataholder.TriStripProperties [1].ID];

						} else if (t > Global.Dataholder.TriStripProperties [2].StartT && t < Global.Dataholder.TriStripProperties [2].StopT) {
							Colliders [i].tag = TagsByRibbonID [Global.Dataholder.TriStripProperties [2].ID];

						}
					} else {
						Colliders [i].tag = TagsByRibbonID [Global.Dataholder.LoneStripProperties.ID];

					}

					i++;

				}




				PlayerLastPosition = Player.position;


			}
		}


	}
}
