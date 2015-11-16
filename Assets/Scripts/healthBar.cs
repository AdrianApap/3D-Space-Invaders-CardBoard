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
        GameObject gui = GameObject.Find("GUI");
        Score score = gui.GetComponent<Score>();

        if (lives > 0) {
            for (int i = 0; i < lives; i++) {
                Rect rectLeft = new Rect((score.getX() + (i * 30)), 50 + (score.getY() + score.getScoreHeight() + 20), texWidth * lives, texHeight);
                Rect rectRight = new Rect(((Screen.width / 2) + (score.getX() + (i * 30))), 50 + (score.getY() + score.getScoreHeight() + 20), texWidth * lives, texHeight);

                GUI.DrawTexture(rectLeft, tex);
                GUI.DrawTexture(rectRight, tex);
            }
        } else {
            lives = 3;
            score.zeroLives();
        }
    }
}