using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class www : MonoBehaviour {

    public Button start;
    public Button tutorial;
    public Button info;
    public Button exit;

    public Text infotext;
    public Button back;

    public AudioClip aud;
    private AudioSource sourse;

    void Awake() {
        sourse = GetComponent<AudioSource>();
    }

	public void OnClickStart() {

        sourse.PlayOneShot(aud);
        Application.LoadLevel(1); 

    }

    public void OnClickExit()
    {
        sourse.PlayOneShot(aud);

        Application.Quit();
    }

    public void OnClickInfo()
    {
        sourse.PlayOneShot(aud);

        start.gameObject.SetActive(false);
        tutorial.gameObject.SetActive(false);
        info.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);

        back.gameObject.SetActive(true);
        infotext.enabled = true;

    
    }


    public void OnClickBack()
    {
        sourse.PlayOneShot(aud);

        start.gameObject.SetActive(true);
        tutorial.gameObject.SetActive(true);
        info.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);

        back.gameObject.SetActive(false);
        infotext.enabled = false;


    }


    public void OnClickTutorial()
    {
        sourse.PlayOneShot(aud);

    }


}
