using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonHistory : MonoBehaviour
{
    // Start is called before the first frame update
    //create an array of button press history
    private List<string> buttonHistory = new List<string>();
    private TMP_Text history;
    private int pressCount = 0;

    void Start()
    {
        history = GameObject.Find("historyText").GetComponent<TMP_Text>();
        string str = string.Join("\n", buttonHistory);
        history.SetText(str);
    }

    /*
     * Updates the printed list (visible to the user) of the hsitory of button presses so far every frame
     */
    void Update()
    {
        if(buttonHistory.Count >= 0)
        {
            string str = string.Join("\n", buttonHistory);
            history.SetText(str);
        }
    }

    /*
     * Add string to buttonHistory list
     */

    public void historyAdd(string buttonName)
    {
        
        if (buttonName != "POWER ON" && buttonName != "POWER OFF")
        {
             buttonHistory.Add(buttonName);
             pressCount++;
        }
           
    }

    

    /*
     * Search buttonHistory list for string
     */
    public bool search(string name)
    {
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

    public void setHistory(List<string> newHistory)
    {
            buttonHistory = newHistory;
    }

    public int getCount()
    {
        return pressCount;
    }

    /*
     * Clear list
     */
    public void clearHistory()
    {
        buttonHistory.Clear();
        pressCount = 0;
    }
}
