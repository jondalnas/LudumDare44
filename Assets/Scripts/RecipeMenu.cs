using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeMenu : MonoBehaviour {
    private Transform render;

    void Start() {
        render = transform.GetChild(0);
    }

	public void showMenu(int building) {
        render.gameObject.SetActive(true);

        for (int i = 0; i < render.childCount; i++) {
            Transform child = render.GetChild(i);
            if (child.childCount == 0) continue;

            child.GetComponentInChildren<Text>().text = "0";
        }

        for (int i = 0; i < Resources.recipes[building].resources.Length; i++) {
            ResourceTypes resource = Resources.recipes[building].resources[i];

            render.Find(char.ToUpper(System.Enum.GetName(typeof(ResourceTypes), resource)[0]) + System.Enum.GetName(typeof(ResourceTypes), resource).Substring(1)).Find("Text").GetComponent<Text>().text = Resources.recipes[building].numberOfResources[i] + "";
        }
    }

    void Update() {
        if (!render.gameObject.activeSelf) return;

        transform.position = Input.mousePosition;
    }
}
