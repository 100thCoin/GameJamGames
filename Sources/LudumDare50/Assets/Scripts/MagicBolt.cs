using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBolt : MonoBehaviour {

	public Vector3 Speed;
	public SpriteRenderer Colored;
	public SpriteRenderer White;

	public Rigidbody RB;

	public Color[] Randoms;

	public GameObject[] Explosions;

	public GameObject SpriteExplosion;

	public MoveChar Char;

	public GameObject[] Explosion;
	public GameObject Laser;

	void OnEnable()
	{
		Instantiate(Laser);
	}

	void Start()
	{
		Colored.color = Randoms[Random.Range(0,Randoms.Length)];
	}

	// Update is called once per frame
	void FixedUpdate () {

		RB.MovePosition(RB.position + Speed);
		transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(Speed.y,Speed.x) * Mathf.Rad2Deg);

		White.transform.localScale = new Vector3(Speed.magnitude*2,3f/16f,1);
		Colored.transform.localScale = new Vector3(Speed.magnitude*2+2f/16f,5f/16f,1);

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Tile")
		{
			Explode(other);
			Destroy(gameObject);

		}
		else if(other.tag == "Dino")
		{
			if(!Char.ClearedTutorial3)
			{
				Char.AssignTextT("");
			}


			Explode(other);

			other.gameObject.GetComponent<Dinosaur>().Hurt();

			Destroy(gameObject);
		}


	}

	void Explode(Collider other)
	{
		int r = Random.Range(0,Explosions.Length);
		int r2 = Random.Range(0,Explosions.Length);

		ColorExplosion Cx = Instantiate(Explosions[r],(transform.position*6 + other.transform.position)/7,transform.rotation).GetComponent<ColorExplosion>();
		ColorExplosion Cx2 = Instantiate(Explosions[r2],(transform.position*6 + other.transform.position)/7,transform.rotation).GetComponent<ColorExplosion>();
		Instantiate(SpriteExplosion,(transform.position*6 + other.transform.position)/7,transform.rotation);


		int r3 = Random.Range(0,Explosion.Length);
		Instantiate(Explosion[r3]);


		Cx.Colorr = Colored.color;
		Cx2.Colorr = Colored.color;
	}


}
