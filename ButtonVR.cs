using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ButtonVR : MonoBehaviour
{
    private GameObject button;
    public UnityEvent onRelease;
    private AudioSource sound;
    private bool isPressed;
    private TMP_Text buttonText;
    private StartUp screenScript;
    private float delay;
    private TMP_Text buttonHistory;
    private string name;

    private ButtonHistory bh;
    private GameObject buttons;


    // Start is called before the first frame update
    void Start()
    {

        buttons = GameObject.Find("Buttons");
        bh = buttons.GetComponent<ButtonHistory>();

        name = this.transform.parent.name;
        string parentName = this.transform.parent.name;
        GameObject parent = GameObject.Find(parentName);
        button = GameObject.Find(parentName + "/Press");
        sound = GameObject.Find("buttonClick").GetComponent<AudioSource>();
        screenScript = GameObject.Find("screen").GetComponent<StartUp>();
        isPressed = false;
        buttonHistory = GameObject.Find("historyText").GetComponent<TMP_Text>();
        buttonText = GameObject.Find("buttonText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0f)
        {
            delay -= Time.deltaTime;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (!isPressed && delay <= 0)
        {
            button.transform.localPosition -= new Vector3(0, 0.0055f, 0);

            isPressed = true;
            sound.Play();
            StartCoroutine(ButtonUp());
        }
    }

    IEnumerator ButtonUp()
    {
        yield return new WaitForSeconds(0.2f);
        if (isPressed )
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
    }
}
