using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathGame : MonoBehaviour
{
    public Button confirm;
    public InputField answer;

    void Start()
    {
        confirm.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if(answer.text == "2")
        {
            PlayerRaycast.mathGameOn = false;
        }
    }
}
