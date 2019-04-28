using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour {
	[HideInInspector]
	public static float GRAVITATIONAL_CONSTANT = 6.67408f * Mathf.Pow(10, -11);

	void Start () {
	}
	
	void Update () {
		Vector2 vSum = Vector2.zero;

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Astroid")) {
			Vector2 diff = g.transform.position - transform.position;
			float distance = diff.sqrMagnitude;

			float f = g.GetComponent<AstroidGravity>().calculateGravityForce(distance);
			Vector2 v = diff.normalized * f;
			vSum += v;
		}

		GetComponent<Rigidbody2D>().AddForce(vSum);
	}
}
