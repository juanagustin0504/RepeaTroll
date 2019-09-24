using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour {

    public static PortalManager instance;

    public GameObject portalPrefab;
    public Transform[] spawnPoints;

    public GameObject portalObj;

    public bool _isEnemyAlive;

    private PortalManager() {

    }

    public static PortalManager Instance {
        get { return instance; }
        set {
            if (instance == null)
                instance = value;
        }
    }

	// Use this for initialization
	void Start () {
        Instance = this;

        PortalSpawn();

        StartCoroutine("EnemyAlive");

        
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    void PortalSpawn() {
        portalObj = Instantiate(portalPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);

        portalObj.transform.position = new Vector3(portalObj.transform.position.x, portalObj.transform.position.y, 0);

        PortalScript portalSc = portalObj.GetComponent<PortalScript>();

        portalSc._attackBtn = GameObject.FindGameObjectWithTag("AttackButton");

        portalObj.SetActive(false);
        
    }

    IEnumerator EnemyAlive() {
        while (true) {

            if (!_isEnemyAlive) {
                portalObj.SetActive(true);
            } else {
                portalObj.SetActive(false);
            }

            yield return null;
        }
    }
}
