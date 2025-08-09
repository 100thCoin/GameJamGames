#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CustomEditor(typeof(CubicBezierCurve))]
public class CubicBezierCurveInspector : Editor {

	private CubicBezierCurve curve;
	private Transform t;
	private Quaternion rot;
	private const int lineSteps = 128;

	private int selectedIndex = -1;
	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;


	void OnSceneGUI()
	{
		curve = target as CubicBezierCurve;
		t = curve.transform;
		rot = Tools.pivotRotation == PivotRotation.Local ? t.rotation : Quaternion.identity;

		Vector3 p0 = ShowPoint(0);
		Vector3 p1 = ShowPoint(1);
		Vector3 p2 = ShowPoint(2);
		Vector3 p3 = ShowPoint(3);




		Handles.color = Color.gray;
		Handles.DrawLine(p0, p1);
		Handles.DrawLine(p2, p3);

		Handles.color = Color.white;
		Vector3 sPos = curve.GetPoint(0f);
		Vector3 dPos;
		int i = 1;
		while (i <= lineSteps)
		{
			dPos = curve.GetPoint(i/(float)lineSteps);
			Handles.DrawLine(sPos,dPos);
			sPos = dPos;
			i++;
		}

		// show distance from point D to curve




	

		if(curve.ShowBounds)
		{
			Handles.color = Color.green;
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[2],curve.boundingBox[4])), t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[2],curve.boundingBox[4]))); // nX,nY,nZ - xX,nY,nZ
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[2],curve.boundingBox[4])), t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[3],curve.boundingBox[4]))); // nX,nY,nZ - nX,xY,nZ
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[2],curve.boundingBox[4])), t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[2],curve.boundingBox[5]))); // nX,nY,nZ - nX,nY,xZ
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[2],curve.boundingBox[4])), t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[3],curve.boundingBox[4]))); // xX,nY,nZ - xX,xY,nZ
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[2],curve.boundingBox[4])), t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[2],curve.boundingBox[5]))); // nX,nY,nZ - xX,nY,xZ
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[3],curve.boundingBox[4])), t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[3],curve.boundingBox[4]))); // nX,xY,nZ - xX,xY,nZ
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[3],curve.boundingBox[4])), t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[3],curve.boundingBox[5]))); // nX,xY,nZ - nX,xY,xZ
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[3],curve.boundingBox[4])), t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[3],curve.boundingBox[5]))); // xX,xY,nZ - xX,xY,xZ
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[2],curve.boundingBox[5])), t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[2],curve.boundingBox[5]))); // nX,nY,xZ - xX,nY,xZ
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[2],curve.boundingBox[5])), t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[3],curve.boundingBox[5]))); // nX,nY,xZ - nX,xY,xZ
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[2],curve.boundingBox[5])), t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[3],curve.boundingBox[5]))); // xX,nY,xZ - xX,xY,xZ
			Handles.DrawLine(t.TransformPoint(new Vector3(curve.boundingBox[0],curve.boundingBox[3],curve.boundingBox[5])), t.TransformPoint(new Vector3(curve.boundingBox[1],curve.boundingBox[3],curve.boundingBox[5]))); // nX,xY,xZ - xX,xY,xZ
		}


	}


	private Vector3 ShowPoint(int index)
	{
		Vector3 p = t.TransformPoint(curve.p[index]);
		float size = HandleUtility.GetHandleSize(p);
		Handles.color = Color.white;
		if(Handles.Button(p,rot,handleSize * size,pickSize * size,Handles.DotCap))
		{
			selectedIndex = index;
		}
		if(selectedIndex == index)
		{
			EditorGUI.BeginChangeCheck();
			p = Handles.DoPositionHandle(p, rot);
			if(EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(curve, "Move Point");
				EditorUtility.SetDirty(curve);
				curve.p[index] = t.InverseTransformPoint(p);

			}
		}
		return p;
	}
	private Vector3 ShowDistPoint()
	{
		Vector3 p = t.TransformPoint(curve.TestPoint);
		float size = HandleUtility.GetHandleSize(p);
		Handles.color = Color.black;
		if(Handles.Button(p,rot,handleSize * size,pickSize * size,Handles.DotCap))
		{
			selectedIndex = 4;
		}
		if(selectedIndex == 4)
		{
			EditorGUI.BeginChangeCheck();
			p = Handles.DoPositionHandle(p, rot);
			if(EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(curve, "Move Point");
				EditorUtility.SetDirty(curve);
				curve.TestPoint = t.InverseTransformPoint(p);

			}
		}
		return p;
	}

}
