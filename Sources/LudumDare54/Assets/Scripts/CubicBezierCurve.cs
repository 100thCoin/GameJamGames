using UnityEngine;

public class CubicBezierCurve : MonoBehaviour {

	public Vector3[] p;
	float[] boundingTs;
	public bool ShowBounds;
	public float[] boundingBox;

	public Vector3 TestPoint;

	public bool TempDebug_ShowDistFromTest;
	public Vector3 DistCurveOffset;

	public void Reset()
	{
		p = new Vector3[]
		{
			new Vector3(0,0.5f,0),
			new Vector3(0.5f,1,0),
			new Vector3(0.5f,0,0),
			new Vector3(1,0.5f,0)
		};
	}

	public Vector3 GetPoint(float t)
	{

		return transform.TransformPoint(CubicBezier.GetPoint(p[0],p[1],p[2],p[3],t));
	}



}
