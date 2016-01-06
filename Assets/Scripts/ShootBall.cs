using UnityEngine;
using System.Collections;

public class ShootBall : MonoBehaviour {
    public GameObject projectile;
    public float speed = 30;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("space") || Cardboard.SDK.Triggered)
        {
            //print("Shooting");
            this.shoot();
        }
    }

    void shoot()
    {
        Rigidbody myBullet = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
        myBullet.velocity = transform.TransformDirection(new Vector3(0,0, speed));
        Destroy(myBullet.gameObject, 3);
    }
}
