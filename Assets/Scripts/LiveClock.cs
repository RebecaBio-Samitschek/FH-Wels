using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveClock : MonoBehaviour {

 public Transform secondHand;
    public Transform minuteHand;
    public Transform hourHand;
    private string oldSeconds;

    void Update () {
        string seconds = System.DateTime.UtcNow.ToString("ss");

        if (seconds != oldSeconds) {
            UpdateTimer();
        }
        oldSeconds = seconds;
    }

    void UpdateTimer() {
        int secondsInt = int.Parse(System.DateTime.UtcNow.ToString("ss"));
        int minutesInt = int.Parse(System.DateTime.UtcNow.ToString("mm"));
        int hoursInt = int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("hh"));

        // Calculate the rotation angles
        float secondAngle = secondsInt * 6;
        float minuteAngle = minutesInt * 6;
        float hourAngle = (hoursInt % 12 + (float)minutesInt / 60f) * 30f;

        // Apply the rotations
        secondHand.localRotation = Quaternion.Euler(0, 0, -secondAngle);
        minuteHand.localRotation = Quaternion.Euler(0, 0, -minuteAngle);
        hourHand.localRotation = Quaternion.Euler(0, 0, -hourAngle);
    }
}