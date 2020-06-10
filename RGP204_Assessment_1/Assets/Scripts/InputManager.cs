using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Amount of times pressed
    public int input;

    public delegate void InputPressed(int inputAmt);
    public static event InputPressed inputEvent;

    void Update()
    {
        if(Input.anyKeyDown)
        {
            inputted(0);
        }
    }
    void inputted(int i)
    {
        inputEvent?.Invoke(i);
        Debug.Log(inputEvent);
    }
}
