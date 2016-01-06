using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    public string scoreLabel = "Score: ";
    public string highScoreLabel = "High Score: ";
    private int scoreFontScale;
    private int score = 0;
    private int highScore = 0;
    private int scoreY;
    private int scoreHeight = 30;
    private int scoreWidth;
    private GUIStyle guiStyle;

    void Start() {
        scoreFontScale = Screen.height / 16;
        scoreY = ((Screen.height / 8) + (scoreFontScale / 2));

        guiStyle = new GUIStyle();
        guiStyle.fontSize = (Screen.width / Screen.height) * scoreFontScale;
        guiStyle.normal.textColor = Color.white;
    }

    void OnGUI() {
        useGUILayout = false;
        // string tmp = scoreLabel + score + " " + highScoreLabel + highScore;
        string tmp = scoreLabel + score;
        scoreWidth = (tmp.Length * scoreFontScale) / 2;
        //Left label
        GUI.Label(new Rect((Screen.width / 2) - (scoreWidth + (Screen.width / 32)), scoreY, scoreWidth, scoreHeight), tmp, guiStyle);
        //Right label
        GUI.Label(new Rect(Screen.width - (scoreWidth + (Screen.width / 32)), scoreY, scoreWidth, scoreHeight), tmp, guiStyle);
    }

    void increaseScore() {
        score += 10;
    }

    public void zeroLives() {
        if (score > highScore) {
            highScore = score;
        }
        score = 0;
    }
}
