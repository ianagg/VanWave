using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TextSlider : MonoBehaviour {
    public GameObject button;
    float firstTapTime;
    float deltaTime;

    public AudioClip aud;
    private AudioSource sourse;


    void Awake()
    {
        sourse = GetComponent<AudioSource>();

    }


	// Use this for initialization
	void Start () {
        firstTapTime = 0.1f;
        deltaTime = 0.3f;
	}


    public void OnClikNext() {
        sourse.PlayOneShot(aud);
        SceneManager.LoadScene("lvl_1");
    }

	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+0.8f);
        if (this.transform.position.y > Screen.height*1.5f)
        {
            Destroy(this.gameObject);
        }
        bool keyDown = Input.GetKeyDown(KeyCode.Space);
        if (keyDown)
        {
            if (Time.time - firstTapTime < deltaTime)
            {
                ExecuteEvents.Execute<IPointerClickHandler>(button, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                Debug.Log("Double tap detected!");
            }
            else
            {
                ExecuteEvents.Execute<ISelectHandler>(button, new PointerEventData(EventSystem.current), ExecuteEvents.selectHandler);
            }
            firstTapTime = Time.time;
        }
	}
}
