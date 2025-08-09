using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMove : MonoBehaviour {

	public float speed;
	public SpriteRenderer SR;
	public Transform Shadow;
	public SpriteRenderer ShadowR;
	public float timer;
	public bool EnemyFire;
	public GameObject Explode;

	public float Dist;

	public GameObject Trail;

	public bool Trailless;

	public Environment Env;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(EnemyFire)
		{
			
			if(Env.NecroDied || Env.PlayerDied)
			{
				Explodee();
			}
		}
		transform.localPosition += new Vector3(0,speed*Time.fixedDeltaTime,0);
		if(Random.Range(0,5f) >4f)
		{
			SR.flipX = !SR.flipX;
		}

		timer += Time.fixedDeltaTime * 0.2f;

		if(!EnemyFire)
		{
			if(Shadow != null)
			{
				Shadow.transform.localScale = new Vector3(0.5f - timer*2, 0.5f - timer*2,1);
				ShadowR.color = new Vector4(0,0,0,0.25f - timer);
			}
		}
		else
		{
			Dist = (transform.localPosition.y + 1)/20;

			Shadow.transform.localScale = new Vector3(0.65f/(Dist+1),0.65f/(Dist+1),1);
			ShadowR.color = new Vector4(0,0,0,0.5f/(Dist+1)-Dist/10);
			if(transform.localPosition.y <= 0.6f && !Trailless)
			{
				GameObject TrailHolder = Instantiate(Explode,transform.position - new Vector3(0,0.5f,0),Explode.transform.rotation);
				Trail.transform.parent = TrailHolder.transform;
				Trail.GetComponent<ParticleSystem>().emissionRate = 0;
				Destroy(transform.parent.gameObject);
			}

		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player" && EnemyFire)
		{
			if(Dist <= 0.1f)
			{
				CharMovement CHR = other.GetComponent<CharMovement>();
				CHR.HeartVis = 1f;
				CHR.HP--;
				if(CHR.HP != 0)
				{
					Instantiate(CHR.Hurt,transform.position,transform.rotation);
				}

				Explodee();

			}
			else if(Trailless)
			{
				CharMovement CHR = other.GetComponent<CharMovement>();
				CHR.HeartVis = 1f;
				CHR.HP--;
				if(CHR.HP != 0)
				{
					Instantiate(CHR.Hurt,transform.position,transform.rotation);
				}
				Explodee();
			}
		}

	}

	public void Explodee()
	{
		GameObject TrailHolder = Instantiate(Explode,transform.position - new Vector3(0,0.5f,0),Explode.transform.rotation);
		Trail.transform.parent = TrailHolder.transform;
		Trail.GetComponent<ParticleSystem>().emissionRate = 0;
		Destroy(transform.parent.gameObject);
	}

}
