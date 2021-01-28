using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Text textField;
    public Image storyScene;
    public CanvasGroup fadeBlack;
    private float textSpeed = 0.03f;
    private float reminderTimeSet;
    private float reminderTimer = 0;
    private int textID;
    private string textString;
    private bool typing;
    private List<string> textList = new List<string>();

    void Start()
    {
        textList.Add("This is text 0.");
        textList.Add("This is text 1.");
        textList.Add("This is text 2.");
        textList.Add("This is text 3.");
        textList.Add("This is text 4.");
        textList.Add("THE END");
        if (RocketDrag.lockedCount == 7)
        {
            textID = 5;
            reminderTimeSet = 5;
            StartCoroutine(ChangeImage2());
        }
        else
        {
            textID = 0;
            reminderTimeSet = 2;
            StartCoroutine(ChangeImage1());
        }
    }

    void Update()
    {
        textField.text = textString;
        if (!typing)
        {
            reminderTimer += Time.deltaTime;
        }
        else
        {
            reminderTimer = 0;
        }
        if (reminderTimer > reminderTimeSet)
        {
            transform.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(4).gameObject.SetActive(false);
        }
    }

    IEnumerator AnimateText()
    {
        int i = 0;
        textString = "";
        typing = true;
        while (i < textList[textID].Length)
        {
            textString += textList[textID][i++];
            yield return new WaitForSeconds(textSpeed);
        }
        typing = false;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        textID++;
        if (textID != 5)
        {
            StartCoroutine(AnimateText());
        }
    }

    IEnumerator ChangeImage1()
    {
        for (float i = 1; i >= 0; i -= 0.049f)
        {
            fadeBlack.alpha = i;
            yield return new WaitForSeconds(0.03f);
        }
        StartCoroutine(AnimateText());
        yield return new WaitUntil(() => textID == 2);
        storyScene.GetComponent<Image>().sprite = Resources.Load<Sprite>("Intro2");
        yield return new WaitUntil(() => textID == 3);
        yield return new WaitUntil(() => textID == 5);
        textString = "";
        typing = true;
        for (float i = 0; i <= 1; i += 0.049f)
        {
            fadeBlack.alpha = i;
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("World");
    }

    IEnumerator ChangeImage2()
    {
        for (float i = 1; i >= 0; i -= 0.049f)
        {
            fadeBlack.alpha = i;
            yield return new WaitForSeconds(0.03f);
        }
        StartCoroutine(AnimateText());
    }
}
