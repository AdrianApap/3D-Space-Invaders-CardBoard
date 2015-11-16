using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    public Rect leftCrosshairRect;
    public Rect rightCrosshairRect;
    public string scoreLabel = "Score: ";
    public string highScoreLabel = "High Score: ";
    public Font scoreFont;
    private int score = 0;
    private int highScore = 0;
    private int scoreX = 100;
    private int scoreY = 100;
    private int scoreHeight = 30;
    private int scoreWidth = 200;
    private GUIStyle guiStyle = new GUIStyle();
    

    void OnGUI() {
        //guiStyle.font = scoreFont;
        guiStyle.fontSize = 80;
        guiStyle.normal.textColor = Color.white;
        string tmp = scoreLabel + score + " " + highScoreLabel + highScore;
        //Left label
        GUI.Label(new Rect(scoreX, scoreY, scoreWidth, scoreHeight), tmp, guiStyle);
        //Right label
        GUI.Label(new Rect((Screen.width / 2) + scoreX, scoreY, 200, 30), tmp, guiStyle);
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

    public int getX() {
        return scoreX;
    }

    public int getY() {
        return scoreY;
    }

    public int getScoreHeight() {
        return scoreHeight;
    }

    public int getScoreWidth() {
        return scoreWidth;
    }
}
