using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Door : MonoBehaviour
{
    private float delay;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public GameObject door;
    private bool doorOpen;
    private Vector3 startingPos;
    private Vector3 openPos;
    OpenDoor openClose;
    public TMP_Text buttonText;


    private void Awake()
    {
        startingPos = transform.position;
        openPos = new Vector3(0.657999992f, 1.28805482f, 0.582849979f);
    }

    void Start()
    {
        doorOpen = true;
        openClose = GameObject.Find("door").GetComponent<OpenDoor>();

    }


    void Update()
    {
        if (delay > 0f)
        {
            delay -= Time.deltaTime;
        }
    }

    public void Release()
    {
        if (delay <= 0f)
        {
            delay = 1.5f;
            onRelease.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        {
            Release();
        }
    }

    public void Pass()
    { 
        doorOpen = !doorOpen;
        openClose.openDoor(doorOpen);
    }
}
