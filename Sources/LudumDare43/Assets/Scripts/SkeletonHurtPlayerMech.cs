using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHurtPlayerMech : MonoBehaviour {

	Vector2 Dir;
	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.name == "Mary")
		{

			other.gameObject.GetComponent<MaryController>().Hurt();
			Dir = new Vector2(other.transform.position.x - gameObject.transform.position.x,other.transform.position.y - gameObject.transform.position.y - 1).normalized;

			gameObject.transform.localPosition = new Vector3(0,0,-500);
			StartCoroutine(Wait());
		}


	}


	IEnumerator Wait()
	{

		yield return new WaitForSeconds(0.3f);
		Dir = Dir * -1;

		gameObject.transform.parent.GetComponent<AI_Skeleton>().Visual.GetComponent<SpriteRenderer>().sprite = gameObject.transform.parent.GetComponent<AI_Skeleton>().Jump;

		gameObject.transform.parent.GetComponent<AI_Skeleton>().ThreeAxisVel = new Vector3(Dir.x * gameObject.transform.parent.GetComponent<AI_Skeleton>().Speed,Dir.y * gameObject.transform.parent.GetComponent<AI_Skeleton>().Speed,gameObject.transform.parent.GetComponent<AI_Skeleton>().JumpHeight);
		gameObject.transform.parent.GetComponent<AI_Skeleton>().OnGround = false;

	}

}
