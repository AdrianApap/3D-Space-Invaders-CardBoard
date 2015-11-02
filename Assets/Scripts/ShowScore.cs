using UnityEngine;
using System.Collections;

public class ShowScore : MonoBehaviour
{
    public Rect leftCrosshairRect;
    public Rect rightCrosshairRect;
    public string scoreLabel = "Score: ";
    private int score = 0;

    // Use this for initialization
    void Start()
    {
        
    }

    void OnGUI()
    {
        string tmp = scoreLabel + score;
        //Left label
        GUI.Label(new Rect(10,10, 200,30), tmp);
        //Right label
        GUI.Label(new Rect((Screen.width/2) + 10, 10, 200, 30), tmp);
    }

    void increaseScore()
    {
        score += 10;
    }
}
