using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathGame : MonoBehaviour
{
    public Button confirm;
    public Text question;
    public Text progress;
    public InputField answer;
    private RectTransform rectTransform;
    private int result;
    private int mover;
    private int numCorrect = 0;
    private int num1, num2, num3;
    private string sign;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0, -435, 0);
        confirm.onClick.AddListener(TaskOnClick);
        StartCoroutine(MathGameWait(true));
    }

    public IEnumerator MathGameWait(bool start)
    {
        yield return new WaitUntil(() => PlayerRaycast.mathGameOn == true);
        mover = 0;
        if(start)
        {
            Generate();
            for (int i = 29; i > 0; i--)
            {
                mover += i;
                rectTransform.anchoredPosition = new Vector3(0, -435 + mover, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            for (int i = 1; i <= 29; i++)
            {
                mover -= i;
                rectTransform.anchoredPosition = new Vector3(0, mover, 0);
                yield return new WaitForSeconds(0.01f);
            }
            PlayerRaycast.mathGameOn = false;
        }
        
    }

    void TaskOnClick()
    {
        result = (num3 - num2) / num1;
        if (answer.text == result.ToString())
        {
            numCorrect++;
            progress.text += "✓";
            if (numCorrect == 5)
            {
                question.text = "AUTOPILOT ENABLED!";
                StartCoroutine(MathGameWait(false));
            }
            else
            {
                Generate();
            }
        }
        else
        {
            numCorrect = 0;
            progress.text = "";
        }
        answer.text = "";
    }

    void Generate()
    {
        num1 = 5;
        num2 = 1;
        num3 = 2;
        while ((num3 - num2) % num1 != 0)
        {
            num3 = Random.Range(-90, 90);
            num2 = Random.Range(-30, 30);
            num1 = Random.Range(2, 12);
        }
        if(num2 > 0)
        {
            sign = " +";
        }
        else
        {
            sign = " ";
        }
        question.text = num1 + "x" + sign + num2 + " = " + num3;
    }
}
