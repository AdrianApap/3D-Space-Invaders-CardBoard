using UnityEngine;
using System.Collections;

public class ScrollingText : MonoBehaviour {
    private GameObject introText;
    private Vector3 goalLocation;
    public int scrollSpeed = 2;

    void Start() {
        introText = GameObject.Find("IntroText");
        goalLocation = introText.transform.position;
        goalLocation.y += 1000;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKey("space") || Cardboard.SDK.Triggered) {
            Application.LoadLevel(1);
        } else {
            Vector3 pos = introText.transform.position;
            introText.transform.position = Vector3.MoveTowards(pos, goalLocation, 1 * this.scrollSpeed * Time.deltaTime);
        }
    }
}
