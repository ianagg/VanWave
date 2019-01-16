using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    [SerializeField]
    GameObject[] buttons;

    public Text infotext;

    public AudioClip aud;
    private AudioSource sourse;
    int buttonIndex;
    float firstTapTime;
    float deltaTime;

    void Awake()
    {
        sourse = GetComponent<AudioSource>();
        buttonIndex = -1;
        firstTapTime = 0.1f;
        deltaTime = 0.3f;
    }

    public void OnClickStart()
    {
        sourse.PlayOneShot(aud);

        SceneManager.LoadScene("story1");
    }

    public void OnClickExit()
    {
        sourse.PlayOneShot(aud);
        Application.Quit();
    }

    public void OnClickInfo()
    {
        sourse.PlayOneShot(aud);

        buttons[0].gameObject.SetActive(false);
        buttons[1].gameObject.SetActive(false);
        buttons[2].gameObject.SetActive(false);

        buttons[3].gameObject.SetActive(true);
        infotext.enabled = true;


    }


    public void OnClickBack()
    {
        sourse.PlayOneShot(aud);

        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(true);
        buttons[2].gameObject.SetActive(true);

        buttons[3].gameObject.SetActive(false);
        infotext.enabled = false;


    }


    public void OnClickTutorial()
    {
        sourse.PlayOneShot(aud);

    }

    public void Update()
    {
        bool keyDown = Input.GetKeyDown(KeyCode.Space);
        if (keyDown)
        {
            if (Time.time - firstTapTime < deltaTime)
            {
                if(buttonIndex!=3){
                    buttonIndex--;
                }
                ExecuteEvents.Execute<IPointerClickHandler>(buttons[buttonIndex], new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                Debug.Log("Double tap detected!");
            }   
            else
            {
                if (!buttons[3].activeSelf)
                {
                    if (buttonIndex < 2)
                    {
                        buttonIndex++;
                    }
                    else
                    {
                        buttonIndex = 0;
                    }
                }
                else
                {
                    buttonIndex = 3;
                }

                for (int i = 0; i < buttons.Length; i++)
                {
                    ExecuteEvents.Execute<IDeselectHandler>(buttons[i], new PointerEventData(EventSystem.current), ExecuteEvents.deselectHandler);
                }
                ExecuteEvents.Execute<ISelectHandler>(buttons[buttonIndex], new PointerEventData(EventSystem.current), ExecuteEvents.selectHandler);
            }
            firstTapTime=Time.time;
        }
    }

}
