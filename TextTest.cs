using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTest : MonoBehaviour
{
    public TMP_Text testPublic;
    private TMP_Text testPrivate;
    // Start is called before the first frame update
    void Start()
    {
        testPrivate = GameObject.Find("private Test").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //testPrivate.SetText("private works");
        //testPublic.SetText("public works");

    }
}
