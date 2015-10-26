using UnityEngine;
using System.Collections;

public class CardboardTriggerControlMono : MonoBehaviour
{
    public bool magnetDetectionEnabled = true;
    public GameObject Cube;

    void Start()
    {
        CardboardMagnetSensor.SetEnabled(magnetDetectionEnabled);
        // Disable screen dimming:
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update() {
        if (!magnetDetectionEnabled) return;
        if (CardboardMagnetSensor.CheckIfWasClicked())
        {
            Debug.Log("DO SOMETHING HERE");  // PERFORM ACTION.
            Instantiate(Cube, transform.position, Quaternion.identity);
            CardboardMagnetSensor.ResetClick();
        }
    }
}