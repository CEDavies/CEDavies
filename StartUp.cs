using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartUp : MonoBehaviour
{
    
    private bool isPowerOn;
    public Material screenMat;
    public Material wrong;
    public Texture offTxt;
    public Texture warningTxt;
    public Texture startUp1;
    public Texture startUp2;
    public Texture startUp3;
    public Texture startUp4;
    private TMP_Text outputText;
    private ButtonHistory buttonHistory;
    private GameObject buttons;
    public GameObject light;
    public Material lightOff;
    public Material lightGreen;
    private List<string> warmUpSeq = new List<string>();
    private TMP_Text Instructions;
    private AudioSource incorrect;
    private AudioSource correct;
    private TMP_Text test;

    /*
     * Initialize the correct sequence of button presses in the warmUpSeq list
     * This will be used later in the compHisSeq method to compare the current history
     * of button inputs with the required sequence of inputs for the Warm Up sequence
     */
    void Start()
    {
        
        isPowerOn = false;
        screenMat.mainTexture = offTxt;
        warmUpSeq.Add("doorOpen");
        warmUpSeq.Add("doorClosed");
        warmUpSeq.Add("RESET");
        warmUpSeq.Add("ZERO RET");
        warmUpSeq.Add("ALL");
        warmUpSeq.Add("LIST PROG");
        warmUpSeq.Add("SELECT PROG");
        warmUpSeq.Add("MEM");
        warmUpSeq.Add("-10 SPINDLE");
        warmUpSeq.Add("CYCLE START");
        buttons = GameObject.Find("Buttons");
        buttonHistory = buttons.GetComponent<ButtonHistory>();
        Instructions = GameObject.Find("Instructions").GetComponent<TMP_Text>();
        Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 1: Press POWER ON");
        correct = GameObject.Find("successDing").GetComponent<AudioSource>();
        incorrect = GameObject.Find("wrong").GetComponent<AudioSource>();
        outputText = GameObject.Find("output").GetComponent<TMP_Text>();
    }

    

    /*
     * Takes button name, if it is POWER ON then switch the isPowerOn bool to true and begin 
     * accepting future button names to add to the buttonHistory and update screen to starting screen
     * If is POWER OFF then switch screen material to black and change isPowerOn to false, clear buttonHistory
     * If isPowerOn then pass name to warmUp method
     * POWER ON and POWER OFF will not be included in the buttonHistory
     */
    public void Pressed(string buttonName)
    {
        outputText.SetText("Power: " + isPowerOn);
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = screenMat;
        

        if (!isPowerOn) //if power is off and on has been pressed, switch to on display
        {
            if(buttonName == "POWER ON")
            {
                isPowerOn = true;
                outputText.SetText("Power: " + isPowerOn);
                Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 2: Open Door");
                screenMat.mainTexture = warningTxt;
            }
            
        } else 
               /*
               * if power is on, add the buttonName to history,
               * if the Estop or power off has been pressed then clear the history,
               * reset instructions and go to the power off display
               */
        {
            buttonHistory.historyAdd(buttonName);
            if (buttonName == "POWER OFF" || buttonName == "Estop")
            {
                isPowerOn = false;
                buttonHistory.clearHistory();
                screenMat.mainTexture = offTxt;
                light.GetComponent<MeshRenderer>().material = lightOff;
                Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 1: Press POWER ON");
                Pressed("POWER OFF");
            } else
            {
                WarmUp(buttonName);
            }
        }
    }

    /*
     * Compares the current buttonHistory with the required sequence of inputs for the Warm Up sequence
     * (Only compares up to the current count of buttonHistory)
     */

    bool compHisSeq()
    {
        List<string> bh = buttonHistory.getHistory();
        int i;
        if(bh.Count <= 0)
        {
            return true;
        }
        for (i = 0; i < bh.Count; i++)
        {
            if(!(bh[i].Equals(warmUpSeq[i])))
            {
                return false;
                
            }
        }
        return true;
    }


    /*
     * First compares warmUpSeq to buttonHistory each time to ensure no mistakes go unchecked
     * Then checks if the buttonHistory has all the required inputs and their prerequisites
     * Changes the screen at certains steps and displays an error or success message
     */
    void WarmUp(string buttonName)
    {
        bool check = compHisSeq();
        if (check == false) //displays blue screen and plays buzzer noise when incorrect press is detected
        {
            Instructions.SetText("Error! : Invalid Duplicates OR Invalid Entries for WarmUp Sequence, press POWER OFF to restart");
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = wrong;
            incorrect.Play();
        }
        if(buttonHistory.search("doorOpen"))
        {
            if(buttonHistory.search("doorClosed"))
            {
                if (buttonHistory.search("RESET"))
                {
                    if (buttonHistory.search("ZERO RET"))
                    {
                        if (buttonHistory.search("ALL"))
                        {
                            if (buttonHistory.search("LIST PROG"))
                            {
                                if (buttonHistory.search("SELECT PROG"))
                                {
                                    if (buttonHistory.search("MEM"))
                                    {
                                        if (buttonHistory.search("-10 SPINDLE"))
                                        {
                                            if (buttonHistory.search("CYCLE START"))
                                            {

                                                if (compHisSeq()) //if the warmUpSeq and button history are the same 
                                                {
                                                    Instructions.SetText("Warm Up Complete!");
                                                    light.GetComponent<MeshRenderer>().material = lightGreen;
                                                    correct.Play();
                                                }
                                                else //displays blue screen and plays buzzer noise when incorrect press is detected
                                                {
                                                    Instructions.SetText("Error! : Invalid Duplicates OR Invalid Entries for WarmUp Sequence, press POWER OFF to restart");
                                                    MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
                                                    meshRenderer.material = wrong;
                                                    incorrect.Play();
                                                }

                                            }
                                            else //if CYCLE START hasn't been pressed
                                            {
                                                Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 11: Press CYCLE START");
                                                screenMat.mainTexture = startUp4;
                                            }
                                        }
                                        else // if -10 SPINDLE has not been pressed
                                        {
                                            Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 10: Press -10 SPINDLE");
                                            screenMat.mainTexture = startUp3;
                                        }
                                    }
                                    else //if MEM has not been pressed
                                    {
                                        Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 9: Press MEM");
                                    }
                                }
                                else //if SELECT PROG hasn't been pressed
                                {
                                    Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 8: Press SELECT PROG");
                                    screenMat.mainTexture = startUp2;
                                }
                            }
                            else //if LIST PROG has not been pressed
                            {
                                Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 7: Press LIST PROG");
                            }
                        }
                        else //if ALL hasn't been pressed
                        {
                            Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 6: Press ALL");
                            screenMat.mainTexture = startUp1;
                        }
                    }
                    else //if ZERO RET has not been pressed
                    {
                        Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 5: Press ZERO RET");
                    }
                }
                else //if RESET has not been pressed
                {
                    Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 4: Press RESET");
                }
            } else //if doorClosed has not been pressed
            {
                Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 3: Close Door");
            }
        } else //if doorOpen has not been pressed
        {
            Instructions.SetText("Warm Up Instructions:" + "\n" + "Step 2: Open Door");
        }
        
    }
    
}
