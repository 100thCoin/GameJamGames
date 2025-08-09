using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessShader : MonoBehaviour {

	public Material TheShader;

	public float[] Offsets_X;
	public float[] Offsets_Y;

	public GameObject[] Targets;

	// Use this for initialization
	void Start () {

		float[] Base = new float[1023];

		TheShader.SetFloatArray("_OffsetX",Base);
		TheShader.SetFloatArray("_OffsetY",Base);
		TheShader.SetFloat("_DotCount",0);

	}
	
	// Update is called once per frame
	void Update () {
		int i = 0;
		while(i < Targets.Length)
		{
			if(Targets[i] != null)
			{
				Offsets_X[i] = -((Targets[i].transform.position.x - gameObject.transform.position.x) / 24f );
				Offsets_Y[i] = -((Targets[i].transform.position.y - gameObject.transform.position.y) / 24f);
			}
			else
			{
				Offsets_X[i] = -1000;
				Offsets_Y[i] = -1000;
			}
			i++;
		}

		TheShader.SetFloatArray("_OffsetX",Offsets_X);
		TheShader.SetFloatArray("_OffsetY",Offsets_Y);
		TheShader.SetFloat("_DotCount",Targets.Length);
	}

	[ContextMenu("Update Shader")]
	public void UpdateShader()
	{
		
		TheShader.SetFloatArray("_OffsetX",Offsets_X);
		TheShader.SetFloatArray("_OffsetY",Offsets_Y);
		TheShader.SetFloat("_DotCount",Targets.Length);
	}

	[ContextMenu("Update Arrays")]
	public void UpdateArrays()
	{
		DataHolder Main = GameObject.Find("Main").GetComponent<DataHolder>();
		int s = Main.LightSources.Length;
		Offsets_X = new float[256];
		Offsets_Y = new float[256];
		Targets = new GameObject[s];
		int i = 0;
		while(i < s)
		{
			Targets[i] = Main.LightSources[i];
			i++;
		}

	}

}
