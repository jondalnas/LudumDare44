using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public static Transform currentFocus;
    public static List<Transform> focuses = new List<Transform>();

    private static int focusIndex;

    void Start() {
        Transform player = GameObject.Find("Player").transform;

        focuses.Add(player);
        currentFocus = player;
    }

	void FixedUpdate () {
		transform.position = new Vector3(currentFocus.position.x, currentFocus.position.y, transform.position.z);
	}

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            changeFocus();
        }
    }

    public static void changeFocus() {
        PlayerController pc;
        if (pc = currentFocus.GetComponent<PlayerController>()) {
            pc.enabled = false;
        }

        currentFocus = focuses[focusIndex++%focuses.Count];

        if (pc = currentFocus.GetComponent<PlayerController>()) {
            MainGameLoop.currentPlayer = currentFocus.gameObject;
            pc.enabled = true;
        }
    }
}
