using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageController : MonoBehaviour {
	[HideInInspector]
	public GameObject building;

	void OnCollisionEnter2D(Collision2D col) {
		ContactPoint2D[] points = new ContactPoint2D[2];
		col.GetContacts(points);
		GameObject buildingGameObject = (GameObject) Instantiate(building, points[0].point, Quaternion.identity, col.transform);

        buildingGameObject.transform.up = points[0].normal;
        col.gameObject.GetComponent<AstroidHandler>().buildings.Add(buildingGameObject.GetComponent<BuildingHandler>().type);

        Destroy(gameObject);
	}
}
