using UnityEngine;
using System.Collections;

public class InvaderClose : MonoBehaviour {
    public GameObject dangerImage;
    public Color dangerColor;
    public float flashSpeed;
    private bool danger = false;
    private SpriteRenderer myRenderer;

    void OnTriggerEnter(Collider other) {
        if (this.gameObject.CompareTag("DangerTrigger")) {
            if (other.gameObject.CompareTag("SpaceInvader")) {
                //print("DANGER! Invader Close");
                Handheld.Vibrate();
                danger = true;
            }
        }
    }

    void Start() {
        myRenderer = dangerImage.GetComponent<SpriteRenderer>();
    }

    void Update() {
       if(danger) {
            myRenderer.color = dangerColor;
        }else {
            myRenderer.color = Color.Lerp(myRenderer.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        danger = false;
    }
    
}
