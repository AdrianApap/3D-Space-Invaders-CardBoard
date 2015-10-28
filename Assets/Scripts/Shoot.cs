using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public Rigidbody dead_invader_block;
    public float damage = 100;
    private Ray ray;
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") || Cardboard.SDK.Triggered)
        {
            print("Shooting");
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            ray = Camera.main.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
            {
                print("Hit");
                for (int i = 0; i < 10; i++)
                {
                   // Rigidbody deadBlocks = Instantiate(dead_invader_block, hit.point, Quaternion.LookRotation(hit.normal)) as Rigidbody;
                    Rigidbody deadBlocks = Instantiate(dead_invader_block, hit.point, Quaternion.LookRotation(hit.normal)) as Rigidbody;
                    Destroy(deadBlocks.gameObject, 3);
                }
                hit.transform.SendMessage("hitInvader", damage, SendMessageOptions.DontRequireReceiver);
            }
        }

    }
}
