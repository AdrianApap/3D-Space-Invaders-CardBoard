using UnityEngine;
using System.Collections;

public class healthBar : MonoBehaviour {
    public Texture tex;
    public int lives = 3;

    private float texWidth;
    private float texHeight;

    void Start() {
        texWidth = 10;
        texHeight = 30;
    }

    void OnGUI() {
        if (lives > 0) {
            for (int i = 0; i < lives; i++) {
                Rect posRect = new Rect(20 + (i * 30), 20, texWidth * lives, texHeight);
                GUI.DrawTexture(posRect, tex);
            }
        } else {
            GameObject gui = GameObject.Find("GUI");
            Score score = gui.GetComponent<Score>();
            lives = 3;
            score.zeroLives();
        }
    }
}