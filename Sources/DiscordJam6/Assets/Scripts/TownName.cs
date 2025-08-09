using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownName : MonoBehaviour {

	public Font CFont;
	public TextMesh TM;
	public TownLore Lore;

	public TextMesh[] Outline;

	public bool TEST;

	public GameObject[] SignExtend;



	[ContextMenu ("Fix Font")]
	void FixFont()
	{
		CFont = TM.font;
		CFont.material.mainTexture.filterMode = FilterMode.Point;
	}


	// Use this for initialization
	void Start () {
		
	}

	void Update()
	{
		if(TEST)
		{
			TEST = false;
			Lore.Names.Clear();
			Lore.Start();
			UpdateText();
		}
	}
	
	[ContextMenu ("UpdateText")]
	public void UpdateText()
	{
		//generate name;

		int i = 0;
		string[] Names = Lore.Names.ToArray();
		string TownName = "";
		while(i < Names.Length)
		{
			TownName += Names[i];
			i++;
		}

		i = 0;
		Names = Lore.CapNames.ToArray();
		while(i < Names.Length)
		{
			TownName += Names[i];
			i++;
		}

		i=0;
		int j = 0;
		string NameWithNewLines = "";
		while(i < TownName.Length)
		{
			NameWithNewLines += TownName.ToCharArray()[i];
			if(j == 80)
			{
				NameWithNewLines += "\n";
				j = 2;
			}
			j++;
			i++;
		}
		i = 0;


		TownName = NameWithNewLines;
		TM.text = TownName;
		Outline[0].text = TownName;
		Outline[1].text = TownName;
		Outline[2].text = TownName;
		Outline[3].text = TownName;
		Outline[4].text = TownName;
		Outline[5].text = TownName;
		FullTownName = TownName;
		if(TownName.Length > 40)
		{
			TM.fontSize = 16;
			Outline[0].fontSize= 16;
			Outline[1].fontSize= 16;
			Outline[2].fontSize= 16;
			Outline[3].fontSize= 16;
			Outline[4].fontSize= 16;
			Outline[5].fontSize= 16;
		}
		else if(TownName.Length > 20)
		{
			TM.fontSize = 32;
			Outline[0].fontSize= 32;
			Outline[1].fontSize= 32;
			Outline[2].fontSize= 32;
			Outline[3].fontSize= 32;
			Outline[4].fontSize= 32;
			Outline[5].fontSize= 32;
		}

		if(TownName.Length > 79*3)
		{
			SignExtend[0].SetActive(true);
		}
		if(TownName.Length > 79*6)
		{
			SignExtend[1].SetActive(true);
		}
		if(TownName.Length > 79*9)
		{
			SignExtend[2].SetActive(true);
		}

		if((Lore.Names.Count + Lore.CapNames.Count) % 4 == 0)
		{
			int RNG = Random.Range(0,Lore.StarterSeconds.Length);
			Lore.CapNames.Add(Lore.StarterSeconds[RNG]);
			UpdateText();
		}

	}

	public string FullTownName;
}
