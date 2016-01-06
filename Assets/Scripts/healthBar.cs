using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
    public Texture tex;
    public int lives = 3;

    private float texWidth;
    private float texHeight;

    GameObject myInavderSpawner;
    GameManager myManager;
    GameObject gui;
    Score score;


    void Start() {
        texWidth = Screen.height / 8;
        texHeight = Screen.height / 8;

        myInavderSpawner = GameObject.Find("InvaderSpawn");
        myManager = myInavderSpawner.GetComponent<GameManager>();
        gui = GameObject.Find("GUI");
        score = gui.GetComponent<Score>();
    }

    void OnGUI() {
        useGUILayout = false;
        if (lives > 0) {
            for (int i = 0; i < lives; i++) {
                Rect rectLeft = new Rect(((i * texWidth) + (Screen.width / 32)), texHeight, texWidth, texHeight);
                Rect rectRight = new Rect(((Screen.width / 2) + (i * texWidth) + (Screen.width / 32)), texHeight, texWidth, texHeight);

                GUI.DrawTexture(rectLeft, tex);
                GUI.DrawTexture(rectRight, tex);
            }
        } else {
            lives = 3;
            score.zeroLives();
            myManager.setGameOver();
        }
    }

}