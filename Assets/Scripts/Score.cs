using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    public Rect leftCrosshairRect;
    public Rect rightCrosshairRect;
    public string scoreLabel = "Score: ";
    public string highScoreLabel = "High Score: ";
    private int score = 0;
    private int highScore = 0;

    void OnGUI() {
        string tmp = scoreLabel + score + " " + highScoreLabel + highScore;
        //Left label
        GUI.Label(new Rect(10, 10, 200, 30), tmp);
        //Right label
        GUI.Label(new Rect((Screen.width / 2) + 10, 10, 200, 30), tmp);
    }

    void increaseScore() {
        score += 10;
    }

    public void zeroLives() {
        if(score > highScore) {
            highScore = score;
        }
        score = 0;
    }
}
