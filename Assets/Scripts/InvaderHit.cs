using UnityEngine;
using System.Collections;

public class InvaderHit : MonoBehaviour {

    public float health = 100;

    // Update is called once per frame
    void Update() {
        if (health <= 0) {
            print("Killed");
            destroyed();
        }
    }

    void hitInvader(float damage) {
        //print("In Invader Hit Script Removing " + damage+ " HP");
        health -= damage;
    }

    void destroyed() {
        Destroy(gameObject);
    }
}
