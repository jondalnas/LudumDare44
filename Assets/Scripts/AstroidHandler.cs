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
    private int[] localResources = new int[Resources.resources.Length];
    private int loadResourceTimer;
    public float discoverAstroidChance = 0.2f;
    private float instability;

    public void updateBuildings() {
        if (buildings.Count == 0) return;

        int numOfDrills = 0;
        int numOfRefineries = 0;
        int numOfBarracks = 0;
        maxNumberOfHumans = 0;

		foreach (BuildingTypes building in buildings) {
            if (building == BuildingTypes.drill) numOfDrills++;
            else if (building == BuildingTypes.researchFacility) {
                if (Random.Range(0f, 100f) < discoverAstroidChance) discoverNewAstroid();
            } else if (building == BuildingTypes.house) {
                maxNumberOfHumans += 5;
            } else if (building == BuildingTypes.refinery) {
                numOfRefineries++;
            } else if (building == BuildingTypes.barrack) {
                numOfBarracks++;
            }
        }

        instability += 1f / (3f + numOfBarracks) * 2;

        if (instability >= 100) {
            BuildingTypes type = buildings[Random.Range(0, buildings.Count)];
            for (int i = 0; i < transform.GetComponentsInChildren<BuildingHandler>().Length; i++) {
                BuildingHandler bh = transform.GetComponentsInChildren<BuildingHandler>()[i];
                if (bh.type == type) {
                    Destroy(bh.gameObject);
                    break;
                }
            }

            buildings.Remove(type);

            instability = Mathf.Pow(1f/2f,numOfBarracks+1-Mathf.Log(100)/Mathf.Log(2));
        }

        if (numberOfHumans < maxNumberOfHumans/5) numberOfHumans = maxNumberOfHumans/5;

        localResources[(int)resource] += (int) ((vainSize * Mathf.Pow(numOfDrills, 2f / 3f)) * Mathf.Max(numberOfHumans / 3, 1) * Mathf.Pow(3, numOfRefineries));
        localResources[(int)ResourceTypes.rock] += (int) ((Mathf.Pow(numOfDrills, 1f / 2f)) * Mathf.Max(numberOfHumans / 3, 1) * numOfRefineries / 2);

        if (humanTimer > timeBetweenHumans) {
            humanTimer -= timeBetweenHumans;
            numberOfHumans++;
        }

        //Resources.resources[(int)ResourceTypes.fuel] -= numberOfHumans/5;
	}

    public void loadResources() {
        if (instability > 0) instability -= 10f * Time.deltaTime;

        if (loadResourceTimer++ % (30/numberOfHumans) != 0) return;

        if (localResources[(int)resource] > Mathf.CeilToInt(numberOfHumans / 5f) - 1) {
            Resources.resources[(int)resource] += Mathf.CeilToInt(numberOfHumans / 5f);
            localResources[(int)resource] -= Mathf.CeilToInt(numberOfHumans / 5f);
        }

        if (localResources[(int)ResourceTypes.rock] > Mathf.CeilToInt(numberOfHumans / 5f) - 1) {
            Resources.resources[(int)ResourceTypes.rock] += Mathf.CeilToInt(numberOfHumans / 5f);
            localResources[(int)ResourceTypes.rock] -= Mathf.CeilToInt(numberOfHumans / 5f);
        }
    }

    public void discoverNewAstroid() {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();

        float distance = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject astroid in GameObject.FindGameObjectsWithTag("Astroid")) {
            float currDistance = (transform.position - astroid.transform.position).sqrMagnitude;

            if (!player.getKnownAstroids().Contains(astroid.transform) && currDistance < distance) {
                closest = astroid;
                distance = currDistance;
            }
        }

        PlayerController.seeAstroid(closest.transform);
    }

    void Update() {
        humanTimer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            PlayerController.seeAstroid(transform);
        }
    }
}
