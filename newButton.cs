using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class newButton : MonoBehaviour
{
    private GameObject button;
    public UnityEvent onRelease;
    AudioSource sound; //add sound to button (optional)
    bool isPressed;
    Vector3 originalPosition;
    private TMP_Text buttonText;
    private StartUp screenScript;
    private float delay;
    private Vector3 startingPos;
    private TMP_Text buttonHistory;
    // Start is called before the first frame update
    void Start()
    {
        string parentName = this.transform.parent.name;
        GameObject parent = GameObject.Find(parentName);
        button = GameObject.Find(parentName + "/Press");
        sound = GameObject.Find("buttonClick").GetComponent<AudioSource>();
        //screen = GameObject.Find("screen");
        screenScript = GameObject.Find("screen").GetComponent<StartUp>();
        isPressed = false;
        startingPos = button.transform.position;
        buttonHistory = GameObject.Find("historyText").GetComponent<TMP_Text>();
        buttonText = GameObject.Find("buttonText").GetComponent<TMP_Text>();
        button.transform.localPosition = new Vector3(0f, 0.01f, 0f);
        //Push();

    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0f)
        {
            delay -= Time.deltaTime;
        } else
        {
            //button.transform.localPosition = startingPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && delay <= 0)
        {
            button.transform.localPosition -= new Vector3(0, 0.0055f, 0);
            isPressed = true;
            sound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed && delay <= 0)
        {
            button.transform.localPosition = new Vector3(0, 0.01f, 0);
            isPressed = false;
            delay = 2f;
            Pass();
        }
    }

    public void Release()
    {
        onRelease.Invoke();
    }
    public void Pass()
    {
        
        buttonText.SetText("Button Pressed: \n" + this.transform.parent.name);
        screenScript.Pressed(this.transform.parent.name);
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        sphere.transform.localPosition = new Vector3(0, 1, 2);

        sphere.AddComponent<Rigidbody>();

        
        //screenScript.Pressed(this.transform.parent.name);
        //buttonHistory.SetText(this.transform.parent.name);


        
    }
}
