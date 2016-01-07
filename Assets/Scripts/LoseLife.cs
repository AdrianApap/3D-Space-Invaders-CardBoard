using UnityEngine;
using System.Collections;

public class LoseLife : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        print(other.gameObject.name.ToString());
        if (other.gameObject.name.ToString().Contains("Head")) {
            GameObject gui = GameObject.Find("GUI");
            HealthBar bar = gui.GetComponent<HealthBar>();
            bar.lives -= 1;
            Handheld.Vibrate();
            Destroy(gameObject);
        }

    }
}
