using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour {
    public BuildingTypes type;
}

public enum BuildingTypes {
    house, drill, rocketSilo, barrack, researchFacility, refinery, length
}