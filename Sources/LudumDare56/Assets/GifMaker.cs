using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GifMaker : MonoBehaviour {
	public int count = 0;
	// Use this for initialization
	void Start () {
		
	}

	public RenderTexture RT;

	// Update is called once per frame
	void FixedUpdate () {
		Texture2D RTRender = new Texture2D(512,16,TextureFormat.RGBA32,false);
		RenderTexture.active = RT;

		RTRender.ReadPixels (new Rect (0, 0, 512, 16), 0, 0);
		File.WriteAllBytes ("C:\\Users\\100th_Coin\\Documents\\LudumDare56\\birdgif\\Image_" + count + ".png",RTRender.EncodeToPNG());
		count++;
	}
}
