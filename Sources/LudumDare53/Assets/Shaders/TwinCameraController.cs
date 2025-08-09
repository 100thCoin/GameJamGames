using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinCameraController : MonoBehaviour 
{
	public bool ForTransition;
	public Camera AltCam;

	void Start()
	{
		RenderTexture rt = new RenderTexture(768, 512, 24);

		rt.filterMode = FilterMode.Point;
		if (ForTransition) {
			Shader.SetGlobalTexture ("_VoidTexture1", rt);
		} else {
			Shader.SetGlobalTexture ("_VoidTexture", rt);

		}
		AltCam.targetTexture = rt;
		AltCam.ResetAspect();
		AltCam.ResetCullingMatrix();
		AltCam.ResetProjectionMatrix();
	}


}