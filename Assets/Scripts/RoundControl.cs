using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RoundControl : MonoBehaviour
{

    public enum RoundState { PREPARING, PLAYING, GOODRESULT, BADRESULT, ENDING };

    [SerializeField] private Button menuButton, menuButton2;
    [SerializeField] private Text countdownGUI;
    [SerializeField] private AudioClip click;

    [SerializeField] private GameObject ship, castle, fire, timebar;

    [SerializeField] private Transform target;
    [SerializeField] private float timeToPress, minTimeToPress = 0.6f, coefficient;

    private AudioSource sourse;

    private bool time, win, init, goDisplayed;
    private float currentCountdownValue = 3;
    private float startTime, currentTime;
    private float speed = 2.5f;
    private Timer timer;

    private bool shipMoves, canPlay = false, doOnce = false, roundEnd = false;

    private RoundState state = RoundState.PREPARING;
    public RoundState State
    {
        get { return state; }
    }


    void Awake()
    {
        sourse = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        timer = GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            //Initializing the scene;
            //Waiting for animation to finish;
            //Start the round
            case RoundState.PREPARING:
                if (init)
                    Initialize();

                countdownGUI.text = "GET READY\nTap \"Space\" one time \n when you see \"GO\" being displayed!";

                if (ship.transform.position != target.position)
                {
                    shipMoves = true;
                    ship.transform.position = Vector2.MoveTowards(ship.transform.position, target.position, speed * Time.deltaTime);

                }
                else
                {
                    shipMoves = false;
                }


                if (!shipMoves)
                {
                    StartCoroutine(PlayRound());
                }

                break;

            //Detect the buton push 
            case RoundState.PLAYING:

                if (goDisplayed&&!doOnce)
                {
                    timer.Reset(timeToPress);
                    doOnce = true;
                }

                if(canPlay)
                    CheckSpaceDown();

                break;

              //Display results to the player
            case RoundState.GOODRESULT:

                StartCoroutine(Win());


                if (timeToPress < minTimeToPress)
                {
                    SceneManager.LoadScene("menu");

                }
                else
                {
                    if (!win)
                        state = RoundState.PREPARING;
                }

                break;

            case RoundState.BADRESULT:

                StartCoroutine(Lose());

                break;




        }



    }

//Set animations and values to default
    private void Initialize()
    {
        timeToPress = timeToPress * coefficient;
        doOnce = false;
        fire.SetActive(false);
        castle.GetComponentInChildren<Animator>().SetBool("Attack", false);
        ship.GetComponentInChildren<Animator>().SetBool("Down", false);
        castle.GetComponentInChildren<Animator>().SetBool("Down", false);
        ship.GetComponentInChildren<Animator>().SetBool("Win", false);

        menuButton.gameObject.SetActive(false);
        menuButton2.gameObject.SetActive(false);

        init = false;

    }

    IEnumerator StartCountdown()
    {

        yield return new WaitForSeconds(4.0f);

        Initialize();
//countdown for the round to start
        while (currentCountdownValue > 0)
        {
            countdownGUI.text = currentCountdownValue.ToString();
            sourse.PlayOneShot(click);
            yield return new WaitForSeconds(1.0f);
            currentCountdownValue--;
        }

        countdownGUI.text = "";
        canPlay = true;

    }

    //waits for countdown to finish,
    //then displays the word randomly
    IEnumerator PlayRound()
    {
        state = RoundState.PLAYING;
        yield return StartCoroutine(StartCountdown());

        yield return new WaitForSeconds(Random.Range(0.5f, 2f));

        if (canPlay)
          countdownGUI.text = "Go!";

        goDisplayed = true;
        startTime = Time.time;
       
    }

    //Checking if the player click on the button:
    //1 - in the rigth time; 2 - to early; 3 - to late;
    //switch game state, depending on the result
    private void CheckSpaceDown()
    {
       
        bool spaceDown = Input.GetKeyDown(KeyCode.Space);
        currentTime = Time.time;
        time = currentTime - startTime < timeToPress;

        if (spaceDown)
            timer.Stop();

        if (time & spaceDown & goDisplayed)
        {
            state = RoundState.GOODRESULT;
            win = true;
            init = true;
        }
        else
        {
            if (!goDisplayed && spaceDown)
            {
                countdownGUI.text = "To early! You lose!";
                state = RoundState.BADRESULT;

            }
            if(goDisplayed&&spaceDown||goDisplayed&&timer.Time==0.0f)
            {
                countdownGUI.text = "To late! You lose!";
                state = RoundState.BADRESULT;

            }


        }




    }

   
    IEnumerator Win()
    {
        canPlay = false;
        countdownGUI.text = "You Win!";
        castle.GetComponentInChildren<Animator>().SetBool("Attack", true);
        ship.GetComponentInChildren<Animator>().SetBool("Down", true);
        goDisplayed = false;

        yield return new WaitForSeconds(5.0f);
        ship.transform.position = new Vector3(13f, -3f, -0.03f);

        win = false;
    }

    IEnumerator Lose()
    {

        canPlay = false;
        castle.GetComponentInChildren<Animator>().SetBool("Down", true);
        ship.GetComponentInChildren<Animator>().SetBool("Win", true);
        fire.SetActive(true);
        goDisplayed = false;
        menuButton.gameObject.SetActive(true);
        menuButton2.gameObject.SetActive(true);


        yield return new WaitForSeconds(4.0f);


    }


}
