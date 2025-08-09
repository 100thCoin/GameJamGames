using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectJumped : MonoBehaviour {

	public bool Active;
	public bool FirstFrameDone;
	public int DetectCount;
	public DataHolder Main;

	public Transform PointBubbleOrigin;
	public GameObject PointBubblePrefab;
	public GameObject SpawnedBubble;

	public Sprite score100;
	public Sprite score300;
	public Sprite score500;
	public Sprite score800;
	public MoveCharacter P;

	public GameObject ScoreJingle;

	public Vector3 JumpOrigin;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(Active)
		{
			if(!FirstFrameDone)
			{
				transform.position = JumpOrigin;
				FirstFrameDone = true;
			}
			else
			{
				int score = 0;
				switch(DetectCount)
				{
				case 0: break;
				case 1: score = 100; break;
				case 2: score = 300; break;
				default: score = 500; break;
				}
				FirstFrameDone = false;
				Active = false;
				transform.localPosition = new Vector3(0,0,-500);
				if(score > 0)
				{
					Main.Score += score;
					Main.NewestScore = score;

					DetectCount = 0;
					FirstFrameDone = false;
					Active = false;
					SpawnedBubble = Instantiate(PointBubblePrefab,PointBubbleOrigin.transform.position,transform.rotation);
					switch(score)
					{
					case 100: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = score100; break;
					case 300: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = score300; break;
					case 500: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = score500; break;
					case 800: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = score800; break;
					}
					SpawnedBubble.transform.position *= 16;
					SpawnedBubble.transform.position = new Vector3(Mathf.Round(SpawnedBubble.transform.position.x),Mathf.Round(SpawnedBubble.transform.position.y),Mathf.Round(SpawnedBubble.transform.position.z));
					SpawnedBubble.transform.position /= 16;
					P.DetectedJumpedOver = true;
					Instantiate(ScoreJingle,transform.position,transform.rotation);
				}
			}
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "JumpScore")
		{
			DetectCount++;
		}

	}

}
