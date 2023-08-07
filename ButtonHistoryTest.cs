using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonHistoryTest : MonoBehaviour
{
    public List<string> buttonHistory = new List<string>();
    public TMP_Text history;
    public int pressCount = 0;




    // Start is called before the first frame update
    void Start()
    { //initialize text to print list to
        history = GameObject.Find("historyText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    { //prints the full list
        string str = "";
        for (int i = 0; i < pressCount; i++)
        {
            str += buttonHistory[i] + "\n";
            
        }
        history.SetText(str);
    }

    public void historyAdd(string buttonName)
    { //add name to list
        if (buttonName != "POWER ON" && buttonName != "POWER OFF")
        {
            buttonHistory.Add(buttonName);
            pressCount++;
        }

    }

    public bool search(string name)
    { //search list for specific string name
        bool res = false;

        for (int i = 0; i < buttonHistory.Count; i++)
        {
            if (buttonHistory[i].Equals(name))
            {
                res = true;
                Debug.Log(res);
                return res;
            }
        }

        Debug.Log(res);
        return res;
    }

    /*
     * Return current list
     */
    public List<string> getHistory()
    {
        if (buttonHistory.Count == 0)
        {
            Debug.Log("History Empty");
            return null;
        }
        return buttonHistory;
    }

    public void clearHistory()
    {
        buttonHistory.Clear();
        pressCount = 0;
    }
}
