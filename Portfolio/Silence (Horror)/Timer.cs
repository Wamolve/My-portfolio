using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMesh timerText;
    private float startTime;
    private bool running;
    void Start()
    {
        startTime = Time.time;
        running = true;
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (running)
        {
            float t = Time.time - startTime;

            string hours = ((int)t / 3600).ToString("00");
            string minutes = ((int)t / 60 % 60).ToString("00");
            string seconds = (t % 60).ToString("00");

            timerText.text = "REC " + hours + ":" + minutes + ":" + seconds;

            yield return null;
        }
    }
}
