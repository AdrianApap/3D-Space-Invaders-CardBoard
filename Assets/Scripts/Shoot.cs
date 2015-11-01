using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public Rigidbody dead_invader_block;
    public GameObject LaserBeam;
    public float damage = 100;
    private Ray ray;
    private RaycastHit hit;

    public AudioClip shootSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;


    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("space") || Cardboard.SDK.Triggered)
        {
            print("Shooting");
            GameObject laserBeamI = Instantiate(LaserBeam, transform.position, transform.rotation) as GameObject;
            Destroy(laserBeamI, 5);

            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

            Debug.DrawLine(transform.position, hit.point, Color.red);

            ray = Camera.main.ScreenPointToRay(screenCenter);
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(shootSound, vol);

            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
            {
                if (hit.collider.CompareTag("SpaceInvader"))
                {
                    print("Hit Invader");
                    for (int i = 0; i < 10; i++)
                    {
                        Rigidbody deadBlocks = Instantiate(dead_invader_block, hit.point, Quaternion.LookRotation(hit.normal)) as Rigidbody;
                        Destroy(deadBlocks.gameObject, 3);
                    }
                    hit.transform.SendMessage("hitInvader", damage, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

    }
}
