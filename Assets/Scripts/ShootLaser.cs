using UnityEngine;
using System.Collections;

public class ShootLaser : MonoBehaviour {
    public Rigidbody projectile;
    public float speed = 20;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("space"))
        {
            print("Shooting");
            this.shoot();
        }
    }

    void shoot()
    {
        Rigidbody myBullet = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
        myBullet.velocity = transform.TransformDirection(new Vector3(Camera.main.transform.eulerAngles.y, 360-Camera.main.transform.eulerAngles.x, speed));
    }
}
