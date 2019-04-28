using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidHandler : MonoBehaviour {
	public ResourceTypes resource;
	public float vainSize = 5;
    public float timeBetweenHumans = 60f;
	[HideInInspector]
	public List<BuildingTypes> buildings;
    private int numberOfHumans = 0;
    private int maxNumberOfHumans = 0;
    private float humanTimer;

	public void updateBuildings() {
		int numOfDrills = 0;
        maxNumberOfHumans = 0;

		foreach (BuildingTypes building in buildings) {
			if (building == BuildingTypes.drill) numOfDrills++;
            if (building == BuildingTypes.house) maxNumberOfHumans += 5;
		}

		Resources.resources[(int)resource] += (int) (vainSize * Mathf.Pow(numOfDrills, 2f / 3f)) * numberOfHumans/3;

        if (humanTimer > timeBetweenHumans) {
            humanTimer = 0;
            numberOfHumans++;
        }

        Resources.resources[(int)ResourceTypes.fuel] -= numberOfHumans/5;
	}
}
