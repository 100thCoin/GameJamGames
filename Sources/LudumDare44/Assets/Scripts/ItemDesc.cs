using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDesc : MonoBehaviour {

	public TextMesh Name;
	public TextMesh Price;

	public ItemStat Itemstat;

	public float XDelayTimer;
	public float XDelay;
	public float XTimer;
	public float LocalXSize;
	public float XDuration;


	// Use this for initialization
	void Start () {

		Name.text = "" + Itemstat.Name;
		Price.text = "$" + Itemstat.Price;

	}
	
	// Update is called once per frame
	void FixedUpdate () {


		XDelayTimer += Time.fixedDeltaTime;

		XTimer += Time.fixedDeltaTime;

			//LocalXPos = ((((LastXpos-2*((TargetXpos + 2 - (0.5f*LastXpos))-2)))/2)*Mathf.Pow((1/XDuration)*XTimer,2)-(LastXpos-2*((TargetXpos + 2 - (0.5f*LastXpos))-2))*(1/XDuration)*XTimer+(LastXpos));

			LocalXSize = (((0 - 1) * Mathf.Pow(XTimer,2))/Mathf.Pow(XDuration,2) - ((2*0 - 2*1) * XTimer)/XDuration + 0);

			//	Y = (((a - h) * Mathf.Pow(timer,2))/Mathf.Pow(dur,2) - ((a - h) * timer)/dur + a);


		if(XTimer >= XDuration)
		{
			LocalXSize = 1;
		}

		transform.localScale = new Vector3(LocalXSize,1,1);

		Name.color = new Vector4(1,1,1,5 - XDelayTimer);
		Price.color = new Vector4(1,1,1,5 - XDelayTimer);

		if(XDelayTimer > 5)
		{
			Destroy(gameObject);
		}

	}
}
