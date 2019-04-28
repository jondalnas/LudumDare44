using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidGravity : MonoBehaviour {
	private float astroidMassGravConstant;
	public float massOfSpacecraft = 129700f;

	public void Start() {
		//Multiplies the mass of the astroid with the gravitational constant, based on the fomula
		//F=G*(m_1*m_2)/d^2
		//Where m_1, m_2 and G are constant, for faster calculations
		astroidMassGravConstant = PlanetGravity.GRAVITATIONAL_CONSTANT * transform.GetComponent<Rigidbody2D>().mass * massOfSpacecraft * massOfSpacecraft;
	}

	public float calculateGravityForce(float distanceSqr) {
		return astroidMassGravConstant / distanceSqr;
	}
}
