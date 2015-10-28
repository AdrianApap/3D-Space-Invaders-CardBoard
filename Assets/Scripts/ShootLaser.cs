using UnityEngine;
using System.Collections;

public class ShootLaser : MonoBehaviour {
    public Rigidbody projectile;
    public float speed = 30;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("space") || Cardboard.SDK.Triggered)
        {
            print("Shooting");
            this.shoot();
        }
    }

    void shoot()
    {
        Rigidbody myBullet = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
        myBullet.velocity = transform.TransformDirection(new Vector3(0,0, speed));
    }
}
