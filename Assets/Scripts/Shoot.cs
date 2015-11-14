using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
    public Rigidbody dead_invader_block;
    public GameObject LaserBeam;
    public float damage = 100;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    private Ray ray;
    private RaycastHit hit;

    public AudioClip shootSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;


    void Awake() {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey("space") || Cardboard.SDK.Triggered) {
            if (Time.time > nextFire) {
                nextFire = Time.time + fireRate;
                print("Shooting");
                shoot();
            }
        }
    }

    void shoot() {
        //Create particle laser and destroy after 5 seconds
        GameObject laserBeamI = Instantiate(LaserBeam, transform.position, transform.rotation) as GameObject;
        Destroy(laserBeamI, 5);

        //Find the center of the screen
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        //(DEBUG)Draw a line from camera to where ray hits
        Debug.DrawLine(transform.position, hit.point, Color.red);

        //Draw a ray from camera to screen center
        ray = Camera.main.ScreenPointToRay(screenCenter);

        //Volume to play sound
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(shootSound, vol);

        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane)) {
            //If the object that the ray hits is an invader destroy the invader
            if (hit.collider.CompareTag("SpaceInvader")) {
                print("Hit Invader");

                //Spawn some Blocks where the ray hits to simulate explosion
                for (int i = 0; i < 10; i++) {
                    Rigidbody deadBlocks = Instantiate(dead_invader_block, hit.point, Quaternion.LookRotation(hit.normal)) as Rigidbody;
                    Destroy(deadBlocks.gameObject, 3);
                }

                //send message to children of gun object which is child of main camera
                hit.transform.SendMessage("hitInvader", damage, SendMessageOptions.DontRequireReceiver);
                //Broadcast message to all children of camera
                Camera.main.BroadcastMessage("hitInvaderRemove", hit.collider.name, SendMessageOptions.DontRequireReceiver);
                Camera.main.BroadcastMessage("increaseScore", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
