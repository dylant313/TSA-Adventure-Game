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
    public AudioSource end;
    private float textSpeed = 0.04f;
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
        for (float j = 1; j >= 0; j -= 0.049f)
        {
            fadeBlack.alpha = j;
            yield return new WaitForSeconds(0.03f);
        }
        int i = 0;
        typing = true;
        while (i < textList[textID].Length)
        {
            textString += textList[textID][i++];
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(textSpeed);
        }
        typing = false;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        textString = "";
        for (float j = 0; j <= 1; j += 0.049f)
        {
            fadeBlack.alpha = j;
            yield return new WaitForSeconds(0.03f);
        }
        textID++;
        if (textID != 5)
        {
            StartCoroutine(AnimateText());
        }
    }

    IEnumerator ChangeImage1()
    {
        StartCoroutine(AnimateText());
        yield return new WaitUntil(() => textID == 2);
        storyScene.GetComponent<Image>().sprite = Resources.Load<Sprite>("Intro2");
        yield return new WaitUntil(() => textID == 3);
        yield return new WaitUntil(() => textID == 5);
        textString = "";
        typing = true;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("World");
    }

    IEnumerator ChangeImage2()
    {
        end.Play();
        StartCoroutine(AnimateText());
        yield return new WaitUntil(() => textID == 6);
        Application.Quit();
    }
}
