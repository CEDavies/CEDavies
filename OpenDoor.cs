using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Vector3 startingPos;
    public Vector3 openPos;
    private StartUp screenScript;
    private AudioSource sound;

    void Start()
    {
        screenScript = GameObject.Find("screen").GetComponent<StartUp>();
        sound = GameObject.Find("doorSlide").GetComponent<AudioSource>();
    }

    public void openDoor(bool doorOpen)
    {
        StartCoroutine(move(doorOpen));
    }

    IEnumerator move(bool doorOpen)
    {
        if (doorOpen)
        { //close door
            screenScript.Pressed("doorClosed");
            sound.Play();
            while (this.transform.localPosition.x >= startingPos.x)
            {
                yield return new WaitForSeconds(0.001f);
                this.transform.localPosition -= new Vector3(0.01f, 0, 0);
            }
        }
        else 
        { //open door
            screenScript.Pressed("doorOpen");
            sound.Play();
            while (this.transform.localPosition.x <= openPos.x )
            {
                yield return new WaitForSeconds(0.001f);
                this.transform.localPosition += new Vector3(0.01f, 0, 0);
            }
        }
    }
}
