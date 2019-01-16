using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SmolButtonController : MonoBehaviour
{

    [SerializeField]
    GameObject[] buttons;
    int buttonIndex;
    float firstTapTime;
    float deltaTime;

    public AudioClip aud;
    private AudioSource sourse;


    void Awake()
    {
        sourse = GetComponent<AudioSource>();
        buttonIndex = -1;
        firstTapTime = 0.1f;
        deltaTime = 0.3f;
    }

    void Update()
    {
        
        bool keyDown = Input.GetKeyDown(KeyCode.Space);
        if (keyDown)
        {
            if (buttonIndex < 1)
            {
                buttonIndex++;
            }
            else
            {
                buttonIndex = 0;
            }
            if (Time.time - firstTapTime < deltaTime)
            {
                if (buttonIndex == 0)
                {
                    buttonIndex=1;
                }
                else
                {
                    buttonIndex--;
                }
                ExecuteEvents.Execute<IPointerClickHandler>(buttons[buttonIndex], new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                Debug.Log("Double tap detected!");
            }
            else
            {

                for (int i = 0; i < buttons.Length; i++)
                {
                    ExecuteEvents.Execute<IDeselectHandler>(buttons[i], new PointerEventData(EventSystem.current), ExecuteEvents.deselectHandler);
                }
                ExecuteEvents.Execute<ISelectHandler>(buttons[buttonIndex], new PointerEventData(EventSystem.current), ExecuteEvents.selectHandler);
            }
            firstTapTime = Time.time;
        }
    }


    public void OnClickMenu()
    {
       sourse.PlayOneShot(aud);
        SceneManager.LoadScene("menu");
    
    }

    public void OnClickRetry()
    {
        sourse.PlayOneShot(aud);
        SceneManager.LoadScene("lvl_1");
    }

}
