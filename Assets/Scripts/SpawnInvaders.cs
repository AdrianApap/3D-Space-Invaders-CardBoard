using UnityEngine;
using System.Collections;

public class SpawnInvaders : MonoBehaviour {
    public static int invaderCount = 20;
    public GameObject invader;

    // Use this for initialization
    void Start () {
        this.spawnInvaders();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void spawnInvaders()
    {
        for (int i = 0; i < invaderCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
            GameObject myInvader = Instantiate(invader, pos, transform.rotation) as GameObject;
        }
    }
}
