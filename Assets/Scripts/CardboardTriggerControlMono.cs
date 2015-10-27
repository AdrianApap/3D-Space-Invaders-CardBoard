using UnityEngine;
using System.Collections;

public class CardboardTriggerControlMono : MonoBehaviour
{
    public bool magnetDetectionEnabled = true;

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

            print("Trigger pressed spawning cube");
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<Rigidbody>();
            cube.transform.position = new Vector3(0, 1, 0);


            CardboardMagnetSensor.ResetClick();
        }
    }
}