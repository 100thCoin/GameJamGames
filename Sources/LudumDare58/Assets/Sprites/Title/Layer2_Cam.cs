using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer2_Cam : MonoBehaviour {

	RenderTexture RT;
	public Camera VoidCam;
	public Material Layer2Mat;

	void OnRenderImage (RenderTexture src, RenderTexture dst)
	{

		RT = VoidCam.targetTexture;
		//Graphics.Blit (src, dst, Pixelated);

		src.antiAliasing = 1;
		src.filterMode = FilterMode.Point;


		if (RT != null) {
			RT.filterMode = FilterMode.Point;
		} else {
			RT = new RenderTexture (384, 256, 0);
			Graphics.Blit (src, RT);
			RT.filterMode = FilterMode.Point;

		}
		src.antiAliasing = 1;

		Graphics.Blit (src, dst);

		Layer2Mat.SetTexture ("_MainTex", src);
		Graphics.Blit (RT, dst, Layer2Mat);




	}
}
