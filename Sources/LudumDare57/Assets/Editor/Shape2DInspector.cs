#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Shape2D))]
public class Shape2DInspector : Editor {

	void OnSceneGUI()
	{
		shape2d = target as Shape2D;
		t = shape2d.transform;
		rot = Tools.pivotRotation == PivotRotation.Local ? t.rotation : Quaternion.identity;
		List<Vector2> points = shape2d.vertecies;
		if (shape2d.vertecies.Count > 0) {
			for (int i = 0; i < points.Count; i++) {
				ShowPoint (i);
			}
		}

		Handles.color = Color.green;
		for (int i = 0; i < points.Count; i++) {
			int next = (i + points.Count + 1) % points.Count;
			Handles.DrawLine(t.position + new Vector3(points[i].x,points[i].y,0),t.position+ new Vector3(points[next].x,points[next].y,0));
		}

	}

	private Shape2D shape2d;
	private Transform t;
	private Quaternion rot;
	private int selectedIndex = -1;
	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;

	private Vector3 ShowPoint(int index)
	{
		Vector3 p = t.TransformPoint(shape2d.vertecies[index]);
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
				Undo.RecordObject(shape2d, "Move Point");
				EditorUtility.SetDirty(shape2d);
				shape2d.vertecies[index] = t.InverseTransformPoint(p);
			}
		}
		return p;
	}

}
