using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    public float _dmg = 1f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.CompareTag("Player")) {
            Player player = collision.transform.GetComponent<Player>();
            player.Hit(_dmg);

            Destroy(this.gameObject);
        }
    }

}
