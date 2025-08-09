using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubicBezier {

	public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		//t = Mathf.Clamp01(t);


		/*
		float oneMinusT = 1f-t;

		return 
			oneMinusT * oneMinusT * oneMinusT * p0 +
			3f * oneMinusT * oneMinusT * t * p1 +
			3f * oneMinusT * t * t * p2 +
			t * t * t * p3;
		*/


		//standard form (fastest to compute)
	
		Vector3 a = -p0 + 3*p1 -3*p2 + p3;
		Vector3 b = 3*p0 - 6*p1 + 3*p2;
		Vector3 c = -3*p0 + 3*p1;
		Vector3 d = p0;

		return
			a * t*t*t +
			b * t*t +
			c * t +
			d;
		

		// bernstein polynomial form
		/*
		 return 
			p0 *( -t*t*t + 3*t*t - 3*t + 1) +
			p1 *( 3*t*t*t -6*t*t + 3*t) +
			p2 * (-3*t*t*t + 3*t*t) +
			p3 * t*t*t; 
		 */

	}









}