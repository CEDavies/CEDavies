using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EStop : MonoBehaviour
{ //initialization
    public GameObject top;
    public UnityEvent onPress;
    private bool isPressed;
    private AudioSource downsound;
    private AudioSource upsound;
    private float delay;
    private StartUp screenScript;

    void Start()
    {
        //Audio sources
        downsound = GameObject.Find("estop down").GetComponent<AudioSource>();
        upsound = GameObject.Find("estop up").GetComponent<AudioSource>();
        //StartUp script
        screenScript = GameObject.Find("screen").GetComponent<StartUp>();
        isPressed = false;
        delay = 0f;
    }

    //Timer for delay
    void Update()
    {
        if (delay > 0f)
        {
            delay -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    { //animates the movement of the button twisting/popping back into the up position
        if(isPressed && delay <= 0f)
        {
            upsound.Play();
            top.transform.eulerAngles = new Vector3(-0f, 0f, 90f);
            StartCoroutine(Move2());
            StartCoroutine(Move3());
            isPressed = false;
            delay = 2f;
        }
    }
    private void OnTriggerExit(Collider other)
    { //presses button down and sends the press to StartUp script
        if (!isPressed && delay <= 0f)
        {
            top.transform.localPosition += new Vector3(0.005f, 0, 0);
            isPressed = true;
            downsound.Play();
            delay = 3f;
            screenScript.Pressed("Estop");
        }
        
    }

    
    IEnumerator Move2()
    { //animate twist
        while (top.transform.eulerAngles.x > -45)
        {
            yield return new WaitForSeconds(0.001f);
            top.transform.eulerAngles -= new Vector3(1f, 0, 0);
        }
    }
    
    IEnumerator Move3()
    { //animate pop up
        while (top.transform.localPosition.x > 0.5733)
        {
            yield return new WaitForSeconds(0.001f);
            top.transform.localPosition -= new Vector3(0.0005f, 0, 0);
        }
    } 

}
