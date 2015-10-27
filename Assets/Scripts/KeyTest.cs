using UnityEngine;
using System.Collections;

public class KeyTest : MonoBehaviour {

    public Vector3 spawnSpot = new Vector3(0, 2, 0);

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("space"))
        {
            print("Space pressed spawning cube");
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<Rigidbody>();
            cube.transform.position = new Vector3(0, 1, 0);
        }
    }
}
