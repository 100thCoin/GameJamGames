using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorblindTooltipZone : MonoBehaviour {

	public bool WeirdFace;
	public bool PRaaanked;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if(WeirdFace)
			{
				if(!G.Main.ArbitrarilyLargeArrayOfBooleans[4])
				{
					G.Main.ArbitrarilyLargeArrayOfBooleans[4] = true;
					Instantiate(G.Main.VALines[11]);
				}
			}
			if(PRaaanked)
			{
				if(!G.Main.ArbitrarilyLargeArrayOfBooleans[55])
				{
					G.Main.ArbitrarilyLargeArrayOfBooleans[55] = true;
					Instantiate(G.Main.VALines[12]);
				}
			}
			else
			{
				if(!G.Main.ArbitrarilyLargeArrayOfBooleans[3])
				{
					G.Main.ArbitrarilyLargeArrayOfBooleans[3] = true;
					Instantiate(G.Main.VALines[6]);
				}
			}
			
			
			Destroy(gameObject);

		}


	}


}
