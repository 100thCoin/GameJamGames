using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistRibbonMan : MonoBehaviour {

	public Mesh RibbonMesh;
	public int SliceCount;
	public float DistanceBetweenSlices;
	public float RibbonHeight;
	public List<Vector3> PointsTop;
	public List<Vector3> PointsBottom;
	public MeshFilter RibbonMeshFilter;

	public float Twisty;

	public float Delay;



	public float Timer;

	public bool TWIST_IT_YO;



	// Use this for initialization
	void Start () {
		int i = 0;
		while (i < SliceCount) {
			PointsTop.Add(new Vector3(-i*DistanceBetweenSlices,RibbonHeight,0));
			PointsBottom.Add(new Vector3(-i*DistanceBetweenSlices,0,0));
			i++;	
		}
	}
	
	// Update is called once per frame
	void Update () {

		Delay += Time.deltaTime;
		if (Delay > 1) {
			TWIST_IT_YO = true;
		}

		if (TWIST_IT_YO) {

			Timer += Time.deltaTime*0.5f;

			if (Timer >= 1) {
				Timer = 1;
				TWIST_IT_YO = false;
			}

			Twisty = DataHolder.TwoCurveLerp (0, 70, Timer, 1);

		}

		// the player moved, so now we have to tug the entire ribbon along one node at a time.
		List<Vector3> NewPointsTop = new List<Vector3>();
		List<Vector3> NewPointsBottom = new List<Vector3>();


		float twosty = Twisty * 0.001f;

		int n = 0;
		while (n < SliceCount) {
			//diff is the distance we need to move the slice.
			float PointOffset = n * DistanceBetweenSlices;
	
			NewPointsTop.Add (new Vector3 (PointsTop [n].x,4 * Mathf.Cos(n*twosty),4 * Mathf.Sin(n*twosty)));				
			NewPointsBottom.Add (new Vector3 (PointsTop [n].x,-4 * Mathf.Cos(n*twosty),-4 * Mathf.Sin(n*twosty)));

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
				uvs [i * 2] = new Vector2 ((i + 0f) / SliceCount, 1);
				uvs [(i * 2) + 1] = new Vector2 ((i + 0f) / SliceCount, 0);
			
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
}
