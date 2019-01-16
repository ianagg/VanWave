using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;

public class ttt : MonoBehaviour
{

    public float countdownValue;
    public Text countdownGUI;
    public Text tipGUI;
    public float timeToPress;
    public float koeff;
    float currCountdownValue;
    float startTime;
    float currentTime;
    bool goDisplayed;
    bool roundEnded;
    int tapCount;
    int targetTapCount;

    void OnGUI()
    {
        GUI.Box(new Rect(new Vector2(60, 60), new Vector2(50, 600)), " ");
        if (goDisplayed)
        {
            GUI.Box(new Rect(new Vector2(60, 60), new Vector2(50, Mathf.Min(600 * ((currentTime - startTime) / timeToPress), 600))), " ");
        }
        else
        {
            GUI.Box(new Rect(new Vector2(60, 60), new Vector2(50, 600)), " ");
        }
    }

    // Use this for initialization
    void Start()
    {
        StartNewRound(1);
    }

    // Update is called once per frame
    void Update()
    {
        RoundFlowControl();
    }

    private void RoundFlowControl()
    {
        bool spaceDown = Input.GetKeyDown(KeyCode.Space);
        currentTime = Time.time;
        //Debug.Log("currentTime = " + startTime);

        if (spaceDown)
        {
            tapCount++;
            //Debug.Log("tapCount = " + tapCount);
            if (goDisplayed)
            {
                if (currentTime - startTime < timeToPress)
                {
                    if (tapCount == targetTapCount)
                    {
                        countdownGUI.text = "You Win!";
                        roundEnded = true;
                        goDisplayed = false;
                        if (timeToPress < 0.6) { 
                            this.targetTapCount++;
                            this.timeToPress=2.0f;
                        }
                        StartNewRound(targetTapCount);
                    }
                }
                else
                {
                    countdownGUI.text = "Too late!";
                    roundEnded = true;
                }
            }
            else
            {
                countdownGUI.text = "Too early!";
                roundEnded = true;
            }
        }
        if (currentTime - startTime > timeToPress && goDisplayed)
        {
            if (tapCount == 0)
            {
                countdownGUI.text = "Press Space to Win!";
            }
            else
            {
                countdownGUI.text = "Press more to Win!";
            }
            roundEnded = true;
        }
    }

    public IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(2.0f);
        while (currCountdownValue > 0 && !roundEnded)
        {
            countdownGUI.text = currCountdownValue.ToString();
            //Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        countdownGUI.text = "";
        StartCoroutine(StartRoundTime());
    }

    public IEnumerator StartRoundTime()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        if (!roundEnded)
        {
            countdownGUI.text = "Go!";
            goDisplayed = true;
        }
        startTime = Time.time;
        //Debug.Log("startTime = " + startTime);
    }

    public void StartNewRound(int targetTapCount)
    {
        goDisplayed = false;
        roundEnded = false;
        currCountdownValue = countdownValue;
        StartCoroutine(StartCountdown());
        timeToPress = timeToPress * koeff;
        this.tapCount = 0;
        this.targetTapCount = targetTapCount;
        if (targetTapCount == 1)
        {
            tipGUI.text = "Tap \"Space\" " + this.targetTapCount + " time!";
        }
        else
        {
            tipGUI.text = "Tap \"Space\" " + this.targetTapCount + " times!";
        }
    }
}
