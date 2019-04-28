using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameLoop : MonoBehaviour {
	public GameObject[] astroidsList;
    [HideInInspector]
    public List<GameObject> astroids = new List<GameObject>();
    public float astroidUpdateTime = 2f;
    public static GameObject currentPlayer;

    public GameObject player;

    public float astroidDensity = 0.02f;
    public float[] astroidPopulation;

    private float astroidUpdateTimer;

    private GameObject canvas;

	void Start () {
        currentPlayer = GameObject.Find("Player");

        canvas = GameObject.Find("Canvas");

        astroids.AddRange(GameObject.FindGameObjectsWithTag("Astroid"));

        foreach (GameObject astroid in astroids) PlayerController.seeAstroid(astroid.transform);

        float gameWidth = GetComponent<BoxCollider2D>().size.x;
        float gameHeight = GetComponent<BoxCollider2D>().size.y;

        for (float y = -gameHeight / 2 / 20; y < gameHeight / 2 / 20; y++) {
            for (float x = -gameWidth / 2 / 20; x < gameWidth / 2 / 20; x++) {
                if (transform.GetChild(0).GetComponent<CircleCollider2D>().bounds.Contains(new Vector3(y*20, x*20))) continue;

                if (Random.Range(0f, 1f) < astroidDensity) {
                    float type = Random.Range(0f, 1f);
                    for (int i = 0; i < astroidPopulation.Length; i++) {
                        if ((type -= astroidPopulation[i]) < 0) {
                            GameObject currentAstroid = Instantiate(astroidsList[i], new Vector3(x*20+Random.Range(-5f, 5f), y*20+Random.Range(-5f,5f)), Quaternion.identity);

                            float sizeScale = Random.Range(1f, 5f);
                            currentAstroid.transform.localScale *= sizeScale;
                            currentAstroid.GetComponent<AstroidHandler>().vainSize *= sizeScale += Random.Range(-1f, 2f);

                            Collider2D[] cols = new Collider2D[1];
                            currentAstroid.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D(), cols);

                            if (cols[0] != null) Destroy(currentAstroid);
                            else astroids.Add(currentAstroid);

                            break;
                        }
                    }
                }
            }
        }

        transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = false;
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if (!currentPlayer.GetComponent<PlayerController>().enabled) {
            canvas.SetActive(false);
        } else canvas.SetActive(true);

        BuildingHandler bh;
        if (bh = CameraController.currentFocus.GetComponent<BuildingHandler>()) {
            if (bh.type == BuildingTypes.rocketSilo) {
                if (Input.GetButtonDown("Fire")) {
                    currentPlayer = Instantiate(player, bh.transform.position+bh.transform.up*1.0f, bh.transform.rotation);
                    CameraController.focuses.Add(currentPlayer.transform);
                    CameraController.currentFocus = currentPlayer.transform;
                }
            }
        }

        astroidUpdateTimer += Time.deltaTime;

		if (astroidUpdateTimer < astroidUpdateTime) return;

		astroidUpdateTimer = 0;

		foreach (GameObject astroid in astroids) {
			astroid.GetComponent<AstroidHandler>().updateBuildings();
		}
	}
}
