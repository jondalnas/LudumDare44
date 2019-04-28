using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameLoop : MonoBehaviour {
	public GameObject[] astroids;
	public float astroidUpdateTime = 2f;

	private float astroidUpdateTimer;

	void Start () {
		astroids = GameObject.FindGameObjectsWithTag("Astroid");
	}
	

	void Update () {
		astroidUpdateTimer += Time.deltaTime;

		if (astroidUpdateTimer < astroidUpdateTime) return;

		astroidUpdateTimer = 0;

		foreach (GameObject astroid in astroids) {
			astroid.GetComponent<AstroidHandler>().updateBuildings();
		}
	}
}
