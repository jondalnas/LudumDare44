using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarArrow : MonoBehaviour {
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Color[] waypointColors;
    public float disapearingCloseMargin = 10f;

    private float maxSize;
    private float aspectRatio;

    void Start() {
        Canvas canvas = FindObjectOfType<Canvas>();

        maxSize = Mathf.Min(canvas.GetComponent<RectTransform>().rect.width, canvas.GetComponent<RectTransform>().rect.height)/2-transform.GetComponent<RectTransform>().rect.width;
        aspectRatio = canvas.GetComponent<RectTransform>().rect.width/canvas.GetComponent<RectTransform>().rect.height;
        
        GetComponent<Image>().color = waypointColors[(int)target.GetComponent<AstroidHandler>().resource];

        Vector2 dir = target.position - CameraController.currentFocus.position;
        float distance = dir.magnitude;
        dir.Normalize();

        transform.up = dir;

        transform.GetComponent<RectTransform>().localPosition = new Vector3(dir.x * aspectRatio, dir.y) * maxSize;
        distance = Mathf.Floor(distance * 100f) / 100f;
        transform.GetChild(0).GetComponent<Text>().text = distance + (Mathf.Floor(distance) == distance ? ".00" : ((distance + "").Split('.')[1].Length == 2 ? "" : "0")) + " m";
        if (distance > 999.99) transform.GetChild(0).gameObject.SetActive(false);
        else transform.GetChild(0).gameObject.SetActive(true);
    }

	void Update() {
        Vector2 dir = target.position - CameraController.currentFocus.position;
        float distance = dir.magnitude;

        if (distance <= disapearingCloseMargin) {
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetComponent<Text>().enabled = false;
            return;
        }

        GetComponent<Image>().enabled = true;
        transform.GetChild(0).GetComponent<Text>().enabled = true;

        dir.Normalize();

        transform.up = dir;

        transform.GetComponent<RectTransform>().localPosition = new Vector3(dir.x * aspectRatio, dir.y) * maxSize;
        distance = Mathf.Floor(distance * 100f) / 100f;
        transform.GetChild(0).GetComponent<Text>().text = distance + (Mathf.Floor(distance)==distance ? ".00" : ((distance+"").Split('.')[1].Length == 2 ? "" : "0")) + " m";
        if (distance > 999.99) transform.GetChild(0).gameObject.SetActive(false);
        else transform.GetChild(0).gameObject.SetActive(true);
    }
}
