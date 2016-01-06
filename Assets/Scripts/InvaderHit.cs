using UnityEngine;
using System.Collections;

public class InvaderHit : MonoBehaviour {

    public float health;
    public GameObject Explosion_Particle;

    // Update is called once per frame
    public void Update() {
        if (health <= 0) {
            //print("Killed");
            destroyed();
        }
    }

    public void hitInvader(float damage) {
        //print("In Invader Hit Script Removing " + damage+ " HP");
        health -= damage;
    }

    public void destroyed() {
        Destroy(gameObject);
        GameObject explosionSpawn = Instantiate(Explosion_Particle, this.transform.position, this.transform.rotation) as GameObject;
        Destroy(explosionSpawn, 3);
    }
}
