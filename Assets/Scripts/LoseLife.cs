using UnityEngine;
using System.Collections;

public class LoseLife : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        print("trigger lose life");
        if (other.gameObject.name.ToString().Contains("Head")) {
            GameObject gui = GameObject.Find("GUI");
            healthBar bar = gui.GetComponent<healthBar>();
            bar.lives -= 1;
            print("Lost Life");
            Destroy(gameObject);
        }
    }

}
