using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour {

    private static PortalScript instance; 

    private PortalScript() { }

    public static PortalScript Instance {
        get {
            return instance;
        }

        set {
            if (instance == null)
                instance = value;
        }
    }

    public GameObject _attackBtn;
    public bool checkTriggerEnter = false;

    private void Start() {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.CompareTag("Player")) {
            checkTriggerEnter = true;
            _attackBtn.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            checkTriggerEnter = false;
            _attackBtn.SetActive(true);
        }
    }


}
