using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer2 : MonoBehaviour {

	public Camera VoidCam;

	void Start()
	{
		RenderTexture rt = new RenderTexture(640, 480, 24);

		rt.filterMode = FilterMode.Point;

		Shader.SetGlobalTexture("_VoidTexture", rt);
		VoidCam.targetTexture = rt;
		VoidCam.ResetAspect();
		VoidCam.ResetCullingMatrix();
		VoidCam.ResetProjectionMatrix();
	}
}
