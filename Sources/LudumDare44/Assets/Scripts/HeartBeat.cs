using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour {

	public TextMesh MoneyCount;


	public float ContantHeartbeatDuration;
	public float HeartbeatTimer;
	public GameObject oof;
	public GameObject Heart;

	public GameObject OofCoin;

	// Use this for initialization
	void Start () {
		MoneyCount.text = "Money: " + GetComponent<DataHolder>().Money;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(GetComponent<DataHolder>().Money <= 0)
		{
			GetComponent<DataHolder>().Dead = true;
		}


		if(!GetComponent<DataHolder>().WinGame && !GetComponent<DataHolder>().Dead)
		{
		HeartbeatTimer += Time.fixedDeltaTime;
		}
		if(HeartbeatTimer > ContantHeartbeatDuration)
		{
			HeartbeatTimer -= ContantHeartbeatDuration;

			if(GetComponent<DataHolder>().Outside)
			{
				Instantiate(oof,new Vector3(MoneyCount.transform.position.x+2,MoneyCount.transform.position.y,MoneyCount.transform.position.z),transform.rotation,MoneyCount.transform.parent);
				GetComponent<DataHolder>().Money--;
				Instantiate(OofCoin,GetComponent<DataHolder>().Player.transform.position,gameObject.transform.rotation,gameObject.transform);
				if(Random.Range(0,5) == 0)
				{
					Instantiate(OofCoin,GetComponent<DataHolder>().Player.transform.position,gameObject.transform.rotation,gameObject.transform);
				}
				if(Random.Range(0,5) == 0)
				{
					Instantiate(OofCoin,GetComponent<DataHolder>().Player.transform.position,gameObject.transform.rotation,gameObject.transform);
				}
				GetComponent<DataHolder>().TotalHeart++;
				MoneyCount.text = "Money: " + GetComponent<DataHolder>().Money;
			}
		}

		if(GetComponent<DataHolder>().Outside)
		{
			MoneyCount.color = new Vector4(1,HeartbeatTimer,HeartbeatTimer,1);
		}
		else
		{
			MoneyCount.color = new Vector4(1-HeartbeatTimer,1,1-HeartbeatTimer,1);
		}

		Heart.transform.localScale = Vector3.one * (HeartbeatTimer + 0.2f)/(HeartbeatTimer+0.3f);

	}
}
