using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {
    private int step = 10;
    private bool inTrigger = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveUp = false;
    private bool moveDown = false;
    private bool moveTowards = false;
    private bool inCounter = false;
    private int frameCounter = 0;
    private const int maxFrames = 30;

    void OnTriggerEnter(Collider other) {
        if (this.gameObject.CompareTag("SpaceInvader")) {
            if (other.gameObject.CompareTag("InvaderTrigger")) {
                this.inTrigger = true;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (this.gameObject.CompareTag("SpaceInvader")) {
            if (other.gameObject.CompareTag("InvaderTrigger")) {
                this.inTrigger = false;
            }
        }
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (this.inTrigger == false) {
            Vector3 pos = transform.position;
            if (inCounter == false) {
                float randNum = Random.Range(0.0f, 125.0F);
                if (randNum >= 0 && randNum < 25) {
                    this.moveUp = true;
                    this.moveLeft = false;
                    this.moveDown = false;
                    this.moveRight = false;
                    this.moveTowards = false;
                } else if (randNum >= 25 && randNum < 50) {
                    this.moveUp = false;
                    this.moveLeft = true;
                    this.moveDown = false;
                    this.moveRight = false;
                    this.moveTowards = false;
                } else if (randNum >= 50 && randNum < 75) {
                    this.moveUp = false;
                    this.moveLeft = false;
                    this.moveDown = true;
                    this.moveRight = false;
                    this.moveTowards = false;
                } else if (randNum >= 75 && randNum < 100) {
                    this.moveUp = false;
                    this.moveLeft = false;
                    this.moveDown = false;
                    this.moveRight = true;
                    this.moveTowards = false;
                } else if (randNum >= 100 && randNum < 125) {
                    this.moveUp = false;
                    this.moveLeft = false;
                    this.moveDown = false;
                    this.moveRight = false;
                    this.moveTowards = true;
                }
                this.inCounter = true;
            } else {
                if (this.frameCounter < maxFrames) {
                    this.frameCounter++;
                } else {
                    this.inCounter = false;
                    this.frameCounter = 0;
                }
            }

            transform.LookAt(new Vector3(0, 0, 0));
            Vector3 tmpVector = pos;

            if (this.moveUp) {
                tmpVector.y += step;
                transform.position = Vector3.MoveTowards(pos, tmpVector, 1 * step * Time.deltaTime);
            } else if (this.moveRight) {
                tmpVector.x += step;
                transform.position = Vector3.MoveTowards(pos, tmpVector, 1 * step * Time.deltaTime);
            } else if (this.moveDown) {
                tmpVector.y -= step;
                transform.position = Vector3.MoveTowards(pos, tmpVector, 1 * step * Time.deltaTime);
            } else if (this.moveLeft) {
                tmpVector.y -= step;
                transform.position = Vector3.MoveTowards(pos, tmpVector, 1 * step * Time.deltaTime);
            } else if (this.moveTowards) {
               transform.position = Vector3.MoveTowards(pos, new Vector3(0, 0, 0), 1 * step * Time.deltaTime);
            }
        }
    }
}
