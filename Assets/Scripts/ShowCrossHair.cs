using UnityEngine;
using System.Collections;

public class ShowCrossHair : MonoBehaviour {

    public Texture crossHairImage;
    private Rect leftCrosshairRect;
    private Rect rightCrosshairRect;

    // Use this for initialization
    void Start() {
        float crosshairSize = Screen.width * 0.1f;
        leftCrosshairRect = new Rect((Screen.width / 4) - (crosshairSize / 2),
            (Screen.height / 2) - (crosshairSize / 2), crosshairSize, crosshairSize);
        rightCrosshairRect = new Rect((Screen.width / 2) + (Screen.width / 4) - (crosshairSize / 2),
             (Screen.height / 2) - (crosshairSize / 2), crosshairSize, crosshairSize);
    }

    void OnGUI() {
        GUI.Label(leftCrosshairRect, crossHairImage);
        GUI.Label(rightCrosshairRect, crossHairImage);
    }
}
