using UnityEngine;
using System.Collections;

public class LoseLife : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        print("In lose life script trigger");
        print(other.gameObject.name.ToString());
        if (other.gameObject.name.ToString().Contains("Head")) {
            print("Contains head");
            GameObject gui = GameObject.Find("GUI");
            HealthBar bar = gui.GetComponent<HealthBar>();
            bar.lives -= 1;
            Handheld.Vibrate();
            print("Lost Life");
            Destroy(gameObject);
        }

    }
}
