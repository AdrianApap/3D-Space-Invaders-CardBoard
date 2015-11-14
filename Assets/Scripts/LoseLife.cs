using UnityEngine;
using System.Collections;

public class LoseLife : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        print("trigger lose life");
        if (other.gameObject.name.ToString().Contains("Head"))
        {
            GameObject gui =GameObject.Find("GUI");
            healthBar bar = gui.GetComponent<healthBar>();
            bar.lives -=1 ;
            print("Lost Life");
            Destroy(gameObject);
        }
    }

}
