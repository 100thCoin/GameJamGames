using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur : MonoBehaviour {

	public bool Door;

	public int Health;

	public SpriteRenderer SR;
	public SpriteRenderer Skeleton;
	public SpriteRenderer White;

	public float WhiteTimer;
	public float DeadTimer = 0.5f;

	public int Bones;

	public GameObject Bone;

	public bool floating;

	public RuntimeAnimatorController Wiggle1;
	public RuntimeAnimatorController Wiggle2;
	public RuntimeAnimatorController Floaty;
	public Animator Anim;

	public Sprite[] DinoSprites;
	public Sprite[] Skeletons;
	public Sprite[] Whites;

	public int Level;
	public Collider MainCol;

	public bool Ptera;


	// Use this for initialization
	void Start () {

		if(Door)
		{
			return;
		}


		if(Level == 0)
		{
			int r = Random.Range(0,3);
			if(Ptera)
			{
				r = 1;
			}
			SR.sprite = DinoSprites[r];
			Skeleton.sprite = Skeletons[r];
			White.sprite = Whites[r];
			floating = r == 1;
			if(r == 1)
			{
				//pterodactly
				transform.parent.position += new Vector3(0,Random.Range(4f,8f),0);

			}
		}
		else if(Level == 1)
		{

			int r = Random.Range(0,4);
			SR.sprite = DinoSprites[3+r];
			Skeleton.sprite = Skeletons[3+r];
			White.sprite = Whites[3+r];
		}
		else
		{
			int r = Random.Range(0,3);
			SR.sprite = DinoSprites[7+r];
			Skeleton.sprite = null;
			White.sprite = null;
			Health = 1;
			MainCol.enabled = false;
		}

		if(floating)
		{
			Anim.runtimeAnimatorController = Floaty;

		}
		else
		{
			bool sure = Random.Range(0,2)==1;
			Anim.runtimeAnimatorController = sure ? Wiggle1 : Wiggle2;
		}

		Anim.Play("Main",0,Random.Range(0f,1f));
	}
	
	// Update is called once per frame
	void Update () {

		if(Door)
		{
			if(Health <= 0)
			{
				Destroy(gameObject);
			}
			return;
		}

		WhiteTimer -= Time.deltaTime*2f;
		White.color = new Vector4(1,1,1,WhiteTimer);
		if(Health <= 0)
		{
			if(Level == 2)
			{
				DeadTimer = -1;
			}

			Anim.speed = 0;
			SR.sortingOrder = 5;
			SR.color = Color.black;
			Skeleton.enabled = true;
			White.color = new Vector4(1,1,1,0);
			DeadTimer -= Time.deltaTime;
		}

		if(DeadTimer < 0)
		{
			int i = 0;
			while(i < Bones)
			{

				Instantiate(Bone,transform.position + new Vector3(Random.Range(2f,-2f),Random.Range(2f,-2f),0),transform.rotation);

				i++;
			}

			Destroy(gameObject);
		}

	}

	public void Hurt()
	{
		WhiteTimer = 1;
		Health -= 1;

	}

}
