using UnityEngine;
using System.Collections;

public class SpawnInvaders : MonoBehaviour {
    public static int invaderCount = 20;
    public int step = 1;
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
                invaders[i].transform.position = Vector3.MoveTowards(invaders[i].transform.position, new Vector3(0,0,0), 1 * step * Time.deltaTime);
            }
        }
	}

    void spawnInvaders()
    {
        for (int i = 0; i < invaderCount; i++)
        {
            int[] posArray = makeNew();
            Vector3 pos = new Vector3(posArray[0], posArray[1], posArray[2]);
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
        int[] posArray = makeNew();
        Vector3 pos = new Vector3(posArray[0], posArray[1], posArray[2]);
        invaders[index] = Instantiate(invader, pos, transform.rotation) as GameObject;
        invaders[index].name = index.ToString();
    }
    //to make them not spawn close to the camera
    int[] makeNew()
    {
        int x = Random.Range(-50, 50);
        int y = Random.Range(-50, 50);
        int z = Random.Range(-50, 50);

        if (x < 0)
        {
            x -= 10;
        }
        else
        {
            x += 10;
        }
        if (y < 0)
        {
            y -= 10;
        }
        else
        {
            y += 10;
        }
        if (z < 0)
        {
            z -= 10;
        }
        else
        {
            z += 10;
        }
        int[] pos = new int[3] { x, y, z };
        return pos;
    }
}
