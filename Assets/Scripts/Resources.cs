using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour {
    public GameObject menu;

	[HideInInspector]
	public static int[] resources = new int[(int) ResourceTypes.length+1];
	public static Recipe[] recipes = new Recipe[] { new Recipe(BuildingTypes.house, new ResourceTypes[] { ResourceTypes.rock, ResourceTypes.iron, ResourceTypes.fuel }, new int[] { 100, 20, 5 }),
													 new Recipe(BuildingTypes.drill, new ResourceTypes[] { ResourceTypes.iron, ResourceTypes.fuel  }, new int[] { 200, 25 }),
													 new Recipe(BuildingTypes.rocketSilo, new ResourceTypes[] { ResourceTypes.iron, ResourceTypes.copper, ResourceTypes.rock, ResourceTypes.fuel  }, new int[] { 500, 250, 1000, 2000 }),
													 new Recipe(BuildingTypes.barrack, new ResourceTypes[] { ResourceTypes.rock, ResourceTypes.iron, ResourceTypes.fuel  }, new int[] { 100, 40, 50 }),
													 new Recipe(BuildingTypes.researchFacility, new ResourceTypes[] { ResourceTypes.rock, ResourceTypes.iron, ResourceTypes.copper, ResourceTypes.fuel  }, new int[] { 200, 40, 10, 500 }),
													 new Recipe(BuildingTypes.refinery, new ResourceTypes[] { ResourceTypes.iron, ResourceTypes.copper, ResourceTypes.aluminium, ResourceTypes.fuel  }, new int[] { 500, 500, 75, 500 }) };

    void Start() {
        resources[(int)ResourceTypes.rock] = 500;
        resources[(int)ResourceTypes.iron] = 1000;
        resources[(int)ResourceTypes.fuel] = 500;
        resources[(int)ResourceTypes.copper] = 50;
    }

    void Update() {
        for (int i = 0; i < resources.Length-1; i++) {
            menu.transform.Find(char.ToUpper(System.Enum.GetName(typeof(ResourceTypes), (ResourceTypes) i)[0]) + System.Enum.GetName(typeof(ResourceTypes), (ResourceTypes) i).Substring(1)).Find("Text").GetComponent<Text>().text = resources[i] + "";
        }
    }

    public static bool canCreateRecipe(int index) {
        Recipe recipe = recipes[index];
        for (int i = 0; i < recipe.resources.Length; i++) {
            int resourceCount = resources[(int) recipe.resources[i]];
            int neededResources = recipe.numberOfResources[i];
            if (neededResources > resourceCount) return false;
        }

        for (int i = 0; i < recipe.resources.Length; i++) {
            resources[(int) recipe.resources[i]] -= recipe.numberOfResources[i];
        }

        return true;
    }

    public static void useResource(int resource, int count) {
        resources[resource] -= count;
    }
}

public enum ResourceTypes {
	rock, iron, fuel, copper, aluminium, length
}

public class Recipe {
    public BuildingTypes type;
    public ResourceTypes[] resources;
    public int[] numberOfResources;

    public Recipe(BuildingTypes type, ResourceTypes[] resources, int[] numberOfResources) {
        this.resources = resources;
        this.numberOfResources = numberOfResources;
        this.type = type;
    }
}