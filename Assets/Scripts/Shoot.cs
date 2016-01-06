using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
    public GameObject Explosion_Particle;
    public GameObject LaserBeam;
    public float damage = 100;
    public float range = 500.0f;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    private Ray ray;
    private RaycastHit[] hits;

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
                //print("Shooting");
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
       // Debug.DrawLine(transform.position, hit.point, Color.red);

        //Draw a ray from camera to screen center
        ray = Camera.main.ScreenPointToRay(screenCenter);

        //Volume to play sound
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(shootSound, vol);

        hits = Physics.RaycastAll(transform.position, transform.forward, range);

        for (int i = 0; i < hits.Length; i++) {
            RaycastHit hit = hits[i];
            if(hit.collider.CompareTag("SpaceInvader")) {
                print("Hit Invader");
                Destroy(laserBeamI, 0.25f);


                //send message to children of gun object which is child of main camera
                hit.transform.SendMessage("hitInvader", damage, SendMessageOptions.DontRequireReceiver);
                //Broadcast message to all children of camera
                //Camera.main.BroadcastMessage("hitInvaderRemove", hit.collider.name, SendMessageOptions.DontRequireReceiver);
                Camera.main.BroadcastMessage("increaseScore", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
