using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text text;
    private float targetTime;
    private float time;

    public float TargetTime { get => targetTime; set => targetTime = value; }
    public float Time { get => time; set => time = value; }

    private bool canCount = true;
    private bool doOnce = false;


    public Timer(float targetTime)
    {
        this.targetTime = targetTime;
    }

    void Start()
    {
        Time = targetTime;
    }

    void Update()
    {
        if (Time >= 0.0f && canCount)
        {
            Time -= UnityEngine.Time.deltaTime;
            text.text = Time.ToString("F");
        }

        else if (Time <= 0.0f && !doOnce)
        {

            Stop();
        }
    }

    public void Reset(float newTime)
    {
        Time = newTime;
        canCount = true;
        doOnce = false;
    }

    public void Stop()
    {
        canCount = false;
        doOnce = true;
        text.text = "";
        Time = 0.0f;

    }

}
