﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 5f;
	public float rotationSpeed = 10f;
	public Transform thrustPosition;
	public GameObject package;
	public GameObject[] buildings;
    public static int currentBuilding;
    public float fuelBurningMultiplier = 2.5f;
    private float fuelBurnt;

	void Update() {
		if (Input.GetButtonDown("Fire")) {
            if (Resources.canCreateRecipe(currentBuilding))
			    Instantiate(package, transform.position, transform.rotation).GetComponent<PackageController>().building = buildings[currentBuilding];
		}
	}
	
	void FixedUpdate () {
        if (Resources.resources[(int)ResourceTypes.fuel] <= 0) return;

        float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		transform.Rotate(new Vector3(0, 0, -horizontal * rotationSpeed));

		Vector2 f = transform.rotation * Vector2.up * vertical * speed;

		GetComponent<Rigidbody2D>().AddForce(f);

        transform.GetChild(0).GetComponent<Animator>().SetBool("burning", vertical > 0);

        fuelBurnt += (Mathf.Abs(vertical) * speed + Mathf.Abs(horizontal) * rotationSpeed * 1/3f) * Time.fixedDeltaTime * fuelBurningMultiplier;

        if (fuelBurnt >= 1) {
            fuelBurnt--;
            Resources.useResource((int)ResourceTypes.fuel, 1);
        }
	}

    public void setCurrentBuilding(int buildingIndex) {
        currentBuilding = buildingIndex;
    }
}
