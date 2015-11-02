using UnityEngine;
using System.Collections;

public class SpawnInvaders : MonoBehaviour {
    public static int invaderCount = 20;
    public GameObject invader;
    private GameObject[] invaders;
    public Transform target;

    // Use this for initialization
    void Start () {
        invaders = new GameObject[invaderCount];
        this.spawnInvaders();
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < invaderCount; i++)
        {
            if (invaders[i] != null)
            {
                Vector3 pos = invaders[i].transform.position;
                invaders[i].transform.LookAt(target);
                invaders[i].transform.Translate(Vector3.right * Time.deltaTime, Camera.main.transform);
            }
        }
	}

    void spawnInvaders()
    {
        for (int i = 0; i < invaderCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
            invaders[i] = Instantiate(invader, pos, transform.rotation) as GameObject;
            invaders[i].name = i.ToString();
        }
    }

    void hitInvaderRemove(string name)
    {
        print("spawned new invader after killing invader " + name);
        int index = 0;
        System.Int32.TryParse(name, out index);
        print(index);
        //invaders[index] = null;
        Vector3 pos = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
        invaders[index] = Instantiate(invader, pos, transform.rotation) as GameObject;
        invaders[index].name = index.ToString();
    }
}
