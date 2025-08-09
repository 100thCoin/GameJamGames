using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape2D : MonoBehaviour {

	public List<Vector2> vertecies;
	public Vector3[] debugTris;
	public bool DebugTestEveryFrame;
	public bool ScaleUVs;
	public MeshFilter MF;
	public Mesh M;
	public PolygonCollider2D PolyCol;
	public PolygonCollider2D PolyColTrigger;

	[ContextMenu("Circle")]
	public void MakeCircle()
	{
		float radius = 8;
		for (int i = 0; i < vertecies.Count; i++) {

			vertecies[i] = new Vector2(-Mathf.Cos(((i+0.0f)/vertecies.Count)*Mathf.PI*2)*radius,Mathf.Sin(((i+0.0f)/vertecies.Count)*Mathf.PI*2)*radius);

			/*
			if (i < vertecies.Count / 2) {
				vertecies[i] = new Vector2(-Mathf.Cos(((i+0.0f)/vertecies.Count)*Mathf.PI*4)*radius,Mathf.Sin(((i+0.0f)/vertecies.Count)*Mathf.PI*4)*radius);

			} else {
				vertecies[i] = new Vector2(-Mathf.Cos(((i+0.0f)/vertecies.Count)*Mathf.PI*4)*radius*0.6f,-Mathf.Sin(((i+0.0f)/vertecies.Count)*Mathf.PI*4)*radius*0.6f);

			}*/

		}


	}

	// Use this for initialization
	void Start () {
		
	}



	static bool IsTriangleClockwise(Vector2 a, Vector2 b, Vector2 c)
	{
		//sum the edges
		float ab = (b.x-a.x)*(b.y+a.y);
		float bc = (c.x-b.x)*(c.y+b.y);
		float ca = (a.x-c.x)*(a.y+c.y);
		float sum = ab + bc + ca;
		return sum > 0;

	}

	static void debugClockwise(Vector2 a, Vector2 b, Vector2 c)
	{
		//sum the edges
		float ab = (b.x-a.x)*(b.y+a.y);
		float bc = (c.x-b.x)*(c.y+b.y);
		float ca = (a.x-c.x)*(a.y+c.y);
		float sum = ab + bc + ca;
		print (sum);

	}

	static bool IsAngleConcave(Vector2 a, Vector2 b, Vector2 c)
	{
		Vector2 ba = a-b;
		Vector2 bc = c-b;
		// the cross product
		float cross = ba.x *bc.y - ba.y - bc.x;
		return cross < 0;
	}

	static float sign(Vector2 a, Vector2 b, Vector2 c)
	{
		return (a.x - c.x) * (b.y - c.y) - (b.x - c.x) * (a.y - c.y);
	}

	static bool PointInsideTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
	{
		
		float d1 = sign (p, a, b);
		float d2 = sign (p, b, c);
		float d3 = sign (p, c, a);
		bool has_negative = (d1 < 0 || d2 < 0 || d3 < 0);
		bool has_positive = (d1 > 0 || d2 > 0 || d3 > 0);
		return !(has_negative && has_positive);
	}

	// Update is called once per frame
	void Update () {
		
		if (DebugTestEveryFrame) {
			MakeMesh ();
		}
	
	}

	[ContextMenu("CreateMesh")]
	public void MakeMesh()
	{
		if (vertecies.Count >= 3) {
			M = new Mesh();
			MF.sharedMesh = M;

			M.Clear ();
			List<int> tris = new List<int> ();
			int C = vertecies.Count;
			bool[] EliminatedVertices = new bool[C];
			while (C > 3) {
				int prevC = C;
				int i = 0;
				while (i < vertecies.Count) {
					// determine if the angle at this vertex is convex
					if (EliminatedVertices [i]) {
						i++;
						continue;
					}
					int prev = vertecies.Count + i - 1;
					int next = vertecies.Count + i + 1;
					while (EliminatedVertices [prev % vertecies.Count]) {
						prev--;
					}
					while (EliminatedVertices [next % vertecies.Count]) {
						next++;
					}
					prev %= vertecies.Count;
					next %= vertecies.Count;

					if (!IsTriangleClockwise (vertecies [prev], vertecies [i], vertecies [next])) {
						i++;
						continue;
					}
					// now we need to check if all other vertices are inside this triangle.
					int j = 0;
					bool EmptyTri = true;
					while (j < vertecies.Count) {
						if (EliminatedVertices [j] || j == prev || j == next || j == i) {
							j++;
							continue;
						}

						// check if this vertex is inside the triangle.
						if (PointInsideTriangle (vertecies [j], vertecies [prev], vertecies [i], vertecies [next])) {
							EmptyTri = false;
							break;
						}
						j++;
					}
					if (EmptyTri) {
						EliminatedVertices [i] = true;
						tris.Add (prev);
						tris.Add (i);
						tris.Add (next);							
						C--;
						break;
					} else {
						i++;
					}
				}
				if (prevC == C) {
					print ("ERROR");
					return;
				}
			}
			if (C == 3) {
				// add the remaiing points to the triangle list.
				int i = 0;
				while (i < vertecies.Count) {
					if (EliminatedVertices [i]) {
						i++;
						continue;
					}
					tris.Add (i);
					i++;
				}
			}
			Vector3[] verts = new Vector3[vertecies.Count];
			for (int i = 0; i < verts.Length; i++) {
				verts [i] = new Vector3 (vertecies [i].x, vertecies [i].y, 0);
			}
			debugTris = new Vector3[tris.Count / 3];
			for (int i = 0; i < debugTris.Length; i++) {
				debugTris [i] = new Vector3 (tris[i*3],tris[i*3+1],tris[i*3+2]);
			}
			M.vertices = verts;
			M.triangles = tris.ToArray ();
			if (ScaleUVs) {
				float smallestX = float.PositiveInfinity;
				float smallestY = float.PositiveInfinity;
				float largestX = float.NegativeInfinity;
				float largestY = float.NegativeInfinity;
				for (int i = 0; i < verts.Length; i++) {
					if(verts[i].x < smallestX){smallestX = verts [i].x;}
					if(verts[i].x < smallestY){smallestY = verts [i].y;}
					if(verts[i].x > largestX){largestX = verts [i].x;}
					if(verts[i].x > largestY){largestY = verts [i].y;}
				}
				float diffX = largestX-smallestX;
				float diffY = largestY-smallestY;
				Vector2[] Uvs =vertecies.ToArray();
				for (int i = 0; i < Uvs.Length; i++) {
					Uvs [i] = new Vector2 ((Uvs [i].x- smallestX) / diffX, (Uvs [i].y - smallestY) / diffY);
				}
				M.uv = Uvs;

			} else {
				M.uv = vertecies.ToArray();

			}

			PolyCol.points = vertecies.ToArray();
			PolyColTrigger.points = vertecies.ToArray ();
		}
	}

	[ContextMenu("Clear")]
	public void ClearMesh()
	{
		M = null;
		MF.sharedMesh = null;
		PolyCol.points = new Vector2[0];
		PolyColTrigger.points = new Vector2[0];
	}

}
