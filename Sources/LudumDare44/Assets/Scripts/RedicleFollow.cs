using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedicleFollow : MonoBehaviour {

	public GameObject Redicle;

	public Vector2 GridOffset;

	public GameObject CurrentBlock;

	public int Mode;

	public Sprite Mode0;
	public Sprite Mode1;
	public Sprite Mode2;

	public Sprite R_Circle;
	public Sprite R_Ladder;
	public Sprite R_Lantern;

	public GameObject Mode0_col;
	public GameObject Mode1_col;

	public bool CanPlaceLadder;

	public DataHolder Main;

	public GameObject Ladder;
	public GameObject Lantern;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() 
	{
		if(Mode == -1)
		{
			GetComponent<SpriteRenderer>().enabled = false;
			Redicle.GetComponent<SpriteRenderer>().enabled = false;

		}
		else
		{
			Redicle.GetComponent<SpriteRenderer>().enabled = true;
		}
		if(Input.GetKey(KeyCode.Alpha1) && Main.UnlockedPick)
		{
			Mode = 0;
		}
		if(Input.GetKey(KeyCode.Alpha2) && Main.TUTHasEverLadderite)
		{
			Mode = 1;
		}
		if(Input.GetKey(KeyCode.Alpha3) && Main.TUTHasEverLantern)
		{
			Mode = 2;
		}

		transform.position = new Vector3(Mathf.Round(Redicle.transform.position.x + GridOffset.x) - GridOffset.x,Mathf.Round(Redicle.transform.position.y + GridOffset.y) - GridOffset.y,-1);

		if(Mode == 0)
		{
			GetComponent<SpriteRenderer>().sprite = Mode0;
			Redicle.GetComponent<SpriteRenderer>().sprite = R_Circle;

			Mode1_col.SetActive(false);
			Mode0_col.SetActive(true);

		}
		else if(Mode == 1)
		{
			GetComponent<SpriteRenderer>().sprite = Mode1;
			Redicle.GetComponent<SpriteRenderer>().sprite = R_Ladder;

			Mode1_col.SetActive(true);
			Mode0_col.SetActive(false);
			if(CurrentBlock != null)
			{
				CurrentBlock.GetComponent<BlockMining>().RedicleOver = false;
			
				if(CurrentBlock.GetComponent<BlockMining>().LadderBase != null)
				{
					CurrentBlock = CurrentBlock.GetComponent<BlockMining>().LadderBase;
				}

				if(Input.GetKeyDown(KeyCode.Mouse0) && CurrentBlock.GetComponent<BlockMining>().NOLADDER == false)
				{
					if(Main.RefinedLadderite >0)
					{
						Main.RefinedLadderite--;
						GameObject TheLadder = Instantiate(Ladder,new Vector3(CurrentBlock.transform.position.x,CurrentBlock.transform.position.y + 1 + CurrentBlock.GetComponent<BlockMining>().Ladders,0),gameObject.transform.rotation,CurrentBlock.transform.parent) as GameObject;
						TheLadder.GetComponent<BlockMining>().LadderBase = CurrentBlock;
						CurrentBlock.GetComponent<BlockMining>().Ladders++;
					}
				}
			}
		}
		else if(Mode == 2)
		{
			GetComponent<SpriteRenderer>().sprite = Mode2;
			Redicle.GetComponent<SpriteRenderer>().sprite = R_Lantern;

			Mode1_col.SetActive(true);
			Mode0_col.SetActive(false);
			if(CurrentBlock != null)
			{
				CurrentBlock.GetComponent<BlockMining>().RedicleOver = false;


				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					if(Main.Lanterns >0)
					{
						Main.Lanterns--;
						GameObject TheLantern = Instantiate(Lantern,new Vector3(CurrentBlock.transform.position.x,CurrentBlock.transform.position.y + 1 + CurrentBlock.GetComponent<BlockMining>().Ladders,0),gameObject.transform.rotation,CurrentBlock.transform.parent) as GameObject;
						//TheLadder.GetComponent<BlockMining>().LadderBase = CurrentBlock;
						//CurrentBlock.GetComponent<BlockMining>().Ladders++;
					}
				}
			}
		}
		
	
	
	}

	void OnTriggerStay(Collider other)
	{
		if(Mode == 0)
		{
			if(other.GetComponent<BlockMining>())
			{
				if(!other.GetComponent<BlockMining>().Indestructible)
				{
					CurrentBlock = other.gameObject;
					CurrentBlock.GetComponent<BlockMining>().RedicleOver = true;
					GetComponent<SpriteRenderer>().enabled = true;
				}
			}
		}
		else
		{
			if(other.GetComponent<BlockMining>())
			{
				CurrentBlock = other.gameObject;
				//CurrentBlock.GetComponent<BlockMining>().RedicleOver = true;
				GetComponent<SpriteRenderer>().enabled = true;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(Mode == 0)
		{
			if(other.GetComponent<BlockMining>())
			{
				if(CurrentBlock != null)
				{
				CurrentBlock.GetComponent<BlockMining>().RedicleOver = false;
				}
				GetComponent<SpriteRenderer>().enabled = false;
				CurrentBlock = null;

			}
		}
		else
		{
			if(other.GetComponent<BlockMining>())
			{
				if(CurrentBlock != null)
				{
					CurrentBlock.GetComponent<BlockMining>().RedicleOver = false;
				}
				GetComponent<SpriteRenderer>().enabled = false;
				CurrentBlock = null;

			}
		}
	}


}
